using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetworkLibrary;
using System.Diagnostics;
namespace MachineLearning
{
    public partial class MachineLearning : Form
    {
        public NeuralNetwork nn;
        NNTrainer trainer;
        Stopwatch stopwatch = new Stopwatch();
        bool continueTraining = false;

        public MachineLearning()
        {
            InitializeComponent();
            btnStopTraining.Hide();
            //timer.tick event occures every second and is used to update the training timer
            timer.Tick += Timer_Tick;

            lblAccuracy.Text = "";
            lblCharDetected.Text = "";
            lblSamplesSeen.Text = "";
            lblTimeTraining.Text = "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTimeTraining.Text = Math.Round(stopwatch.Elapsed.TotalSeconds ,1).ToString();
        }

        private void btnGenNN_Click(object sender, EventArgs e)
        {
            var genNN = new GenerateNeuralNetwork();
            genNN.ShowDialog();
            nn = genNN.nn;
            genNN.Dispose();

            //DEBUG
            //MessageBox.Show(nn.ToString());
        }
        private void btnSaveNN_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogue = new SaveFileDialog();
            dialogue.ShowDialog();
            string path = dialogue.FileName;
            dialogue.Dispose();
            NeuralNetwork.SaveNeuralNet(path, nn);
        }

        private void btnLoadNN_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogue = new OpenFileDialog();
            dialogue.ShowDialog();
            string path = dialogue.FileName;
            dialogue.Dispose();
            try
            {
                nn = NeuralNetwork.LoadNeuralNetwork(path);
                //DEBUG
                //MessageBox.Show(nn.ToString());
            }
            catch 
            {
                MessageBox.Show("Error - file selected is not a valid json file");
            }
        }

        private async void btnStartTraining_Click(object sender, EventArgs e)
        {
            //DEBUG
            //MessageBox.Show(nn.ToString());

            //if we have a neural net loaded
            if (nn != null)
            {
                //instantiate a new trainer with the inputs of batch size and learning rate
                //the nn is passed by ref so changes made by the trainer internally will change our loaded one
                trainer = new NNTrainer(ref nn,
                                  (int)nupBatchSize.Value, (float)nupLearningRate.Value);
                //hide the start training button 
                btnStartTraining.Hide();
                //show the stop training button in the same place
                btnStopTraining.Show();

                timer.Start();
                stopwatch.Start();

                //Start training in a new background thread while the rest of the 
                //program can keep running normally using an async method
                //this means the UI won't freeze
                await Task.Run(Train);//StartTrainingAsync(trainer);
            }
            else
            {
                MessageBox.Show("No neural network loaded");
            }
        }

        void Train()
        {
            //trains the neural network continuously until continueTraining is set to false
            //This can only be done externally so this has to be ran as an async task

            //current accuracy to output
            float accuracy;
            //current number of samples used to train the network
            int imagesSeen = 0;

            continueTraining = true;
            while (continueTraining)
            {
                //train neural network with a single batch of samples
                trainer.TrainForSingleBatch(out accuracy);
                
                imagesSeen += trainer.batchSize;

                //as this is running in a different thread, a delegate is needed to output values
                //on the IO thread
                Invoke((MethodInvoker)delegate
                {
                    lblAccuracy.Text = accuracy.ToString();
                    lblSamplesSeen.Text = imagesSeen.ToString();
                });

            }
            //dispose of the trainer at the end to remove its file streams
            trainer.Dispose();
        }

        private void btnStopTraining_Click(object sender, EventArgs e)
        {
            //trainer will need to have been instantiated for this code to run 
            continueTraining = false;
            btnStartTraining.Show();
            btnStopTraining.Hide();
            timer.Stop();
            stopwatch.Reset();

            //DEBUG
            //MessageBox.Show(nn.ToString());
        }
        private void btnRecogniseNewChar_Click(object sender, EventArgs e)
        {
            //reads a random bit of training data and tries to read it

            if (nn != null )
            {
                //if we tried to open the training data file during trainign then we'd get
                //an exception so only do this if we are not training
                if (!continueTraining)
                {
                    TrainingDataReader tdReader = new TrainingDataReader();
                    //skip to a random sample between sample 1 and 1000
                    Random random = new Random();
                    int limit = random.Next(1000);
                    for (int i = 0; i < limit; i++)
                    {
                        tdReader.GetNextTrainingSample();
                    }
                    var sample = tdReader.GetNextTrainingSample();
                    pcbxOutput.Image = ConvertSampleToBitmap(sample.data, 28);
                    //Debug
                    //MessageBox.Show(sample.label + "");
                    lblCharDetected.Text = nn.RecogniseImage(sample.data).ToString();

                    tdReader.Dispose();
                }
                else
                {
                    MessageBox.Show("Cannot use neural network during training");
                }
            }
            else
            {
                MessageBox.Show("No neural network loaded");
            }
        }
        private Bitmap ConvertSampleToBitmap(float[] data, int width)
        {
            Bitmap image = new Bitmap(width, width);
            int counter = 0;
         
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    int brightness = (int)(data[counter++] * 250f);
                    Color colour = Color.FromArgb(brightness, brightness, brightness);
                    image.SetPixel(i, j, colour);
                }

            }
            return image;
        }


    }
}
