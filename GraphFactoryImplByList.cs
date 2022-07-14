class GraphFactoryImplByList : IGraphFactory
{
    public IGraph<TNodeData, TEdgeData> Create<TNodeData, TEdgeData>()
    {
        return new GraphImplByList<TNodeData, TEdgeData>();
    }

    public IGraph Create()
    {
        return new GraphImplByList();
    }
}