﻿using System.Text.Json.Serialization;

namespace ContactRegistry.Common.Utilities;

public class BaseResponse<T>
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }

    public T Data { get; set; }

    [JsonConstructor]
    public BaseResponse()
    {
    }

    public BaseResponse(bool success, string message = "")
    {
        Success = success;
        Message = message;
    }

    public BaseResponse(T data, bool success, string message = "") : this(success, message)
    {
        Data = data;
    }
}

public class BaseResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }

    [JsonConstructor]
    public BaseResponse()
    {
    }

    public BaseResponse(bool success, string message = "")
    {
        Success = success;
        Message = message;
    }
}