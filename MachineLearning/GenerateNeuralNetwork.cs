using System;
using System.Windows.Forms;
using NeuralNetworkLibrary;

namespace MachineLearning
{
   
    public partial class GenerateNeuralNetwork : Form
    {
        public NeuralNetwork nn;
        public GenerateNeuralNetwork()
        {
            InitializeComponent();
            lblOut.Text = "";
            btnSave.Hide();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //for potential retrys
            btnSave.Hide();

            //this value can only be between 0 and 20 from the designer so validation is handled for char input or input < 1
            int numberOfLayers = (int)nupNumOfLayers.Value;
            //remove any previous NumericUpDowns
            flwlayoutLayers.Controls.Clear();
            for (int i = 0; i < numberOfLayers; i++)
            {
                //using a numericUpDown means only numbers can be entered so I don't have to validate this
                var nudLayerLength = new NumericUpDown();
                nudLayerLength.Minimum = 1;
                nudLayerLength.Maximum = 10000;
                nudLayerLength.Value = 1;

                flwlayoutLayers.Controls.Add(nudLayerLength);
                //moves to new line - make sure flow direction is set to lefttoright
                flwlayoutLayers.SetFlowBreak(nudLayerLength, true);
            }
            NumericUpDown inputlayer = (NumericUpDown)flwlayoutLayers.Controls[0];
            NumericUpDown outputlayer = (NumericUpDown)flwlayoutLayers.Controls[^1];

            inputlayer.Value = 784;
            inputlayer.Enabled = false;
            outputlayer.Value = 28;
            outputlayer.Enabled = false;

        }

        private void btnSetLayerlengths_Click(object sender, EventArgs e)
        {
            //only generate something if there is an input to read
            if(flwlayoutLayers.Controls.Count > 0)
            {
                int[] layerLengths = new int[flwlayoutLayers.Controls.Count];
                for (int i = 0; i < layerLengths.Length; i++)
                {
                    //get the next layer length input
                    NumericUpDown nudLayerLength = (NumericUpDown)flwlayoutLayers.Controls[i];
                    //assign that input's value to the corrisponding layer length
                    layerLengths[i] = (int)nudLayerLength.Value;
                }
                
                nn = new NeuralNetwork(layerLengths);
                DisplayLayerLengths(layerLengths);
                btnSave.Show();
            }
        }
        private void DisplayLayerLengths(int[] layerLengths)
        {
            lblOut.Text = "New Neural Network:\n";
            for (int i = 0; i < layerLengths.Length; i++)
            {
                lblOut.Text += $"layer {i + 1}: {layerLengths[i]} nodes\n";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //after this form closes its parent form will take the public value of nn
            this.Close();
        }
    }
}
