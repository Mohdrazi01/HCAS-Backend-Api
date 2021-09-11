using System.ComponentModel;
using System.Runtime.Serialization;

namespace APSystem.Models.Enums
{
    public enum GeneralCodes
    {
        [EnumMember(Value = "Not Found")]
        [Description("There are some bad parameters in the request.")]
        E1000
    }
}
