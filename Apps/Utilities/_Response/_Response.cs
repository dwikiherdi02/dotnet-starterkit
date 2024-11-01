using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Apps.Utilities._Response
{
    public class _Response
    {
        private readonly ControllerBase _controller;

        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        private string? _message;

        private object? _result;

        private object? _error;

        public _Response(ControllerBase controller)
        {
            _controller = controller;
        }

        public _Response(ControllerBase controller, HttpStatusCode statusCode)
        {
            _controller = controller;
            _statusCode = statusCode;
        }

        public _Response(ControllerBase controller, HttpStatusCode statusCode, string message)
        {
            _controller = controller;
            _statusCode = statusCode;
            _message = message;
        }

        public _Response SetHeader(string key, string value)
        {
            _controller.Response.Headers[key] = value;
            return this;
        }

        public _Response WithCode(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
            return this;
        }

        public _Response WithMessage(string message)
        {
            _message = message;
            return this;
        }
        

        public _Response WithResult(object result)
        {
            _result = result;
            return this;
        }

        public _Response WithError(object error)
        {
            _error = error;
            return this;
        }

        public ActionResult Json()
        {
            
            if (_message == null)
            {
                _message = _statusCode.ToString();
            }
            
            var data = new _Entity {
                Code = _statusCode,
                Message = _message,
            };

            if (_result != null)
            {
                data.Results = _result;
            }

            if (_error != null)
            {
                data.Errors = _error;
            }
            
            return new _ActionResult(_statusCode, JsonSerializer.Serialize(data));
        }
    }
}