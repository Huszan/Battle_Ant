
public enum TimeFormat
{
    SECOND,
    MINUTE,
    HOUR
}
public static class TimeFormatter
{

    public static string GetFullTime(float time)
    {
        return 
            $"{GetTimeFormatedTo(time, TimeFormat.HOUR)}:" +
            $"{GetTimeFormatedTo(time ,TimeFormat.MINUTE)}:" +
            $"{GetTimeFormatedTo(time ,TimeFormat.SECOND)}";
    }
    public static string GetTimeFormatedTo(float time, TimeFormat tf)
    {
        int count = 0;
        switch (tf)
        {
            case TimeFormat.SECOND:
                {
                    count = (int)time % 60;
                    break;
                }
            case TimeFormat.MINUTE:
                {
                    count = (int)time % 3600 / 60;
                    break;
                }
            case TimeFormat.HOUR:
                {
                    count = (int)time / 3600;
                    break;
                }
        }
        if (count < 10)
            return $"0{count}";
        else
            return $"{count}";
    }

}
