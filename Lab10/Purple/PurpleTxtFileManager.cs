namespace Lab10.Purple;

public class PurpleTxtFileManager<T>:PurpleFileManager<T> where T:Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name):base(name){}
    public PurpleTxtFileManager(string name, string folderpath, string filename, string fileextension = "txt"):base(name,folderpath,filename,fileextension){}

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
        string convert = "";
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict["Input"] = obj.Input;
        dict["Type"] = obj.GetType().Name;
        if (dict["Type"] == "Task1")
        {
            dict["Output"] = (obj as Lab9.Purple.Task1).ToString();
            convert = $"Input: {dict["Input"]}\n" +
                      $"Output: {dict["Output"]}\n" +
                      $"Type: {dict["Type"]}";
            ChangeFileExtension("txt");
            base.EditFile(convert);
        }
        else if (dict["Type"] == "Task2")
        {
            dict["Output"] = (obj as Lab9.Purple.Task2).ToString();
            convert = $"Input: {dict["Input"]}\n" +
                      $"Output: {dict["Output"]}\n" +
                      $"Type: {dict["Type"]}";
            ChangeFileExtension("txt");
            base.EditFile(convert);
        }
        else if (dict["Type"] == "Task3")
        {
            dict["Output"] = (obj as Lab9.Purple.Task3).ToString();
            int count = 0;
            for (int i = 0; i < (obj as Lab9.Purple.Task3).Codes.Length; i++)
            {
                dict[$"Item{count}"] = (obj as Lab9.Purple.Task3).Codes[i].Item1;
                dict[$"Item{count + 1}"] = (obj as Lab9.Purple.Task3).Codes[i].Item2 + "";
                count += 2;
            }

            convert = $"Input: {dict["Input"]}\n" +
                      $"Output: {dict["Output"]}\n" +
                      $"Type: {dict["Type"]}\n" +
                      $"Codes: \n";
            count = 0;
            for (int i = 0; i < (obj as Lab9.Purple.Task3).Codes.Length; i++)
            {
                if (i < (obj as Lab9.Purple.Task3).Codes.Length - 1)
                {
                    convert += $"Item{count}: {dict[$"Item{count}"]}\n" +
                               $"Item{count + 1}: {dict[$"Item{count + 1}"]}\n";
                }
                else
                {
                    convert += $"Item{count}: {dict[$"Item{count}"]}\n" +
                               $"Item{count + 1}: {dict[$"Item{count + 1}"]}";
                }

                count += 2;
            }
            ChangeFileExtension("txt");
            base.EditFile(convert);
        }
        else if (dict["Type"] == "Task4")
        {
            dict["Output"] = (obj as Lab9.Purple.Task4).ToString();
            int count = 0;
            for (int i = 0; i < (obj as Lab9.Purple.Task4).Codes.Length; i++)
            {
                dict[$"Item{count}"] = (obj as Lab9.Purple.Task4).Codes[i].Item1;
                dict[$"Item{count + 1}"] = (obj as Lab9.Purple.Task4).Codes[i].Item2 + "";
                count += 2;
            }

            convert = $"Input: {dict["Input"]}\n" +
                      $"Output: {dict["Output"]}\n" +
                      $"Type: {dict["Type"]}\n" +
                      $"Codes: \n";
            count = 0;
            for (int i = 0; i < (obj as Lab9.Purple.Task4).Codes.Length; i++)
            {
                if (i < (obj as Lab9.Purple.Task4).Codes.Length - 1)
                {
                    convert += $"Item{count}: {dict[$"Item{count}"]}\n" +
                               $"Item{count + 1}: {dict[$"Item{count + 1}"]}\n";
                }
                else
                {
                    convert += $"Item{count}: {dict[$"Item{count}"]}\n" +
                               $"Item{count + 1}: {dict[$"Item{count + 1}"]}";
                }

                count += 2;
            }
            ChangeFileExtension("txt");
            base.EditFile(convert);
        }
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null;
        if (!(FileExtension == "txt")) return null;
        T obj = null;
        string content = File.ReadAllText(FullPath);
        string[] strings = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        int count = 0;
        for (int i = 0; i < strings.Length; i++)
        {
            if (strings[i].Contains("Input"))
            {
                string[] tmp = strings[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string input = "";
                for (int j = 1; j < tmp.Length; j++)
                {
                    if (j < tmp.Length - 1)
                    {
                        input += tmp[j];
                        input += ' ';
                    }
                    else
                    {
                        input += tmp[j];
                    }
                }

                input = input.Trim(new char[] { ' ', '"' });
                dict["Input"] = input;
            }
            else if (strings[i].Contains("Output"))
            {
                string[] tmp = strings[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string output = "";
                for (int j = 1; j < tmp.Length; j++)
                {
                    if (j < tmp.Length - 1)
                    {
                        output += tmp[j];
                        output += ' ';
                    }
                    else
                    {
                        output += tmp[j];
                    }
                }

                output = output.Trim(new char[] { ' ', '"' });
                dict["Output"] = output;
            }
            else if (strings[i].Contains("Type"))
            {
                string[] tmp = strings[i].Split(':', StringSplitOptions.RemoveEmptyEntries);
                dict["Type"] = tmp[1].Trim(new char[] { ' ', '"' });
            }
            else if (strings[i].Contains("Item"))
            {
                string[] tmp = strings[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                dict[$"Item{count}"] = tmp[1].Trim(new char[] { ' ', '"' });
                count++;
            }
        }

        (string, char)[] array = new (string, char)[count / 2];
        int i1 = 0;
        for (int i = 0; i < count; i+=2)
        {
            array[i1].Item1 = dict[$"Item{i}"];
            char.TryParse(dict[$"Item{i + 1}"], out array[i1].Item2);
            i1++;
        }

        if (!dict.ContainsKey("Type"))
        {
            dict["Input"] = content;
            dict["Type"] = "Task1";
        }
        if (dict["Type"] == "Task1")
        {
            obj = new Lab9.Purple.Task1(dict["Input"]) as T;
        }
        else if (dict["Type"] == "Task2")
        {
            obj = new Lab9.Purple.Task2(dict["Input"]) as T;
        }
        else if (dict["Type"] == "Task3")
        {
            obj = new Lab9.Purple.Task3(dict["Input"]) as T;
        }
        else if (dict["Type"] == "Task4")
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
