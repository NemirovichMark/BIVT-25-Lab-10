using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Lab9.Purple;

namespace Lab10.Purple
{
    public class DTOPurple
    {
        public string TypeName { get; set; }
        public string Input { get; set; }
        public List<XmlCodeData> Codes { get; set; }

        public DTOPurple()
        {
            Codes = new List<XmlCodeData>();
        }
    }

    public class XmlCodeData
    {
        public string Pair { get; set; }
        public string Code { get; set; }
    }

    public class PurpleXmlFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
    {
        public PurpleXmlFileManager(string name) : base(name)
        {
            ChangeFileFormat("xml");
        }

        public PurpleXmlFileManager(string name, string folderName, string fileName, string fileExtension = "xml")
            : base(name, folderName, fileName, fileExtension)
        {
            ChangeFileFormat("xml");
        }

        public override void ChangeFileExtension(string text)
        {
            T currentTask = Deserialize();
            
            ChangeFileFormat("xml");
            
            if (currentTask != null)
            {
                Serialize(currentTask);
            }
        }

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

            var dto = new DTOPurple
            {
                TypeName = obj.GetType().Name,
                Input = obj.Input ?? ""
            };

            if (obj is Task4 task4 && task4.Codes != null)
            {
                foreach (var item in task4.Codes)
                {
                    dto.Codes.Add(new XmlCodeData { Pair = item.Item1, Code = item.Item2.ToString() });
                }
            }
            else if (obj is Task3 task3 && task3.Codes != null)
            {
                foreach (var item in task3.Codes)
                {
                    dto.Codes.Add(new XmlCodeData { Pair = item.Item1, Code = item.Item2.ToString() });
                }
            }

            var serializer = new XmlSerializer(typeof(DTOPurple));
            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                serializer.Serialize(stream, dto);
            }
        }

        public override T Deserialize()
        {
            if (!File.Exists(FullPath)) return null;

            try
            {
                DTOPurple dto;
                var serializer = new XmlSerializer(typeof(DTOPurple));
                
                using (var stream = new FileStream(FullPath, FileMode.Open))
                {
                    dto = (DTOPurple)serializer.Deserialize(stream);
                }

                if (dto == null || string.IsNullOrEmpty(dto.TypeName)) return null;

                var codesList = new List<(string, char)>();
                if (dto.Codes != null)
                {
                    foreach (var c in dto.Codes)
                    {
                        if (!string.IsNullOrEmpty(c.Code))
                        {
                            codesList.Add((c.Pair, c.Code[0]));
                        }
                    }
                }

                Lab9.Purple.Purple task;
                var codesArray = codesList.ToArray();

                switch (dto.TypeName)
                {
                    case "Task2":
                        task = new Task2(dto.Input);
                        break;
                    case "Task3":
                        task = new Task3(dto.Input);
                        break;
                    case "Task4":
                        task = new Task4(dto.Input, codesArray);
                        break;
                    case "Task1":
                    default:
                        task = new Task1(dto.Input);
                        break;
                }

                task.Review();
                return (T)task;
            }
            catch
            {
                return null;
            }
        }
    }
}