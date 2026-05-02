using System.Xml.Serialization;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T>:PurpleFileManager<T> where T:Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name):base(name){}
    public PurpleXmlFileManager(string name, string folderpath, string filename, string fileextension = "txt"):base(name,folderpath,filename,fileextension){}

    public override void EditFile(string text)
    {
        T obj = Deserialize();
        obj.ChangeText(text);
        Serialize(obj);
    }
    public override void Serialize(T obj)
    {
        if (obj == null) return;
        Dictionary<string, string> dict = new Dictionary<string, string>();
        DTOPurple tmp = null;
        dict["Type"] = obj.GetType().Name;
        dict["Input"] = obj.Input;
        if (dict["Type"] == "Task1")
        {
            dict["Output"] = (obj as Lab9.Purple.Task1).ToString();
            tmp = new DTOPurple(dict["Type"], dict["Input"], dict["Output"]);
            XmlSerializer xmlSerializer = new XmlSerializer(tmp.GetType());
            ChangeFileFormat("xml");
            using (FileStream fs = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,tmp);
            }
        }
        else if (dict["Type"] == "Task2")
        {
            dict["Output"]=(obj as Lab9.Purple.Task2).ToString();
            tmp = new DTOPurple(dict["Type"], dict["Input"], dict["Output"]);
            XmlSerializer xmlSerializer = new XmlSerializer(tmp.GetType());
            ChangeFileFormat("xml");
            using (FileStream fs = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,tmp);
            }
        }
        else if (dict["Type"] == "Task3")
        {
            dict["Output"] = (obj as Lab9.Purple.Task3).ToString();
            tmp = new DTOPurple(dict["Type"], dict["Input"], dict["Output"], (obj as Lab9.Purple.Task3).Codes);
            XmlSerializer xmlSerializer = new XmlSerializer(tmp.GetType());
            ChangeFileFormat("xml");
            using (FileStream fs = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,tmp);
            }
        }
        else if (dict["Type"] == "Task4")
        {
            dict["Output"] = (obj as Lab9.Purple.Task4).ToString();
            tmp = new DTOPurple(dict["Type"], dict["Input"], dict["Output"], (obj as Lab9.Purple.Task4).Codes);
            XmlSerializer xmlSerializer = new XmlSerializer(tmp.GetType());
            ChangeFileFormat("xml");
            using (FileStream fs = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,tmp);
            }
        }
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null;
        if (!(FileExtension == "xml")) return null;
        T obj = null;
        DTOPurple tmp = null;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DTOPurple));
        try
        {
            using (FileStream fs = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                tmp = xmlSerializer.Deserialize(fs) as DTOPurple;
            }
        }
        catch (Exception)
        {
            tmp = new DTOPurple("Task1", File.ReadAllText(FullPath), "");
        }
        if (tmp.Type == "Task1")
        {
            obj = new Lab9.Purple.Task1(tmp.Input) as T;
        }
        else if (tmp.Type == "Task2")
        {
            obj = new Lab9.Purple.Task2(tmp.Input) as T;
        }
        else if (tmp.Type == "Task3")
        {
            obj = new Lab9.Purple.Task3(tmp.Input) as T;
        }
        else if (tmp.Type == "Task4")
        {
            obj = new Lab9.Purple.Task4(tmp.Input, tmp.Codes) as T;
        }
        obj.Review();
        return obj;
    }

    public override void ChangeFileExtension(string extension = "xml")
    {
        T obj = Deserialize(); 
        ChangeFileFormat(extension);
        Serialize(obj);
    }
}

public class DTOPurple
{
    public string Type { get; set; }
    public string Input { get; set; }
    public string Output { get; set; }
    public (string, char)[] Codes { get; set; }

    public DTOPurple(string type, string input, string output)
    {
        Type = type;
        Input = input;
        Output = output;
        Codes = null;
    }

    public DTOPurple(string type, string input, string output, (string, char)[] codes)
    {
        Type = type;
        Input = input;
        Output = output;
        Codes = codes;
    }

    public DTOPurple()
    {
        Type = null;
        Input = null;
        Output = null;
        Codes = null;
    }
}
