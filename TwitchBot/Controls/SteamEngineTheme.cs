using System.Drawing;
using System.Windows.Forms;

namespace TwitchBot.Controls
{
    internal class SteamEngineTheme : ThemeContainer151
    {
        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            this.G.Clear(Color.FromArgb(56, 54, 53));
            this.DrawGradient(Color.Black, Color.FromArgb(76, 108, 139), this.ClientRectangle, 35f);
            this.G.FillRectangle((Brush)new SolidBrush(Color.FromArgb(56, 54, 53)), new Rectangle(1, 1, this.Width - 2, this.Height - 2));
            this.DrawGradient(Color.FromArgb(71, 68, 66), Color.FromArgb(57, 55, 54), new Rectangle(1, 1, this.Width - 2, 35), 90f);
            this.DrawText((Brush)new SolidBrush(Color.FromArgb(195, 193, 191)), HorizontalAlignment.Left, 4, 0);
        }
    }
}