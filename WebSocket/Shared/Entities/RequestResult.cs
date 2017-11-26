using System;

namespace Shared.Entities
{
    public class RequestResult
    {
        public RequestState State { get; set; }
        public string Msg { get; set; }
        public string AccessToken { get; set; }
    }

    public enum RequestState
    {
        Failed = -1,
        NotAuth = 0,
        Success = 1
    }
}