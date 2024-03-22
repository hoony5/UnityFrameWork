using Newtonsoft.Json;

[System.Flags, JsonConverter(typeof(TargetSpaceConverter))]
public enum TargetSpace
{
    None          = 1 << 0,
    Random_0      = 1 << 1,
    Random_1      = 1 << 2,
    Random_2      = 1 << 3,
    Random_3      = 1 << 4,
    All           = 1 << 5,
    Self          = 1 << 6,
    One_Front     = 1 << 7,
    One_Back      = 1 << 8,
    Near          = 1 << 9,
    Row_0         = 1 << 10,
    Row_1         = 1 << 11,
    Row_2         = 1 << 12,
    Column_0      = 1 << 13,
    Column_1      = 1 << 14,
}