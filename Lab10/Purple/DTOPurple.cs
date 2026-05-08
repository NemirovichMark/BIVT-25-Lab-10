namespace Lab10.Purple
{
    public class DTOPurple
    {
        public string Type { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DTOPurpleCode[] Codes { get; set; }

        public DTOPurple()
        {
            Type = null;
            Input = null;
            Output = null;
            Codes = Array.Empty<DTOPurpleCode>();
        }

        public DTOPurple(Lab9.Purple.Purple task)
        {
            if (task == null)
                return;

            task.Review();
            Type = task.GetType().Name;
            Input = task.Input ;
            Output = task.ToString() ;
            Codes = ReadCodes(task);
        }

        public Lab9.Purple.Purple GetTask()
        {
            Lab9.Purple.Purple task = null;

            if (Type == nameof(Lab9.Purple.Task1))
            {
                task = new Lab9.Purple.Task1(Input);
            }
            else if (Type == nameof(Lab9.Purple.Task2))
            {
                task = new Lab9.Purple.Task2(Input);
            }
            else if (Type == nameof(Lab9.Purple.Task3))
            {
                task = new Lab9.Purple.Task3(Input);
            }
            else if (Type == nameof(Lab9.Purple.Task4))
            {
                task = new Lab9.Purple.Task4(Input, ToPairs(Codes));
            }

            task.Review();
            return task;
        }

        private static DTOPurpleCode[] ReadCodes(Lab9.Purple.Purple task)
        {
            (string, char)[] codes = Array.Empty<(string, char)>();

            if (task is Lab9.Purple.Task3 task3 && task3.Codes != null)
                codes = task3.Codes;
            else if (task is Lab9.Purple.Task4 task4 && task4.Codes != null)
                codes = task4.Codes;

            DTOPurpleCode[] result = new DTOPurpleCode[codes.Length];

            for (int i = 0; i < codes.Length; i++)
                result[i] = new DTOPurpleCode(codes[i].Item1, codes[i].Item2);

            return result;
        }

        private static (string, char)[] ToPairs(DTOPurpleCode[] codes)
        {
            if (codes == null)
                return Array.Empty<(string, char)>();

            (string, char)[] result = new (string, char)[codes.Length];

            for (int i = 0; i < codes.Length; i++)
            {
                result[i] = (codes[i].Pair , codes[i].Code);
            }

            return result;
        }
    }

    public class DTOPurpleCode
    {
        public string Pair { get; set; }
        public char Code { get; set; }

        public DTOPurpleCode()
        {
            Pair = null;
            Code = '\0';
        }

        public DTOPurpleCode(string pair, char code)
        {
            Pair = pair;
            Code = code;
        }
    }
}
