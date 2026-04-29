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
            comboGameRes = new ComboBox();
            butCancel = new Button();
            butOK = new Button();
            butApply = new Button();
            chkIgnoreVrGames = new CheckBox();
            comboPreset1 = new ComboBox();
            comboPreset2 = new ComboBox();
            comboPreset3 = new ComboBox();
            comboPreset4 = new ComboBox();
            comboPreset5 = new ComboBox();
            chkRunOnStartup = new CheckBox();
            timerCheckGame = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            chkShowTooltip = new CheckBox();
            chkGameHDR = new CheckBox();
            chkPresetHDR1 = new CheckBox();
            chkPresetHDR2 = new CheckBox();
            chkPresetHDR3 = new CheckBox();
            chkPresetHDR4 = new CheckBox();
            chkPresetHDR5 = new CheckBox();
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
            label2.Size = new Size(630, 32);
            label2.TabIndex = 2;
            label2.Text = "Game Resolution: (applied when any Steam game started)";
            // 
            // comboGameRes
            // 
            comboGameRes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboGameRes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboGameRes.FormattingEnabled = true;
            comboGameRes.Location = new Point(22, 54);
            comboGameRes.Name = "comboGameRes";
            comboGameRes.Size = new Size(556, 40);
            comboGameRes.TabIndex = 3;
            comboGameRes.Format += comboRes_Format;
            // 
            // butCancel
            // 
            butCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(532, 465);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(150, 46);
            butCancel.TabIndex = 4;
            butCancel.Text = "&Cancel";
            butCancel.UseVisualStyleBackColor = true;
            butCancel.Click += butCancel_Click;
            // 
            // butOK
            // 
            butOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butOK.DialogResult = DialogResult.OK;
            butOK.Location = new Point(376, 465);
            butOK.Name = "butOK";
            butOK.Size = new Size(150, 46);
            butOK.TabIndex = 5;
            butOK.Text = "&OK";
            butOK.UseVisualStyleBackColor = true;
            butOK.Click += butOK_Click;
            // 
            // butApply
            // 
            butApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butApply.Location = new Point(220, 465);
            butApply.Name = "butApply";
            butApply.Size = new Size(150, 46);
            butApply.TabIndex = 6;
            butApply.Text = "&Apply";
            butApply.UseVisualStyleBackColor = true;
            butApply.Click += butApply_Click;
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
            // chkRunOnStartup
            // 
            chkRunOnStartup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkRunOnStartup.AutoSize = true;
            chkRunOnStartup.Location = new Point(12, 471);
            chkRunOnStartup.Name = "chkRunOnStartup";
            chkRunOnStartup.Size = new Size(206, 36);
            chkRunOnStartup.TabIndex = 13;
            chkRunOnStartup.Text = "Run on Startup";
            chkRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // timerCheckGame
            // 
            timerCheckGame.Enabled = true;
            timerCheckGame.Interval = 500;
            timerCheckGame.Tick += timerCheckGame_Tick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 409);
            label3.Name = "label3";
            label3.Size = new Size(571, 32);
            label3.TabIndex = 14;
            label3.Text = "The first desktop preset will be used as your default.";
            // 
            // chkShowTooltip
            // 
            chkShowTooltip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkShowTooltip.AutoSize = true;
            chkShowTooltip.Location = new Point(130, 100);
            chkShowTooltip.Name = "chkShowTooltip";
            chkShowTooltip.Size = new Size(308, 36);
            chkShowTooltip.TabIndex = 15;
            chkShowTooltip.Text = "Show Tooltip on Change";
            chkShowTooltip.UseVisualStyleBackColor = true;
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
            chkGameHDR.TabIndex = 16;
            chkGameHDR.Text = "HDR";
            chkGameHDR.ThreeState = true;
            chkGameHDR.UseVisualStyleBackColor = true;
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
            chkPresetHDR1.TabIndex = 17;
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
            chkPresetHDR2.TabIndex = 18;
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
            chkPresetHDR3.TabIndex = 19;
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
            chkPresetHDR4.TabIndex = 20;
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
            chkPresetHDR5.TabIndex = 21;
            chkPresetHDR5.Text = "HDR";
            chkPresetHDR5.ThreeState = true;
            chkPresetHDR5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 523);
            Controls.Add(chkPresetHDR5);
            Controls.Add(chkPresetHDR4);
            Controls.Add(chkPresetHDR3);
            Controls.Add(chkPresetHDR2);
            Controls.Add(chkPresetHDR1);
            Controls.Add(chkGameHDR);
            Controls.Add(chkShowTooltip);
            Controls.Add(label3);
            Controls.Add(chkRunOnStartup);
            Controls.Add(comboPreset5);
            Controls.Add(comboPreset4);
            Controls.Add(comboPreset3);
            Controls.Add(comboPreset2);
            Controls.Add(comboPreset1);
            Controls.Add(chkIgnoreVrGames);
            Controls.Add(butApply);
            Controls.Add(butOK);
            Controls.Add(butCancel);
            Controls.Add(comboGameRes);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(720, 594);
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
        private ComboBox comboGameRes;
        private Button butCancel;
        private Button butOK;
        private Button butApply;
        private CheckBox chkIgnoreVrGames;
        private ComboBox comboPreset1;
        private ComboBox comboPreset2;
        private ComboBox comboPreset3;
        private ComboBox comboPreset4;
        private ComboBox comboPreset5;
        private CheckBox chkRunOnStartup;
        private System.Windows.Forms.Timer timerCheckGame;
        private Label label3;
        private CheckBox chkShowTooltip;
        private CheckBox chkGameHDR;
        private CheckBox chkPresetHDR1;
        private CheckBox chkPresetHDR2;
        private CheckBox chkPresetHDR3;
        private CheckBox chkPresetHDR4;
        private CheckBox chkPresetHDR5;
    }
}
