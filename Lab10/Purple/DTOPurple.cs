using System;
using System.Xml.Serialization;

namespace Lab10.Purple
{
    public class DTOPurple
    {
        public string Type { get; set; } = string.Empty;
        public string Input { get; set; } = string.Empty;
        public DTOPair[] Codes { get; set; } = new DTOPair[0];
    }

    public class DTOPair
    {
        public string pair { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
    }
}
