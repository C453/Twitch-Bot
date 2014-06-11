namespace TwitchBot
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.steamEngineTheme1 = new TwitchBot.Controls.SteamEngineTheme();
            this.steamButton2 = new TwitchBot.Controls.SteamButton();
            this.steamMinimizeButton1 = new TwitchBot.Controls.SteamMinimizeButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.viewerListBox = new System.Windows.Forms.ListBox();
            this.steamCloseButton1 = new TwitchBot.Controls.SteamCloseButton();
            this.steamButton1 = new TwitchBot.Controls.SteamButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.steamSeparator1 = new TwitchBot.Controls.SteamSeparator();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.steamEngineTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // steamEngineTheme1
            // 
            this.steamEngineTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.steamEngineTheme1.Colors = new TwitchBot.Controls.Bloom[0];
            this.steamEngineTheme1.Controls.Add(this.comboBox1);
            this.steamEngineTheme1.Controls.Add(this.steamButton2);
            this.steamEngineTheme1.Controls.Add(this.steamMinimizeButton1);
            this.steamEngineTheme1.Controls.Add(this.label2);
            this.steamEngineTheme1.Controls.Add(this.label1);
            this.steamEngineTheme1.Controls.Add(this.viewerListBox);
            this.steamEngineTheme1.Controls.Add(this.steamCloseButton1);
            this.steamEngineTheme1.Controls.Add(this.steamButton1);
            this.steamEngineTheme1.Controls.Add(this.richTextBox1);
            this.steamEngineTheme1.Controls.Add(this.steamSeparator1);
            this.steamEngineTheme1.Controls.Add(this.textBox1);
            this.steamEngineTheme1.Customization = "";
            this.steamEngineTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamEngineTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.steamEngineTheme1.Image = null;
            this.steamEngineTheme1.Location = new System.Drawing.Point(0, 0);
            this.steamEngineTheme1.Movable = true;
            this.steamEngineTheme1.Name = "steamEngineTheme1";
            this.steamEngineTheme1.NoRounding = false;
            this.steamEngineTheme1.Sizable = false;
            this.steamEngineTheme1.Size = new System.Drawing.Size(834, 472);
            this.steamEngineTheme1.TabIndex = 3;
            this.steamEngineTheme1.Text = "steamEngineTheme1";
            this.steamEngineTheme1.TransparencyKey = System.Drawing.Color.Empty;
            // 
            // steamButton2
            // 
            this.steamButton2.Colors = new TwitchBot.Controls.Bloom[0];
            this.steamButton2.Customization = "";
            this.steamButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.steamButton2.Font = new System.Drawing.Font("Verdana", 12F);
            this.steamButton2.Image = null;
            this.steamButton2.Location = new System.Drawing.Point(650, 439);
            this.steamButton2.Name = "steamButton2";
            this.steamButton2.NoRounding = false;
            this.steamButton2.Size = new System.Drawing.Size(172, 19);
            this.steamButton2.TabIndex = 9;
            this.steamButton2.Text = "    Disconnect";
            this.steamButton2.Transparent = false;
            this.steamButton2.UseVisualStyleBackColor = true;
            this.steamButton2.Click += new System.EventHandler(this.steamButton2_Click);
            // 
            // steamMinimizeButton1
            // 
            this.steamMinimizeButton1.AutoSize = true;
            this.steamMinimizeButton1.BackColor = System.Drawing.Color.Empty;
            this.steamMinimizeButton1.Location = new System.Drawing.Point(766, 1);
            this.steamMinimizeButton1.Name = "steamMinimizeButton1";
            this.steamMinimizeButton1.Size = new System.Drawing.Size(38, 23);
            this.steamMinimizeButton1.TabIndex = 8;
            this.steamMinimizeButton1.Text = " _ ";
            this.steamMinimizeButton1.Click += new System.EventHandler(this.steamMinimizeButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Log:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(650, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Viewers:";
            // 
            // viewerListBox
            // 
            this.viewerListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.viewerListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewerListBox.ForeColor = System.Drawing.Color.Silver;
            this.viewerListBox.FormattingEnabled = true;
            this.viewerListBox.Location = new System.Drawing.Point(650, 66);
            this.viewerListBox.Name = "viewerListBox";
            this.viewerListBox.Size = new System.Drawing.Size(172, 366);
            this.viewerListBox.TabIndex = 4;
            // 
            // steamCloseButton1
            // 
            this.steamCloseButton1.AutoSize = true;
            this.steamCloseButton1.BackColor = System.Drawing.Color.Empty;
            this.steamCloseButton1.Location = new System.Drawing.Point(798, 1);
            this.steamCloseButton1.Name = "steamCloseButton1";
            this.steamCloseButton1.Size = new System.Drawing.Size(39, 23);
            this.steamCloseButton1.TabIndex = 3;
            this.steamCloseButton1.Text = " X ";
            this.steamCloseButton1.Click += new System.EventHandler(this.steamCloseButton1_Click);
            // 
            // steamButton1
            // 
            this.steamButton1.Colors = new TwitchBot.Controls.Bloom[0];
            this.steamButton1.Customization = "";
            this.steamButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.steamButton1.Font = new System.Drawing.Font("Verdana", 12F);
            this.steamButton1.Image = null;
            this.steamButton1.Location = new System.Drawing.Point(553, 438);
            this.steamButton1.Name = "steamButton1";
            this.steamButton1.NoRounding = false;
            this.steamButton1.Size = new System.Drawing.Size(91, 20);
            this.steamButton1.TabIndex = 2;
            this.steamButton1.Text = "   Send";
            this.steamButton1.Transparent = false;
            this.steamButton1.Click += new System.EventHandler(this.steamButton1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.ForeColor = System.Drawing.Color.Silver;
            this.richTextBox1.Location = new System.Drawing.Point(12, 66);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(632, 366);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // steamSeparator1
            // 
            this.steamSeparator1.Colors = new TwitchBot.Controls.Bloom[0];
            this.steamSeparator1.Customization = "";
            this.steamSeparator1.Font = new System.Drawing.Font("Verdana", 8F);
            this.steamSeparator1.Image = null;
            this.steamSeparator1.Location = new System.Drawing.Point(1, 46);
            this.steamSeparator1.Name = "steamSeparator1";
            this.steamSeparator1.NoRounding = false;
            this.steamSeparator1.Size = new System.Drawing.Size(832, 23);
            this.steamSeparator1.TabIndex = 7;
            this.steamSeparator1.Text = "steamSeparator1";
            this.steamSeparator1.Transparent = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(54)))), ((int)(((byte)(53)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.Maroon;
            this.textBox1.Location = new System.Drawing.Point(112, 438);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(435, 20);
            this.textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 437);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(94, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // Main
            // 
            this.AcceptButton = this.steamButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(834, 472);
            this.Controls.Add(this.steamEngineTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.steamEngineTheme1.ResumeLayout(false);
            this.steamEngineTheme1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private TwitchBot.Controls.SteamButton steamButton1;
        private TwitchBot.Controls.SteamCloseButton steamCloseButton1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ListBox viewerListBox;
        internal System.Windows.Forms.RichTextBox richTextBox1;
        internal TwitchBot.Controls.SteamEngineTheme steamEngineTheme1;
        private TwitchBot.Controls.SteamSeparator steamSeparator1;
        private System.Windows.Forms.Label label2;
        private Controls.SteamMinimizeButton steamMinimizeButton1;
        private Controls.SteamButton steamButton2;
        internal System.Windows.Forms.ComboBox comboBox1;
    }
}