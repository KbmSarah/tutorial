using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JsonExam
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string jsonFilePath = Directory.GetCurrentDirectory() + "\\test.json";

            string json = ReadJsonString(jsonFilePath);

            /////////////////////////////////////////////////
            // Native Object 사용 방법
            /////////////////////////////////////////////////
            Console.WriteLine("=================== Native Object =======================");

            // Native Object 생성
            JObject jObject = JObject.Parse(json);

            // Json Data 전체 출력
            Console.WriteLine(jObject.ToString());

            // JSON 데이터 중 'Radiation resistance' 데이터까지 접근하는 방법
            Console.WriteLine(jObject["list"][0]["plate_num"]);

            // JSON 데이터 하위 객체인 members 객체의 name 값을 반복적으로 접근하는 방법
            JToken jToken = jObject["list"];
            foreach (JToken members in jToken)
            {
                Console.WriteLine(members["entry_id"]);
            }

            /////////////////////////////////////////////////
            // 역직렬화(Deserialize) 방법
            /////////////////////////////////////////////////

            Console.WriteLine("=================== Deserialize =======================");

            // 역직렬화 수행 후 미리 선언한 클래스에 저장
            Root rootObject = JsonConvert.DeserializeObject<Root>(json);

            // JSON 데이터 중 'Radiation resistance' 데이터까지 접근하는 방법
            Console.WriteLine(rootObject.list[0].in_time);

            // JSON 데이터 하위 객체인 members 객체의 name 값을 반복적으로 접근하는 방법
            foreach (Members members in rootObject.list)
            {
                Console.WriteLine(members.in_time);
            }


            /////////////////////////////////////////////////
            // 직렬화(Serialize) 방법
            /////////////////////////////////////////////////

            Console.WriteLine("=================== serialize =======================");

            // 직렬화 수행 후 string 변수에 저장
            string serializeResult = JsonConvert.SerializeObject(rootObject);

            // Json Data 전체 출력
            Console.WriteLine(serializeResult);
                                 
        }

        public static string ReadJsonString(string filePath)
        {
            string jsonString = string.Empty;
            using (StreamReader sr = new StreamReader(filePath))
            {
                jsonString = sr.ReadToEnd();
            }

            return jsonString;
        }

        public static T ReadJson<T>(string filePath)    // Json파일 읽기
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

    }

    public class Members
    {
        public string entry_id { get; set; }
        public string plate_num { get; set; }
        public string ticket_type { get; set; }
        public string pay_stat { get; set; }
        public string in_time { get; set; }
        public string out_time { get; set; }
        public object pay_time { get; set; }
        public string in_image_path { get; set; }
        public string out_image_path { get; set; }
        public string in_lpr_name { get; set; }
        public string out_lpr_name { get; set; }
        public string pay_station_name { get; set; }
    }



    public class Root
    {
        public string result_code { get; set; }
        public string result_msg { get; set; }
        public List<Members> list { get; set; }
    }


    
}
