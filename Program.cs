using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

using Postman2RestClient.Model.Postman;
using Postman2RestClient.Model.RestClient;

namespace Postman2RestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputFilesPaths = new List<string>();

            Console.WriteLine("How many Postman's environments you want to convert?");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Provide file path to Postman enviroment {i + 1}");
                string temp = Console.ReadLine();

                while (!File.Exists(temp))
                {
                    Console.WriteLine($"File does not exist. Provide valid file path to Postman's enviroment {i + 1}");
                    temp = Console.ReadLine();
                }

                inputFilesPaths.Add(temp);
            }

            Dictionary<string, Model.RestClient.Environment> restClientEnvs = new Dictionary<string, Model.RestClient.Environment>();

            foreach (string path in inputFilesPaths)
            {
                PostmanEnv postmanEnv = JsonConvert.DeserializeObject<PostmanEnv>(File.ReadAllText(path));
                restClientEnvs.Add(postmanEnv.Name, new Model.RestClient.Environment());

                foreach (PostmanKey key in postmanEnv.Values)
                {
                    if (key.Enabled == true)
                    {
                        restClientEnvs[postmanEnv.Name].Add(key.Key, key.Value);
                    }
                }
            }

            VsCodeSettings vsCodeSettings = new VsCodeSettings()
            {
                Envs = restClientEnvs
            };

            string outputFile = JsonConvert.SerializeObject(vsCodeSettings, Formatting.Indented);
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

            using (StreamWriter sw = File.CreateText(outputFilePath))
            {
                sw.Write(outputFile);
            }

            Console.WriteLine($"Output file saved at: {outputFilePath}");
        }
    }
}

