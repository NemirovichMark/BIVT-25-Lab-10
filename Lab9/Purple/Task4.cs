using Lab9.Purple;

namespace Lab9.Purple
{
    public class Task4 : Purple
    {
        public (string, char)[] Codes { get; private set; }

        // constructor
        public Task4(string text, (string, char)[] codes) : base(text)
        {
            Codes = codes ?? new (string, char)[0];
        }

        // methods
        public override void Review()
        {
            string result = Input ?? "";


            for (int i = 0; i < Codes.Length; i++)
            {
                string pair = Codes[i].Item1;
                char code = Codes[i].Item2;

                result = result.Replace(code.ToString(), pair);
            }

            Output = result;
        }

        public override string ToString()
        {
            return Output ?? "";
        }
    }
}