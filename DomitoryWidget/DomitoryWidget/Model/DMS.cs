using EasyHttp.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomitoryWidget.Model
{
    static class DMS
    {
        static class URL
        {
            public const string BASEPATH = "http://dsm2015.cafe24.com";
            public const string AUTH = BASEPATH + "/auth";                
        }

        public static HttpResponse Auth(string id, string password)
        {
            var client = new HttpClient();

            var body = new Dictionary<string, object>
            {
                { "id", id },
                { "password", password },
            };

            var response = client.Post
            (
                uri: URL.AUTH, data: body, 
                contentType: HttpContentTypes.ApplicationJson
            );

            return response;
        }
    }
}
