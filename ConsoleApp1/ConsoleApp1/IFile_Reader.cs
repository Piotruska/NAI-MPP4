namespace ConsoleApp1;

public abstract class IFile_Reader
{
    public abstract List<KeyValuePair<string,List<string>>> ReadFile(string path);
    
}