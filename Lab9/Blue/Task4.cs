using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Blue
{
    public class Task4:Blue
    {
        private int output;

        
        public int Output
        {
            get { return output; }
        }

        
        public Task4(string text) : base(text)
        {
            output = 0;
        }

        
        public override void Review()
        {
            
            if (Input == null)
            {
                output = 0;
                return;
            }
            long sum = 0;          
            long currentNumber = 0; 
            bool inNumber = false;  

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];

                
                if (c >= '0' && c <= '9')
                {
                    
                    currentNumber = currentNumber * 10 + (c - '0'); // 'a' - 'b'
                    inNumber = true;
                }
                else
                {
                    
                    if (inNumber)
                    {
                        sum += currentNumber;
                        currentNumber = 0;
                        inNumber = false;
                    }
                }
            }
            if (inNumber)
            {
                sum += currentNumber;
            }

            output = (int)sum;
        }

        
        public override string ToString()
        {
            return output.ToString();
        }

    }
}
