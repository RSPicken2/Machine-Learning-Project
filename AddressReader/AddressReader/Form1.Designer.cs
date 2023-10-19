
namespace AddressReader
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pcbxOut = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReadImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.lblInputFile = new System.Windows.Forms.Label();
            this.btnOutputFile = new System.Windows.Forms.Button();
            this.btnInputFile = new System.Windows.Forms.Button();
            this.btnAutoOn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAutoOff = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblReadAddress = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOut)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(-11, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 115);
            this.panel1.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("HoloLens MDL2 Assets", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(218)))), ((int)(((byte)(36)))));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Address Reader";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(218)))), ((int)(((byte)(36)))));
            this.panel2.Location = new System.Drawing.Point(3, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 45);
            this.panel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(16, 290);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 25);
            this.label6.TabIndex = 24;
            this.label6.Text = "Read Address:";
            // 
            // pcbxOut
            // 
            this.pcbxOut.Location = new System.Drawing.Point(389, 224);
            this.pcbxOut.Name = "pcbxOut";
            this.pcbxOut.Size = new System.Drawing.Size(320, 182);
            this.pcbxOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbxOut.TabIndex = 23;
            this.pcbxOut.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(389, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 28);
            this.label5.TabIndex = 22;
            this.label5.Text = "Manual Input";
            // 
            // btnReadImage
            // 
            this.btnReadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnReadImage.ForeColor = System.Drawing.SystemColors.Control;
            this.btnReadImage.Location = new System.Drawing.Point(389, 151);
            this.btnReadImage.Name = "btnReadImage";
            this.btnReadImage.Size = new System.Drawing.Size(201, 70);
            this.btnReadImage.TabIndex = 21;
            this.btnReadImage.Text = "Read From Single Image";
            this.btnReadImage.UseVisualStyleBackColor = false;
            this.btnReadImage.Click += new System.EventHandler(this.btnReadImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 20;
            this.label3.Text = "Automation";
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblOutputFile.Location = new System.Drawing.Point(186, 154);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(114, 25);
            this.lblOutputFile.TabIndex = 19;
            this.lblOutputFile.Text = "lblOutputFile";
            // 
            // lblInputFile
            // 
            this.lblInputFile.AutoSize = true;
            this.lblInputFile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblInputFile.Location = new System.Drawing.Point(190, 114);
            this.lblInputFile.Name = "lblInputFile";
            this.lblInputFile.Size = new System.Drawing.Size(99, 25);
            this.lblInputFile.TabIndex = 18;
            this.lblInputFile.Text = "lblInputFile";
            // 
            // btnOutputFile
            // 
            this.btnOutputFile.Location = new System.Drawing.Point(12, 148);
            this.btnOutputFile.Name = "btnOutputFile";
            this.btnOutputFile.Size = new System.Drawing.Size(172, 36);
            this.btnOutputFile.TabIndex = 17;
            this.btnOutputFile.Text = "Chose Output File";
            this.btnOutputFile.UseVisualStyleBackColor = true;
            this.btnOutputFile.Click += new System.EventHandler(this.btnOutputFile_Click);
            // 
            // btnInputFile
            // 
            this.btnInputFile.Location = new System.Drawing.Point(12, 108);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(172, 36);
            this.btnInputFile.TabIndex = 16;
            this.btnInputFile.Text = "Chose Input Folder";
            this.btnInputFile.UseVisualStyleBackColor = true;
            this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_Click);
            // 
            // btnAutoOn
            // 
            this.btnAutoOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnAutoOn.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAutoOn.Location = new System.Drawing.Point(13, 151);
            this.btnAutoOn.Name = "btnAutoOn";
            this.btnAutoOn.Size = new System.Drawing.Size(201, 70);
            this.btnAutoOn.TabIndex = 15;
            this.btnAutoOn.Text = "Turn on Automatic Reading and Output";
            this.btnAutoOn.UseVisualStyleBackColor = false;
            this.btnAutoOn.Click += new System.EventHandler(this.btnAutoOn_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.btnAutoOff);
            this.panel3.Controls.Add(this.btnInputFile);
            this.panel3.Controls.Add(this.btnOutputFile);
            this.panel3.Controls.Add(this.lblOutputFile);
            this.panel3.Controls.Add(this.lblInputFile);
            this.panel3.Location = new System.Drawing.Point(1, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 202);
            this.panel3.TabIndex = 25;
            // 
            // btnAutoOff
            // 
            this.btnAutoOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnAutoOff.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAutoOff.Location = new System.Drawing.Point(12, 33);
            this.btnAutoOff.Name = "btnAutoOff";
            this.btnAutoOff.Size = new System.Drawing.Size(201, 70);
            this.btnAutoOff.TabIndex = 27;
            this.btnAutoOff.Text = "Turn off Automatic Reading and Output";
            this.btnAutoOff.UseVisualStyleBackColor = false;
            this.btnAutoOff.Click += new System.EventHandler(this.btnAutoOff_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.lblReadAddress);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(373, 118);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(380, 364);
            this.panel4.TabIndex = 26;
            // 
            // lblReadAddress
            // 
            this.lblReadAddress.AutoSize = true;
            this.lblReadAddress.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblReadAddress.Location = new System.Drawing.Point(16, 315);
            this.lblReadAddress.Name = "lblReadAddress";
            this.lblReadAddress.Size = new System.Drawing.Size(135, 25);
            this.lblReadAddress.TabIndex = 28;
            this.lblReadAddress.Text = "lblReadAddress";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 494);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pcbxOut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnReadImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAutoOn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOut)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pcbxOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReadImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.Button btnOutputFile;
        private System.Windows.Forms.Button btnInputFile;
        private System.Windows.Forms.Button btnAutoOn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblReadAddress;
        private System.Windows.Forms.Button btnAutoOff;
    }
}

