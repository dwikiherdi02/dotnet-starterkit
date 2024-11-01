using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Apps.Utilities._Response
{
    public class _ActionResult : ActionResult
    {
        private static readonly UTF8Encoding utf = new UTF8Encoding();

        public HttpStatusCode StatusCode { get; set; }

        public object? Data { get; set; }

        public Dictionary<string, string> Headers { get; private set; }

        public _ActionResult()
        {
            StatusCode = HttpStatusCode.OK;
            Headers = new Dictionary<string, string>();
        }

        public _ActionResult(HttpStatusCode statusCode)
            : this()
        {
            StatusCode = statusCode;
        }

        public _ActionResult(HttpStatusCode statusCode, object data)
            : this()
        {
            StatusCode = statusCode;
            Data = data;
        }

        private string Json
        {
            get
            {
                if (Data != null)
                {
                    return Data is string ? Data.ToString() ?? string.Empty : JsonConvert.SerializeObject(Data);
                }
                return string.Empty;
            }
        }

        public byte[] GetBuffer() => utf.GetBytes(Json);

        public override void ExecuteResult(ActionContext context)
        {
            SetHeaders(context);
            SetResponse(context);
            context.HttpContext.Response.Body.Write(GetBuffer(), 0, GetBuffer().Length);
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            SetHeaders(context);
            SetResponse(context);
            await context.HttpContext.Response.Body.WriteAsync(GetBuffer(), 0, GetBuffer().Length);
        }

        private void SetHeaders(ActionContext context)
        {
            if (Headers.Count > 0)
            {
                foreach (var item in Headers)
                {
                    context.HttpContext.Response.Headers.Append(item.Key, item.Value);
                }
            }
        }

        private void SetResponse(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            context.HttpContext.Response.StatusCode = (int)StatusCode;
        }
    }
}