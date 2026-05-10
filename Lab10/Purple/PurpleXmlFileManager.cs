using System.Xml.Serialization;
using Lab9.Purple;

namespace Lab10.Purple;

public class PurpleXmlFileManager<T>: PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleXmlFileManager(string name) : base(name)
    {
    }
    
    public PurpleXmlFileManager(string name, string folderPath, string fileName, string fileExtension = "") : base(name, folderPath,
        fileName, fileExtension)
    {
    }

    public override void EditFile(string content)
    {
        T obj = Deserialize();
        obj.ChangeText(content);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        ChangeFileFormat("xml");
    }

    public override T Deserialize()
    {
        string fullPath = FullPath;
        
        XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

        DTOPurple dto;

        using (StreamReader reader = new StreamReader(fullPath))
        {
            dto = (DTOPurple)serializer.Deserialize(reader);
        }
        
        Type type = Type.GetType($"{dto.Type}, Lab9");
        
        if (type == typeof(Task4))
        {
            (string, char)[] codes = dto.Codes.Select(kvp => (kvp.Key, kvp.Value)).ToArray();
            
            T result = (T)Activator.CreateInstance(type, new object[] { dto.Input, codes }); //чтобы не писать условие для каждого типа объекта
            result.Review();
            return result;
        }
        T result2 = (T)Activator.CreateInstance(type, new object[] {dto.Input});
        result2.Review();
        return result2;
    }

    public override void Serialize(T item)
    {
        string fullPath = FullPath;
        
        DTOPurple dto = new DTOPurple(item);
        
        XmlSerializer serializer = new XmlSerializer(typeof(DTOPurple));

        string xmlString = null;
        
        using (TextWriter writer = new StringWriter())
        {
            serializer.Serialize(writer, dto);
            xmlString = writer.ToString();
        }
        
        CreateFile();
        File.WriteAllText(fullPath, xmlString);
    }
}