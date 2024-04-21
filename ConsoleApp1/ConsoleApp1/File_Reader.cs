namespace ConsoleApp1;

public class File_Reader : IFile_Reader
{
    public override List<Tuple<string,List<string>>> ReadFile(string filePath)
    {
        List<Tuple<string,List<string>>> data = new List<Tuple<string,List<string>>>();
        try {
            using (StreamReader reader = new StreamReader(filePath)) {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var l = line.Split(",");
                    string d = l[0];
                    List<string> att = new List<string>(l.Skip(1));
                    data.Add(Tuple.Create(d,att));
                }
            }
        }
        catch (Exception e) {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return data;
    }
}