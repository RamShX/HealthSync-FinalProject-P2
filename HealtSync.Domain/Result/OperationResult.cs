﻿namespace HealtSync.Domain.Result
{
    public class OperationResult
    {
        public OperationResult()
        {
            Success = true;
        }
        public string? Message { get; set; }
        public bool Success { get; set; }
        public dynamic? Data { get; set; }
        

    }
}
