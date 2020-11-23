using System;
using System.Collections.Generic;

namespace Domain
{
    public sealed class ResponseWrapper<T> where T : class
    {
        private static readonly Lazy<ResponseWrapper<T>> lazy = new Lazy<ResponseWrapper<T>>(() => new ResponseWrapper<T>());
        private ResponseWrapper()
        {
            
        }

        public static ResponseWrapper<T> GetInstance(int statusCode, string errorMessage, bool hasData, T contents)
        {
            lazy.Value.StatusCode = statusCode;
            lazy.Value.ErrorMessage = errorMessage;
            lazy.Value.HasData = hasData;
            lazy.Value.Contents = contents;
            return lazy.Value;
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasData { get; set; }
        public T Contents { get; set; }
    }
}