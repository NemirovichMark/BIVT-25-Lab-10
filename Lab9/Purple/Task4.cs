namespace Lab9.Purple
{
    public class Task4(string input, (string, char)[] codes) : Purple(input)
    {
        public new string Output { get; private set; } = "";
        public (string s, char c)[] Codes { get; } = codes;

        public override void Review()
        {
            this.Output = this.Input;

            for (var i = 0; i < this.Codes.Length; i += 1)
            {
                this.Output = this.Output.Replace(this.Codes[i].c.ToString(), this.Codes[i].s);
            }
        }

        public override string ToString() => this.Output;
    }
}