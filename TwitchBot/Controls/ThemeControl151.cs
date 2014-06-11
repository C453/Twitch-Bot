using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TwitchBot.Controls
{
    internal abstract class ThemeControl151 : Control
    {
        private Dictionary<string, Color> Items = new Dictionary<string, Color>();
        protected Graphics G;
        protected Bitmap B;
        protected MouseState State;
        private Color BackColorWait;
        private bool _NoRounding;
        private Image _Image;
        private Size _ImageSize;
        private int _LockWidth;
        private int _LockHeight;
        private bool _Transparent;
        private string _Customization;
        private Point CenterReturn;
        private Bitmap MeasureBitmap;
        private Graphics MeasureGraphics;
        private SolidBrush DrawCornersBrush;
        private Point DrawTextPoint;
        private Size DrawTextSize;
        private Point DrawImagePoint;
        private LinearGradientBrush DrawGradientBrush;
        private Rectangle DrawGradientRectangle;

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (this.IsHandleCreated)
                    base.BackColor = value;
                else
                    this.BackColorWait = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return Color.Empty;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return (Image)null;
            }
            set
            {
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return ImageLayout.None;
            }
            set
            {
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.Invalidate();
            }
        }

        public bool NoRounding
        {
            get
            {
                return this._NoRounding;
            }
            set
            {
                this._NoRounding = value;
                this.Invalidate();
            }
        }

        public Image Image
        {
            get
            {
                return this._Image;
            }
            set
            {
                this._ImageSize = value != null ? value.Size : Size.Empty;
                this._Image = value;
                this.Invalidate();
            }
        }

        protected Size ImageSize
        {
            get
            {
                return this._ImageSize;
            }
        }

        protected int LockWidth
        {
            get
            {
                return this._LockWidth;
            }
            set
            {
                this._LockWidth = value;
                if (this.LockWidth == 0 || !this.IsHandleCreated)
                    return;
                this.Width = this.LockWidth;
            }
        }

        protected int LockHeight
        {
            get
            {
                return this._LockHeight;
            }
            set
            {
                this._LockHeight = value;
                if (this.LockHeight == 0 || !this.IsHandleCreated)
                    return;
                this.Height = this.LockHeight;
            }
        }

        public bool Transparent
        {
            get
            {
                return this._Transparent;
            }
            set
            {
                if (!value && (int)this.BackColor.A != (int)byte.MaxValue)
                    throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                this.SetStyle(ControlStyles.Opaque, !value);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, value);
                if (value)
                    this.InvalidateBitmap();
                else
                    this.B = (Bitmap)null;
                this._Transparent = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Bloom[] Colors
        {
            get
            {
                List<Bloom> list = new List<Bloom>();
                Dictionary<string, Color>.Enumerator enumerator = this.Items.GetEnumerator();
                while (enumerator.MoveNext())
                    list.Add(new Bloom(enumerator.Current.Key, enumerator.Current.Value));
                return list.ToArray();
            }
            set
            {
                foreach (Bloom bloom in value)
                {
                    if (this.Items.ContainsKey(bloom.Name))
                        this.Items[bloom.Name] = bloom.Value;
                }
                this.InvalidateCustimization();
                this.ColorHook();
                this.Invalidate();
            }
        }

        public string Customization
        {
            get
            {
                return this._Customization;
            }
            set
            {
                if (value == this._Customization)
                    return;
                Bloom[] colors = this.Colors;
                try
                {
                    byte[] numArray = Convert.FromBase64String(value);
                    for (int index = 0; index <= colors.Length - 1; ++index)
                        colors[index].Value = Color.FromArgb(BitConverter.ToInt32(numArray, index * 4));
                }
                catch
                {
                    return;
                }
                this._Customization = value;
                this.Colors = colors;
                this.ColorHook();
                this.Invalidate();
            }
        }

        public ThemeControl151()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this._ImageSize = Size.Empty;
            this.MeasureBitmap = new Bitmap(1, 1);
            this.MeasureGraphics = Graphics.FromImage((Image)this.MeasureBitmap);
            this.Font = new Font("Verdana", 8f);
            this.InvalidateCustimization();
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this._LockWidth != 0)
                width = this._LockWidth;
            if (this._LockHeight != 0)
                height = this._LockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override sealed void OnSizeChanged(EventArgs e)
        {
            if (this._Transparent && (this.Width != 0 && this.Height != 0))
            {
                this.B = new Bitmap(this.Width, this.Height);
                this.G = Graphics.FromImage((Image)this.B);
            }
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (this.Width == 0 || this.Height == 0)
                return;
            if (this._Transparent)
            {
                this.PaintHook();
                e.Graphics.DrawImage((Image)this.B, 0, 0);
            }
            else
            {
                this.G = e.Graphics;
                this.PaintHook();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            this.InvalidateCustimization();
            this.ColorHook();
            if (this._LockWidth != 0)
                this.Width = this._LockWidth;
            if (this._LockHeight != 0)
                this.Height = this._LockHeight;
            this.BackColor = this.BackColorWait;
            this.OnCreation();
            base.OnHandleCreated(e);
        }

        protected virtual void OnCreation()
        {
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.SetState(MouseState.Over);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.SetState(MouseState.Over);
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.SetState(MouseState.Down);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetState(MouseState.None);
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (this.Enabled)
                this.SetState(MouseState.None);
            else
                this.SetState(MouseState.Block);
            base.OnEnabledChanged(e);
        }

        private void SetState(MouseState current)
        {
            this.State = current;
            this.Invalidate();
        }

        private void InvalidateBitmap()
        {
            if (this.Width == 0 || this.Height == 0)
                return;
            this.B = new Bitmap(this.Width, this.Height);
            this.G = Graphics.FromImage((Image)this.B);
        }

        protected Color GetColor(string name)
        {
            return this.Items[name];
        }

        protected void SetColor(string name, Color color)
        {
            if (this.Items.ContainsKey(name))
                this.Items[name] = color;
            else
                this.Items.Add(name, color);
        }

        protected void SetColor(string name, byte r, byte g, byte b)
        {
            this.SetColor(name, Color.FromArgb((int)r, (int)g, (int)b));
        }

        protected void SetColor(string name, byte a, byte r, byte g, byte b)
        {
            this.SetColor(name, Color.FromArgb((int)a, (int)r, (int)g, (int)b));
        }

        protected void SetColor(string name, byte a, Color color)
        {
            this.SetColor(name, Color.FromArgb((int)a, color));
        }

        private void InvalidateCustimization()
        {
            MemoryStream memoryStream = new MemoryStream(this.Items.Count * 4);
            foreach (Bloom bloom in this.Colors)
                memoryStream.Write(BitConverter.GetBytes(bloom.Value.ToArgb()), 0, 4);
            memoryStream.Close();
            this._Customization = Convert.ToBase64String(memoryStream.ToArray());
        }

        protected abstract void ColorHook();

        protected abstract void PaintHook();

        protected Point Center(Rectangle r1, Size s1)
        {
            this.CenterReturn = new Point(r1.Width / 2 - s1.Width / 2 + r1.X, r1.Height / 2 - s1.Height / 2 + r1.Y);
            return this.CenterReturn;
        }

        protected Point Center(Rectangle r1, Rectangle r2)
        {
            return this.Center(r1, r2.Size);
        }

        protected Point Center(int w1, int h1, int w2, int h2)
        {
            this.CenterReturn = new Point(w1 / 2 - w2 / 2, h1 / 2 - h2 / 2);
            return this.CenterReturn;
        }

        protected Point Center(Size s1, Size s2)
        {
            return this.Center(s1.Width, s1.Height, s2.Width, s2.Height);
        }

        protected Point Center(Rectangle r1)
        {
            ThemeControl151 themeControl151 = this;
            Rectangle clientRectangle = this.ClientRectangle;
            int width1 = clientRectangle.Width;
            clientRectangle = this.ClientRectangle;
            int height1 = clientRectangle.Height;
            int width2 = r1.Width;
            int height2 = r1.Height;
            return themeControl151.Center(width1, height1, width2, height2);
        }

        protected Point Center(Size s1)
        {
            return this.Center(this.Width, this.Height, s1.Width, s1.Height);
        }

        protected Point Center(int w1, int h1)
        {
            return this.Center(this.Width, this.Height, w1, h1);
        }

        protected Size Measure(string text)
        {
            return this.MeasureGraphics.MeasureString(text, this.Font, this.Width).ToSize();
        }

        protected Size Measure()
        {
            return this.MeasureGraphics.MeasureString(this.Text, this.Font, this.Width).ToSize();
        }

        protected void DrawCorners(Color c1)
        {
            this.DrawCorners(c1, 0, 0, this.Width, this.Height);
        }

        protected void DrawCorners(Color c1, Rectangle r1)
        {
            this.DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
        }

        protected void DrawCorners(Color c1, int x, int y, int width, int height)
        {
            if (this._NoRounding)
                return;
            if (this._Transparent)
            {
                this.B.SetPixel(x, y, c1);
                this.B.SetPixel(x + (width - 1), y, c1);
                this.B.SetPixel(x, y + (height - 1), c1);
                this.B.SetPixel(x + (width - 1), y + (height - 1), c1);
            }
            else
            {
                this.DrawCornersBrush = new SolidBrush(c1);
                this.G.FillRectangle((Brush)this.DrawCornersBrush, x, y, 1, 1);
                this.G.FillRectangle((Brush)this.DrawCornersBrush, x + (width - 1), y, 1, 1);
                this.G.FillRectangle((Brush)this.DrawCornersBrush, x, y + (height - 1), 1, 1);
                this.G.FillRectangle((Brush)this.DrawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
            }
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
        {
            this.DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
        }

        protected void DrawBorders(Pen p1, int offset)
        {
            this.DrawBorders(p1, 0, 0, this.Width, this.Height, offset);
        }

        protected void DrawBorders(Pen p1, Rectangle r, int offset)
        {
            this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
        }

        protected void DrawBorders(Pen p1, int x, int y, int width, int height)
        {
            this.G.DrawRectangle(p1, x, y, width - 1, height - 1);
        }

        protected void DrawBorders(Pen p1)
        {
            this.DrawBorders(p1, 0, 0, this.Width, this.Height);
        }

        protected void DrawBorders(Pen p1, Rectangle r)
        {
            this.DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
        }

        protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
        {
            this.DrawText(b1, this.Text, a, x, y);
        }

        protected void DrawText(Brush b1, Point p1)
        {
            this.DrawText(b1, this.Text, p1.X, p1.Y);
        }

        protected void DrawText(Brush b1, int x, int y)
        {
            this.DrawText(b1, this.Text, x, y);
        }

        protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
        {
            if (text.Length == 0)
                return;
            this.DrawTextSize = this.Measure(text);
            this.DrawTextPoint = this.Center(this.DrawTextSize);
            switch (a)
            {
                case HorizontalAlignment.Left:
                    this.DrawText(b1, text, x, this.DrawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    this.DrawText(b1, text, this.Width - this.DrawTextSize.Width - x, this.DrawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    this.DrawText(b1, text, this.DrawTextPoint.X + x, this.DrawTextPoint.Y + y);
                    break;
            }
        }

        protected void DrawText(Brush b1, string text, Point p1)
        {
            this.DrawText(b1, text, p1.X, p1.Y);
        }

        protected void DrawText(Brush b1, string text, int x, int y)
        {
            if (text.Length == 0)
                return;
            this.G.DrawString(text, this.Font, b1, (float)x, (float)y);
        }

        protected void DrawImage(HorizontalAlignment a, int x, int y)
        {
            this.DrawImage(this._Image, a, x, y);
        }

        protected void DrawImage(Point p1)
        {
            this.DrawImage(this._Image, p1.X, p1.Y);
        }

        protected void DrawImage(int x, int y)
        {
            this.DrawImage(this._Image, x, y);
        }

        protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
        {
            if (image == null)
                return;
            this.DrawImagePoint = this.Center(image.Size);
            switch (a)
            {
                case HorizontalAlignment.Left:
                    this.DrawImage(image, x, this.DrawImagePoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    this.DrawImage(image, this.Width - image.Width - x, this.DrawImagePoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    this.DrawImage(image, this.DrawImagePoint.X + x, this.DrawImagePoint.Y + y);
                    break;
            }
        }

        protected void DrawImage(Image image, Point p1)
        {
            this.DrawImage(image, p1.X, p1.Y);
        }

        protected void DrawImage(Image image, int x, int y)
        {
            if (image == null)
                return;
            this.G.DrawImage(image, x, y, image.Width, image.Height);
        }

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
        {
            this.DrawGradient(blend, x, y, width, height, 90f);
        }

        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
        {
            this.DrawGradient(c1, c2, x, y, width, height, 90f);
        }

        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
        {
            this.DrawGradientRectangle = new Rectangle(x, y, width, height);
            this.DrawGradient(blend, this.DrawGradientRectangle, angle);
        }

        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            this.DrawGradientRectangle = new Rectangle(x, y, width, height);
            this.DrawGradient(c1, c2, this.DrawGradientRectangle, angle);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
        {
            this.DrawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
            this.DrawGradientBrush.InterpolationColors = blend;
            this.G.FillRectangle((Brush)this.DrawGradientBrush, r);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
        {
            this.DrawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
            this.G.FillRectangle((Brush)this.DrawGradientBrush, r);
        }
    }
}