namespace Million.Domain.Entities.Enums
{
    using System.ComponentModel;

    public enum BaseRepositpryEnum
    {
        [Description("BATCHSIZE")]
        BATCHSIZE = 5000,

        [Description("BULKCOPYTIMEOUT")]
        BULKCOPYTIMEOUT = 3600000,

        [Description("ERRORFIVEHOUNDREDFORTYSEVEN")]
        ERRORFIVEHOUNDREDFORTYSEVEN = 547
    }
}
