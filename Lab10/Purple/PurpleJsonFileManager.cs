using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    private class JsonPurpleData
    {
        public string Type { get; set; }
        public string Input { get; set; }
        public List<JsonCodeData> Codes { get; set; } = new();
    }

    private class JsonCodeData
    {
        public string Pair { get; set; }
        public string Code { get; set; }
    }

    public PurpleJsonFileManager(string name) : base(name) => ChangeFileFormat("json");

    public PurpleJsonFileManager(string name, string folderName, string fileName, string fileExtension = "json")
        : base(name, folderName, fileName, fileExtension) => ChangeFileFormat("json");

    public override void ChangeFileExtension(string text) => ChangeFileFormat("json");

    public override void EditFile(string text)
    {
        T task = Deserialize();
        if (task == null) return;
        task.ChangeText(text);
        Serialize(task);
    }

    public override void Serialize(T obj)
    {
        if (obj == null) return;

        var data = new JsonPurpleData
        {
            Type = obj.GetType().Name,
            Input = obj.Input
        };

        if (obj is Task4 t4 && t4.Codes != null)
        {
            foreach (var c in t4.Codes) 
                data.Codes.Add(new JsonCodeData { Pair = c.Item1, Code = c.Item2.ToString() });
        }
        else if (obj is Task3 t3 && t3.Codes != null)
        {
            foreach (var c in t3.Codes) 
                data.Codes.Add(new JsonCodeData { Pair = c.Item1, Code = c.Item2.ToString() });
        }

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FullPath, json);
    }

    public override T Deserialize()
    {
        if (!File.Exists(FullPath)) return null;

        try
        {
            string jsonText = File.ReadAllText(FullPath);
            var data = JsonSerializer.Deserialize<JsonPurpleData>(jsonText);
            if (data == null) return null;

            // Восстанавливаем список кодов в формат кортежей
            var codes = new List<(string, char)>();
            foreach (var c in data.Codes)
            {
                if (!string.IsNullOrEmpty(c.Code))
                    codes.Add((c.Pair, c.Code[0]));
            }

            Lab9.Purple.Purple task = data.Type switch
            {
                "Task2" => new Task2(data.Input),
                "Task3" => new Task3(data.Input),
                "Task4" => new Task4(data.Input, codes.ToArray()),
                _ => new Task1(data.Input)
            };

            task.Review();
            
            return (T)task;
        }
        catch { return null; }
    }
}