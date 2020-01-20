using System.Collections.Generic;

namespace Postman2RestClient.Model.Postman
{
    class PostmanGlobals
    {
        public string Id { get; set; }
        public PostmanKey[] Values { get; set; }
        public string Name { get; set; }
        public string eScope { get; set; }
        public string EportedAt { get; set; }
        public string ExportedUsing { get; set; }

    }
}