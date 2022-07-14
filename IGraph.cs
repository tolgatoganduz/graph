interface IGraph<TNodeData, TEdgeData>
{
    Node<TNodeData> CreateNode();

    Node<TNodeData> CreateNode(TNodeData? nodeData);

    void RemoveNode(Node<TNodeData> node);

    void ClearNodes();

    bool HasNode(Node<TNodeData> node);

    bool HasNode(NodeId nodeId);

    IReadOnlyList<Node<TNodeData>> Nodes { get; }

    IReadOnlyList<Edge<TNodeData, TEdgeData>> Edges { get; }

    int NodeCount { get; }

    int EdgeCount { get; }


    Edge<TNodeData, TEdgeData> CreateEdge(Node<TNodeData> start, Node<TNodeData> end);

    Edge<TNodeData, TEdgeData> CreateEdge(Node<TNodeData> start, Node<TNodeData> end, TEdgeData? edgeData);


    void RemoveEdge(EdgeId edgeId);

    void RemoveEdge(Node<TNodeData> start, Node<TNodeData> end);

    void RemoveEdge(NodeId startNodeId, NodeId endNodeId);

    void RemoveEdge(Edge<TNodeData, TEdgeData> edge);

    void ClearEdges();

    bool HasEdge(Edge<TNodeData, TEdgeData> edge);

    bool HasEdge(Node<TNodeData> start, Node<TNodeData> end);

    bool HasEdge(NodeId startNodeId, NodeId endNodeId);

    bool HasEdge(EdgeId edgeId);



    IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(Node<TNodeData> node);

    IReadOnlyList<Node<TNodeData>> GetAdjacentNodes(NodeId nodeId);

    IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(Node<TNodeData> node);

    IReadOnlyList<Node<TNodeData>> GetInboundAdjacentNodes(NodeId nodeId);

    IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(Node<TNodeData> node);

    IReadOnlyList<Node<TNodeData>> GetOutboundAdjacentNodes(NodeId nodeId);
}

interface IGraph : IGraph<object, object>
{

}