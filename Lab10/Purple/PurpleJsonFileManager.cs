using System.Text.Json;

namespace Lab10.Purple;

public class PurpleJsonFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public PurpleJsonFileManager(string name) : base(name, string.Empty, string.Empty, "json")
    {
    }

    public PurpleJsonFileManager(string name, string folder, string fileName, string fileExtension = "json")
        : base(name, folder, fileName, string.IsNullOrWhiteSpace(fileExtension) ? "json" : fileExtension)
    {
    }

    public override void EditFile(string text)
    {
        T? obj = Deserialize();

        if (obj == null)
        {
            return;
        }

        obj.ChangeText(text ?? string.Empty);
        Serialize(obj);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        if (!string.Equals(fileExtension, "json", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string oldPath = FullPath;
        string content = !string.IsNullOrWhiteSpace(oldPath) && File.Exists(oldPath)
            ? File.ReadAllText(oldPath)
            : string.Empty;

        ChangeFileFormat("json");

        if (string.IsNullOrWhiteSpace(FullPath))
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(oldPath) &&
            File.Exists(oldPath) &&
            !string.Equals(oldPath, FullPath, StringComparison.OrdinalIgnoreCase))
        {
            File.WriteAllText(FullPath, content);
            File.Delete(oldPath);
        }
    }

    public override void Serialize(T obj)
    {
        if (obj == null || string.IsNullOrWhiteSpace(FullPath))
        {
            return;
        }

        DTOPurple dto = BuildDto(obj);
        string json = JsonSerializer.Serialize(dto, JsonOptions);
        CreateFile();
        File.WriteAllText(FullPath, json);
    }

    public override T? Deserialize()
    {
        if (string.IsNullOrWhiteSpace(FullPath) || !File.Exists(FullPath))
        {
            return null;
        }

        try
        {
            string content = File.ReadAllText(FullPath);
            DTOPurple? dto = JsonSerializer.Deserialize<DTOPurple>(content);
            return CreateTaskFromDto(dto);
        }
        catch
        {
            return null;
        }
    }
}
