namespace Lab10.Purple
{
    public class DTOPurple
    {
        public string TypeName { get; set; }
        public string Input { get; set; }
        public (string, char)[] Codes { get; set; }

        public DTOPurple(Lab9.Purple.Purple obj)
        {
            TypeName = obj.GetType().Name;
            Input = obj.Input;
            if (obj is Lab9.Purple.Task4 task4 && task4.Codes != null)
            {
                Codes = new (string, char)[task4.Codes.Length];
                for (int i = 0; i < Codes.Length; i++)
                    Codes[i] = (task4.Codes[i].Item1, task4.Codes[i].Item2);
            }
        }
    }
}
