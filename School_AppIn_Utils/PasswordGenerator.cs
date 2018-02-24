using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_AppIn_Utils
{
    public class PasswordGenerator
    {

        public static string GeneratePWD()
        {
            int passwordLength = 6;
            int quantity = 1;
            ArrayList arrCharPool = new ArrayList();
            Random rndNum = new Random();
            arrCharPool.Clear();
            string password = "";


            //Lower Case 
            for (int i = 97; i < 123; i++)
            {
                arrCharPool.Add(Convert.ToChar(i).ToString());
            }
            //Number
            for (int i = 48; i < 58; i++)
            {
                arrCharPool.Add(Convert.ToChar(i).ToString());
            }

            //Upper Case 
            for (int i = 65; i < 91; i++)
            {
                arrCharPool.Add(Convert.ToChar(i).ToString());
            }



            for (int x = 0; x < quantity; x++)
            {
                //Iterate through the number of characters required in the password
                for (int i = 0; i < passwordLength; i++)
                {
                    password += arrCharPool[rndNum.Next(arrCharPool.Count)].ToString();
                }
            }

            return string.Format("!P{0}1j?", password);

        }




    }
}




