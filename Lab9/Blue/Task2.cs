namespace Lab9.Blue;

public class Task2 : Blue
{
    private string _comb;
    private string _output;
    public string Output => _output;
    public string Comb => _comb;
    

    public Task2(string input, string comb) : base(input)
    {
        _output = "";
        _comb = comb;
    }

    public override void Review()
    {
        string result = "";
        string[] words = Input.Split(' ');
        bool added = false; //обозначает, есть ли в слове с подстрокой знаки
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Contains(_comb))
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if (Char.IsPunctuation(words[i][j])) //проверяем наличие знаков пунктуации
                    {
                        if (!Char.IsPunctuation(result[^2])) //смотрим, что было до этого: слово или знак
                        {
                            result = result.TrimEnd(); //если слово, то лепим знак к нему
                            result += words[i][j];
                            added = true;
                        }
                        else
                        {
                            result += words[i][j]; //если знак, то оставляем предшевствующий пробел
                            added = true;
                        }
                    }
                }

                if (added) //если мы добавили знаки, то ставим пробел
                {
                    result += " ";
                    added = false;
                }
            }
            else //слово без подстроки просто добавляем к результату
            {
                result += words[i];
                if (i != words.Length - 1) result += " ";
            }
        }
        _output = result;
    }

    public override string ToString()
    {
        return _output;
    }
}