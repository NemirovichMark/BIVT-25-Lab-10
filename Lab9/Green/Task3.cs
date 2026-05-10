namespace Lab9.Green;
public class Task3 : Green
{
    private string[] _output;
    private string _root;
    
    public string[]  Output => _output.ToArray();
    public string Root => _root;
    

    public Task3(string input, string root) : base(input)
    {
        _output = new string[3];
        _root = root;
    }

    public override void Review()
    {
        //запомнить индексы которые начало слов 
        //запомнить индексы когда конеец слов
        // ходить циклом по словам по каждой бкве и сравнивать флагом если да то добовляем в аутпут
        int c = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            if (char.IsLetter(char.ToLower(char.ToLower(Input[i]))))
            {
                if (i == 0)
                {
                    c++;
                }
                
                else if (char.IsLetter(char.ToLower(char.ToLower(Input[i - 1]))) == false)
                {
                    if (char.ToLower(Input[i - 1]) != '-' && char.ToLower(Input[i - 1]) != '\'' && char.ToLower(Input[i - 1]) != '`')
                    {
                        c++;
                    }
                }

            }
        }

        int[] beggining = new int[c];
        int h = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            if (char.IsLetter(char.ToLower(Input[i])))
            {
                if (i == 0)
                {
                    beggining[h] = i;
                    h++;
                }
                else if (char.IsLetter(char.ToLower(Input[i - 1])) == false)
                {
                    if (char.ToLower(Input[i - 1]) != '-' && char.ToLower(Input[i - 1]) != '\'' && char.ToLower(Input[i - 1]) != '`')
                    {
                        beggining[h] = i;
                        h++;
                    }
                }
            }
        }
        
        int[] ending = new int[c];
        int k = 0;
        // Убрали -1, теперь идем до самого конца строки!
        for (int i = 0; i < Input.Length; i++) 
        {
            if (char.IsLetter(char.ToLower(Input[i])))
            {
                // Если это последний символ во всем тексте - это 100% конец слова
                if (i == Input.Length - 1)
                {
                    ending[k] = i;
                    k++;
                }
                // Иначе проверяем следующий символ, как ты и делала
                else if (char.IsLetter(char.ToLower(Input[i + 1])) == false)
                {
                    if (char.ToLower(Input[i + 1]) != '-' && char.ToLower(Input[i + 1]) != '\'' && char.ToLower(Input[i + 1]) != '`')
                    {
                        ending[k] = i;
                        k++;
                    }
                }
            }
        }
        
        string[] words = new string[c];
        for (int i = 0; i < c; i++)
        {
            int start = beggining[i];
            int end = ending[i];

            int length = end - start + 1;

            if ((start > 0 && char.IsDigit(Input[start - 1]) == false) ||
                (end < Input.Length - 1 && char.IsDigit(Input[end + 1]) == false))
            {
                words[i] = Input.Substring(start, length);
            }
            else
            {
                words[i] = "";

            }
        }

        string[] output = new string[c];
        int u = 0;
        for (int i = 0; i < c; i++)
        {
            if (words[i] != "" && words[i].ToLower().Contains(Root.ToLower()))
            {
                bool flag = true;
                for (int j = 0; j < u; j++)
                {
                    if (output[j].ToLower() == words[i].ToLower())
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    output[u] = words[i];
                    u++;
                }
            }
        }
        _output = output.Take(u).ToArray();
    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < _output.Length; i++)
        {
            res += _output[i] + '\n' ;

        }
        
        return res.TrimEnd();
    }
}