using System;

namespace Postman2RestClient.Model.Postman
{
    class PostmanEnv
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PostmanKey[] Values { get; set; }
        public string VariableScope { get; set; }
        public string ExportedAt { get; set; }
        public string PostmanVersion{ get; set; }

    }
}