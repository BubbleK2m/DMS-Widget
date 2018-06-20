using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DomitoryWidget.Model
{
    internal static class DMS
    {
        private static class URL
        {
            public const string BASEPATH = "http://dsm2015.cafe24.com";
            public const string AUTH = "/auth";
            public const string MYPAGE = "/mypage";
            public const string STAY = "/stay";
        }

        internal static RestResponse Auth(string id, string password)
        {
            var client = new RestClient(URL.BASEPATH);
            var request = new RestRequest(URL.AUTH, Method.POST);

            request.AddParameter("id", id, ParameterType.GetOrPost);
            request.AddParameter("pw", password, ParameterType.GetOrPost);

            var response = client.Execute(request);
            return response as RestResponse;
        }

        internal static RestResponse MyPage(string accessToken)
        {
            var client = new RestClient(URL.BASEPATH);
            var request = new RestRequest(URL.MYPAGE, Method.GET);
            request.AddHeader("Authorization", $"JWT {accessToken}");

            var response = client.Execute(request);
            return response as RestResponse;
        }

        internal static RestResponse GetStayApply(string accessToken)
        {
            var client = new RestClient(URL.BASEPATH);
            var request = new RestRequest(URL.STAY, Method.GET);
            request.AddHeader("Authorization", $"JWT {accessToken}");

            var response = client.Execute(request);
            return response as RestResponse;
        }

        internal static RestResponse SetStayApply(int stay, string accessToken)
        {
            var client = new RestClient(URL.BASEPATH);
            var request = new RestRequest(URL.STAY, Method.POST);

            request.AddParameter("value", stay, ParameterType.GetOrPost);
            request.AddHeader("Authorization", $"JWT {accessToken}");

            var response = client.Execute(request);
            return response as RestResponse;
        }
    }
}
