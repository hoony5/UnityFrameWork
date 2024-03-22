namespace Share.Enums
{
    public enum WriterTextStyle
    {
        Default, // default {some value} is {another value}
        Split, // like csv or something like that
        Indent, // like code block
        Bullet, // like header - sub header - content
        Number, // just numbering
        MarkDown,
        CSV
    }
}