using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PrimeRealty.ActionFilters
{

 [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            bool intArray = false;
            string intArrayKey = null;
            string[] intArrayValues = null;
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                string decrptedString = Decrypt(encryptedQueryString.ToString());
                char[] delim = new char[] {'?' };
                string[] paramsArrs = decrptedString.Split(delim,StringSplitOptions.RemoveEmptyEntries);
                intArrayValues = new string[paramsArrs.Length-1];

                if (Array.FindIndex(paramsArrs, a => a.Contains("intarray=")) > 0)
                {
                        intArray = true;
                        for (int i = 0; i < paramsArrs.Length; i++)
                        {
                            string[] paramArr = paramsArrs[i].Split('=');
                            if (intArray && paramArr[0] == "intarray")
                            {
                                intArrayKey = paramArr[1];
                            }
                        }

                }
                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');
                    if (intArray && paramArr[0] != "intarray" && paramArr[0] == intArrayKey)
                    {
                        intArrayValues[i] =  paramArr[1];
                    }
                    else if (paramArr[0] != "intarray")
                    {
                        decryptedParameters.Add(paramArr[0], paramArr[1]);

                    }
                }
            }

            if (intArray)
            {
                intArrayValues = intArrayValues.Where(a => !string.IsNullOrEmpty(a)).ToArray();
                filterContext.ActionParameters[intArrayKey] = Array.ConvertAll(intArrayValues, s => int.Parse(s.ToString()));

                decimal parseResult;
                for (int i = 0; i < decryptedParameters.Count; i++)
                {

                    if (decimal.TryParse(decryptedParameters.Values.ElementAt(i).ToString(), out parseResult))
                    {
                        filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = parseResult;
                    }
                    else
                    {
                        filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                    }
                }

            
            }
            else
            {
                decimal parseResult;
                for (int i = 0; i < decryptedParameters.Count; i++)
                {
               
                        if (decimal.TryParse(decryptedParameters.Values.ElementAt(i).ToString(), out parseResult))
                        {
                            filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = parseResult;
                        }
                        else
                        {
                            filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                        }
                }
           }
            
            base.OnActionExecuting(filterContext);

        }

        private string Decrypt(string encryptedText)
        {
            string key = "jdsg432387#";
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(encryptedText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}