namespace ConsoleApp1;

public class BayesNaiveClassifier
{
    public readonly List<Tuple<string, List<string>>> trainData;
    public readonly List<Tuple<string, List<string>>> testData;

    private Dictionary<string,List<Dictionary<string, double>>> _probabilities =
        new Dictionary<string, List<Dictionary<string, double>>>();

    public BayesNaiveClassifier(List<Tuple<string, List<string>>> trainData, List<Tuple<string, List<string>>> testData)
    {
        this.trainData = trainData;
        this.testData = testData;
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
            for (int idx = 1; idx < list.Count ; idx++)
            {
                if (list[idx].ContainsKey(Class.Item2[idx-1]))
                {
                    list[idx][Class.Item2[idx-1]]++;
                }
                else
                {
                    list[idx].Add(Class.Item2[idx-1], 1);
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
                VARIABLE.Value[idx].Add("Smoothing",1.0/(VARIABLE.Value[0]["Total"] + VARIABLE.Value[idx].Count));
            }
        }
    }

    public string Classify(List<string> vector)
    {
        Dictionary<string, double> classification = new Dictionary<string, double>();
        var allrecords = _probabilities["e"][0]["Total"] + _probabilities["p"][0]["Total"];
        classification.Add("e",_probabilities["e"][0]["Total"]/allrecords);
        classification.Add("p",_probabilities["p"][0]["Total"]/allrecords);
        
        for (int idx = 0; idx < vector.Count; idx++)
        {
            if (_probabilities["e"][idx+1].ContainsKey(vector[idx]))
            {
                classification["e"] *= _probabilities["e"][idx + 1][vector[idx]];
            }
            else
            {
                classification["e"] *= _probabilities["e"][idx + 1]["Smoothing"];
            }
            
            if (_probabilities["p"][idx+1].ContainsKey(vector[idx]))
            {
                classification["p"] *= _probabilities["p"][idx + 1][vector[idx]];
            }
            else
            {
                classification["p"] *= _probabilities["p"][idx + 1]["Smoothing"];
            }
            
            
            // classification["e"] *= _probabilities["e"][idx+1].GetValueOrDefault(vector[idx],_probabilities["e"][idx+1]["Smoothing"]);
            // classification["p"] *= _probabilities["p"][idx+1].GetValueOrDefault(vector[idx],_probabilities["p"][idx+1]["Smoothing"]);
        }
        string larger = "";
        if (classification["e"] > classification["p"])
        {
            larger = "e";
        }else if (classification["e"] < classification["p"])
        {
            larger = "p";
        }
    
        return larger;
    }
    
    
    public Dictionary<string,double> Test()
    {
        var testResults = new Dictionary<string, double>();
        double true_positive_countr = 0;
        double false_positive_countr = 0;
        double false_negative_countr = 0;
        double true_negative_countr = 0;
        
        foreach (var VARIABLE in testData)
        {
            var expected = VARIABLE.Item1;
            var result = Classify(VARIABLE.Item2);
            if (result == expected && expected == "e")
            {
                true_positive_countr++;
            }
            if (result == expected && expected == "p")
            {
                true_negative_countr++;
            }
            if (result != expected && expected == "p")
            {
                false_positive_countr++;
            }
            if (result != expected && expected == "e")
            {
                false_negative_countr++;
            }
        }

        double accuracy = (true_positive_countr + true_negative_countr) /
                          (true_positive_countr + true_negative_countr + false_positive_countr + false_negative_countr);
        double precision = true_positive_countr / (true_positive_countr + false_positive_countr);
        double recall = true_positive_countr / (true_positive_countr + false_negative_countr);
        double fMeasuer = (2 * precision * recall) / (precision + recall);
        
        testResults.Add("accuracy",accuracy);
        testResults.Add("precision",precision);
        testResults.Add("recall",recall);
        testResults.Add("fMeasuer",fMeasuer);
        
        return testResults;
    }
}