class Node<TNodeData>
{
    public Node(NodeId id, TNodeData? data)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }
                
        Id = id;
        Data = data;
    }

    public Node(NodeId id) : this(id, default(TNodeData))
    {
    }

    public NodeId Id { get; }

    public TNodeData? Data { get; set; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && typeof(Node<TNodeData>).IsInstanceOfType(obj) && ((Node<TNodeData>)obj).Id == Id;
    }

    public static bool operator ==(Node<TNodeData> a, Node<TNodeData> b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(Node<TNodeData> a, Node<TNodeData> b)
    {
        return !(a == b);
    }      
}

class Node : Node<object>
{
    public Node(NodeId id, object data) : base(id, data)
    {
    }

    public Node(NodeId id) : base(id, null)
    {
    }    
}

