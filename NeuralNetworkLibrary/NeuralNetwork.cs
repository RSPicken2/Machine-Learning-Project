using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NeuralNetworkLibrary
{
    public class NeuralNetwork : MatrixFunctions
    {
        [JsonInclude] //this means that a json serialisor will save this attribute
        public float[][] activations;
        [JsonInclude]
        public float[][] weightedSums;
        [JsonInclude]
        public float[][] biases;
        [JsonInclude]
        public float[][][] weights;

        public NeuralNetwork(int[] layerLengths)
        {
            activations = Setup2DArray(layerLengths);
            weightedSums = Setup2DArray(layerLengths);
            biases = Randomise2DArray(Setup2DArray(layerLengths), -1, 1);
            weights = SetupWeights(layerLengths);
        }
        [JsonConstructor] //this means that a json Deserialisor will use this constructor
        public NeuralNetwork(float[][] activations, float[][] weightedSums, float[][] biases, float[][][] weights)
        {
            this.activations = activations;
            this.weightedSums = weightedSums;
            this.biases = biases;
            this.weights = weights;
        }

        #region ImageRecognition
        public char RecogniseImage(float[] imageValues)
        {
            SetInputLayerActivations(imageValues);
            PropegateActivations();
            float largestActivation = GetLargestActivation(activations[activations.Length - 1]);
            return (char)(largestActivation + 65);
        }

        private float GetLargestActivation(float[] activations)
        {
            int currentLargest = 0;
            for (int i = 1; i < activations.Length; i++)
            {
                if (activations[currentLargest] < activations[i])
                {
                    currentLargest = i;
                }
            }
            return currentLargest;
        }

        private void PropegateActivations()
        {
            //Moves forward through the neural network calculating each layer's
            //activations using the activations from the prev layer and the current
            //layer's weights and biases

            //loop through each layer
            for (int i = 1; i < activations.Length; i++)
            {

                //multiply the weights leading into this layer by the prev layer's
                //activations using matrix multiplication
                float[] temp = MatrixMult(weights[i - 1], activations[i - 1]);
                //vector add the biases vector to the result
                temp = VectorAdd(temp, biases[i]);
                //create a byref copy of this weighted sum to be used by a training program
                temp.CopyTo(weightedSums[i], 0);

                //run through each weighted sum we've calculated and normalise them down to 
                //0-1 with the sigmoid function
                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = Sigmoid(temp[j]);
                }
                //set the activations of the current layer to the values we just calculated
                activations[i] = temp;
            }
        }
        private void SetInputLayerActivations(float[] layerActivations)
        {
            //sets the input layer of the neural net's activations to the parameter input
            if (activations[0].Length == layerActivations.Length)
            {
                activations[0] = layerActivations;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        #endregion

        public override string ToString()
        {
            //overrides base ToString method
            //this is mostly for testing 
            string output = "Neural Network Values:\n";
            output += "nodes per layer:\n";
            for (int i = 0; i < activations.Length; i++)
            {
                output += i + " - " + activations[i].Length + "\n";
            }

            //output += "\nactivations\n";
            //for (int i = 0; i < activations.Length; i++)
            //{
            //    output += i + " - ";
            //    for (int j = 0; j < activations[i].Length; j++)
            //    {
            //        output += activations[i][j] + " ";
            //    }
            //    output += "\n";
            //}
            //output += "\nweighted Sums\n";
            //for (int i = 0; i < weightedSums.Length; i++)
            //{
            //    output += i + " - ";
            //    for (int j = 0; j < weightedSums[i].Length; j++)
            //    {
            //        output += weightedSums[i][j] + " ";
            //    }
            //    output += "\n";
            //}
            //output += "\nbiases\n";
            //for (int i = 0; i < biases.Length; i++)
            //{
            //    output += i + " - ";
            //    for (int j = 0; j < biases[i].Length; j++)
            //    {
            //        output += biases[i][j] + " ";
            //    }
            //    output += "\n";
            //}
            //output += "\nweights\n";
            //for (int i = 0; i < weights.Length; i++)
            //{
            //    output += i + " -\n";
            //    for (int j = 0; j < weights[i].Length; j++)
            //    {
            //        for (int k = 0; k < weights[i][j].Length; k++)
            //        {
            //            output += weights[i][j][k] + " ";
            //        }
            //        output += "\n";
            //    }
            //    output += "\n";
            //}
            return output;
        }
        static public void SaveNeuralNet(string filePath, NeuralNetwork nn)
        {
            //saves a neural network to a JSON file
            //These are static so we don't need to instantiate an object for this to work

            string output = JsonSerializer.Serialize(nn,
                    new JsonSerializerOptions { WriteIndented = true });
            //Console.WriteLine(output);
            File.WriteAllText(filePath, output);
        }
        static public NeuralNetwork LoadNeuralNetwork(string filepath)
        {
            //returns a neural network stored in a JSON file - exception thrown if not a json file
            //make sure to validate outside of here
                string input = File.ReadAllText(filepath);
                return JsonSerializer.Deserialize<NeuralNetwork>(input,
                    new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
