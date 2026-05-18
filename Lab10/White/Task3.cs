using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.White
{
    public class Task3 : White
    {
        private string[,] _codeTable;
        private string _output;
        public string Output => _output;

        public Task3(string input, string[,] codeTable) : base(input)
        {
            _codeTable = codeTable;
            Review();
        }

        public override void Review()
        {
            if (string.IsNullOrEmpty(Input))
            {
                _output = Input ?? "";
                return;
            }

            if (_codeTable == null || _codeTable.GetLength(0) == 0)
            {
                _output = Input;
                return;
            }

            // Разбираем текст на части
            string result = "";
            string currentWord = "";

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];

                // Если это буква, дефис или апостроф - часть слова
                if (char.IsLetter(c) || c == '-' || c == '\'')
                {
                    currentWord += c;
                }
                else
                {
                    // Обрабатываем накопленное слово
                    if (currentWord.Length > 0)
                    {
                        result += ReplaceWord(currentWord);
                        currentWord = "";
                    }
                    // Добавляем разделитель
                    result += c;
                }
            }

            // Обрабатываем последнее слово
            if (currentWord.Length > 0)
            {
                result += ReplaceWord(currentWord);
            }

            _output = result;
        }

        private string ReplaceWord(string word)
        {
            // Ищем слово в таблице кодов
            for (int i = 0; i < _codeTable.GetLength(0); i++)
            {
                if (_codeTable[i, 0] == word)
                {
                    return _codeTable[i, 1];
                }
            }
            return word;
        }

        public override string ToString()
        {
            return _output ?? "";
        }
    }
}
