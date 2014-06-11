using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchBot.Controls
{
    public class SteamCloseButton : Label
    {
        public SteamCloseButton()
        {
            base.BackColor = System.Drawing.Color.Transparent;
        }

        new public System.Drawing.Color BackColor { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.BackColor = System.Drawing.Color.Black;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.BackColor = System.Drawing.Color.Transparent;
            base.OnMouseLeave(e);
        }

        public override System.Drawing.Font Font { get { return new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); } }
        public override string Text { get { return " X "; } }
        public override System.Drawing.Color ForeColor { get { return System.Drawing.Color.FromArgb(195, 193, 191); } }
    }

    public class SteamMinimizeButton : Label
    {
        public SteamMinimizeButton()
        {
            base.BackColor = System.Drawing.Color.Transparent;
        }

        new public System.Drawing.Color BackColor { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.BackColor = System.Drawing.Color.Black;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.BackColor = System.Drawing.Color.Transparent;
            base.OnMouseLeave(e);
        }

        public override System.Drawing.Font Font { get { return new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))); } }
        public override string Text { get { return " _ "; } }
        public override System.Drawing.Color ForeColor { get { return System.Drawing.Color.FromArgb(195, 193, 191); } }
    }
}
