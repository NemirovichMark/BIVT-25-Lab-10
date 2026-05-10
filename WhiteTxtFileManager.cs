namespace Lab10.White;

public class WhiteTxtFileManager
{
    private string _name;
    public string Name => _name;
    private string _extension;
    public string Extension => _extension;
    
    public  WhiteTxtFileManager(string name,  string something = "txt")
    {
        _name = name;
        _extension = something;
    }

    public void Serialize(Lab9.Purple.Purple obj)
    {
        var folder = Directory.GetCurrentDirectory();
        
        folder = Directory.GetParent(folder).Parent.Parent.FullName;
        
        folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        
        var filePath = Path.Combine(folder, Name);
        filePath += "." + Extension;
            
        if (File.Exists(filePath))
            File.Delete(filePath);
        if (!File.Exists(filePath))
            File.Create(filePath).Close();
        File.WriteAllText(filePath, obj.Input);
        File.AppendAllText(filePath, obj.Input);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("Type", obj.GetType().Name);
        dict.Add("Input", obj.Input);
        string[] lines = new string[dict.Count];
        var d = dict.ToArray();
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = d[i].Key +  ": " + d[i].Value;
        }
    }
}