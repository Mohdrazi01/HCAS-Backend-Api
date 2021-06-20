 using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using APSystem.Models.Enums;

public class AppException : Exception
    {
        public string ErrorMessage { get; internal set; }
        public string ErrorCode { get; internal set; }
        public string ErrorDescription { get; internal set; }
        public AppException(GeneralCodes code) : base()
        {
            ErrorMessage = code.GetEnumMemberAttrValue();
            ErrorCode = code.ToString();
            ErrorDescription = code.GetEnumDescriptionAttrValue();
        }
        public AppException(AuthCodes code) : base()
        {
            ErrorMessage = code.GetEnumMemberAttrValue();
            ErrorCode = code.ToString();
            ErrorDescription = code.GetEnumDescriptionAttrValue();
        }
          }

    public static class Extension
    {
        public static string GetEnumMemberAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Value;
        }
        public static string GetEnumDescriptionAttrValue(this Enum @enum)
        {
            var attr =
                @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                    GetCustomAttributes(false).OfType<DescriptionAttribute>().
                    FirstOrDefault();
            if (attr == null)
                return @enum.ToString();
            return attr.Description;
        }
    }