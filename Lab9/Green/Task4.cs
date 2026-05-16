using System;
using System.Linq;

namespace Lab9.Green
{
    public class Task4 : Green
    {
        private string[] _arr;

        public string[] Output => _arr.ToArray();

        public Task4(string text) : base(text)
        {
            _arr = new string[0];
        }

        public override void Review()
        {
            _arr = new string[0];

            string text = Input;
            if (text == null)
                text = "";

            string current = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',')
                {
                    AddSurname(current);
                    current = "";
                }
                else
                {
                    current += text[i];
                }
            }

            AddSurname(current);
            Sort();
        }

        private void AddSurname(string value)
        {
            string surname = value == null ? "" : value.Trim();
            if (surname.Length == 0)
                return;

            Array.Resize(ref _arr, _arr.Length + 1);
            _arr[_arr.Length - 1] = surname;
        }

        private int CompareStrings(string a, string b)
        {
            string left = a == null ? "" : a.ToLower();
            string right = b == null ? "" : b.ToLower();

            int min = left.Length < right.Length ? left.Length : right.Length;
            for (int i = 0; i < min; i++)
            {
                if (left[i] < right[i])
                    return -1;
                if (left[i] > right[i])
                    return 1;
            }

            if (left.Length < right.Length)
                return -1;
            if (left.Length > right.Length)
                return 1;

            return 0;
        }

        private void Sort()
        {
            for (int i = 1; i < _arr.Length; i++)
            {
                int j = i;
                while (j > 0 && CompareStrings(_arr[j], _arr[j - 1]) < 0)
                {
                    string temp = _arr[j];
                    _arr[j] = _arr[j - 1];
                    _arr[j - 1] = temp;
                    j--;
                }
            }
        }

        public override string ToString()
        {
            string ans = "";
            for (int i = 0; i < _arr.Length; i++)
            {
                ans += _arr[i];
                if (i != _arr.Length - 1)
                    ans += "\r\n";
            }
            return ans;
        }
    }
}
