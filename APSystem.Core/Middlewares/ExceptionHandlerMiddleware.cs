using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
/// <summary>
/// Central error/exception handler Middleware
/// </summary>
public class ExceptionHandlerMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate request;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.request = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Raised Exception:-{exception}");
                var httpStatusCode = ConfigurateExceptionTypes(exception, out AppError appError);
                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = JsonContentType;
                BaseResponse baseResponse = new BaseResponse();
                baseResponse.TraceIdentifier = context.TraceIdentifier;
                baseResponse.AppError = appError;
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(baseResponse, jsonSerializerSettings));
            }
        }

        private static int ConfigurateExceptionTypes(Exception exception, out AppError appError)
        {
            int httpStatusCode;
            appError = new AppError()
            {
                Message = string.Empty,
                Code = string.Empty
            };
            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is FluentValidation.ValidationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    appError.Message = "Request validation failed.";
                    appError.Description = exception.Message;
                    appError.Code = httpStatusCode.ToString();
                    break;
                case var _ when exception is UnauthorizedAccessException:
                    httpStatusCode = (int)HttpStatusCode.Unauthorized;
                    appError.Message = $"Unauthorized access.{exception.Message}";
                    appError.Description = "Invalid Username/Email ID or Password. Please enter the correct login credentials.";
                    appError.Code = httpStatusCode.ToString();
                    break;
                case var _ when exception is AppException:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    appError.Code = ((AppException)exception).ErrorCode;
                    appError.Message = ((AppException)exception).ErrorMessage;
                    appError.Description = ((AppException)exception).ErrorDescription;
                    break;
                case var _ when exception is InvalidOperationException:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    appError.Code = httpStatusCode.ToString();
                    appError.Message = $"{exception.Message}-{exception.StackTrace}";
                    appError.Description =  $"Message:-{exception.Message}--\nStackTrace:-{exception.StackTrace}--\nInnerException:-{exception.InnerException}";
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    appError.Message = $"{exception.Message}-{exception.StackTrace}";
                    appError.Description = "Something went wrong, please try again.";
                    appError.Code = httpStatusCode.ToString();
                    break;
            }
            return httpStatusCode;
        }
    }