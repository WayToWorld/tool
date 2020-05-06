using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace 文字提取小程序
{
    class Program
    {
        string ImagePath = string.Empty;
        JObject OutResult = default;
        private string APP_ID = "";
        private string API_Key = "";
        private string SECRE_KEY = "";
        static void Main(string[] args)
        {
            Program p = new Program();
            int ModelNumber = p.ChoosedModeNumber();
            while(true)
            {
                Console.WriteLine("\n请输入需要识别图片的URl或将图片拖入");
                p.ImagePath = Console.ReadLine();
                p.GetModelResult(ModelNumber);
                p.OutputResult(p.OutResult);
            }
            
        }

        public void GetModelResult(int ModelNumber)
        {
            switch (ModelNumber)
            {
                case 1:
                    GeneralBasicDemo();
                    break;
                case 3:
                    AccurateBasicDemo();
                    break;
                case 2:
                    GeneralDemo();
                    break;
                case 4:
                    AccurateDemo();
                    break;
                case 7:
                    IdcardDemo();
                    break;
                case 8:
                    BankcardDemo();
                    break;
                case 9:
                    DrivingLicenseDemo();
                    break;
                case 10:
                    VehicleLicenseDemo();
                    break;
                case 11:
                    LicensePlateDemo();
                    break;
                case 12:
                    ReceiptDemo();
                    break;
                default:
                    break;
            }
        }

        public void OutputResult(JObject result)
        {
            Console.WriteLine("共识别数量："+result["words_result_num"]);
            int count= result.GetValue("words_result").Count();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i+1+":"+ result["words_result"][i]["words"]);
            }

        }

        public int ChoosedModeNumber()
        {
            string Models = string.Empty;
            int result = default;
            int[] ModelNums = new int[20];
            Console.WriteLine("请输入您要解析的方式所对应的数字序号");
            try
            {
                string json = File.ReadAllText(Path.GetFullPath("../../../") + "Models.json", Encoding.Default);

                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    foreach (JsonElement element in document.RootElement.GetProperty("Model").EnumerateArray())
                    {
                        Models += element.GetProperty("Num").GetInt32().ToString() + "、" + element.GetProperty("Name").GetString() + "\n";
                        ModelNums.Append(element.GetProperty("Num").GetInt32());
                        // Console.WriteLine(element.GetProperty("Num").GetInt32());
                    }
                }
                Console.WriteLine(Models);
                int x = int.Parse(Console.ReadLine());
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    string Description = string.Empty;
                    foreach (JsonElement element in document.RootElement.GetProperty("Model").EnumerateArray())
                    {
                        if (element.GetProperty("Num").GetInt32() == x)
                        {
                            Description = element.GetProperty("Description").GetString();
                            result = x;
                        }
                    }
                    Console.WriteLine($"该方法可{Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生异常，信息：" + ex.Message);
            }
            return result;
        }

        public void GeneralBasicDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.GeneralBasic(image);
        }

        public void AccurateBasicDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000;  
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.AccurateBasic(image);
        }

        public void GeneralDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.General(image);

        }

        public void AccurateDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.Accurate(image);

        }

        public void IdcardDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000;  
            var image = File.ReadAllBytes(ImagePath);
            var idCardSide = "back";
            OutResult = client.Idcard(image, idCardSide);
        }

        public void BankcardDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000;  
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.Bankcard(image);
        }

        public void DrivingLicenseDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.DrivingLicense(image);
           
        }

        public void VehicleLicenseDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.VehicleLicense(image);
            
        }

        public void LicensePlateDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000; 
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.LicensePlate(image);
           
        }

        public void ReceiptDemo()
        {
            var client = new Baidu.Aip.Ocr.Ocr(API_Key, SECRE_KEY);
            client.Timeout = 60000;  
            var image = File.ReadAllBytes(ImagePath);
            OutResult = client.Receipt(image);
        }
    }
}
