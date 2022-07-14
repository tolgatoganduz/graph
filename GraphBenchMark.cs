using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
[RankColumn]
public class GraphBenchMark
{
    [Benchmark]
    public void ListGraph()
    {
        TestGraph(new GraphFactoryImplByList());
    }

    [Benchmark]
    public void DictionaryGraph()
    {
        TestGraph(new GraphFactoryImplByDictionary());
    }

    private void TestGraph(IGraphFactory graphFactory)
    {
        var graph = graphFactory.Create();

        for (int i = 0; i < 10000; i++)
        {
            graph.CreateNode();
        }

        var nodes = graph.Nodes;

        for (int i = 0; i < (nodes.Count - 1); i++)
        {
            var startNode = nodes[i];

            var endNode = nodes[i + 1];

            graph.CreateEdge(startNode, endNode);
        }
    }
}