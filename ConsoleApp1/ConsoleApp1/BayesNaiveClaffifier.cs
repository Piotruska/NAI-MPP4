namespace ConsoleApp1;

public class BayesNaiveClaffifier
{
    public readonly List<Tuple<string, List<string>>> trainData;
    public readonly List<Tuple<string, List<string>>> testData;
    private Igraphics _graphics;

    public BayesNaiveClaffifier(List<Tuple<string, List<string>>> trainData, List<Tuple<string, List<string>>> testData,Igraphics graphics)
    {
        this.trainData = trainData;
        this.testData = testData;
        _graphics = graphics;
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