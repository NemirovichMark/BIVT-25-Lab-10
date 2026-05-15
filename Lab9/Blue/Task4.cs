namespace Lab9.Blue
{
    public class Task4 : Blue
    {
        private int _output;

        public int Output => _output;

        public Task4(string input) : base(input)
        {
            _output = 0;
        }

        public override void Review()
        {
            if (Input == null || Input.Length == 0)
            {
                _output = 0;
                return;
            }

            int sum = 0;
            int currentNumber = 0;
            bool insideNumber = false;

            for (int i = 0; i < Input.Length; i++)
            {
                char symbol = Input[i];

                if (char.IsDigit(symbol))
                {
                    currentNumber = currentNumber * 10 + (symbol - '0');
                    insideNumber = true;
                }
                else if (insideNumber)
                {
                    sum += currentNumber;
                    currentNumber = 0;
                    insideNumber = false;
                }
            }

            if (insideNumber)
            {
                sum += currentNumber;
            }

            _output = sum;
        }

        public override string ToString()
        {
            return _output.ToString();
        }
    }
}
