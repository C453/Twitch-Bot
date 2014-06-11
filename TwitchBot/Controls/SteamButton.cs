using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwitchBot.Controls
{
    internal class SteamButton : ThemeControl152, IButtonControl
    {
        private DialogResult dr;

        public SteamButton()
        {
            this.Font = new Font("Verdana", 12f);
            this.Size = new Size(150, 40);
        }

        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            this.G.Clear(Color.FromArgb(56, 54, 53));
            this.DrawBorders(new Pen((Brush)new SolidBrush(Color.FromArgb(77, 75, 72))));
            this.DrawCorners(Color.FromArgb(56, 54, 53));
            switch (this.State)
            {
                case MouseState.None:
                    this.DrawGradient(Color.FromArgb(92, 89, 86), Color.FromArgb(74, 72, 70), this.ClientRectangle, 90f);
                    this.DrawBorders(new Pen((Brush)new SolidBrush(Color.FromArgb(112, 109, 105))));
                    this.DrawCorners(Color.FromArgb(82, 79, 77));
                    break;
                case MouseState.Over:
                    this.DrawGradient(Color.FromArgb(112, 109, 106), Color.FromArgb(94, 92, 90), this.ClientRectangle, 90f);
                    this.DrawBorders(new Pen((Brush)new SolidBrush(Color.FromArgb(141, 148, 130))));
                    this.DrawCorners(Color.FromArgb(82, 79, 77));
                    break;
                case MouseState.Down:
                    this.DrawGradient(Color.FromArgb(56, 54, 53), Color.FromArgb(73, 71, 69), this.ClientRectangle, 90f);
                    this.DrawBorders(new Pen((Brush)new SolidBrush(Color.FromArgb(112, 109, 105))));
                    this.DrawCorners(Color.FromArgb(82, 79, 77));
                    break;
            }
            this.DrawText((Brush)new SolidBrush(Color.FromArgb(195, 193, 191)), this.Text.ToUpper(), HorizontalAlignment.Left, 4, 0);
        }

        public DialogResult DialogResult
        {
            get
            {
                return this.dr;
            }

            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    this.dr = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
            if (this.IsDefault != value)
            {
                this.IsDefault = value;
            }
        }

        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.OnClick(EventArgs.Empty);
            }
        }
    }
}