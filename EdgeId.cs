class EdgeId
{
    public EdgeId(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public override int GetHashCode()
    {
        return Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && typeof(EdgeId).IsInstanceOfType(obj) && ((EdgeId) obj).Value == Value;
    }    

    public static bool operator ==(EdgeId a, EdgeId b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(EdgeId a, EdgeId b)
    {
        return !(a == b);
    }      
}