class GraphImplByList<TNodeData, TEdgeData> : IGraph<TNodeData, TEdgeData>
{
    private int _nodeIdCounter = 0;
    private int _edgeIdCounter = 0;

    private List<Node<TNodeData>> _nodes = new();

    private List<Edge<TNodeData, TEdgeData>> _edges = new();

    public void ClearEdges()
    {
        _edges.Clear();
    }

    public void ClearNodes()
    {
        ClearEdges();

        _nodes.Clear();
    }

    public Edge<TNodeData, TEdgeData> CreateEdge(Node<TNodeData> start, Node<TNodeData> end)
    {
        return CreateEdge(start, end, default(TEdgeData));
    }

    public Edge<TNodeData, TEdgeData> CreateEdge(Node<TNodeData> start, Node<TNodeData> end, TEdgeData? edgeData)
    {
        if (HasEdge(start, end))
        {
            throw new ArgumentException("Edge with this start and end nodes exists");
        }

        if (!HasNode(start))
        {
            throw new ArgumentException("Start node does not exist in graph");
        }

        if (!HasNode(end))
        {
            throw new ArgumentException("End node does not exist in graph");
        }

        var edge = new Edge<TNodeData, TEdgeData>(new EdgeId(++_edgeIdCounter), start, end, edgeData);

        _edges.Add(edge);

        return edge;
    }

    public Node<TNodeData> CreateNode()
    {
        return CreateNode(default(TNodeData));
    }

    public Node<TNodeData> CreateNode(TNodeData? nodeData)
    {
        var node = new Node<TNodeData>(new NodeId(++_nodeIdCounter), nodeData);

        _nodes.Add(node);

        return node;
    }

    public IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(Node<TNodeData> node)
    {
        return GetAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(NodeId nodeId)
    {
        var result = new List<Node<TNodeData>>();

        result.AddRange(_edges.Where(w => w.Start.Id == nodeId).Select(t => t.End).ToList());

        result.AddRange(_edges.Where(w => w.End.Id == nodeId).Select(t => t.Start).ToList());

        return result;
    }

    public IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(Node<TNodeData> node)
    {
        return GetInboundAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(NodeId nodeId)
    {
        return _edges.Where(w => w.End.Id == nodeId).Select(t => t.Start).ToList();
    }

    public IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(Node<TNodeData> node)
    {
        return GetOutboundAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(NodeId nodeId)
    {
        return _edges.Where(w => w.Start.Id == nodeId).Select(t => t.End).ToList();
    }

    public bool HasEdge(Edge<TNodeData, TEdgeData> edge)
    {
        return _edges.Exists(w => w == edge);
    }

    public bool HasEdge(Node<TNodeData> start, Node<TNodeData> end)
    {
        return HasEdge(start.Id, end.Id);
    }

    public bool HasEdge(NodeId startNodeId, NodeId endNodeId)
    {
        return _edges.Exists(w => w.Start.Id == startNodeId && w.End.Id == endNodeId);
    }

    public bool HasEdge(EdgeId edgeId)
    {
        return _edges.Exists(w => w.Id == edgeId);
    }

    public bool HasNode(Node<TNodeData> node)
    {
        return _nodes.Exists(w => w == node);
    }

    public bool HasNode(NodeId nodeId)
    {
        return _nodes.Exists(w => w.Id == nodeId);
    }

    public void RemoveEdge(Edge<TNodeData, TEdgeData> edge)
    {
        _edges.Remove(edge);
    }

    public void RemoveEdge(EdgeId edgeId)
    {
        _edges.RemoveAll(w => w.Id == edgeId);
    }

    public void RemoveEdge(Node<TNodeData> start, Node<TNodeData> end)
    {
        RemoveEdge(start.Id, end.Id);
    }

    public void RemoveEdge(NodeId startNodeId, NodeId endNodeId)
    {
        _edges.RemoveAll(w => w.Start.Id == startNodeId && w.End.Id == endNodeId);
    }

    public IReadOnlyList<Node<TNodeData>> Nodes => _nodes.ToList();

    public IReadOnlyList<Edge<TNodeData, TEdgeData>> Edges => _edges.ToList();

    public void RemoveNode(Node<TNodeData> node)
    {
        _nodes.Remove(node);

        _edges.RemoveAll(w => w.Start == node || w.End == node);
    }

    public int NodeCount => _nodes.Count;

    public int EdgeCount => _edges.Count;
}


class GraphImplByList : GraphImplByList<object, object>, IGraph
{
}