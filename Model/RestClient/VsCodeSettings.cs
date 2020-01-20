using System.Collections.Generic;
using Newtonsoft.Json;

namespace Postman2RestClient.Model.RestClient
{
    class VsCodeSettings
    {
        [JsonProperty("rest-client.environmentVariables")]
        public Dictionary<string,  RestClientEnv> Envs { get; set; }
    }
}