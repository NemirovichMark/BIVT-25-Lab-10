using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab9.White
{
    public class WhiteJsonFileManager : WhiteFileManager
    {
        public WhiteJsonFileManager(string name) : base(name) { }

        public WhiteJsonFileManager(string name, string folderPath, string fileName, string fileExtension = "json")
            : base(name, folderPath, fileName, fileExtension) { }

        public override void EditFile(string content)
        {
            White obj = Deserialize();
            obj?.ChangeText(content);
            Serialize(obj);
        }

        public override void ChangeFileExtension(string newExtension)
        {
            if (newExtension?.ToLower() != "json")
                ChangeFileFormat("json");
            else
                base.ChangeFileExtension(newExtension);
        }

        public override void Serialize(White obj)
        {
            if (obj == null) return;

            var wrapper = new WhiteWrapper
            {
                TypeName = obj.GetType().AssemblyQualifiedName,
                Data = JsonSerializer.Serialize(obj, obj.GetType())
            };

            string json = JsonSerializer.Serialize(wrapper);
            EditFile(json);
        }

        public override White Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            try
            {
                string json = File.ReadAllText(FullPath);
                var wrapper = JsonSerializer.Deserialize<WhiteWrapper>(json);

                if (wrapper == null || string.IsNullOrEmpty(wrapper.TypeName) || string.IsNullOrEmpty(wrapper.Data))
                    return null;

                Type type = Type.GetType(wrapper.TypeName);
                if (type == null) return null;

                return (White)JsonSerializer.Deserialize(wrapper.Data, type);
            }
            catch
            {
                return null;
            }
        }

        private class WhiteWrapper
        {
            public string TypeName { get; set; }
            public string Data { get; set; }
        }
    }
}
