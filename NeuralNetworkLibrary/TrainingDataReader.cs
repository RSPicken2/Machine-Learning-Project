using System;
using System.IO;

namespace NeuralNetworkLibrary
{
    public class TrainingDataReader
    {
        //binary readers read the binary files storing the labels and images
        BinaryReader imageReader;
        BinaryReader labelReader;
        int numberOfImages;
        int imageWidth;
        int imageHeight;
        //imageCount stores how many images have been read
        int imageCount;

        //End of File - set to true when imageCount = imageLength
        //this can be used for error checking outside this object
        public bool EOF { get; private set; } = false;

        public TrainingDataReader()
        {
            //instantiator 

            imageReader = new BinaryReader(File.Open(
                "emnist-letters/emnist-letters-train-images-idx3-ubyte", FileMode.Open));
            labelReader = new BinaryReader(File.Open(
                "emnist-letters/emnist-letters-train-labels-idx1-ubyte", FileMode.Open));

            imageReader.ReadInt32(); //discard data we don't need
            //the next 3 sets of 32 bytes represent the number of images, height and width 
            numberOfImages = ReadIntFromBinary(imageReader, 32);
            imageHeight = ReadIntFromBinary(imageReader, 32);
            imageWidth = ReadIntFromBinary(imageReader, 32);

            //discard data we don't need as we've already gotten 
            //all we need from the other file
            labelReader.ReadInt64();

            imageCount = 0;
        }
        public void Dispose()
        {
            imageReader.Dispose();
            labelReader.Dispose();
        }
        public TrainingSample GetNextTrainingSample()
        {
            //returns the next training sample in the files

            //this array will store a value for each pixel in an image
            float[] inData = new float[imageHeight * imageWidth];
            for (int i = 0; i < inData.Length; i++)
            {
                //each byte represents a pixel
                inData[i] = (float)imageReader.ReadByte();
                inData[i] /= 255f; //normalise down to 0-1
            }
            //indexing starts at 1 in the file so add 64 when converting to a char
            char inLabel = (char)(labelReader.ReadByte() + 64);

            //instantiate a new sample with the data we've just read
            TrainingSample sample = new TrainingSample
            {
                data = inData,
                label = inLabel
            };
            //increment count of images we've read
            imageCount++;
            //check if we're at the end of the file
            if (imageCount >= numberOfImages) EOF = true;
            return sample;
        }
        int ReadIntFromBinary(BinaryReader binaryReader, int bitCount)
        {
            //This is just to handle reading binary on little endian architectures
            //by flipping the bits around
            byte[] bytes = binaryReader.ReadBytes(bitCount / 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            //start index of 0 because we want all the data
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
