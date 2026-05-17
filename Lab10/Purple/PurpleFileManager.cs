namespace Lab10.Purple;

public abstract class PurpleFileManager<T> : MyFileManager, ISerializer<T> where T : Lab9.Purple.Purple
{
    public PurpleFileManager(string name) : base(name)
    {
    }

    public PurpleFileManager(string name, string folder, string fileName, string fileExtension = "")
        : base(name, folder, fileName, fileExtension)
    {
    }

    public override void EditFile(string text)
    {
        if (string.IsNullOrWhiteSpace(FullPath))
        {
            return;
        }

        base.EditFile(text);
    }

    public override void ChangeFileExtension(string fileExtension)
    {
        if (string.IsNullOrWhiteSpace(FullPath))
        {
            return;
        }

        base.ChangeFileExtension(fileExtension);
    }

    public abstract void Serialize(T obj);

    public abstract T? Deserialize();

    protected static DTOPurple BuildDto(Lab9.Purple.Purple? obj)
    {
        DTOPurple dto = new();

        if (obj == null)
        {
            return dto;
        }

        dto.Type = obj.GetType().Name;
        dto.Input = obj.Input ?? string.Empty;

        if (obj is Lab9.Purple.Task4 task4)
        {
            dto.Codes = ConvertCodes(task4.Codes);
        }

        return dto;
    }

    protected static T? CreateTaskFromDto(DTOPurple? dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Type))
        {
            return null;
        }

        Lab9.Purple.Purple? task = dto.Type switch
        {
            nameof(Lab9.Purple.Task1) => new Lab9.Purple.Task1(dto.Input ?? string.Empty),
            nameof(Lab9.Purple.Task2) => new Lab9.Purple.Task2(dto.Input ?? string.Empty),
            nameof(Lab9.Purple.Task3) => new Lab9.Purple.Task3(dto.Input ?? string.Empty),
            nameof(Lab9.Purple.Task4) => new Lab9.Purple.Task4(dto.Input ?? string.Empty, ConvertCodes(dto.Codes)),
            _ => null
        };

        if (task == null)
        {
            return null;
        }

        task.Review();
        return task as T;
    }

    protected static DTOPurpleCode[] ConvertCodes((string, char)[]? codes)
    {
        if (codes == null)
        {
            return [];
        }

        DTOPurpleCode[] result = new DTOPurpleCode[codes.Length];

        for (int i = 0; i < codes.Length; i++)
        {
            result[i] = new DTOPurpleCode
            {
                Pair = codes[i].Item1 ?? string.Empty,
                Code = codes[i].Item2
            };
        }

        return result;
    }

    protected static (string, char)[] ConvertCodes(DTOPurpleCode[]? codes)
    {
        if (codes == null)
        {
            return [];
        }

        (string, char)[] result = new (string, char)[codes.Length];

        for (int i = 0; i < codes.Length; i++)
        {
            string pair = codes[i]?.Pair ?? string.Empty;
            char code = codes[i]?.Code ?? '\0';
            result[i] = (pair, code);
        }

        return result;
    }
}
