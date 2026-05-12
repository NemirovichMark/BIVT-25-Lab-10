using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10.Purple
{
    internal class PurpleTxtFileManager: ISerializer<Lab9.Purple.Purple>
    {
        private string _name;

        private string _extension;
        public string Name => _name;

        public string Extension => _extension;
        
        public PurpleTxtFileManager(string name, string something = "txt")
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

            File.Create(filePath);

            if (File.Exists(filePath)) 
                File.Delete(filePath);

            if (File.Exists(filePath))
                File.Create(filePath);

            File.WriteAllText(filePath, obj.Input);

            File.AppendAllText(filePath, obj.Input);

            Dictionary <string, string> dict = new Dictionary<string, string>();

            dict.Add("Type", obj.GetType().Name);
            dict.Add("Input", obj.Input);

            string[] lines = new string[dict.Count];

            var d = dict.ToArray();

            for(int i = 0; i < lines.Length; i++)
            {
                lines[i] = dict[i].Key + ":" + d[i].Value + Environment.NewLine;
            }
        }
    } 
}
