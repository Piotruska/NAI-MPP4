namespace ConsoleApp1;

public abstract class IFile_Reader
{
    public abstract List<Tuple<string,List<string>>> ReadFile(string path);
    
}