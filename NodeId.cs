class NodeId
{
    public NodeId(int value)
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
        return obj is not null && typeof(NodeId).IsInstanceOfType(obj) && ((NodeId) obj).Value == Value;
    }

    public static bool operator ==(NodeId a, NodeId b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(NodeId a, NodeId b)
    {
        return !(a == b);
    }       
}