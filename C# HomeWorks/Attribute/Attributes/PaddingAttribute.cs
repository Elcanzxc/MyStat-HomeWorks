namespace Attribute.Attributes;
using System;
public class PaddingAttribute : Attribute
{
    public PaddingAttribute(int size, PaddingSide side) { }
}

public enum PaddingSide
{
    Left,
    Right,
}