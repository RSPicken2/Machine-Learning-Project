using System;

namespace NeuralNetworkLibrary
{
    public abstract class MatrixFunctions
    {
        public float Sigmoid(float x)
        {
            //returns x normalised to be between 0 and 1
            return (float)(1 / (1 + Math.Pow(Math.E, -x)));
        }
        public float SigmoidPrime(float x)
        {
            //this is Sigmoid's derivative funtion
            double temp = Math.Pow(Math.E, x);
            temp /= Math.Pow(1 + temp, 2);
            return (float)temp;
        }

        public float[][] Scalar2DMatrixMult(float scalar, float[][] matrix)
        {
            //multiplies a 2D matrix by a float
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] *= scalar;
                }
            }
            return matrix;
        }
        public float[][] Scalar2DMatrixDiv(float scalar, float[][] matrix)
        {
            //divides a 2D matrix by a float
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] /= scalar;
                }
            }
            return matrix;
        }
        public float[][][] Scalar3DMatrixMult(float scalar, float[][][] matrix)
        {
            //multiplies a 3D matrix by a float
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    for (int k = 0; k < matrix[i][j].Length; k++)
                    {
                        matrix[i][j][k] *= scalar;
                    }
                }
            }
            return matrix;
        }
        public float[][][] Scalar3DMatrixDiv(float divider, float[][][] matrix)
        {
            //divides a 2D matrix by a float
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    for (int k = 0; k < matrix[i][j].Length; k++)
                    {
                        matrix[i][j][k] /= divider;
                    }
                }
            }
            return matrix;
        }

        public float[][] MatrixSubtract2D(float[][] mat1, float[][] mat2)
        {
            //subtracts mat2 from mat1
            for (int i = 0; i < mat1.Length; i++)
            {
                for (int j = 0; j < mat1[i].Length; j++)
                {
                    //Console.Write(mat1[i][j] + " ");
                    mat1[i][j] -= mat2[i][j];
                    //Console.Write(mat1[i][j] + " ");
                }
                //Console.WriteLine();
            }
            return mat1;
        }
        public float[][] MatrixAdd2D(float[][] mat1, float[][] mat2)
        {
            //adds mat1 to mat2
            for (int i = 0; i < mat1.Length; i++)
            {
                for (int j = 0; j < mat1[i].Length; j++)
                {
                    //Console.Write(mat1[i][j] + " ");
                    mat1[i][j] += mat2[i][j];
                    //Console.Write(mat1[i][j] + " ");
                }
                //Console.WriteLine();
            }
            return mat1;
        }
        public float[][][] MatrixSubtract3D(float[][][] mat1, float[][][] mat2)
        {
            //subtracts mat2 from mat1
            for (int i = 0; i < mat1.Length; i++)
            {
                for (int j = 0; j < mat1[i].Length; j++)
                {
                    for (int k = 0; k < mat1[i][j].Length; k++)
                    {
                        mat1[i][j][k] -= mat2[i][j][k];
                    }
                }
            }
            return mat1;
        }
        public float[][][] MatrixAdd3D(float[][][] mat1, float[][][] mat2)
        {
            //adds mat2 to mat1
            for (int i = 0; i < mat1.Length; i++)
            {
                for (int j = 0; j < mat1[i].Length; j++)
                {
                    for (int k = 0; k < mat1[i][j].Length; k++)
                    {
                        mat1[i][j][k] += mat2[i][j][k];
                    }
                }
            }
            return mat1;
        }
        public float[] VectorAdd(float[] vec1, float[] vec2)
        {
            //adds vec1 to vec2
            if (vec1.Length != vec2.Length)
            {
                throw new Exception("vectors don't have the same dimentions");
            }
            float[] result = new float[vec1.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = vec1[i] + vec2[i];
            }
            return result;
        }
        public float[] MatrixMult(float[][] mat, float[] vec)
        {
            //multiplies a 2D matrix and a vector
            if (mat.Length != vec.Length)
            {
                throw new Exception("vector and matrix are different lengths");
            }
            float[] result = new float[mat[0].Length];
            for (int i = 0; i < mat[0].Length; i++)
            {
                for (int j = 0; j < vec.Length; j++)
                {
                    result[i] += mat[j][i] * vec[j];
                }
            }
            return result;
        }
        public float[][] Setup2DArray(int[] layerlengths)
        {
            float[][] arr = new float[layerlengths.Length][];
            for (int i = 0; i < layerlengths.Length; i++)
            {
                arr[i] = new float[layerlengths[i]];
            }
            return arr;
        }
        public float[][] Randomise2DArray(float[][] arr, float lower, float upper)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = (float)(rnd.NextDouble() * (upper - lower) + lower);
                }
            }
            return arr;
        }
        public float[][][] SetupWeights(int[] layerlengths)
        {
            Random rnd = new Random();
            //its length - 1 here because the output layer doesnt have any edges to a next layer
            float[][][] arr = new float[layerlengths.Length - 1][][];
            for (int i = 0; i < layerlengths.Length - 1; i++)
            {
                arr[i] = new float[layerlengths[i]][];
                for (int j = 0; j < layerlengths[i]; j++)
                {
                    arr[i][j] = new float[layerlengths[i + 1]];
                    for (int k = 0; k < layerlengths[i + 1]; k++)
                    {
                        arr[i][j][k] = (float)rnd.NextDouble();
                    }
                }
            }
            return arr;
        }
    }
}
