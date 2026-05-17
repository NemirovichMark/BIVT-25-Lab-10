using System.Text;
using System.Text.Json;

namespace Lab10.Purple;

public class PurpleTxtFileManager<T> : PurpleFileManager<T> where T : Lab9.Purple.Purple
{
    public PurpleTxtFileManager(string name) : base(name, string.Empty, string.Empty, "txt")
    {
    }

    public PurpleTxtFileManager(string name, string folder, string fileName, string fileExtension = "txt")
        : base(name, folder, fileName, string.IsNullOrWhiteSpace(fileExtension) ? "txt" : fileExtension)
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
        if (!string.Equals(fileExtension, "txt", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        string oldPath = FullPath;
        string content = !string.IsNullOrWhiteSpace(oldPath) && File.Exists(oldPath)
            ? File.ReadAllText(oldPath)
            : string.Empty;

        ChangeFileFormat("txt");

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
        string data = JsonSerializer.Serialize(dto);

        StringBuilder builder = new();
        builder.AppendLine($"Type:{dto.Type}");
        builder.AppendLine("Input:");
        builder.AppendLine(dto.Input ?? string.Empty);
        builder.Append("Data:");
        builder.Append(data);

        CreateFile();
        File.WriteAllText(FullPath, builder.ToString());
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
            string marker = $"{Environment.NewLine}Data:";
            int markerIndex = content.LastIndexOf(marker, StringComparison.Ordinal);

            if (markerIndex < 0)
            {
                return null;
            }

            string json = content[(markerIndex + marker.Length)..].Trim();
            DTOPurple? dto = JsonSerializer.Deserialize<DTOPurple>(json);
            return CreateTaskFromDto(dto);
        }
        catch
        {
            return null;
        }
    }
}
