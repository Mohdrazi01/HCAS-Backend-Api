using System;
 public class BaseResponse
    {
        public BaseResponse()
        {
            AppError = new AppError();
        }
        public string TraceIdentifier { get; set; }
        public bool Success { get { return string.IsNullOrEmpty(AppError.Code) ? true : false; } }
        public AppError AppError { get; set; }
    }
    public class AppError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }