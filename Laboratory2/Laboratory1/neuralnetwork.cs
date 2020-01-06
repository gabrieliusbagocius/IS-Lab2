using System;
using System.Diagnostics;
using System.Collections;
using Laboratory2;

namespace Laboratory2
{
    public class NeuralNetwork
    {
        private int input_nodes;
        private int hidden_nodes;
        private int output_nodes;
        private Matrixs weights_ih;
        private Matrixs weights_ho;
        private Matrixs bias_h;
        private Matrixs bias_o;
        private double learningRate;

        public NeuralNetwork(int input_nodes, int hidden_nodes, int output_nodes)
        {

            this.input_nodes = input_nodes;
            this.hidden_nodes = hidden_nodes;
            this.output_nodes = output_nodes;

            weights_ih = new Matrixs(this.hidden_nodes, this.input_nodes);
            weights_ho = new Matrixs(this.output_nodes, this.hidden_nodes);
            weights_ih.Randomize();
            weights_ho.Randomize();

            bias_h = new Matrixs(this.hidden_nodes, 1);
            bias_o = new Matrixs(this.output_nodes, 1);
            bias_h.Randomize();
            bias_o.Randomize();

            learningRate = 0.1;

        }
        
        public double[,] FeedForward(double[] inputArray)
        {
            // Generating hidden outputs
            var inputs = Matrixs.FromArray(inputArray);
            var hidden = Matrixs.Multiply(weights_ih, inputs);
            hidden.Add(bias_h);

            // Activation function
            hidden.Map(Sigmoid);

            // Generating output's output
            var output = Matrixs.Multiply(weights_ho, hidden);
            output.Add(bias_o);
            output.Map(Sigmoid);
            Console.WriteLine("Results: " + output.data[0, 0]); //+ "  " + Math.Round(output.data[0, 0]));

            // Sending back the results
            return output.data;

        }

        public double[,] Train(double[] inputArray, double[] targetsArray)
        {
            var inputs = Matrixs.FromArray(inputArray);
            var targets = Matrixs.FromArray(targetsArray);
            
            // Generating hidden outputs
            var hidden = Matrixs.Multiply(weights_ih, inputs);
            hidden.Add(bias_h);
            // Activation function
            hidden.Map(Sigmoid);


            // Generating output's output
            var outputs = Matrixs.Multiply(weights_ho, hidden);
            outputs.Add(bias_o);
            // Activation function
            outputs.Map(Sigmoid);


            // Convert array to matrix object
            var outputErrors = Matrixs.Subtract(targets, outputs);


            // Using stochastic gradient descent
            // Calculate gradient
            var gradients = Matrixs.Map(outputs, DSigmoid);
            //outputs.Map(DSigmoid);
            gradients.Multiply(outputErrors.data); // Element wise multuplication
            gradients.Multiply(learningRate);


            // Calculate deltas
            var hidden_T = Matrixs.Transpose(hidden);
            var weights_ho_deltas = Matrixs.Multiply(gradients, hidden_T);

            // Adjust the weights by deltas
            weights_ho.Add(weights_ho_deltas);
            // Adjust the bias by its deltas
            bias_o.Add(gradients);


            // Calculate the error
            // ERROR = TARGETS - OUTPUTS

            // Calculate the hidden layer errors
            var weights_ho_t = Matrixs.Transpose(weights_ho);
            var hidden_errors = Matrixs.Multiply(weights_ho_t, outputErrors);
            

            // Calculate the hidden gradient
            var hidden_gradient = Matrixs.Map(hidden, DSigmoid);
            hidden_gradient.Multiply(hidden_errors.data);
            hidden_gradient.Multiply(learningRate);

            // Calculate input->hidden deltas
            var inputs_T = Matrixs.Transpose(inputs);
            var weight_ih_deltas = Matrixs.Multiply(hidden_gradient, inputs_T);

            // Adjust the weights by deltas
            weights_ih.Add(weight_ih_deltas);
            // Adjust the bias by its deltas
            bias_h.Add(hidden_gradient);

            /*
            Console.Write("Inputs: ");
            Matrixs.DisplayMatrix(inputArray);
            Console.Write("Targets: ");
            Matrixs.DisplayMatrix(targetsArray);
            Console.Write("Errors: ");
            Matrixs.DisplayMatrix(outputErrors);
            Console.WriteLine();
            Console.WriteLine();
            */


            return outputs.data;
        }

        public delegate double CalcSigmoid(double number);

        public static double Sigmoid(double number)
        {
            return 1 / (1 + Math.Exp(-number));
        }

        public static double DSigmoid(double number)
        {
            return number * (1 - number);
        }

        public float[,] FeedForward(float[,] input)
        {
            float[,] guess = new float[1,1];
            return guess;
        }
    }
}