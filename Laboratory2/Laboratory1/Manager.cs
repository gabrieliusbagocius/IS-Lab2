using System;
using System.Diagnostics;

namespace Laboratory2
{
    public class Manager
    {

        private static Random r = new Random();

        public Random random
        {
            get { return r; }
            set { r = value; }
        }

        public static void Main()
        {
            var nn = new NeuralNetwork(1, 2, 1);
            double[,] trainingData = new double[20, 2];
            var inputs = new double[1];
            var targets = new double[1];

            for (var i = 0; i < trainingData.GetLength(0); i++)
            {
                trainingData[i, 0] = r.NextDouble();
                trainingData[i, 1] = (1 + 0.6 * Math.Sin(2 * Math.PI * trainingData[i, 0] / 0.7) + 0.3 * Math.Sin(2 * Math.PI * trainingData[i, 0])) / 2;

            }

            for (var n = 0; n < 500000; n++)
            {
                int rand = r.Next(0, trainingData.GetLength(0));
                inputs[0] = trainingData[rand, 0];
                targets[0] = trainingData[rand, 1];
                nn.Train(inputs, targets);
            }

            var inputVariables = new double[] { r.NextDouble(), r.NextDouble(), r.NextDouble(), r.NextDouble() };
            nn.FeedForward(new double[] { inputVariables[0] });
            Console.WriteLine("Actuals: " + (1 + (0.6 * Math.Sin(2 * Math.PI * inputVariables[0] / 0.7)) + (0.3 * Math.Sin(2 * Math.PI * inputVariables[0]))) / 2);
            Console.WriteLine();

            nn.FeedForward(new double[] { inputVariables[1] });
            Console.WriteLine("Actuals: " + (1 + (0.6 * Math.Sin(2 * Math.PI * inputVariables[1] / 0.7)) + (0.3 * Math.Sin(2 * Math.PI * inputVariables[1]))) / 2);
            Console.WriteLine();

            nn.FeedForward(new double[] { inputVariables[2] });
            Console.WriteLine("Actuals: " + (1 + (0.6 * Math.Sin(2 * Math.PI * inputVariables[2] / 0.7)) + (0.3 * Math.Sin(2 * Math.PI * inputVariables[2]))) / 2);
            Console.WriteLine();

            nn.FeedForward(new double[] { inputVariables[3] });
            Console.WriteLine("Actuals: " + (1 + (0.6 * Math.Sin(2 * Math.PI * inputVariables[3] / 0.7)) + (0.3 * Math.Sin(2 * Math.PI * inputVariables[3]))) / 2);
            Console.WriteLine();

            Console.ReadKey();

        }
    }
}