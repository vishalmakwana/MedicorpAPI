using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Medicorp.Core
{
    public class ApiResponse<T> 
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public bool IsError { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public string ErrorTitle { get; set; }

        public Validation validation { get; set; }

        public void ConstructErrorResponse(string title, string message)
        {
            ErrorTitle = title;
            ErrorMessage = message;
            IsError = true;
        }
    }

    public class Validation
    {
        public string source { get; set; }
        public string[] keys { get; set; }
    }

}


   