using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Blue
{
  public class BlueTxtFileManager<T> : BlueFileManager<T> where T : Lab9.Blue.Blue
  {
    public BlueTxtFileManager(string name) : base(name) { }
    public BlueTxtFileManager(string name, string folder, string file, string extension = "txt") : base(name, folder, file, extension) { }
    public override void EditFile(string countent)
    {
      T obj = Deserialize();
      obj.ChangeText(countent);
      Serialize(obj);
    }
    public override void ChangeFileExtension(string format)
    {
      ChangeFileFormat("txt");
    }
    public override void Serialize(T obj)
    {
      if (obj == null) return;

      CreateFile();
      string text = "";

      text += "Type:" + obj.GetType().Name + Environment.NewLine;
      text += "Input:" + obj.Input + Environment.NewLine;

      if (obj is Lab9.Blue.Task2 task2)
      {
        text += "Sequence:" + task2.Sequence + Environment.NewLine;
      }

      File.WriteAllText(FullPath, text);

    }
    public override T Deserialize()
    {
      if (!File.Exists(FullPath)) return null;

      string[] lines = File.ReadAllLines(FullPath);

      string type = "";
      string input = "";
      string sequence = "";

      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].StartsWith("Type:"))
        {
          type = lines[i].Substring("Type:".Length);
        }
        else if (lines[i].StartsWith("Input:"))
        {
          input = lines[i].Substring("Input:".Length);
        }
        else if (lines[i].StartsWith("Sequence:"))
        {
          sequence = lines[i].Substring("Sequence:".Length);
        }
      }

      Lab9.Blue.Blue obj = null;

      if (type == "Task1")
      {
        obj = new Lab9.Blue.Task1(input);
      }
      else if (type == "Task2")
      {
        obj = new Lab9.Blue.Task2(input, sequence);
      }
      else if (type == "Task3")
      {
        obj = new Lab9.Blue.Task3(input);
      }
      else if (type == "Task4")
      {
        obj = new Lab9.Blue.Task4(input);
      }
      obj.Review();

      return obj as T;
    }
  }
}
