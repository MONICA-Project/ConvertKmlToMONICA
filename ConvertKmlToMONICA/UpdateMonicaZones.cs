using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConvertKmlToMONICA
{
    class UpdateMonicaZones
    {

        public bool LoadZones(string filename, string endpoint)
        {

            var o1 = JArray.Parse(File.ReadAllText(filename));
            int i = 1;
            endpoint += "zone";
            foreach (JObject p in o1)
            {
                string payload = p.ToString();
                
                // string endpoint = Environment.GetEnvironmentVariable("HttpAddress"); ;

                if (i % 5 == 0)
                    System.Threading.Thread.Sleep(5000);
                WebClient client = new WebClient();
                try
                {
                    System.Console.WriteLine("Endpoint:" + endpoint);
                    System.Console.WriteLine("Payload:" + payload);

                    client.Encoding = System.Text.Encoding.UTF8;
                    client.Headers["Accept"] = "application/json";
                    client.Headers["Content-Type"] = "application/json";
                    client.Headers["Authorization"] = "6ffdcacb-c485-499c-bce9-23f76d06aa36";
                    string JsonResult = client.UploadString(endpoint, payload);

                }
                catch (WebException exception)
                {
                    System.Console.WriteLine("Invokation error: " + exception.Message);
                   
                    return false;
                }
                i++;
            }


            return true;

        }
    }
}
