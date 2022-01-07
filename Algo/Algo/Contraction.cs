using System;

namespace Algo
{
    public class Node
    {
        public int Val { get; set; }
        public List<Node> SuperVertexNodes { get; }
        public List<int> EdgesIds { get; }

        public Node(int val)
        {
            Val = val;
            SuperVertexNodes = new List<Node>();
            SuperVertexNodes.Add(this);
            EdgesIds = new List<int>();
        }

        public void AddEdge(int id)
        {
            EdgesIds.Add(id);           
        }

        public void RemoveEdge(int id)
        {
            EdgesIds.Remove(id);
        }
    }
    
    
    public class UndirectedGraph
    {
        public Dictionary<int, Node> NodesDict { get;}
        public Dictionary<int, Node[]> EdgesDict { get;}
        private int nodesCounter = 0;
        private int edgesCounter = 0;

        public UndirectedGraph()
        {
            NodesDict = new Dictionary<int, Node>();
            EdgesDict = new Dictionary<int, Node[]>();
        }

        public UndirectedGraph(int n) : this()
        {
            NodesDict.EnsureCapacity(n);
            for (int i = 0; i < n; i++)
            {
                NodesDict.Add(i, new Node(i));
            }
        }

        public Node AddNode(int val)
        {
            Node node = new (val);
            NodesDict.Add(val, node);
            return node;
        }

        public void AddMultiEdge(int node1Key, int node2Key)
        {
            Node node1 = NodesDict[node1Key];
            Node node2 = NodesDict[node2Key];
            AddMultiEdge(node1, node2);
        }

        public void AddMultiEdge(Node node1, Node node2)
        {     
            var edge = new Node[] { node1, node2 };
            edgesCounter++;
            EdgesDict.Add(edgesCounter, edge);
            node1.EdgesIds.Add(edgesCounter);
            node2.EdgesIds.Add(edgesCounter);
        }


        // repeat
        public int Contract()
        {
            Random random = new();
            
            while (NodesDict.Count > 2 && EdgesDict.Count > 0)
            {
                nodesCounter++; 
                int r = random.Next(EdgesDict.Count);
                Node[] e = EdgesDict.ElementAt(r).Value;
                Contract(e);
            }

            return NodesDict.First().Value.EdgesIds.Count;
        }

        public void Contract(Node[] edge)
        {
            Node mergedNode = AddNode(-nodesCounter);
            mergedNode.SuperVertexNodes.RemoveAt(0);
            
            Node node1 = edge[0];
            Node node2 = edge[1];
            mergedNode.SuperVertexNodes.AddRange(node1.SuperVertexNodes);
            mergedNode.SuperVertexNodes.AddRange(node2.SuperVertexNodes);

            Merge(mergedNode, node1, node2);
            Merge(mergedNode, node2, node1);

            node1.EdgesIds.ForEach(id => EdgesDict.Remove(id));
            node2.EdgesIds.ForEach(id => EdgesDict.Remove(id));

            NodesDict.Remove(node1.Val);
            NodesDict.Remove(node2.Val);

            void Merge(Node mergedNode, Node node1, Node skipNode)
            {             
                var neighbors = node1.EdgesIds;
                foreach (int edgeId in neighbors)
                {
                    var edge = EdgesDict[edgeId];
                    var neighbor = (edge[1] == node1)? edge[0] : edge[1];
                    if (neighbor == node1 || neighbor == skipNode)
                    {
                        continue;
                    }

                    AddMultiEdge(mergedNode, neighbor);
                    neighbor.RemoveEdge(edgeId);
                    EdgesDict.Remove(edgeId);
                }
            }
        }        
    }    
}
