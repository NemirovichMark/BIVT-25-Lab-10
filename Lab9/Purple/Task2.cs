using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Purple
{
    public class Task2: Purple
    {
        private string[] _output;
        public string[] Output
        {
            get
            {
                string [] result = new string[_output.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = _output[i];
                }
                return result;
            }
        }

        public Task2(string input) : base(input)
        {
            _output = new string[0];
        }
        public override void Review()
        {

            int count = 0;

            string ot = "";
            string input = _input;

            input = input.Replace(" - ", "-");
            string[] inputarray = input.Split(' ');
            for (int i = 0; i < inputarray.Length; i++)
            {
               
               
                if ((count + inputarray[i].Length) <= 50)
                {
                    ot += inputarray[i] + " ";
                    count += inputarray[i].Length + 1;

                }
               
                else
                {

                    count = 0;
                    ot += "\n" + inputarray[i] + " ";
                    count += inputarray[i].Length+1;


                }


            }
            //ot = ot.Replace("-", " - ");
            string[] output = ot.Split('\n');
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = output[i].Trim(' ');
            }
            for (int i = 0; i < output.Length; i++)
            {

                string a = output[i];
                int c = a.Length;
                string itog = "";
                char[] array = new char[50];
                int probel = 0;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == ' ')
                    {
                        probel += 1;

                    }

                }
                for (int j = 0; j < a.Length; j++)
                {
                    if (c != 50 && probel != 0)
                    {
                        itog += a[j];
                        if ((50 - c) % probel != 0)
                        {
                            if (a[j] == ' ')
                            {
                                int g = (((50 - c) / probel) + 1);
                                itog += new string(' ', g);
                                c++;
                            }

                        }
                        else
                        {
                            if (a[j] == ' ')
                            {
                                int g = (((50 - c) / probel));
                                itog += new string(' ', g);
                                c++;
                            }
                        }
                    }
                    else
                    {
                        itog += a[j];
                    }

                }


                output[i] = itog;

            }

            _output = output;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Output); // Environment.NewLine - возвращает строку содержащую символ перехода на новую строку, подходящий для текущей среды
        }
        



    }
}
