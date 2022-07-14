class Edge<TNodeData, TEdgeData>
{
    public Edge(EdgeId id, Node<TNodeData> start, Node<TNodeData> end, TEdgeData? data)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (start is null)
        {
            throw new ArgumentNullException(nameof(start));
        }

        if (end is null)
        {
            throw new ArgumentNullException(nameof(end));
        }

        if (start == end)
        {
            throw new ArgumentException("Start and end nodes cannot be same");
        }

        Id = id;
        Start = start;
        End = end;
        Data = data;
    }

    public Edge(EdgeId id, Node<TNodeData> start, Node<TNodeData> end) : this(id, start, end, default(TEdgeData))
    {
    }

    public EdgeId Id { get; }

    public Node<TNodeData> Start { get; }

    public Node<TNodeData> End { get; }

    public TEdgeData? Data { get; set; }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && typeof(Edge<TNodeData, TEdgeData>).IsInstanceOfType(obj) && ((Edge<TNodeData, TEdgeData>)obj).Id == Id;
    }

    public static bool operator ==(Edge<TNodeData, TEdgeData> a, Edge<TNodeData, TEdgeData> b)
    {
        return a is not null && b is not null && a.Equals(b);
    }

    public static bool operator !=(Edge<TNodeData, TEdgeData> a, Edge<TNodeData, TEdgeData> b)
    {
        return !(a == b);
    }
}

class Edge : Edge<object, object>
{
    public Edge(EdgeId id, Node<object> start, Node<object> end, object data) : base(id, start, end, data)
    {
    }

    public Edge(EdgeId id, Node<object> start, Node<object> end) : base(id, start, end, null)
    {
    }
}