
namespace MachineLearning
{
    partial class GenerateNeuralNetwork
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nupNumOfLayers = new System.Windows.Forms.NumericUpDown();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flwlayoutLayers = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSetLayerlengths = new System.Windows.Forms.Button();
            this.lblOut = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupNumOfLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.nupNumOfLayers);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 84);
            this.panel1.TabIndex = 16;
            // 
            // nupNumOfLayers
            // 
            this.nupNumOfLayers.Location = new System.Drawing.Point(0, 37);
            this.nupNumOfLayers.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nupNumOfLayers.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nupNumOfLayers.Name = "nupNumOfLayers";
            this.nupNumOfLayers.Size = new System.Drawing.Size(162, 31);
            this.nupNumOfLayers.TabIndex = 18;
            this.nupNumOfLayers.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(168, 37);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 34);
            this.btnGenerate.TabIndex = 17;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 25);
            this.label1.TabIndex = 17;
            this.label1.Text = "Number of layers";
            // 
            // flwlayoutLayers
            // 
            this.flwlayoutLayers.AutoScroll = true;
            this.flwlayoutLayers.Location = new System.Drawing.Point(0, 90);
            this.flwlayoutLayers.Name = "flwlayoutLayers";
            this.flwlayoutLayers.Size = new System.Drawing.Size(162, 356);
            this.flwlayoutLayers.TabIndex = 18;
            // 
            // btnSetLayerlengths
            // 
            this.btnSetLayerlengths.Location = new System.Drawing.Point(168, 372);
            this.btnSetLayerlengths.Name = "btnSetLayerlengths";
            this.btnSetLayerlengths.Size = new System.Drawing.Size(112, 60);
            this.btnSetLayerlengths.TabIndex = 0;
            this.btnSetLayerlengths.Text = "Set layer Lengths";
            this.btnSetLayerlengths.UseVisualStyleBackColor = true;
            this.btnSetLayerlengths.Click += new System.EventHandler(this.btnSetLayerlengths_Click);
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(372, 90);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(61, 25);
            this.lblOut.TabIndex = 19;
            this.lblOut.Text = "lblOut";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(654, 398);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 34);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // GenerateNeuralNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 444);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.btnSetLayerlengths);
            this.Controls.Add(this.flwlayoutLayers);
            this.Controls.Add(this.panel1);
            this.Name = "GenerateNeuralNetwork";
            this.Text = "GenerateNeuralNetwork";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupNumOfLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FlowLayoutPanel flwlayoutLayers;
        private System.Windows.Forms.Button btnSetLayerlengths;
        private System.Windows.Forms.NumericUpDown nupNumOfLayers;
        private System.Windows.Forms.Label lblOut;
        private System.Windows.Forms.Button btnSave;
    }
}