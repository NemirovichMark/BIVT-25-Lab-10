using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    public class DTO
    {
        public string Type_ { get; set; }
        public string Input { get; set; }
        public string[] Codes { get; set; }

        public DTO()
        {
        }

        public DTO(Lab9.Purple.Purple purple)
        {
            Type_ = purple.GetType().AssemblyQualifiedName!;
            Input = purple.Input;

            if (purple is Lab9.Purple.Task4 task4)
            {
                Codes = new string[5];
                for (int i = 0; i < 5; i++)
                {
                    Codes[i] = task4.Codes[i].Item1 + " " + task4.Codes[i].Item2;
                }
            }
        }

        public Lab9.Purple.Purple Transform()
        {
            Type objectType = Type.GetType(Type_);
            Lab9.Purple.Purple purple;
            if (objectType == typeof(Lab9.Purple.Task1))
            {
                purple = new Lab9.Purple.Task1(Input);
            }
            else if (objectType == typeof(Lab9.Purple.Task2))
            {
                purple = new Lab9.Purple.Task2(Input);
            }
            else if (objectType == typeof(Lab9.Purple.Task3))
            {
                purple = new Lab9.Purple.Task3(Input);
            }
            else if (objectType == typeof(Lab9.Purple.Task4))
            {
                (string, char)[] codes = new (string, char)[5];
                for (int i = 0; i < 5; i++)
                {
                    codes[i] = (Codes[i].Split()[0], Codes[i].Split()[1][0]);
                }

                purple = new Lab9.Purple.Task4(Input, codes.ToArray());
            }
            else return null!;

            purple.Review();
            return purple;
        }
    }
}
