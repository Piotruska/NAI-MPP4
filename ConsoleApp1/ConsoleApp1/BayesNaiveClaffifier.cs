namespace ConsoleApp1;

public class BayesNaiveClaffifier
{
    public readonly List<Tuple<string, List<string>>> trainData;
    public readonly List<Tuple<string, List<string>>> testData;
    private Igraphics _graphics;

    private Dictionary<string,List<Dictionary<string, double>>> _probabilities =
        new Dictionary<string, List<Dictionary<string, double>>>();

    public BayesNaiveClaffifier(List<Tuple<string, List<string>>> trainData, List<Tuple<string, List<string>>> testData,Igraphics graphics)
    {
        this.trainData = trainData;
        this.testData = testData;
        _graphics = graphics;
        _probabilities.Add("e",new List<Dictionary<string, double>>());
        _probabilities.Add("p",new List<Dictionary<string, double>>());
    }

    public void Train()
    {
        
    }

    public void Classify()
    {
        
    }

    public void CheckError()
    {
        
    }
}