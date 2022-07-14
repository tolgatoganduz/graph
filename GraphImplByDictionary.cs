class GraphImplByDictionary<TNodeData, TEdgeData> : IGraph<TNodeData, TEdgeData>
{
    private class NodeRecord : Node<TNodeData>
    {
        public List<Edge<TNodeData, TEdgeData>> Edges = new();

        public NodeRecord(NodeId id) : base(id)
        {

        }

        public NodeRecord(NodeId id, TNodeData? data) : base(id, data)
        {
        }


    }

    private int _nodeIdCounter = 0;
    private int _edgeIdCounter = 0;



    private Dictionary<NodeId, NodeRecord> _nodes = new();






    public void ClearEdges()
    {
        foreach (var node in _nodes)
        {
            node.Value.Edges.Clear();
        }
    }

    public void ClearNodes()
    {
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

        var startNode = _nodes[start.Id];

        var endNode = _nodes[end.Id];

        var edge = new Edge<TNodeData, TEdgeData>(new EdgeId(++_edgeIdCounter), start, end, edgeData);

        startNode.Edges.Add(edge);

        endNode.Edges.Add(edge);

        return edge;
    }

    public Node<TNodeData> CreateNode()
    {
        return CreateNode(default(TNodeData));
    }

    public Node<TNodeData> CreateNode(TNodeData? nodeData)
    {
        var node = new NodeRecord(new NodeId(++_nodeIdCounter), nodeData);

        _nodes.Add(node.Id, node);

        return node;
    }

    public IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(Node<TNodeData> node)
    {
        return GetAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(NodeId nodeId)
    {
        var result = new List<Node<TNodeData>>();

        if (_nodes.ContainsKey(nodeId))
        {
            var node = _nodes[nodeId];

            result.AddRange(node.Edges.Where(w => w.Start.Id == nodeId).Select(t => t.End).ToList());

            result.AddRange(node.Edges.Where(w => w.End.Id == nodeId).Select(t => t.Start).ToList());
        }

        return result;
    }

    public IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(Node<TNodeData> node)
    {
        return GetInboundAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(NodeId nodeId)
    {
        var result = new List<Node<TNodeData>>();

        if (_nodes.ContainsKey(nodeId))
        {
            var node = _nodes[nodeId];

            result.AddRange(node.Edges.Where(w => w.End.Id == nodeId).Select(t => t.Start).ToList());
        }

        return result;
    }

    public IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(Node<TNodeData> node)
    {
        return GetOutboundAdjacentNodes(node.Id);
    }

    public IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(NodeId nodeId)
    {
        var result = new List<Node<TNodeData>>();

        if (_nodes.ContainsKey(nodeId))
        {
            var node = _nodes[nodeId];

            result.AddRange(node.Edges.Where(w => w.Start.Id == nodeId).Select(t => t.End).ToList());
        }

        return result;
    }

    public bool HasEdge(Edge<TNodeData, TEdgeData> edge)
    {
        return HasEdge(edge.Start, edge.End);
    }

    public bool HasEdge(Node<TNodeData> start, Node<TNodeData> end)
    {
        return HasEdge(start.Id, end.Id);
    }

    public bool HasEdge(NodeId startNodeId, NodeId endNodeId)
    {
        if (!_nodes.ContainsKey(startNodeId))
        {
            return false;
        }

        var startNode = _nodes[startNodeId];

        return startNode.Edges.Exists(w => w.Start.Id == startNodeId || w.End.Id == endNodeId);
    }

    public bool HasEdge(EdgeId edgeId)
    {
        foreach (var node in _nodes)
        {
            if (node.Value.Edges.Exists(w => w.Id == edgeId))
            {
                return true;
            }
        }

        return false;
    }

    public bool HasNode(Node<TNodeData> node)
    {
        return HasNode(node.Id);
    }

    public bool HasNode(NodeId nodeId)
    {
        return _nodes.ContainsKey(nodeId);
    }

    public void RemoveEdge(Edge<TNodeData, TEdgeData> edge)
    {
        RemoveEdge(edge.Id);
    }

    public void RemoveNode(Node<TNodeData> node)
    {
        _nodes.Remove(node.Id);

        foreach (var otherNode in _nodes)
        {
            otherNode.Value.Edges.RemoveAll(w => w.Start == node || w.End == node);
        }
    }

    public void RemoveEdge(EdgeId edgeId)
    {
        foreach (var node in _nodes)
        {
            if (node.Value.Edges.RemoveAll(w => w.Id == edgeId) > 0)
            {
                return;
            }
        }
    }

    public void RemoveEdge(Node<TNodeData> start, Node<TNodeData> end)
    {
        RemoveEdge(start.Id, end.Id);
    }

    public void RemoveEdge(NodeId startNodeId, NodeId endNodeId)
    {
        if (_nodes.ContainsKey(startNodeId) && _nodes.ContainsKey(endNodeId))
        {
            var startNode = _nodes[startNodeId];

            var endNode = _nodes[endNodeId];

            startNode.Edges.RemoveAll(w => w.End.Id == endNodeId);

            endNode.Edges.RemoveAll(w => w.Start.Id == startNodeId);
        }
    }

    public IReadOnlyList<Node<TNodeData>> Nodes => _nodes.Values.ToList();


    public IReadOnlyList<Edge<TNodeData, TEdgeData>> Edges
    {
        get
        {
            var result = new List<Edge<TNodeData, TEdgeData>>();

            foreach (var node in _nodes.Values)
            {
                result.AddRange(node.Edges.Where(w => w.Start.Id == node.Id).ToList());
            }

            return result;
        }
    }

    public int NodeCount => _nodes.Count;

    public int EdgeCount
    {
        get
        {
            var result = 0;

            foreach (var node in _nodes.Values)
            {
                result += node.Edges.Where(w => w.Start.Id == node.Id).Count();
            }

            return result;
        }
    }
}


class GraphImplByDictionary : GraphImplByDictionary<object, object>, IGraph
{
}