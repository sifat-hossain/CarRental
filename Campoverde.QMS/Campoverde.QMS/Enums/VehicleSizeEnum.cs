using System.ComponentModel;

namespace Campoverde.QMS.Enums;

public enum VehicleSizeEnum
{
    [Description("4 Seat")]
    Small = 0,
    [Description("7 Seat")]
    Medium = 1,
    [Description("More than 7 seat")]
    Large = 2
}
