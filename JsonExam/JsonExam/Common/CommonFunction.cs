using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonExam.Common
{
    public static class CommonFunction
    {
        #region JSON
        /// <summary>
        /// Json File을 읽어 Josn string 가져오기
        /// </summary>
        /// <param name="filePath">Json File Path</param>
        /// <returns>Json string</returns>
        public static string ReadJsonString(string filePath)
        {
            string jsonString = string.Empty;
            using (StreamReader sr = new StreamReader(filePath))
            {
                jsonString = sr.ReadToEnd();
            }

            return jsonString;
        }

        /// <summary>
        /// Json File 읽어 Deserialize
        /// </summary>
        /// <typeparam name="T">Json Class Type</typeparam>
        /// <param name="filePath">Json File Path</param>
        /// <returns>Json DeserializeObject</returns>
        public static T ReadJson<T>(string filePath)
        {
            T result = default(T);

            try
            {
                string layoutStream = string.Empty;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    layoutStream = sr.ReadToEnd();
                }
                JsonSerializerSettings settings = null;
                if (!string.IsNullOrEmpty(layoutStream) && layoutStream.Contains("**:"))
                {
                    settings = new JsonSerializerSettings();
                    //settings.Formatting = Formatting.Indented;
                    //settings.ContractResolver = new EncryptedStringPropertyResolver("My-Sup3r-Secr3t-Key");
                    layoutStream = layoutStream.Replace("**:", "");
                }

                result = JsonConvert.DeserializeObject<T>(layoutStream, settings);
            }
            catch (Exception ex)
            {
                //Log.SysLog_Error_Lv1(MyType, ex);
            }

            return result;
        }
        #endregion
    }
}
