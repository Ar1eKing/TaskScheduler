using System.ComponentModel;

namespace TaskScheduler
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum status
    {
        [Description("Wait For Trigger")]
        WaitForTrigger,
        [Description("Completed")]
        Completed,
        [Description("Paused")]
        Paused
    }
}