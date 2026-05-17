namespace Lab9.White
{
    public class Task4 : White
    {
        private int _output;

        public Task4(string text) : base(text)
        {
        }

        public int Output
        {
            get
            {
                return _output;
            }
        }

        public override void Review()
        {
            _output = CalculateSumOfDigits(Input);
        }

        private int CalculateSumOfDigits(string text)//сумма цифр в тексте 
        {
            int sum = 0;

            foreach (char i in text)
            {
                if (char.IsDigit(i))//если симфол цифра 
                {
                    sum += i - '0';//'0' нужен потому что исдиджит это код цифры то есть цифра 0- код 48 след из кода цифры вычитаем код 0 чтобы понять что за цифра 
                }
            }

            return sum;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
