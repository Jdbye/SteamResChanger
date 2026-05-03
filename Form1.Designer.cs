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
            hotkeyGame = new HotkeyBox();
            hotkeyPreset1 = new HotkeyBox();
            hotkeyPreset2 = new HotkeyBox();
            hotkeyPreset3 = new HotkeyBox();
            hotkeyPreset4 = new HotkeyBox();
            hotkeyPreset5 = new HotkeyBox();
            hotkeyPreset6 = new HotkeyBox();
            hotkeyPreset7 = new HotkeyBox();
            hotkeyPreset8 = new HotkeyBox();
            hotkeyPreset9 = new HotkeyBox();
            hotkeyPreset0 = new HotkeyBox();
            labelHotkeyHDR = new Label();
            hotkeyHDR = new HotkeyBox();
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
            label1.Location = new Point(22, 147);
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
            label3.Location = new Point(22, 693);
            label3.Name = "label3";
            label3.Size = new Size(571, 32);
            label3.TabIndex = 3;
            label3.Text = "The first desktop preset will be used as your default.";
            // 
            // comboGameRes
            // 
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
            chkShowTooltip.AutoSize = true;
            chkShowTooltip.Location = new Point(22, 100);
            chkShowTooltip.Name = "chkShowTooltip";
            chkShowTooltip.Size = new Size(308, 36);
            chkShowTooltip.TabIndex = 6;
            chkShowTooltip.Text = "Show Tooltip on Change";
            chkShowTooltip.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreVrGames
            // 
            chkIgnoreVrGames.AutoSize = true;
            chkIgnoreVrGames.Checked = true;
            chkIgnoreVrGames.CheckState = CheckState.Checked;
            chkIgnoreVrGames.Location = new Point(348, 100);
            chkIgnoreVrGames.Name = "chkIgnoreVrGames";
            chkIgnoreVrGames.Size = new Size(230, 36);
            chkIgnoreVrGames.TabIndex = 7;
            chkIgnoreVrGames.Text = "Ignore VR Games";
            chkIgnoreVrGames.UseVisualStyleBackColor = true;
            // 
            // comboPreset1
            // 
            comboPreset1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset1.FormattingEnabled = true;
            comboPreset1.Location = new Point(22, 186);
            comboPreset1.Name = "comboPreset1";
            comboPreset1.Size = new Size(556, 40);
            comboPreset1.TabIndex = 8;
            comboPreset1.Format += comboRes_Format;
            // 
            // comboPreset2
            // 
            comboPreset2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset2.FormattingEnabled = true;
            comboPreset2.Location = new Point(22, 232);
            comboPreset2.Name = "comboPreset2";
            comboPreset2.Size = new Size(556, 40);
            comboPreset2.TabIndex = 9;
            comboPreset2.Format += comboRes_Format;
            // 
            // comboPreset3
            // 
            comboPreset3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset3.FormattingEnabled = true;
            comboPreset3.Location = new Point(22, 278);
            comboPreset3.Name = "comboPreset3";
            comboPreset3.Size = new Size(556, 40);
            comboPreset3.TabIndex = 10;
            comboPreset3.Format += comboRes_Format;
            // 
            // comboPreset4
            // 
            comboPreset4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset4.FormattingEnabled = true;
            comboPreset4.Location = new Point(22, 324);
            comboPreset4.Name = "comboPreset4";
            comboPreset4.Size = new Size(556, 40);
            comboPreset4.TabIndex = 11;
            comboPreset4.Format += comboRes_Format;
            // 
            // comboPreset5
            // 
            comboPreset5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset5.FormattingEnabled = true;
            comboPreset5.Location = new Point(22, 370);
            comboPreset5.Name = "comboPreset5";
            comboPreset5.Size = new Size(556, 40);
            comboPreset5.TabIndex = 12;
            comboPreset5.Format += comboRes_Format;
            // 
            // comboPreset6
            // 
            comboPreset6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset6.FormattingEnabled = true;
            comboPreset6.Location = new Point(22, 416);
            comboPreset6.Name = "comboPreset6";
            comboPreset6.Size = new Size(556, 40);
            comboPreset6.TabIndex = 13;
            comboPreset6.Format += comboRes_Format;
            // 
            // comboPreset7
            // 
            comboPreset7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset7.FormattingEnabled = true;
            comboPreset7.Location = new Point(22, 462);
            comboPreset7.Name = "comboPreset7";
            comboPreset7.Size = new Size(556, 40);
            comboPreset7.TabIndex = 14;
            comboPreset7.Format += comboRes_Format;
            // 
            // comboPreset8
            // 
            comboPreset8.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset8.FormattingEnabled = true;
            comboPreset8.Location = new Point(22, 508);
            comboPreset8.Name = "comboPreset8";
            comboPreset8.Size = new Size(556, 40);
            comboPreset8.TabIndex = 15;
            comboPreset8.Format += comboRes_Format;
            // 
            // comboPreset9
            // 
            comboPreset9.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset9.FormattingEnabled = true;
            comboPreset9.Location = new Point(22, 554);
            comboPreset9.Name = "comboPreset9";
            comboPreset9.Size = new Size(556, 40);
            comboPreset9.TabIndex = 16;
            comboPreset9.Format += comboRes_Format;
            // 
            // comboPreset0
            // 
            comboPreset0.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPreset0.FormattingEnabled = true;
            comboPreset0.Location = new Point(22, 600);
            comboPreset0.Name = "comboPreset0";
            comboPreset0.Size = new Size(556, 40);
            comboPreset0.TabIndex = 17;
            comboPreset0.Format += comboRes_Format;
            // 
            // chkPresetHDR1
            // 
            chkPresetHDR1.AutoSize = true;
            chkPresetHDR1.Checked = true;
            chkPresetHDR1.CheckState = CheckState.Indeterminate;
            chkPresetHDR1.Location = new Point(588, 188);
            chkPresetHDR1.Name = "chkPresetHDR1";
            chkPresetHDR1.Size = new Size(94, 36);
            chkPresetHDR1.TabIndex = 18;
            chkPresetHDR1.Text = "HDR";
            chkPresetHDR1.ThreeState = true;
            chkPresetHDR1.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR2
            // 
            chkPresetHDR2.AutoSize = true;
            chkPresetHDR2.Checked = true;
            chkPresetHDR2.CheckState = CheckState.Indeterminate;
            chkPresetHDR2.Location = new Point(588, 234);
            chkPresetHDR2.Name = "chkPresetHDR2";
            chkPresetHDR2.Size = new Size(94, 36);
            chkPresetHDR2.TabIndex = 19;
            chkPresetHDR2.Text = "HDR";
            chkPresetHDR2.ThreeState = true;
            chkPresetHDR2.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR3
            // 
            chkPresetHDR3.AutoSize = true;
            chkPresetHDR3.Checked = true;
            chkPresetHDR3.CheckState = CheckState.Indeterminate;
            chkPresetHDR3.Location = new Point(588, 280);
            chkPresetHDR3.Name = "chkPresetHDR3";
            chkPresetHDR3.Size = new Size(94, 36);
            chkPresetHDR3.TabIndex = 20;
            chkPresetHDR3.Text = "HDR";
            chkPresetHDR3.ThreeState = true;
            chkPresetHDR3.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR4
            // 
            chkPresetHDR4.AutoSize = true;
            chkPresetHDR4.Checked = true;
            chkPresetHDR4.CheckState = CheckState.Indeterminate;
            chkPresetHDR4.Location = new Point(588, 326);
            chkPresetHDR4.Name = "chkPresetHDR4";
            chkPresetHDR4.Size = new Size(94, 36);
            chkPresetHDR4.TabIndex = 21;
            chkPresetHDR4.Text = "HDR";
            chkPresetHDR4.ThreeState = true;
            chkPresetHDR4.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR5
            // 
            chkPresetHDR5.AutoSize = true;
            chkPresetHDR5.Checked = true;
            chkPresetHDR5.CheckState = CheckState.Indeterminate;
            chkPresetHDR5.Location = new Point(588, 372);
            chkPresetHDR5.Name = "chkPresetHDR5";
            chkPresetHDR5.Size = new Size(94, 36);
            chkPresetHDR5.TabIndex = 22;
            chkPresetHDR5.Text = "HDR";
            chkPresetHDR5.ThreeState = true;
            chkPresetHDR5.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR6
            // 
            chkPresetHDR6.AutoSize = true;
            chkPresetHDR6.Checked = true;
            chkPresetHDR6.CheckState = CheckState.Indeterminate;
            chkPresetHDR6.Location = new Point(588, 418);
            chkPresetHDR6.Name = "chkPresetHDR6";
            chkPresetHDR6.Size = new Size(94, 36);
            chkPresetHDR6.TabIndex = 23;
            chkPresetHDR6.Text = "HDR";
            chkPresetHDR6.ThreeState = true;
            chkPresetHDR6.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR7
            // 
            chkPresetHDR7.AutoSize = true;
            chkPresetHDR7.Checked = true;
            chkPresetHDR7.CheckState = CheckState.Indeterminate;
            chkPresetHDR7.Location = new Point(588, 464);
            chkPresetHDR7.Name = "chkPresetHDR7";
            chkPresetHDR7.Size = new Size(94, 36);
            chkPresetHDR7.TabIndex = 24;
            chkPresetHDR7.Text = "HDR";
            chkPresetHDR7.ThreeState = true;
            chkPresetHDR7.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR8
            // 
            chkPresetHDR8.AutoSize = true;
            chkPresetHDR8.Checked = true;
            chkPresetHDR8.CheckState = CheckState.Indeterminate;
            chkPresetHDR8.Location = new Point(588, 510);
            chkPresetHDR8.Name = "chkPresetHDR8";
            chkPresetHDR8.Size = new Size(94, 36);
            chkPresetHDR8.TabIndex = 25;
            chkPresetHDR8.Text = "HDR";
            chkPresetHDR8.ThreeState = true;
            chkPresetHDR8.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR9
            // 
            chkPresetHDR9.AutoSize = true;
            chkPresetHDR9.Checked = true;
            chkPresetHDR9.CheckState = CheckState.Indeterminate;
            chkPresetHDR9.Location = new Point(588, 556);
            chkPresetHDR9.Name = "chkPresetHDR9";
            chkPresetHDR9.Size = new Size(94, 36);
            chkPresetHDR9.TabIndex = 26;
            chkPresetHDR9.Text = "HDR";
            chkPresetHDR9.ThreeState = true;
            chkPresetHDR9.UseVisualStyleBackColor = true;
            // 
            // chkPresetHDR0
            // 
            chkPresetHDR0.AutoSize = true;
            chkPresetHDR0.Checked = true;
            chkPresetHDR0.CheckState = CheckState.Indeterminate;
            chkPresetHDR0.Location = new Point(588, 602);
            chkPresetHDR0.Name = "chkPresetHDR0";
            chkPresetHDR0.Size = new Size(94, 36);
            chkPresetHDR0.TabIndex = 27;
            chkPresetHDR0.Text = "HDR";
            chkPresetHDR0.ThreeState = true;
            chkPresetHDR0.UseVisualStyleBackColor = true;
            // 
            // hotkeyGame
            // 
            hotkeyGame.Location = new Point(688, 53);
            hotkeyGame.Name = "hotkeyGame";
            hotkeyGame.ReadOnly = true;
            hotkeyGame.Size = new Size(387, 39);
            hotkeyGame.TabIndex = 28;
            hotkeyGame.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset1
            // 
            hotkeyPreset1.Location = new Point(688, 185);
            hotkeyPreset1.Name = "hotkeyPreset1";
            hotkeyPreset1.ReadOnly = true;
            hotkeyPreset1.Size = new Size(387, 39);
            hotkeyPreset1.TabIndex = 29;
            hotkeyPreset1.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset2
            // 
            hotkeyPreset2.Location = new Point(688, 231);
            hotkeyPreset2.Name = "hotkeyPreset2";
            hotkeyPreset2.ReadOnly = true;
            hotkeyPreset2.Size = new Size(387, 39);
            hotkeyPreset2.TabIndex = 30;
            hotkeyPreset2.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset3
            // 
            hotkeyPreset3.Location = new Point(688, 277);
            hotkeyPreset3.Name = "hotkeyPreset3";
            hotkeyPreset3.ReadOnly = true;
            hotkeyPreset3.Size = new Size(387, 39);
            hotkeyPreset3.TabIndex = 31;
            hotkeyPreset3.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset4
            // 
            hotkeyPreset4.Location = new Point(688, 323);
            hotkeyPreset4.Name = "hotkeyPreset4";
            hotkeyPreset4.ReadOnly = true;
            hotkeyPreset4.Size = new Size(387, 39);
            hotkeyPreset4.TabIndex = 32;
            hotkeyPreset4.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset5
            // 
            hotkeyPreset5.Location = new Point(688, 369);
            hotkeyPreset5.Name = "hotkeyPreset5";
            hotkeyPreset5.ReadOnly = true;
            hotkeyPreset5.Size = new Size(387, 39);
            hotkeyPreset5.TabIndex = 33;
            hotkeyPreset5.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset6
            // 
            hotkeyPreset6.Location = new Point(688, 415);
            hotkeyPreset6.Name = "hotkeyPreset6";
            hotkeyPreset6.ReadOnly = true;
            hotkeyPreset6.Size = new Size(387, 39);
            hotkeyPreset6.TabIndex = 34;
            hotkeyPreset6.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset7
            // 
            hotkeyPreset7.Location = new Point(688, 461);
            hotkeyPreset7.Name = "hotkeyPreset7";
            hotkeyPreset7.ReadOnly = true;
            hotkeyPreset7.Size = new Size(387, 39);
            hotkeyPreset7.TabIndex = 35;
            hotkeyPreset7.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset8
            // 
            hotkeyPreset8.Location = new Point(688, 507);
            hotkeyPreset8.Name = "hotkeyPreset8";
            hotkeyPreset8.ReadOnly = true;
            hotkeyPreset8.Size = new Size(387, 39);
            hotkeyPreset8.TabIndex = 36;
            hotkeyPreset8.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset9
            // 
            hotkeyPreset9.Location = new Point(688, 553);
            hotkeyPreset9.Name = "hotkeyPreset9";
            hotkeyPreset9.ReadOnly = true;
            hotkeyPreset9.Size = new Size(387, 39);
            hotkeyPreset9.TabIndex = 37;
            hotkeyPreset9.Text = "Click to set hotkey ...";
            // 
            // hotkeyPreset0
            // 
            hotkeyPreset0.Location = new Point(688, 601);
            hotkeyPreset0.Name = "hotkeyPreset0";
            hotkeyPreset0.ReadOnly = true;
            hotkeyPreset0.Size = new Size(387, 39);
            hotkeyPreset0.TabIndex = 38;
            hotkeyPreset0.Text = "Click to set hotkey ...";
            // 
            // labelHotkeyHDR
            // 
            labelHotkeyHDR.AutoSize = true;
            labelHotkeyHDR.Location = new Point(536, 651);
            labelHotkeyHDR.Name = "labelHotkeyHDR";
            labelHotkeyHDR.Size = new Size(146, 32);
            labelHotkeyHDR.TabIndex = 39;
            labelHotkeyHDR.Text = "HDR Toggle:";
            // 
            // hotkeyHDR
            // 
            hotkeyHDR.Location = new Point(688, 648);
            hotkeyHDR.Name = "hotkeyHDR";
            hotkeyHDR.ReadOnly = true;
            hotkeyHDR.Size = new Size(354, 39);
            hotkeyHDR.TabIndex = 40;
            hotkeyHDR.Text = "Click to set hotkey ...";
            // 
            // chkRunOnStartup
            // 
            chkRunOnStartup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkRunOnStartup.AutoSize = true;
            chkRunOnStartup.Location = new Point(12, 755);
            chkRunOnStartup.Name = "chkRunOnStartup";
            chkRunOnStartup.Size = new Size(206, 36);
            chkRunOnStartup.TabIndex = 41;
            chkRunOnStartup.Text = "Run on Startup";
            chkRunOnStartup.UseVisualStyleBackColor = true;
            // 
            // butApply
            // 
            butApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butApply.Location = new Point(580, 749);
            butApply.Name = "butApply";
            butApply.Size = new Size(150, 46);
            butApply.TabIndex = 42;
            butApply.Text = "&Apply";
            butApply.UseVisualStyleBackColor = true;
            butApply.Click += butApply_Click;
            // 
            // butOK
            // 
            butOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butOK.DialogResult = DialogResult.OK;
            butOK.Location = new Point(736, 749);
            butOK.Name = "butOK";
            butOK.Size = new Size(150, 46);
            butOK.TabIndex = 43;
            butOK.Text = "&OK";
            butOK.UseVisualStyleBackColor = true;
            butOK.Click += butOK_Click;
            // 
            // butCancel
            // 
            butCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            butCancel.DialogResult = DialogResult.Cancel;
            butCancel.Location = new Point(892, 749);
            butCancel.Name = "butCancel";
            butCancel.Size = new Size(150, 46);
            butCancel.TabIndex = 44;
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
            ClientSize = new Size(1054, 807);
            Controls.Add(butCancel);
            Controls.Add(butOK);
            Controls.Add(butApply);
            Controls.Add(chkRunOnStartup);
            Controls.Add(hotkeyHDR);
            Controls.Add(labelHotkeyHDR);
            Controls.Add(hotkeyPreset0);
            Controls.Add(hotkeyPreset9);
            Controls.Add(hotkeyPreset8);
            Controls.Add(hotkeyPreset7);
            Controls.Add(hotkeyPreset6);
            Controls.Add(hotkeyPreset5);
            Controls.Add(hotkeyPreset4);
            Controls.Add(hotkeyPreset3);
            Controls.Add(hotkeyPreset2);
            Controls.Add(hotkeyPreset1);
            Controls.Add(hotkeyGame);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(1080, 831);
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
        private HotkeyBox hotkeyGame;
        private HotkeyBox hotkeyPreset1;
        private HotkeyBox hotkeyPreset2;
        private HotkeyBox hotkeyPreset3;
        private HotkeyBox hotkeyPreset4;
        private HotkeyBox hotkeyPreset5;
        private HotkeyBox hotkeyPreset6;
        private HotkeyBox hotkeyPreset7;
        private HotkeyBox hotkeyPreset8;
        private HotkeyBox hotkeyPreset9;
        private HotkeyBox hotkeyPreset0;
        private Label labelHotkeyHDR;
        private HotkeyBox hotkeyHDR;
        private CheckBox chkRunOnStartup;
        private Button butApply;
        private Button butOK;
        private Button butCancel;
        private System.Windows.Forms.Timer timerCheckGame;
    }
}
