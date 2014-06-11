using System.Drawing;

namespace TwitchBot.Controls
{
    internal class SteamSeparator : ThemeControl151
    {
        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            this.G.FillRectangle((Brush)new SolidBrush(Color.FromArgb(56, 54, 53)), this.ClientRectangle);
            this.DrawGradient(Color.FromArgb(107, 104, 101), Color.FromArgb(74, 72, 70), new Rectangle(0, this.Height / 2, this.Width, 1), 45f);
        }
    }
}