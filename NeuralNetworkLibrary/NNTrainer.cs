using System;

namespace NeuralNetworkLibrary
{
    public class NNTrainer : MatrixFunctions
    {
        NeuralNetwork nn;
        TrainingDataReader tdReader;

        int numberOfPredictions;
        int numberOfCorrectPredictions;
        public float accuracy { get; private set; }
       // public bool continueTraining { get; private set; }

        public int batchSize { get; private set; }
        public float learningRate { get; private set; }


        public NNTrainer(ref NeuralNetwork nn, int batchSize, float learningRate)
        {
            this.nn = nn;
            this.batchSize = batchSize;
            this.learningRate = learningRate;
            tdReader = new TrainingDataReader();
            accuracy = 0f;
            numberOfCorrectPredictions = 0;
            numberOfPredictions = 0;
           // continueTraining = false;
        }
        public void Dispose()
        {
            tdReader.Dispose();
        }
        public void TrainForSingleBatch(out float currentAccuracy)
        {
            TrainingSample sample;
            char recognisedChar;
            //^1 index means its the first index at the back
            float[] expectedOutputActivations = new float[nn.activations[^1].Length];

            int[] layerlengths = new int[nn.activations.Length];
            for (int i = 0; i < layerlengths.Length; i++)
            {
                layerlengths[i] = nn.activations[i].Length;
            }


            // copy dimentions of array - values will all be overwritten later
            //I have to clone it because arrays are reference types
            float[][][] wDerivativesSum = SetupWeights(layerlengths);
            float[][][] wDerivatives = SetupWeights(layerlengths);
            float[][] bDerivativesSum = Setup2DArray(layerlengths);
            float[][] bDerivatives = Setup2DArray(layerlengths);


            ///////////testing/////////////////////////////////////////////////////

            //sample = tdReader.GetNextTrainingSample();
            //recognisedChar = nn.RecogniseImage(sample.data);



            //the first node in the output layer should be 1 for testing purposes
            //Array.Fill(expectedOutputActivations, 0f);
            //expectedOutputActivations[0] = 1f;
            ////////////////////////////////////////////////

            GetGradients(expectedOutputActivations, ref wDerivativesSum, ref bDerivativesSum);
            for (int i = 0; i < batchSize - 1; i++)
            {
                sample = tdReader.GetNextTrainingSample();

                recognisedChar = nn.RecogniseImage(sample.data);
                accuracy = UpdateAccuracy(sample.label, recognisedChar);

                //expected values should be the label's index as 1 and the rest 0;
                Array.Fill(expectedOutputActivations, 0f);
                expectedOutputActivations[sample.label - 65] = 1f;
                GetGradients(expectedOutputActivations, ref wDerivatives, ref bDerivatives);

                wDerivativesSum = MatrixAdd3D(wDerivativesSum, wDerivatives);
                bDerivativesSum = MatrixAdd2D(bDerivativesSum, bDerivatives);

                //reset training data if at end of file
                if (tdReader.EOF)
                {
                    tdReader.Dispose();
                    tdReader = new TrainingDataReader();
                }
            }

            //get average derivatives over all the samples in the batch
            wDerivatives = Scalar3DMatrixDiv((float)batchSize, wDerivativesSum);
            bDerivatives = Scalar2DMatrixDiv((float)batchSize, bDerivativesSum);

            //remove me
            //OutputGradients(wDerivatives, bDerivatives);
            ///



            //new biases = old biases - learning rate * biases gradients
            nn.biases = MatrixSubtract2D(
                                nn.biases,
                                Scalar2DMatrixMult(learningRate, bDerivatives));

            //new weights = old weights - learning rate * weights gradients
            nn.weights = MatrixSubtract3D(
                                nn.weights,
                                Scalar3DMatrixMult(learningRate, wDerivatives));


            currentAccuracy = accuracy;
        }

        //public void StartTraining()
        //{
        //    TrainingSample sample;
        //    char recognisedChar;
        //    //^1 index means its the first index at the back
        //    float[] expectedOutputActivations = new float[nn.activations[^1].Length];

        //    int[] layerlengths = new int[nn.activations.Length];
        //    for (int i = 0; i < layerlengths.Length; i++)
        //    {
        //        layerlengths[i] = nn.activations[i].Length;
        //    }


