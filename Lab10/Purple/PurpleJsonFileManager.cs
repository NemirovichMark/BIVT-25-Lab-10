using System.Text.Encodings.Web;
using System.Text.Json;
using Lab9.Purple;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T>:PurpleFileManager<T> where T:Lab9.Purple.Purple
{

    public PurpleJsonFileManager(string name) : base(name){}

    public PurpleJsonFileManager(string name, string folderpath, string filename, string fileextension = "txt") : base(
        name, folderpath, filename, fileextension){}
    public override void EditFile(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text)) return;
        if (!File.Exists(FullPath)) return;
        T obj = Deserialize();
        obj.ChangeText(text);
        Serialize(obj);
    }
    public override void Serialize(T obj)
    {
        if (obj == null) return;
        string convert = JsonConvert.SerializeObject(obj, Formatting.Indented);
        Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(convert);
        dict["Type"] = obj.GetType().Name;
        convert = JsonConvert.SerializeObject(dict, Formatting.Indented);
        ChangeFileExtension("json");
        base.EditFile(convert);
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null;
        if (!(FileExtension == "json")) return null;
        string content = File.ReadAllText(FullPath);
        T obj = null;
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] strings = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strings.Length; i++)
        {
            if (strings[i].Contains("Input"))
            {
                strings[i] = strings[i].Replace(@"\""", "\"");
                string[] tmp = strings[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string ans = "";
                for (int j = 1; j < tmp.Length; j++)
                {
                    if (j < tmp.Length - 1)
                    {
                        ans += tmp[j];
                        ans += ' ';
                    }
                    else
                    {
                        ans += tmp[j];
                    }
                }

                ans = ans.Trim(new char[]{'"',','});
                dict["Input"] = ans;
            }
            else if (strings[i].Contains("Type"))
            {
                string[] tmp = strings[i].Split(':', StringSplitOptions.RemoveEmptyEntries);
                dict["Type"] = tmp[1].Trim(new char[] { '"', ' ', ',' });
            }
        }

        int count = 0; 
        for (int i = 0; i < strings.Length; i++) 
        { 
            if (strings[i].Contains("Item")) 
            { 
                string[] tmp = strings[i].Split(':', StringSplitOptions.RemoveEmptyEntries); 
                dict[$"Item{i}"] = tmp[1].Trim(new char[] { ',', ' ', '"' }); 
                count++; 
            } 
        } 
        (string, char)[] array = new (string, char)[count / 2]; 
        count = 0; 
        int i1 = 0; 
        while (i1 < strings.Length) 
        { 
            if (strings[i1].Contains("Item")) 
            { 
                string[] tmp = strings[i1].Split(':', StringSplitOptions.RemoveEmptyEntries); 
                array[count].Item1 = dict[$"Item{i1}"]; 
                char.TryParse(dict[$"Item{i1 + 1}"], out array[count].Item2); 
                count++; 
                i1 += 2; 
            }
            else 
            { 
                i1++; 
            } 
        }

        if (!dict.ContainsKey("Type"))
        {
            dict["Type"] = typeof(Lab9.Purple.Task1).Name;
            dict["Input"] = content;
        }
        if (dict["Type"] == "Task1")
        {
            obj = new Lab9.Purple.Task1(dict["Input"]) as T;
        }
        else if (dict["Type"] == "Task2")
        {
            obj=new Lab9.Purple.Task2(dict["Input"]) as T;
        }
        else if (dict["Type"] == "Task3")
        {
            obj=new Lab9.Purple.Task3(dict["Input"]) as T;
        }
        else if(dict["Type"] == "Task4")
        {
            obj = new Lab9.Purple.Task4(dict["Input"], array) as T;
        }
        obj.Review();
        return obj;
    }

    public override void ChangeFileExtension(string extension)
    {
        base.ChangeFileFormat(extension);
    }
}
