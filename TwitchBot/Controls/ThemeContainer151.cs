using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TwitchBot.Controls
{
    internal abstract class ThemeContainer151 : ContainerControl
    {
        private Message[] Messages = new Message[9];
        private bool _Movable = true;
        private bool _Sizable = false;
        private int _MoveHeight = 24;
        private Dictionary<string, Color> Items = new Dictionary<string, Color>();
        protected Graphics G;
        private Rectangle Header;
        protected MouseState State;
        private Point GetIndexPoint;
        private bool B1;
        private bool B2;
        private bool B3;
        private bool B4;
        private int Current;
        private int Previous;
        private Color BackColorWait;
        private bool _ControlMode;
        private Color _TransparencyKey;
        private FormBorderStyle _BorderStyle;
        private bool _NoRounding;
        private Image _Image;
        private Size _ImageSize;
        private bool _IsParentForm;
        private int _LockWidth;
        private int _LockHeight;
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
                {
                    if (!this._ControlMode)
                        this.Parent.BackColor = value;
                    base.BackColor = value;
                }
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
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
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

        public bool Movable
        {
            get
            {
                return this._Movable;
            }
            set
            {
                this._Movable = value;
            }
        }

        public bool Sizable
        {
            get
            {
                return this._Sizable;
            }
            set
            {
                this._Sizable = value;
            }
        }

        protected int MoveHeight
        {
            get
            {
                return this._MoveHeight;
            }
            set
            {
                if (value < 8)
                    return;
                this.Header = new Rectangle(7, 7, this.Width - 14, value - 7);
                this._MoveHeight = value;
                this.Invalidate();
            }
        }

        protected bool ControlMode
        {
            get
            {
                return this._ControlMode;
            }
            set
            {
                this._ControlMode = value;
            }
        }

        public Color TransparencyKey
        {
            get
            {
                if (this._IsParentForm && !this._ControlMode)
                    return this.ParentForm.TransparencyKey;
                else
                    return this._TransparencyKey;
            }
            set
            {
                if (this._IsParentForm && !this._ControlMode)
                    this.ParentForm.TransparencyKey = value;
                this._TransparencyKey = value;
            }
        }

        public FormBorderStyle BorderStyle
        {
            get
            {
                if (this._IsParentForm && !this._ControlMode)
                    return this.ParentForm.FormBorderStyle;
                else
                    return this._BorderStyle;
            }
            set
            {
                if (this._IsParentForm && !this._ControlMode)
                    this.ParentForm.FormBorderStyle = value;
                this._BorderStyle = value;
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

        protected bool IsParentForm
        {
            get
            {
                return this._IsParentForm;
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

        public ThemeContainer151()
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
            base.OnSizeChanged(e);
            if (this._Movable && !this._ControlMode)
                this.Header = new Rectangle(7, 7, this.Width - 14, this._MoveHeight - 7);
            this.Invalidate();
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (this.Width == 0 || this.Height == 0)
                return;
            this.G = e.Graphics;
            this.PaintHook();
        }

        protected override sealed void OnHandleCreated(EventArgs e)
        {
            this.InitializeMessages();
            this.InvalidateCustimization();
            this.ColorHook();
            this._IsParentForm = this.Parent is Form;
            if (!this._ControlMode)
                this.Dock = DockStyle.Fill;
            if (this._LockWidth != 0)
                this.Width = this._LockWidth;
            if (this._LockHeight != 0)
                this.Height = this._LockHeight;
            this.BackColor = this.BackColorWait;
            if (this._IsParentForm && !this._ControlMode)
            {
                this.ParentForm.FormBorderStyle = this._BorderStyle;
                this.ParentForm.TransparencyKey = this._TransparencyKey;
            }
            this.OnCreation();
            base.OnHandleCreated(e);
        }

        protected virtual void OnCreation()
        {
        }

        private void SetState(MouseState current)
        {
            this.State = current;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this._Sizable && !this._ControlMode)
                this.InvalidateMouse();
            base.OnMouseMove(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (this.Enabled)
                this.SetState(MouseState.None);
            else
                this.SetState(MouseState.Block);
            base.OnEnabledChanged(e);
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

        protected override void OnMouseLeave(EventArgs e)
        {
            this.SetState(MouseState.None);
            if (this._Sizable && !this._ControlMode && this.GetChildAtPoint(this.PointToClient(Control.MousePosition)) != null)
            {
                this.Cursor = Cursors.Default;
                this.Previous = 0;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
                return;
            this.SetState(MouseState.Down);
            if (this._IsParentForm && this.ParentForm.WindowState == FormWindowState.Maximized || this._ControlMode)
                return;
            if (this._Movable && this.Header.Contains(e.Location))
            {
                this.Capture = false;
                this.DefWndProc(ref this.Messages[0]);
            }
            else
            {
                if (!this._Sizable || this.Previous == 0)
                    return;
                this.Capture = false;
                this.DefWndProc(ref this.Messages[this.Previous]);
            }
        }

        private int GetIndex()
        {
            this.GetIndexPoint = this.PointToClient(Control.MousePosition);
            this.B1 = this.GetIndexPoint.X < 7;
            this.B2 = this.GetIndexPoint.X > this.Width - 7;
            this.B3 = this.GetIndexPoint.Y < 7;
            this.B4 = this.GetIndexPoint.Y > this.Height - 7;
            if (this.B1 && this.B3)
                return 4;
            if (this.B1 && this.B4)
                return 7;
            if (this.B2 && this.B3)
                return 5;
            if (this.B2 && this.B4)
                return 8;
            if (this.B1)
                return 1;
            if (this.B2)
                return 2;
            if (this.B3)
                return 3;
            return this.B4 ? 6 : 0;
        }

        private void InvalidateMouse()
        {
            this.Current = this.GetIndex();
            if (this.Current == this.Previous)
                return;
            this.Previous = this.Current;
            switch (this.Previous)
            {
                case 0:
                    this.Cursor = Cursors.Default;
                    break;
                case 1:
                case 2:
                    this.Cursor = Cursors.SizeWE;
                    break;
                case 3:
                case 6:
                    this.Cursor = Cursors.SizeNS;
                    break;
                case 4:
                case 8:
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case 5:
                case 7:
                    this.Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private void InitializeMessages()
        {
            this.Messages[0] = Message.Create(this.Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (int index = 1; index <= 8; ++index)
                this.Messages[index] = Message.Create(this.Parent.Handle, 161, new IntPtr(index + 9), IntPtr.Zero);
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
            ThemeContainer151 themeContainer151 = this;
            Rectangle clientRectangle = this.ClientRectangle;
            int width1 = clientRectangle.Width;
            clientRectangle = this.ClientRectangle;
            int height1 = clientRectangle.Height;
            int width2 = r1.Width;
            int height2 = r1.Height;
            return themeContainer151.Center(width1, height1, width2, height2);
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
            return this.MeasureGraphics.MeasureString(this.Text, this.Font).ToSize();
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
            this.DrawCornersBrush = new SolidBrush(c1);
            this.G.FillRectangle((Brush)this.DrawCornersBrush, x, y, 1, 1);
            this.G.FillRectangle((Brush)this.DrawCornersBrush, x + (width - 1), y, 1, 1);
            this.G.FillRectangle((Brush)this.DrawCornersBrush, x, y + (height - 1), 1, 1);
            this.G.FillRectangle((Brush)this.DrawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
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
            this.DrawTextPoint = new Point(this.Width / 2 - this.DrawTextSize.Width / 2, this.MoveHeight / 2 - this.DrawTextSize.Height / 2);
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
            this.DrawImagePoint = new Point(this.Width / 2 - image.Width / 2, this.MoveHeight / 2 - image.Height / 2);
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