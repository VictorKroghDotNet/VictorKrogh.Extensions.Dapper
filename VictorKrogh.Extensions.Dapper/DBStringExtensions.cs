namespace Dapper;

public static class DBStringExtensions
{
    public static DbString ToANSI(this string value)
    {
        return new DbString
        {
            IsAnsi = true,
            Value = value
        };
    }

    public static DbString ToFixedLengthANSI(this string value, int length)
    {
        var dbString = ToANSI(value);
        dbString.IsFixedLength = true;
        dbString.Length = length;
        return dbString;
    }
}
