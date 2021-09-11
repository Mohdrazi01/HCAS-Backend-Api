using System.ComponentModel;
using System.Runtime.Serialization;

namespace APSystem.Models.Enums
{
    public enum AuthCodes
    {
        [EnumMember(Value = "An error occured.")]
        [Description("An error occured...Please try after sometime.")]
        E6000,
        [EnumMember(Value = "Link has been expired.")]
        [Description("Link has been expired / invalid. Please reset again.")]
        E6001,
        [EnumMember(Value = "Account inactive.")]
        [Description("Account is inactive.")]
        E6002,
        [EnumMember(Value = "User does not exist.")]
        [Description("User does not exist.")]
        E6003,
        [EnumMember(Value = "Invalid Parameters.")]
        [Description("Invalid Parameters")]
        E6004,
        [EnumMember(Value = "User Name or password is incorrect.")]
        [Description("User Name or password is incorrect.")]
        E6005,
        [EnumMember(Value = "Not Found")]
        [Description("Email Id does not exists.")]
        E6006,
        [EnumMember(Value = "Password can not be blank.")]
        [Description("Password can not be blank.")]
        E6007,
        [EnumMember(Value = "Old Password can not be blank.")]
        [Description("Old Password can not be blank.")]
        E6008,
        [EnumMember(Value = "User name already exists.")]
        [Description("User name is already exists.")]
        E6009
    }
}
