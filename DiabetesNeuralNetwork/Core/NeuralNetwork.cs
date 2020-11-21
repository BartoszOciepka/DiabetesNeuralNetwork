using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Globalization;
using System.IO;

namespace DiabetesNeuralNetwork
{
	class NeuralNetwork
	{
		//Set these based on the specification to other absolute paths or relative ones
		//static string csvFileLocation = "C:\\Users\\barto\\Documents\\Visual Studio 2015\\Projects\\DiabetesNeuralNetwork\\Data\\diabetes.csv";
		//static string savedNetworkLocation = "C:\\Users\\barto\\Documents\\Visual Studio 2015\\Projects\\DiabetesNeuralNetwork\\Data\\network.txt";
		static string csvFileLocation = "C:\\Users\\Benek\\OneDrive\\STUDIA\\MS Mag 2\\1. Modelo i ana sys inf - DPołap\\Projekt 2 - decyzje lekarzy cukrzyca\\DiabetesNeuralNetwork\\Data\\diabetes.csv";
		static string savedNetworkLocation = "C:\\Users\\Benek\\OneDrive\\STUDIA\\MS Mag 2\\1. Modelo i ana sys inf - DPołap\\Projekt 2 - decyzje lekarzy cukrzyca\\DiabetesNeuralNetwork\\Data\\network.txt";
		public static void testData()
		{
			Console.WriteLine("Pregnancies:");
			int pregnancies = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Glucose:");
			int glucose = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Blood pressure:");
			int bloodPressure = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Skin thickness:");
			int skinThickness = Int32.Parse(Console.ReadLine());
			Console.WriteLine("Insulin:");
			int insulin = Int32.Parse(Console.ReadLine());
			Console.WriteLine("BMI:");
			double bmi = Double.Parse(Console.ReadLine());
			Console.WriteLine("Diabetes pedigree function:");
			double diabetesPedigreeFunction = Double.Parse(Console.ReadLine());
			Console.WriteLine("Age:");
			int age = Int32.Parse(Console.ReadLine());

			testData(pregnancies, glucose, bloodPressure, skinThickness, insulin, bmi,
				diabetesPedigreeFunction, age);
		}

		public static void testData(int pregnancies,
			int glucose,
			int bloodPressure,
			int skinThickness,
			int insulin,
			double BMI,
			double diabetesPedigreeFunction,
			int age)
		{
			Network network = Network.Load(savedNetworkLocation);
			double[] data = new double[] {pregnancies, glucose, bloodPressure, skinThickness,
				insulin, BMI, diabetesPedigreeFunction, age};
			double[] result = network.Compute(data);

			Console.WriteLine("Outcome: " + result[0]);
			Console.WriteLine("Outcome: " + Math.Round(result[0]));
		}

		public static void trainNetwork()
		{
			trainNetwork(new int[] { 20, 1}, 1.8);
		}
		public static double trainNetwork(int[] neurons, double alphaValue)
		{
			double[][] allData = loadDataFromCSVFile(csvFileLocation);


			Normalize(allData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });

			Shuffle(allData);

			double[][] input = getColumns(allData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });
			double[][] output = getColumns(allData, new int[] { 8 });

			ActivationNetwork network = new ActivationNetwork(
				new SigmoidFunction(alphaValue), //function in neurons
				8,                      //inputs in the network
				neurons					//neuron count in each layer
				);

			BackPropagationLearning teacher = new BackPropagationLearning(network);

			bool needToStop = false;
			int index = 0;
			double error = 100.0;
			while (!needToStop)
			{
				error = teacher.RunEpoch(input, output);
				index++;

				if (index > 1000) needToStop = true;
			}
			
			Console.WriteLine("Error: " + error);
			network.Save(savedNetworkLocation);

			return error;
		}
		public static void addToDataset()
		{

		}

		public static double[][] loadDataFromCSVFile(string fileLocation)
		{
			try
			{
				//As we are not including the header row
				int lineNumber = getLinesNumberInFile(fileLocation) - 1;
				using (var reader = new StreamReader(fileLocation))
				{
					double[][] allData = new double[lineNumber][];

					//Reading first row with headers first
					reader.ReadLine();

					int index = 0;

					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line.Split(',');

						allData[index] = new double[] {
							double.Parse(values[0], CultureInfo.InvariantCulture),
							double.Parse(values[1], CultureInfo.InvariantCulture),
							double.Parse(values[2], CultureInfo.InvariantCulture),
							double.Parse(values[3], CultureInfo.InvariantCulture),
							double.Parse(values[4], CultureInfo.InvariantCulture),
							double.Parse(values[5], CultureInfo.InvariantCulture),
							double.Parse(values[6], CultureInfo.InvariantCulture),
							double.Parse(values[7], CultureInfo.InvariantCulture),
							double.Parse(values[8], CultureInfo.InvariantCulture)
						};

						index++;
					}

					return allData;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public static int getLinesNumberInFile(string fileLocation)
		{
			try
			{
				using (var reader = new StreamReader(fileLocation))
				{
					int i = 0;
					while (reader.ReadLine() != null) { i++; }
					return i;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
		}

		static void Normalize(double[][] dataMatrix, int[] cols)
		{
			foreach (int col in cols)
			{
				double sum = 0.0;
				for (int i = 0; i < dataMatrix.Length; ++i)
					sum += dataMatrix[i][col];
				double mean = sum / dataMatrix.Length;
				sum = 0.0;
				for (int i = 0; i < dataMatrix.Length; ++i)
					sum += (dataMatrix[i][col] - mean) * (dataMatrix[i][col] - mean);

				double sd = Math.Sqrt(sum / (dataMatrix.Length - 1));
				for (int i = 0; i < dataMatrix.Length; ++i)
					dataMatrix[i][col] = (dataMatrix[i][col] - mean) / sd;
			}
		}
		static void ShowVector(double[] vector, int valsPerRow, int decimals, bool newLine)
		{
			for (int i = 0; i < vector.Length; ++i)
			{
				if (i % valsPerRow == 0) Console.WriteLine("");
				Console.Write(vector[i].ToString("F" + decimals).PadLeft(decimals + 4) + " ");
			}
			if (newLine == true) Console.WriteLine("");
		}

		static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool newLine)
		{
			for (int i = 0; i < numRows; ++i)
			{
				Console.Write(i.ToString().PadLeft(3) + ": ");
				for (int j = 0; j < matrix[i].Length; ++j)
				{
					if (matrix[i][j] >= 0.0) Console.Write(" "); else Console.Write("-");
					Console.Write(Math.Abs(matrix[i][j]).ToString("F" + decimals) + " ");
				}
				Console.WriteLine("");
			}
			if (newLine == true) Console.WriteLine("");
		}

		static void Shuffle(double[][] array)
		{
			Random rnd = new Random();
			int n = array.Length;
			for (int i = 0; i < (n - 1); i++)
			{
				int r = rnd.Next(n - 1);
				double[] t = array[r];
				array[r] = array[i];
				array[i] = t;
			}
		}

		static double[][] getColumns(double[][] allData, int[] columns)
		{
			double[][] filteredData = new double[allData.Length][];

			for (int i = 0; i < allData.Length; i++)
			{
				int index = 0;
				filteredData[i] = new double[columns.Length];
				foreach (int column in columns)
				{
					filteredData[i][index] = allData[i][column];
					index++;
				}
			}

			return filteredData;
		}
	}
}
