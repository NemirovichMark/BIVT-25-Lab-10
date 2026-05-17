namespace Lab10.Purple;

public sealed class DTOPurple
{
    public string Type { get; set; } = string.Empty;

    public string Input { get; set; } = string.Empty;

    public DTOPurpleCode[] Codes { get; set; } = [];
}

public sealed class DTOPurpleCode
{
    public string Pair { get; set; } = string.Empty;

    public char Code { get; set; }
}
