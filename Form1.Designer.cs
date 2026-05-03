namespace SteamResChanger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            comboGameRes = new ComboBox();
            chkGameHDR = new CheckBox();
            chkShowTooltip = new CheckBox();
            chkIgnoreVrGames = new CheckBox();
            comboPreset1 = new ComboBox();
            comboPreset2 = new ComboBox();
            comboPreset3 = new ComboBox();
            comboPreset4 = new ComboBox();
            comboPreset5 = new ComboBox();
            comboPreset6 = new ComboBox();
            comboPreset7 = new ComboBox();
            comboPreset8 = new ComboBox();
            comboPreset9 = new ComboBox();
            comboPreset0 = new ComboBox();
            chkPresetHDR1 = new CheckBox();
            chkPresetHDR2 = new CheckBox();
            chkPresetHDR3 = new CheckBox();
            chkPresetHDR4 = new CheckBox();
            chkPresetHDR5 = new CheckBox();
            chkPresetHDR6 = new CheckBox();
            chkPresetHDR7 = new CheckBox();
            chkPresetHDR8 = new CheckBox();
            chkPresetHDR9 = new CheckBox();
            chkPresetHDR0 = new CheckBox();
            chkRunOnStartup = new CheckBox();
            butApply = new Button();
            butOK = new Button();
            butCancel = new Button();
            timerCheckGame = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 139);
            label1.Name = "label1";
            label1.Size = new Size(308, 32);
            label1.TabIndex = 1;
            label1.Text = "Desktop Resolution Presets:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 15);
            label2.Name = "label2";
            label2.Size = new Size(653, 32);
            label2.TabIndex = 2;
            label2.Text = "Game Resolution: (applied when any Steam game is started)";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(22, 639);
            label3.Name = "label3";
            label3.Size = new Size(571, 32);
            label3.TabIndex = 3;
            label3.Text = "The first desktop preset will be used as your default.";
            // 
            // comboGameRes
            // 
            comboGameRes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboGameRes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboGameRes.FormattingEnabled = true;
            comboGameRes.Location = new Point(22, 54);
            comboGameRes.Name = "comboGameRes";
            comboGameRes.Size = new Size(556, 40);
            comboGameRes.TabIndex = 4;
            comboGameRes.Format += comboRes_Format;
            // 
            // chkGameHDR
            // 
            chkGameHDR.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkGameHDR.AutoSize = true;
            chkGameHDR.Checked = true;
            chkGameHDR.CheckState = CheckState.Indeterminate;
            chkGameHDR.Location = new Point(588, 56);
            chkGameHDR.Name = "chkGameHDR";
            chkGameHDR.Size = new Size(94, 36);
            chkGameHDR.TabIndex = 5;
            chkGameHDR.Text = "HDR";
            chkGameHDR.ThreeState = true;
            chkGameHDR.UseVisualStyleBackColor = true;
            // 
            // chkShowTooltip
            // 
            chkShowTooltip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkShowTooltip.AutoSize = true;
            chkShowTooltip.Location = new Point(130, 100);
            chkShowTooltip.Name = "chkShowTooltip";
            chkShowTooltip.Size = new Size(308, 36);
            chkShowTooltip.TabIndex = 6;
            chkShowTooltip.Text = "Show Tooltip on Change";
            chkShowTooltip.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreVrGames
            // 
            chkIgnoreVrGames.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkIgnoreVrGames.AutoSize = true;
            chkIgnoreVrGames.Checked = true;
            chkIgnoreVrGames.CheckState = CheckState.Checked;
            chkIgnoreVrGames.Location = new Point(452, 100);
            chkIgnoreVrGames.Name = "chkIgnoreVrGames";
            chkIgnoreVrGames.Size = new Size(230, 36);
            chkIgnoreVrGames.TabIndex = 7;
            chkIgnoreVrGames.Text = "Ignore VR Games";
            chkIgnoreVrGames.UseVisualStyleBackColor = true;
            // 
            // comboPreset1
            // 
            comboPreset1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset1.FormattingEnabled = true;
            comboPreset1.Location = new Point(22, 178);
            comboPreset1.Name = "comboPreset1";
            comboPreset1.Size = new Size(556, 40);
            comboPreset1.TabIndex = 8;
            comboPreset1.Format += comboRes_Format;
            // 
            // comboPreset2
            // 
            comboPreset2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset2.FormattingEnabled = true;
            comboPreset2.Location = new Point(22, 224);
            comboPreset2.Name = "comboPreset2";
            comboPreset2.Size = new Size(556, 40);
            comboPreset2.TabIndex = 9;
            comboPreset2.Format += comboRes_Format;
            // 
            // comboPreset3
            // 
            comboPreset3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset3.FormattingEnabled = true;
            comboPreset3.Location = new Point(22, 270);
            comboPreset3.Name = "comboPreset3";
            comboPreset3.Size = new Size(556, 40);
            comboPreset3.TabIndex = 10;
            comboPreset3.Format += comboRes_Format;
            // 
            // comboPreset4
            // 
            comboPreset4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset4.FormattingEnabled = true;
            comboPreset4.Location = new Point(22, 316);
            comboPreset4.Name = "comboPreset4";
            comboPreset4.Size = new Size(556, 40);
            comboPreset4.TabIndex = 11;
            comboPreset4.Format += comboRes_Format;
            // 
            // comboPreset5
            // 
            comboPreset5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset5.FormattingEnabled = true;
            comboPreset5.Location = new Point(22, 362);
            comboPreset5.Name = "comboPreset5";
            comboPreset5.Size = new Size(556, 40);
            comboPreset5.TabIndex = 12;
            comboPreset5.Format += comboRes_Format;
            // 
            // comboPreset6
            // 
            comboPreset6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset6.FormattingEnabled = true;
            comboPreset6.Location = new Point(22, 408);
            comboPreset6.Name = "comboPreset6";
            comboPreset6.Size = new Size(556, 40);
            comboPreset6.TabIndex = 13;
            comboPreset6.Format += comboRes_Format;
            // 
            // comboPreset7
            // 
            comboPreset7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset7.FormattingEnabled = true;
            comboPreset7.Location = new Point(22, 454);
            comboPreset7.Name = "comboPreset7";
            comboPreset7.Size = new Size(556, 40);
            comboPreset7.TabIndex = 14;
            comboPreset7.Format += comboRes_Format;
            // 
            // comboPreset8
            // 
            comboPreset8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset8.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset8.FormattingEnabled = true;
            comboPreset8.Location = new Point(22, 500);
            comboPreset8.Name = "comboPreset8";
            comboPreset8.Size = new Size(556, 40);
            comboPreset8.TabIndex = 15;
            comboPreset8.Format += comboRes_Format;
            // 
            // comboPreset9
            // 
            comboPreset9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset9.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset9.FormattingEnabled = true;
            comboPreset9.Location = new Point(22, 546);
            comboPreset9.Name = "comboPreset9";
            comboPreset9.Size = new Size(556, 40);
            comboPreset9.TabIndex = 16;
            comboPreset9.Format += comboRes_Format;
            // 
            // comboPreset0
            // 
            comboPreset0.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboPreset0.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset0.FormattingEnabled = true;
            comboPreset0.Location = new Point(22, 592);
            comboPreset0.Name = "comboPreset0";
            comboPreset0.Size = new Size(556, 40);
            comboPreset0.TabIndex = 17;
            comboPreset0.Format += comboRes_Format;
            // 
            // chkPresetHDR1
            // 
            chkPresetHDR1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR1.AutoSize = true;
            chkPresetHDR1.Checked = true;
            chkPresetHDR1.CheckState = CheckState.Indeterminate;
            chkPresetHDR1.Location = new Point(588, 180);
            chkPresetHDR1.Name = "chkPresetHDR1";
            chkPresetHDR1.Size = new Size(94, 36);
            chkPresetHDR1.TabIndex = 18;
            chkPresetHDR1.Text = "HDR";
            chkPresetHDR1.ThreeState = true;
            chkPresetHDR1.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR2
            // 
            chkPresetHDR2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR2.AutoSize = true;
            chkPresetHDR2.Checked = true;
            chkPresetHDR2.CheckState = CheckState.Indeterminate;
            chkPresetHDR2.Location = new Point(588, 226);
            chkPresetHDR2.Name = "chkPresetHDR2";
            chkPresetHDR2.Size = new Size(94, 36);
            chkPresetHDR2.TabIndex = 19;
            chkPresetHDR2.Text = "HDR";
            chkPresetHDR2.ThreeState = true;
            chkPresetHDR2.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR3
            // 
            chkPresetHDR3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR3.AutoSize = true;
            chkPresetHDR3.Checked = true;
            chkPresetHDR3.CheckState = CheckState.Indeterminate;
            chkPresetHDR3.Location = new Point(588, 272);
            chkPresetHDR3.Name = "chkPresetHDR3";
            chkPresetHDR3.Size = new Size(94, 36);
            chkPresetHDR3.TabIndex = 20;
            chkPresetHDR3.Text = "HDR";
            chkPresetHDR3.ThreeState = true;
            chkPresetHDR3.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR4
            // 
            chkPresetHDR4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR4.AutoSize = true;
            chkPresetHDR4.Checked = true;
            chkPresetHDR4.CheckState = CheckState.Indeterminate;
            chkPresetHDR4.Location = new Point(588, 318);
            chkPresetHDR4.Name = "chkPresetHDR4";
            chkPresetHDR4.Size = new Size(94, 36);
            chkPresetHDR4.TabIndex = 21;
            chkPresetHDR4.Text = "HDR";
            chkPresetHDR4.ThreeState = true;
            chkPresetHDR4.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR5
            // 
            chkPresetHDR5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR5.AutoSize = true;
            chkPresetHDR5.Checked = true;
            chkPresetHDR5.CheckState = CheckState.Indeterminate;
            chkPresetHDR5.Location = new Point(588, 364);
            chkPresetHDR5.Name = "chkPresetHDR5";
            chkPresetHDR5.Size = new Size(94, 36);
            chkPresetHDR5.TabIndex = 22;
            chkPresetHDR5.Text = "HDR";
            chkPresetHDR5.ThreeState = true;
            chkPresetHDR5.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR6
            // 
            chkPresetHDR6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR6.AutoSize = true;
            chkPresetHDR6.Checked = true;
            chkPresetHDR6.CheckState = CheckState.Indeterminate;
            chkPresetHDR6.Location = new Point(588, 410);
            chkPresetHDR6.Name = "chkPresetHDR6";
            chkPresetHDR6.Size = new Size(94, 36);
            chkPresetHDR6.TabIndex = 23;
            chkPresetHDR6.Text = "HDR";
            chkPresetHDR6.ThreeState = true;
            chkPresetHDR6.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR7
            // 
            chkPresetHDR7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR7.AutoSize = true;
            chkPresetHDR7.Checked = true;
            chkPresetHDR7.CheckState = CheckState.Indeterminate;
            chkPresetHDR7.Location = new Point(588, 456);
            chkPresetHDR7.Name = "chkPresetHDR7";
            chkPresetHDR7.Size = new Size(94, 36);
            chkPresetHDR7.TabIndex = 24;
            chkPresetHDR7.Text = "HDR";
            chkPresetHDR7.ThreeState = true;
            chkPresetHDR7.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR8
            // 
            chkPresetHDR8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR8.AutoSize = true;
            chkPresetHDR8.Checked = true;
            chkPresetHDR8.CheckState = CheckState.Indeterminate;
            chkPresetHDR8.Location = new Point(588, 502);
            chkPresetHDR8.Name = "chkPresetHDR8";
            chkPresetHDR8.Size = new Size(94, 36);
            chkPresetHDR8.TabIndex = 25;
            chkPresetHDR8.Text = "HDR";
            chkPresetHDR8.ThreeState = true;
            chkPresetHDR8.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR9
            // 
            chkPresetHDR9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR9.AutoSize = true;
            chkPresetHDR9.Checked = true;
            chkPresetHDR9.CheckState = CheckState.Indeterminate;
            chkPresetHDR9.Location = new Point(588, 548);
            chkPresetHDR9.Name = "chkPresetHDR9";
            chkPresetHDR9.Size = new Size(94, 36);
            chkPresetHDR9.TabIndex = 26;
            chkPresetHDR9.Text = "HDR";
            chkPresetHDR9.ThreeState = true;
            chkPresetHDR9.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR0
            // 
            chkPresetHDR0.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkPresetHDR0.AutoSize = true;
            chkPresetHDR0.Checked = true;
            chkPresetHDR0.CheckState = CheckState.Indeterminate;
            chkPresetHDR0.Location = new Point(588, 594);
            chkPresetHDR0.Name = "chkPresetHDR0";
            chkPresetHDR0.Size = new Size(94, 36);
            chkPresetHDR0.TabIndex = 27;
            chkPresetHDR0.Text = "HDR";
            chkPresetHDR0.ThreeState = true;
            chkPresetHDR0.UseVisualStyleBackColor = true;
            // 
            // chkRunOnStartup
            // 
            chkRunOnStartup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkRunOnStartup.AutoSize = true;
            chkRunOnStartup.Location = new Point(12, 701);
            chkRunOnStartup.Name = "chkRunOnStartup";
            chkRunOnStartup.Size = new Size(206, 36);
            chkRunOnStartup.TabIndex = 28;
            chkRunOnStartup.Text = "Run on Startup";
            chkRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // butApply
            // 
            butApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butApply.Location = new Point(220, 695);
            butApply.Name = "butApply";
            butApply.Size = new Size(150, 46);
            butApply.TabIndex = 29;
            butApply.Text = "&Apply";
            butApply.UseVisualStyleBackColor = true;
            butApply.Click += butApply_Click;
            // 
            // butOK
            // 
            butOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butOK.DialogResult = DialogResult.OK;
            butOK.Location = new Point(376, 695);
            butOK.Name = "butOK";
            butOK.Size = new Size(150, 46);
            butOK.TabIndex = 30;
            butOK.Text = "&OK";
            butOK.UseVisualStyleBackColor = true;
            butOK.Click += butOK_Click;
            // 
            // butCancel
            // 
            butCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(532, 695);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(150, 46);
            butCancel.TabIndex = 31;
            butCancel.Text = "&Cancel";
            butCancel.UseVisualStyleBackColor = true;
            butCancel.Click += butCancel_Click;
            // 
            // timerCheckGame
            // 
            timerCheckGame.Enabled = true;
            timerCheckGame.Interval = 500;
            timerCheckGame.Tick += timerCheckGame_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 753);
            Controls.Add(butCancel);
            Controls.Add(butOK);
            Controls.Add(butApply);
            Controls.Add(chkRunOnStartup);
            Controls.Add(chkPresetHDR0);
            Controls.Add(chkPresetHDR9);
            Controls.Add(chkPresetHDR8);
            Controls.Add(chkPresetHDR7);
            Controls.Add(chkPresetHDR6);
            Controls.Add(chkPresetHDR5);
            Controls.Add(chkPresetHDR4);
            Controls.Add(chkPresetHDR3);
            Controls.Add(chkPresetHDR2);
            Controls.Add(chkPresetHDR1);
            Controls.Add(comboPreset0);
            Controls.Add(comboPreset9);
            Controls.Add(comboPreset8);
            Controls.Add(comboPreset7);
            Controls.Add(comboPreset6);
            Controls.Add(comboPreset5);
            Controls.Add(comboPreset4);
            Controls.Add(comboPreset3);
            Controls.Add(comboPreset2);
            Controls.Add(comboPreset1);
            Controls.Add(chkIgnoreVrGames);
            Controls.Add(chkShowTooltip);
            Controls.Add(chkGameHDR);
            Controls.Add(comboGameRes);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(720, 824);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Steam Resolution Changer";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            VisibleChanged += Form1_VisibleChanged;
            KeyDown += Form1_KeyDown;
            Resize += Form1_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox comboGameRes;
        private CheckBox chkGameHDR;
        private CheckBox chkShowTooltip;
        private CheckBox chkIgnoreVrGames;
        private ComboBox comboPreset1;
        private ComboBox comboPreset2;
        private ComboBox comboPreset3;
        private ComboBox comboPreset4;
        private ComboBox comboPreset5;
        private ComboBox comboPreset6;
        private ComboBox comboPreset7;
        private ComboBox comboPreset8;
        private ComboBox comboPreset9;
        private ComboBox comboPreset0;
        private CheckBox chkPresetHDR1;
        private CheckBox chkPresetHDR2;
        private CheckBox chkPresetHDR3;
        private CheckBox chkPresetHDR4;
        private CheckBox chkPresetHDR5;
        private CheckBox chkPresetHDR6;
        private CheckBox chkPresetHDR7;
        private CheckBox chkPresetHDR8;
        private CheckBox chkPresetHDR9;
        private CheckBox chkPresetHDR0;
        private CheckBox chkRunOnStartup;
        private Button butApply;
        private Button butOK;
        private Button butCancel;
        private System.Windows.Forms.Timer timerCheckGame;
    }
}
