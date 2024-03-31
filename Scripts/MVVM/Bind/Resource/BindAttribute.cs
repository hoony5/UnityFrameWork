using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class BindAttribute : Attribute
{
    /// <summary>
    /// ie. "binder/test/" -> Component
    /// ie. "Resources/binder/test/" -> Asset
    /// </summary>
    public readonly string path;

    public readonly BindType bindType;

    public BindAttribute(string path, BindType bindType = BindType.Component)
    {
        this.path = path;
        this.bindType = bindType;
    }
}