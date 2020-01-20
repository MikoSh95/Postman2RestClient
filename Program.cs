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

            Console.WriteLine($"Provide file path to Postman global variables or leave empty to skip global variables");
            string globalsFilePath = Console.ReadLine();

            while (!File.Exists(globalsFilePath) && globalsFilePath != "")
            {
                Console.WriteLine($"File does not exist. Provide valid file path to Postman's global variables or leave empty to skip global variables");
                globalsFilePath = Console.ReadLine();
            }

            Dictionary<string, Model.RestClient.Environment> environment = new Dictionary<string, Model.RestClient.Environment>();

            if (globalsFilePath != "")
            {

                PostmanGlobals postmanGlobals = JsonConvert.DeserializeObject<PostmanGlobals>(File.ReadAllText(globalsFilePath));
                environment.Add("$shared", new Model.RestClient.Environment());

                foreach (PostmanKey key in postmanGlobals.Values)
                {
                    if (key.Enabled == true)
                    {
                        environment["$shared"].Add(key.Key, key.Value);
                    }
                }
            }

            List<string> globalsFilesPaths = new List<string>();

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

                globalsFilesPaths.Add(temp);
            }

            foreach (string path in globalsFilesPaths)
            {
                PostmanEnv postmanEnv = JsonConvert.DeserializeObject<PostmanEnv>(File.ReadAllText(path));
                environment.Add(postmanEnv.Name, new Model.RestClient.Environment());

                foreach (PostmanKey key in postmanEnv.Values)
                {
                    if (key.Enabled == true)
                    {
                        environment[postmanEnv.Name].Add(key.Key, key.Value);
                    }
                }
            }

            VsCodeSettings vsCodeSettings = new VsCodeSettings()
            {
                Envs = environment
            };

            string outputFile = JsonConvert.SerializeObject(vsCodeSettings, Formatting.Indented);
            string outputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

            using (StreamWriter sw = File.CreateText(outputFilePath))
            {
                sw.Write(outputFile);
            }

            Console.WriteLine($"Output file saved at: {outputFilePath}");
            Console.ReadKey();
        }
    }
}

