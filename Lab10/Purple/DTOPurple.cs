namespace Lab10.Purple
{
    public class DTOPurple
    {
        public string TypeName { get; set; }
        public string Input { get; set; }
        public string[] Codes { get; set; }

        public DTOPurple() { }

        public DTOPurple(Lab9.Purple.Purple obj)
        {
            TypeName = obj.GetType().Name;
            Input = obj.Input;
            if (obj is Lab9.Purple.Task4 task4 && task4.Table != null)
            {
                Codes = new string[task4.Table.Length];
                for (int i = 0; i < Codes.Length; i++)
                    Codes[i] = task4.Table[i].Item1 + "|" + task4.Table[i].Item2;
            }
            if (obj is Task3 task3)
            {
                var codesField = typeof(Task3).GetField("_codes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (codesField != null)
                {
                    var codes = codesField.GetValue(task3) as (string, char)[];
                    if (codes != null)
                {
                    Codes = new string[codes.Length];
                    for (int i = 0; i < Codes.Length; i++)
                        Codes[i] = codes[i].Item1 + " " + codes[i].Item2;
        }
    }
}
        }

        internal (string, char)[] GetDecodedCodes()
        {
            if (Codes == null) return Array.Empty<(string, char)>();
            var result = new (string, char)[Codes.Length];
            for (int i = 0; i < Codes.Length; i++)
            {
                int idx = Codes[i].LastIndexOf('|');
                result[i] = (Codes[i].Substring(0, idx), Codes[i][idx + 1]);
            }
            return result;
        }
    }
}