        //    // copy dimentions of array - values will all be overwritten later
        //    //I have to clone it because arrays are reference types
        //    float[][][] wDerivativesSum = SetupWeights(layerlengths);
        //    float[][][] wDerivatives = SetupWeights(layerlengths);
        //    float[][] bDerivativesSum = Setup2DArray(layerlengths);
        //    float[][] bDerivatives = Setup2DArray(layerlengths);


        //    ///////////testing/////////////////////////////////////////////////////

        //    //sample = tdReader.GetNextTrainingSample();
        //    //recognisedChar = nn.RecogniseImage(sample.data);



        //    //the first node in the output layer should be 1 for testing purposes
        //    //Array.Fill(expectedOutputActivations, 0f);
        //    //expectedOutputActivations[0] = 1f;
        //    ////////////////////////////////////////////////

        //    continueTraining = true;
        //    while (continueTraining)
        //    {
        //        //get the gradients at least once to overwrite all the values in the sum arrays
        //        GetGradients(expectedOutputActivations, ref wDerivativesSum, ref bDerivativesSum);
        //        for (int i = 0; i < batchSize - 1; i++)
        //        {
        //            sample = tdReader.GetNextTrainingSample();

        //            recognisedChar = nn.RecogniseImage(sample.data);
        //            accuracy = UpdateAccuracy(sample.label, recognisedChar);

        //            //expected values should be the label's index as 1 and the rest 0;
        //            Array.Fill(expectedOutputActivations, 0f);
        //            expectedOutputActivations[sample.label - 65] = 1f;
        //            GetGradients(expectedOutputActivations, ref wDerivatives, ref bDerivatives);

        //            wDerivativesSum = MatrixAdd3D(wDerivativesSum, wDerivatives);
        //            bDerivativesSum = MatrixAdd2D(bDerivativesSum, bDerivatives);

        //            //reset training data if at end of file
        //            if (tdReader.EOF)
        //            {
        //                tdReader.Dispose();
        //                tdReader = new TrainingDataReader();
        //            }
        //        }

        //        //get average derivatives over all the samples in the batch
        //        wDerivatives = Scalar3DMatrixDiv((float)batchSize, wDerivativesSum);
        //        bDerivatives = Scalar2DMatrixDiv((float)batchSize, bDerivativesSum);

        //        //remove me
        //        //OutputGradients(wDerivatives, bDerivatives);
        //        ///



        //        //new biases = old biases - learning rate * biases gradients
        //        nn.biases = MatrixSubtract2D(
        //                            nn.biases,
        //                            Scalar2DMatrixMult(learningRate, bDerivatives));

        //        //new weights = old weights - learning rate * weights gradients
        //        nn.weights = MatrixSubtract3D(
        //                            nn.weights,
        //                            Scalar3DMatrixMult(learningRate, wDerivatives));


        //        //debug
        //        //Console.WriteLine(accuracy);
        //        //NeuralNetwork.SaveNeuralNet("NeuralNetTest2.JSON", nn);
        //        /////////
        //    }
        //    tdReader.Dispose();

        //}

        private float UpdateAccuracy(char correctChar, char recognisedChar)
        {
            if (correctChar == recognisedChar)
            {
                numberOfCorrectPredictions++;
            }
            numberOfPredictions++;
            try
            {
                float accuracy = (float)numberOfCorrectPredictions / (float)numberOfPredictions;

                //if (numberOfPredictions > 100)
                //{
                //    numberOfCorrectPredictions = 0;
                //    numberOfPredictions = 0;
                //}
                return (float)Math.Round((decimal)accuracy, 2);
            }
            catch (DivideByZeroException)
            {
                return numberOfCorrectPredictions;
            }

        }

        void OutputGradients(float[][][] wGradients, float[][] bGradients)
        {
            string output = "";
            output += "weights grads\n";
            for (int i = 0; i < wGradients.Length; i++)
            {
                output += i + " -\n";
                for (int j = 0; j < wGradients[i].Length; j++)
                {
                    for (int k = 0; k < wGradients[i][j].Length; k++)
                    {
                        output += wGradients[i][j][k] + " ";
                    }
                    output += "\n";
                }
                output += "\n";
            }
            output += "\nbiases gradients\n";
            for (int i = 0; i < bGradients.Length; i++)
            {
                output += i + " - ";
                for (int j = 0; j < bGradients[i].Length; j++)
                {
                    output += bGradients[i][j] + " ";
                }
                output += "\n";
            }
            Console.WriteLine(output);
        }

