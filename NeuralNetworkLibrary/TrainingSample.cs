namespace NeuralNetworkLibrary
{
    public struct TrainingSample
    {
        //struct representing a single sample in EMNIST,
        //storing its pixel data and which letter it represents

        //init means the property is readonly after instantiation
        public float[] data { get; init; }
        public char label { get; init; }

    }
}
