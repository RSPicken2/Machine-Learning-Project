
namespace MachineLearning
{
    partial class MachineLearning
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
            this.components = new System.ComponentModel.Container();
            this.lblCharDetected = new System.Windows.Forms.Label();
            this.lblSamplesSeen = new System.Windows.Forms.Label();
            this.lblTimeTraining = new System.Windows.Forms.Label();
            this.lblAccuracy = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRecogniseNewChar = new System.Windows.Forms.Button();
            this.btnStartTraining = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadNN = new System.Windows.Forms.Button();
            this.btnSaveNN = new System.Windows.Forms.Button();
            this.pcbxOutput = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenNN = new System.Windows.Forms.Button();
            this.nupLearningRate = new System.Windows.Forms.NumericUpDown();
            this.nupBatchSize = new System.Windows.Forms.NumericUpDown();
            this.btnStopTraining = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOutput)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBatchSize)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCharDetected
            // 
            this.lblCharDetected.AutoSize = true;
            this.lblCharDetected.Location = new System.Drawing.Point(654, 386);
            this.lblCharDetected.Name = "lblCharDetected";
            this.lblCharDetected.Size = new System.Drawing.Size(138, 25);
            this.lblCharDetected.TabIndex = 30;
            this.lblCharDetected.Text = "lblCharDetected";
            // 
            // lblSamplesSeen
            // 
            this.lblSamplesSeen.AutoSize = true;
            this.lblSamplesSeen.Location = new System.Drawing.Point(157, 318);
            this.lblSamplesSeen.Name = "lblSamplesSeen";
            this.lblSamplesSeen.Size = new System.Drawing.Size(136, 25);
            this.lblSamplesSeen.TabIndex = 29;
            this.lblSamplesSeen.Text = "lblSamplesSeen";
            // 
            // lblTimeTraining
            // 
            this.lblTimeTraining.AutoSize = true;
            this.lblTimeTraining.Location = new System.Drawing.Point(157, 284);
            this.lblTimeTraining.Name = "lblTimeTraining";
            this.lblTimeTraining.Size = new System.Drawing.Size(130, 25);
            this.lblTimeTraining.TabIndex = 28;
            this.lblTimeTraining.Text = "lblTimeTraining";
            // 
            // lblAccuracy
            // 
            this.lblAccuracy.AutoSize = true;
            this.lblAccuracy.Location = new System.Drawing.Point(157, 250);
            this.lblAccuracy.Name = "lblAccuracy";
            this.lblAccuracy.Size = new System.Drawing.Size(101, 25);
            this.lblAccuracy.TabIndex = 27;
            this.lblAccuracy.Text = "lblAccuracy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 25);
            this.label6.TabIndex = 24;
            this.label6.Text = "Batch Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "Learning Rate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 22;
            this.label4.Text = "Samples Seen:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 386);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "Character Detected:";
            // 
            // btnRecogniseNewChar
            // 
            this.btnRecogniseNewChar.Location = new System.Drawing.Point(326, 103);
            this.btnRecogniseNewChar.Name = "btnRecogniseNewChar";
            this.btnRecogniseNewChar.Size = new System.Drawing.Size(150, 70);
            this.btnRecogniseNewChar.TabIndex = 17;
            this.btnRecogniseNewChar.Text = "Try to Recognise new character";
            this.btnRecogniseNewChar.UseVisualStyleBackColor = true;
            this.btnRecogniseNewChar.Click += new System.EventHandler(this.btnRecogniseNewChar_Click);
            // 
            // btnStartTraining
            // 
            this.btnStartTraining.Location = new System.Drawing.Point(4, 103);
            this.btnStartTraining.Name = "btnStartTraining";
            this.btnStartTraining.Size = new System.Drawing.Size(150, 70);
            this.btnStartTraining.TabIndex = 18;
            this.btnStartTraining.Text = "Start Training";
            this.btnStartTraining.UseVisualStyleBackColor = true;
            this.btnStartTraining.Click += new System.EventHandler(this.btnStartTraining_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Time Training: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Training Accuracy:";
            // 
            // btnLoadNN
            // 
            this.btnLoadNN.Location = new System.Drawing.Point(334, 3);
            this.btnLoadNN.Name = "btnLoadNN";
            this.btnLoadNN.Size = new System.Drawing.Size(150, 70);
            this.btnLoadNN.TabIndex = 2;
            this.btnLoadNN.Text = "Load Neural Newtwork";
            this.btnLoadNN.UseVisualStyleBackColor = true;
            this.btnLoadNN.Click += new System.EventHandler(this.btnLoadNN_Click);
            // 
            // btnSaveNN
            // 
            this.btnSaveNN.Location = new System.Drawing.Point(174, 3);
            this.btnSaveNN.Name = "btnSaveNN";
            this.btnSaveNN.Size = new System.Drawing.Size(150, 70);
            this.btnSaveNN.TabIndex = 1;
            this.btnSaveNN.Text = "Save Neural Newtwork";
            this.btnSaveNN.UseVisualStyleBackColor = true;
            this.btnSaveNN.Click += new System.EventHandler(this.btnSaveNN_Click);
            // 
            // pcbxOutput
            // 
            this.pcbxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcbxOutput.Location = new System.Drawing.Point(482, 103);
            this.pcbxOutput.Name = "pcbxOutput";
            this.pcbxOutput.Size = new System.Drawing.Size(280, 280);
            this.pcbxOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbxOutput.TabIndex = 20;
            this.pcbxOutput.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnLoadNN);
            this.panel1.Controls.Add(this.btnSaveNN);
            this.panel1.Controls.Add(this.btnGenNN);
            this.panel1.Location = new System.Drawing.Point(-8, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 84);
            this.panel1.TabIndex = 15;
            // 
            // btnGenNN
            // 
            this.btnGenNN.Location = new System.Drawing.Point(14, 3);
            this.btnGenNN.Name = "btnGenNN";
            this.btnGenNN.Size = new System.Drawing.Size(150, 70);
            this.btnGenNN.TabIndex = 0;
            this.btnGenNN.Text = "Generate Neural Network";
            this.btnGenNN.UseVisualStyleBackColor = true;
            this.btnGenNN.Click += new System.EventHandler(this.btnGenNN_Click);
            // 
            // nupLearningRate
            // 
            this.nupLearningRate.DecimalPlaces = 2;
            this.nupLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nupLearningRate.Location = new System.Drawing.Point(162, 176);
            this.nupLearningRate.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupLearningRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nupLearningRate.Name = "nupLearningRate";
            this.nupLearningRate.Size = new System.Drawing.Size(76, 31);
            this.nupLearningRate.TabIndex = 31;
            this.nupLearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // nupBatchSize
            // 
            this.nupBatchSize.Location = new System.Drawing.Point(162, 213);
            this.nupBatchSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupBatchSize.Name = "nupBatchSize";
            this.nupBatchSize.Size = new System.Drawing.Size(76, 31);
            this.nupBatchSize.TabIndex = 32;
            this.nupBatchSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnStopTraining
            // 
            this.btnStopTraining.Location = new System.Drawing.Point(4, 103);
            this.btnStopTraining.Name = "btnStopTraining";
            this.btnStopTraining.Size = new System.Drawing.Size(147, 70);
            this.btnStopTraining.TabIndex = 33;
            this.btnStopTraining.Text = "Stop Training";
            this.btnStopTraining.UseVisualStyleBackColor = true;
            this.btnStopTraining.Click += new System.EventHandler(this.btnStopTraining_Click);
            // 
            // MachineLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 444);
            this.Controls.Add(this.btnStopTraining);
            this.Controls.Add(this.nupBatchSize);
            this.Controls.Add(this.nupLearningRate);
            this.Controls.Add(this.lblCharDetected);
            this.Controls.Add(this.lblSamplesSeen);
            this.Controls.Add(this.lblTimeTraining);
            this.Controls.Add(this.lblAccuracy);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnRecogniseNewChar);
            this.Controls.Add(this.btnStartTraining);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcbxOutput);
            this.Controls.Add(this.panel1);
            this.Name = "MachineLearning";
            this.Text = "Machine Learning";
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOutput)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupLearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBatchSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCharDetected;
        private System.Windows.Forms.Label lblSamplesSeen;
        private System.Windows.Forms.Label lblTimeTraining;
        private System.Windows.Forms.Label lblAccuracy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRecogniseNewChar;
        private System.Windows.Forms.Button btnStartTraining;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadNN;
        private System.Windows.Forms.Button btnSaveNN;
        private System.Windows.Forms.PictureBox pcbxOutput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenNN;
        private System.Windows.Forms.NumericUpDown nupLearningRate;
        private System.Windows.Forms.NumericUpDown nupBatchSize;
        private System.Windows.Forms.Button btnStopTraining;
        private System.Windows.Forms.Timer timer;
    }
}

