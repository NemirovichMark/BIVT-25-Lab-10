namespace Lab10.Purple;

using Newtonsoft.Json.Linq;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleJsonFileManager(string name) : base(name) { }

    public PurpleJsonFileManager(
        string name,
        string folderPath,
        string fileName,
        string fileExtension = "")
        : base(name, folderPath, fileName, fileExtension) { }

    public override void EditFile(string changeFile)
    {
        T obj = Deserialize();
        obj.ChangeText(changeFile);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string newExtension) =>
        base.ChangeFileFormat("json");

    public override void Serialize(T obj)
    {
        ChangeFileFormat("json");
        obj.Review();

        var json = new JObject
        {
            ["Type"] = obj.GetType().AssemblyQualifiedName,
            ["Input"] = obj.Input
        };

        if (obj is Lab9.Purple.Task4 task4)
        {
            var codes = task4.Codes;
            var codesArr = new JArray();
            foreach (var (pair, code) in codes)
            {
                codesArr.Add(new JObject
                {
                    ["Pair"] = pair,
                    ["Code"] = code.ToString()
                });
            }

            json["Codes"] = codesArr;
        }

        File.WriteAllText(FullPath, json.ToString());
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath))
            return null;

        string text = File.ReadAllText(FullPath);
        JObject json = JObject.Parse(text);

        string typeName = json["Type"].ToString();
        string input = json["Input"].ToString();


        Type objectType = Type.GetType(typeName);


        Lab9.Purple.Purple obj;

        if (objectType == typeof(Lab9.Purple.Task1))
            obj = new Lab9.Purple.Task1(input);
        else if (objectType == typeof(Lab9.Purple.Task2))
            obj = new Lab9.Purple.Task2(input);
        else if (objectType == typeof(Lab9.Purple.Task3))
            obj = new Lab9.Purple.Task3(input);
        else if (objectType == typeof(Lab9.Purple.Task4))
        {
            var codes = new List<(string, char)>();
            //var codeArr = json["Codes"];
            if (json["Codes"] is JArray codesArr)
            {
                foreach (var item in codesArr)
                {
                    string pair = item["Pair"].ToString();
                    string codeText = item["Code"].ToString();

                    codes.Add((pair, codeText[0]));
                }
            }

            obj = new Lab9.Purple.Task4(input, codes.ToArray());
        }
        else
            return null;

        obj.Review();
        return (T)obj;
    }
}