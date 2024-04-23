using System;
using System.Collections.Generic;
using ConsoleApp1;

public class GraphicsPiotr : Igraphics
{
    private BayesNaiveClassifier _classifier;
    private File_Reader _fileReader;

    public GraphicsPiotr()
    {
        _fileReader = new File_Reader();
        Console.WriteLine("Loading test data...");
        List<Tuple<string,List<string>>> testdata = _fileReader.ReadFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..") + "/agaricus-lepiota.test.data");
        Console.WriteLine("Testing the model, please wait...");
        Console.WriteLine("Loading training data...");
        List<Tuple<string,List<string>>> traindata = _fileReader.ReadFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..") + "/agaricus-lepiota.data");
        Console.WriteLine("Training the model, please wait...");
        _classifier = new BayesNaiveClassifier(traindata,testdata);
    }

    public void DisplayMainMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Mushroom Classification System");
            Console.WriteLine("1. Train Model");
            Console.WriteLine("2. Test Model");
            Console.WriteLine("3. Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    TrainModel();
                    break;
                case "2":
                    TestModel();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Please select a valid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public void TrainModel()
    {
        
        _classifier.Train();
        Console.WriteLine("Model trained successfully.\nPress any key to continue...");
        Console.ReadKey();
    }

    public void TestModel()
    {
        var results = _classifier.Test();
        Console.WriteLine("Test Results:");
        DisplayResults(results);
    }

    private void DisplayResults(Dictionary<string, double> results)
    {
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Key}: {result.Value:P2}");
        }
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
    }
}
