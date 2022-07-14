class GraphFactoryImplByDictionary : IGraphFactory
{
    public IGraph<TNodeData, TEdgeData> Create<TNodeData, TEdgeData>()
    {
        return new GraphImplByDictionary<TNodeData, TEdgeData>();
    }

    public IGraph Create()
    {
        return new GraphImplByDictionary();
    }
}