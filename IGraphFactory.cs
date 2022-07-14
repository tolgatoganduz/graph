interface IGraphFactory
{
    IGraph Create();
    
    IGraph<TNodeData, TEdgeData> Create<TNodeData, TEdgeData>();
}