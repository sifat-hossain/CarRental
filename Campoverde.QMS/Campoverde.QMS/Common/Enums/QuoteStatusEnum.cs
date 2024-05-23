using System.ComponentModel;

namespace Campoverde.QMS.Common.Enums;

public enum QuoteStatusEnum
{
    New = 0,
    Contacted = 1,
    [Description("Not Interested")]
    NotInterested = 2,
    Rented = 3
}