        //public void StopTraining()
        //{
        //    continueTraining = false;
        //}

        private void GetGradients(float[] expectedOutActivs,
                                  ref float[][][] wDerivatives, ref float[][] bDerivatives)
        {

            //get the derivatives for the output layer's weights and biases
            wDerivatives[^1] = GetOutputLWeightsGradient(expectedOutActivs);
            bDerivatives[^1] = GetOutputLBiasesGradient(expectedOutActivs);

            //this loop find the derivatives for all the hidden layers
            //start at 2 because the index from the back of an array starts at 1
            //end at the last hidden layer because the biases in the input layer are never used
            //and there are no weights going into the input layer
            for (int i = 2; i < nn.activations.Length; i++)
            {
                bDerivatives[^i] = GetHiddenLayerBGradients(bDerivatives[^i].Length, nn.weightedSums[^i],
                                                            nn.weights[^(i - 1)], bDerivatives[^(i - 1)]);


                wDerivatives[^i] = GetHiddenLayerWGradients(nn.activations[^i].Length, bDerivatives[^i],
                                                            nn.activations[^(i + 1)]);

            }


        }

        private float[][] GetOutputLWeightsGradient(float[] expectedOutputActivations)
        {
            //Returns an array storing the derivatives for each weight going into the output layer

            //derivatives stores an array for each element in layer L-1 with each of those
            //storing derivatives for the weight connecting that and a node in layer L
            float[][] derivatives = new float[nn.activations[^2].Length][];
            for (int i = 0; i < derivatives.Length; i++)
            {
                derivatives[i] = new float[nn.activations[^1].Length];
            }

            //loop through every node in the second last layer
            for (int k = 0; k < nn.activations[^2].Length; k++)
            {
                //loop through every node in the output layer
                for (int j = 0; j < nn.activations[^1].Length; j++)
                {
                    //each line here represents a partial derivative in the chain
                    float mTemp = nn.activations[^2][k];
                    mTemp *= SigmoidPrime(nn.weightedSums[^1][j]);
                    mTemp *= 2 * (nn.activations[^1][j] - expectedOutputActivations[j]);
                    derivatives[k][j] = mTemp;
                }
            }
            return derivatives;
        }
        private float[] GetOutputLBiasesGradient(float[] expectedOutputActivations)
        {
            //returns an array storing the derivative for each bias in the output layer

            //derivatives store the derivative for each bias in the output layer
            float[] derivatives = new float[nn.activations[^1].Length];
            //loop through every node in the output layer
            for (int j = 0; j < nn.activations[^1].Length; j++)
            {
                //each line here represents a partial derivative in the chain
                float mTemp = SigmoidPrime(nn.weightedSums[^1][j]);
                mTemp *= 2 * (nn.activations[^1][j] - expectedOutputActivations[j]);
                derivatives[j] = mTemp;
            }
            return derivatives;
        }

        private float[] GetHiddenLayerBGradients(int layerLength, float[] nextWeightedSums,
                                                float[][] nextWeights, float[] nextBiasGradients)
        {
            // returns an array storing the derivative for each bias in a hidden layer
            //for this to work, the derivatives for the next layer must have already been calculated

            float[] gradients = new float[layerLength];
            //loop through each node in the layer
            for (int i = 0; i < layerLength; i++)
            {
                gradients[i] = SigmoidPrime(nextWeightedSums[i]);
                float tempSum = 0;
                //this nested loop is used to find an activation's summed impact on the cost func
                for (int j = 0; j < nextBiasGradients.Length; j++)
                {
                    tempSum += nextWeights[i][j] * nextBiasGradients[j];
                }
                gradients[i] *= tempSum;
            }

            return gradients;
        }
        private float[][] GetHiddenLayerWGradients(int layerLength, float[] biasGradients,
                                                 float[] prevActivations)
        {
            //calculate the weights gradients for a layer using that layer's bias gradients
            //this means that the bias gradients have to be calculated first

            //setup array
            float[][] gradients = new float[prevActivations.Length][];
            for (int i = 0; i < gradients.Length; i++)
            {
                gradients[i] = new float[layerLength];
            }
            //find gradients using the equation in the analysis 
            for (int l = 0; l < gradients.Length; l++)
            {
                for (int k = 0; k < gradients[l].Length; k++)
                {
                    gradients[l][k] = biasGradients[k] * prevActivations[l];
                }
            }
            return gradients;
        }

    }
}
