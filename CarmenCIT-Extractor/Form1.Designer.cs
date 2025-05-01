namespace CarmenCIT_Extractor
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.rCyclone = new System.Windows.Forms.RadioButton();
            this.rXwise = new System.Windows.Forms.RadioButton();
            this.rYwise = new System.Windows.Forms.RadioButton();
            this.r7bit = new System.Windows.Forms.RadioButton();
            this.button6 = new System.Windows.Forms.Button();
            this.txtBitfilter = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbit7 = new System.Windows.Forms.CheckBox();
            this.cbit6 = new System.Windows.Forms.CheckBox();
            this.cbit5 = new System.Windows.Forms.CheckBox();
            this.cbit4 = new System.Windows.Forms.CheckBox();
            this.cbit3 = new System.Windows.Forms.CheckBox();
            this.cbit2 = new System.Windows.Forms.CheckBox();
            this.cbit1 = new System.Windows.Forms.CheckBox();
            this.cbit0 = new System.Windows.Forms.CheckBox();
            this.txtBitDepth = new System.Windows.Forms.TextBox();
            this.chkCIMG = new System.Windows.Forms.CheckBox();
            this.chkAspect = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(180, 368);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(198, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Read Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(180, 20);
            this.txtPath.TabIndex = 2;
            this.txtPath.Text = "D:\\BACKUP\\toplamaprg\\oyunlar\\DOS\\wwcs\\";
            // 
            // txtDesc
            // 
            this.txtDesc.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(516, 41);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(416, 365);
            this.txtDesc.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(938, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 503);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(198, 417);
            this.trackBar1.Maximum = 2000;
            this.trackBar1.Minimum = 8;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(214, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 256;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(337, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "776";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(279, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Pattrn";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(501, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(794, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "label2";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(74, 515);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "+128";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(125, 515);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(45, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "-128";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // rCyclone
            // 
            this.rCyclone.AutoSize = true;
            this.rCyclone.Location = new System.Drawing.Point(198, 454);
            this.rCyclone.Name = "rCyclone";
            this.rCyclone.Size = new System.Drawing.Size(84, 17);
            this.rCyclone.TabIndex = 12;
            this.rCyclone.Text = "Plot Cyclone";
            this.rCyclone.UseVisualStyleBackColor = true;
            // 
            // rXwise
            // 
            this.rXwise.AutoSize = true;
            this.rXwise.Checked = true;
            this.rXwise.Location = new System.Drawing.Point(198, 477);
            this.rXwise.Name = "rXwise";
            this.rXwise.Size = new System.Drawing.Size(80, 17);
            this.rXwise.TabIndex = 13;
            this.rXwise.TabStop = true;
            this.rXwise.Text = "Plot X-Wise";
            this.rXwise.UseVisualStyleBackColor = true;
            // 
            // rYwise
            // 
            this.rYwise.AutoSize = true;
            this.rYwise.Location = new System.Drawing.Point(284, 477);
            this.rYwise.Name = "rYwise";
            this.rYwise.Size = new System.Drawing.Size(80, 17);
            this.rYwise.TabIndex = 14;
            this.rYwise.Text = "Plot Y-Wise";
            this.rYwise.UseVisualStyleBackColor = true;
            // 
            // r7bit
            // 
            this.r7bit.AutoSize = true;
            this.r7bit.Location = new System.Drawing.Point(284, 454);
            this.r7bit.Name = "r7bit";
            this.r7bit.Size = new System.Drawing.Size(64, 17);
            this.r7bit.TabIndex = 15;
            this.r7bit.Text = "Plot 7Bit";
            this.r7bit.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(370, 454);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(42, 40);
            this.button6.TabIndex = 16;
            this.button6.Text = "Save PNG";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtBitfilter
            // 
            this.txtBitfilter.Location = new System.Drawing.Point(95, 44);
            this.txtBitfilter.Name = "txtBitfilter";
            this.txtBitfilter.Size = new System.Drawing.Size(53, 20);
            this.txtBitfilter.TabIndex = 17;
            this.txtBitfilter.Text = "2";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(0, 39);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(74, 45);
            this.trackBar2.TabIndex = 18;
            this.trackBar2.Value = 16;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbit7);
            this.groupBox1.Controls.Add(this.cbit6);
            this.groupBox1.Controls.Add(this.cbit5);
            this.groupBox1.Controls.Add(this.cbit4);
            this.groupBox1.Controls.Add(this.txtBitfilter);
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.cbit3);
            this.groupBox1.Controls.Add(this.cbit2);
            this.groupBox1.Controls.Add(this.cbit1);
            this.groupBox1.Controls.Add(this.cbit0);
            this.groupBox1.Location = new System.Drawing.Point(12, 410);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 73);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Bits 7 to 0 (cyclone)";
            // 
            // cbit7
            // 
            this.cbit7.AutoSize = true;
            this.cbit7.Location = new System.Drawing.Point(135, 19);
            this.cbit7.Name = "cbit7";
            this.cbit7.Size = new System.Drawing.Size(49, 17);
            this.cbit7.TabIndex = 7;
            this.cbit7.Text = "cbit7";
            this.cbit7.UseVisualStyleBackColor = true;
            this.cbit7.CheckedChanged += new System.EventHandler(this.cbit7_CheckedChanged);
            // 
            // cbit6
            // 
            this.cbit6.AutoSize = true;
            this.cbit6.Location = new System.Drawing.Point(116, 19);
            this.cbit6.Name = "cbit6";
            this.cbit6.Size = new System.Drawing.Size(49, 17);
            this.cbit6.TabIndex = 6;
            this.cbit6.Text = "cbit6";
            this.cbit6.UseVisualStyleBackColor = true;
            this.cbit6.CheckedChanged += new System.EventHandler(this.cbit6_CheckedChanged);
            // 
            // cbit5
            // 
            this.cbit5.AutoSize = true;
            this.cbit5.Location = new System.Drawing.Point(99, 19);
            this.cbit5.Name = "cbit5";
            this.cbit5.Size = new System.Drawing.Size(49, 17);
            this.cbit5.TabIndex = 5;
            this.cbit5.Text = "cbit5";
            this.cbit5.UseVisualStyleBackColor = true;
            this.cbit5.CheckedChanged += new System.EventHandler(this.cbit5_CheckedChanged);
            // 
            // cbit4
            // 
            this.cbit4.AutoSize = true;
            this.cbit4.Location = new System.Drawing.Point(80, 19);
            this.cbit4.Name = "cbit4";
            this.cbit4.Size = new System.Drawing.Size(49, 17);
            this.cbit4.TabIndex = 4;
            this.cbit4.Text = "cbit4";
            this.cbit4.UseVisualStyleBackColor = true;
            this.cbit4.CheckedChanged += new System.EventHandler(this.cbit4_CheckedChanged);
            // 
            // cbit3
            // 
            this.cbit3.AutoSize = true;
            this.cbit3.Location = new System.Drawing.Point(61, 19);
            this.cbit3.Name = "cbit3";
            this.cbit3.Size = new System.Drawing.Size(49, 17);
            this.cbit3.TabIndex = 3;
            this.cbit3.Text = "cbit3";
            this.cbit3.UseVisualStyleBackColor = true;
            this.cbit3.CheckedChanged += new System.EventHandler(this.cbit3_CheckedChanged);
            // 
            // cbit2
            // 
            this.cbit2.AutoSize = true;
            this.cbit2.Location = new System.Drawing.Point(44, 19);
            this.cbit2.Name = "cbit2";
            this.cbit2.Size = new System.Drawing.Size(49, 17);
            this.cbit2.TabIndex = 2;
            this.cbit2.Text = "cbit2";
            this.cbit2.UseVisualStyleBackColor = true;
            this.cbit2.CheckedChanged += new System.EventHandler(this.cbit2_CheckedChanged);
            // 
            // cbit1
            // 
            this.cbit1.AutoSize = true;
            this.cbit1.Location = new System.Drawing.Point(25, 19);
            this.cbit1.Name = "cbit1";
            this.cbit1.Size = new System.Drawing.Size(49, 17);
            this.cbit1.TabIndex = 1;
            this.cbit1.Text = "cbit1";
            this.cbit1.UseVisualStyleBackColor = true;
            this.cbit1.CheckedChanged += new System.EventHandler(this.cbit1_CheckedChanged);
            // 
            // cbit0
            // 
            this.cbit0.AutoSize = true;
            this.cbit0.Location = new System.Drawing.Point(6, 19);
            this.cbit0.Name = "cbit0";
            this.cbit0.Size = new System.Drawing.Size(49, 17);
            this.cbit0.TabIndex = 0;
            this.cbit0.Text = "cbit0";
            this.cbit0.UseVisualStyleBackColor = true;
            this.cbit0.CheckedChanged += new System.EventHandler(this.cbit0_CheckedChanged);
            // 
            // txtBitDepth
            // 
            this.txtBitDepth.Location = new System.Drawing.Point(72, 489);
            this.txtBitDepth.Name = "txtBitDepth";
            this.txtBitDepth.Size = new System.Drawing.Size(28, 20);
            this.txtBitDepth.TabIndex = 20;
            this.txtBitDepth.Text = "2";
            // 
            // chkCIMG
            // 
            this.chkCIMG.AutoSize = true;
            this.chkCIMG.Location = new System.Drawing.Point(198, 500);
            this.chkCIMG.Name = "chkCIMG";
            this.chkCIMG.Size = new System.Drawing.Size(152, 17);
            this.chkCIMG.TabIndex = 21;
            this.chkCIMG.Text = "Operate On Carmen Image";
            this.chkCIMG.UseVisualStyleBackColor = true;
            // 
            // chkAspect
            // 
            this.chkAspect.AutoSize = true;
            this.chkAspect.Location = new System.Drawing.Point(418, 15);
            this.chkAspect.Name = "chkAspect";
            this.chkAspect.Size = new System.Drawing.Size(77, 17);
            this.chkAspect.TabIndex = 22;
            this.chkAspect.Text = "Aspect 2:1";
            this.chkAspect.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 492);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Bit Depth";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(18, 517);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(50, 20);
            this.txtOffset.TabIndex = 24;
            this.txtOffset.Text = "0";
            this.txtOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(370, 497);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(42, 40);
            this.button7.TabIndex = 25;
            this.button7.Text = "Save BIN";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(196, 523);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "View Hex Strings";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(196, 38);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(314, 368);
            this.listBox2.TabIndex = 27;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 601);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkAspect);
            this.Controls.Add(this.chkCIMG);
            this.Controls.Add(this.txtBitDepth);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.r7bit);
            this.Controls.Add(this.rYwise);
            this.Controls.Add(this.rXwise);
            this.Controls.Add(this.rCyclone);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Carmen Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton rCyclone;
        private System.Windows.Forms.RadioButton rXwise;
        private System.Windows.Forms.RadioButton rYwise;
        private System.Windows.Forms.RadioButton r7bit;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtBitfilter;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbit7;
        private System.Windows.Forms.CheckBox cbit6;
        private System.Windows.Forms.CheckBox cbit5;
        private System.Windows.Forms.CheckBox cbit4;
        private System.Windows.Forms.CheckBox cbit3;
        private System.Windows.Forms.CheckBox cbit2;
        private System.Windows.Forms.CheckBox cbit1;
        private System.Windows.Forms.CheckBox cbit0;
        private System.Windows.Forms.TextBox txtBitDepth;
        private System.Windows.Forms.CheckBox chkCIMG;
        private System.Windows.Forms.CheckBox chkAspect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ListBox listBox2;
    }
}

