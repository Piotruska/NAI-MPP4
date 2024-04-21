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
        for (int idx = 0; idx < 23; idx++)
        {
            _probabilities["e"].Add(new Dictionary<string, double>());
            _probabilities["p"].Add(new Dictionary<string, double>());
        }
        _probabilities["e"][0].Add("Total",0);
        _probabilities["p"][0].Add("Total",0);
    }

    public void Train()
    {
        foreach (var Class in trainData)
        {
            List<Dictionary<string, double>> list = new List<Dictionary<string, double>>();
            if (Class.Item1 == "e")
            {list = _probabilities["e"];
            }else if (Class.Item1 == "p")
            {list = _probabilities["p"];
            }
            list[0]["Total"]++;
            for (int idx = 1; idx < list.Count-1 ; idx++)
            {
                if (list[idx].ContainsKey(Class.Item2[idx]))
                {
                    list[idx][Class.Item2[idx]]++;
                }
                else
                {
                    list[idx].Add(Class.Item2[idx], 1);
                }
            }
        }

        foreach (var VARIABLE in _probabilities)
        {
            
            for (int idx = 1; idx < 23; idx++)
            {
                foreach (var att in VARIABLE.Value[idx])
                {
                    var x = att.Key;
                    VARIABLE.Value[idx][x] = att.Value / VARIABLE.Value[0]["Total"];
                    
                }
                // Smoothing for each attrbute
                VARIABLE.Value[idx].Add("Smoothing",1/VARIABLE.Value[0]["Total"] + VARIABLE.Value[idx].Count);
            }
        }
    }

    public void Classify()
    {
        
    }

    public void CheckError()
    {
        
    }
}