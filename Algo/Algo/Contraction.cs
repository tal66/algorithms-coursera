using System;

namespace Algo
{
    public class Node
    {
        public int Val { get; set; }
        public readonly List<Node> Neighbors;
        public readonly List<Node> SuperVertexNodes;

        public Node(int val)
        {
            Val = val;
            Neighbors = new List<Node>();
            SuperVertexNodes = new List<Node>();
            SuperVertexNodes.Add(this);
        }

        public void AddNeighbor(Node node)
        {
            Neighbors.Add(node);           
        }

        public void RemoveNeighbor(Node Neighbor)
        {
            Neighbors.Remove(Neighbor);
        }
    }
    
    // could be improved..
    public class Graph
    {
        public List<Node> Nodes { get; } // need this for iteration
        public List<Node[]> Edges { get; } // need this to choose random edge

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Node[]>();
        }

        public Graph(int n) : this()
        {
            Nodes.Capacity = n; 
            for (int i = 0; i < n; i++)
            {
                Nodes.Add(new Node(i));
            }
        }

        public Node AddNode(int val)
        {
            Node node = new (val);
            Nodes.Add(node);
            return node;
        }

        public void AddEdgeFromTo(int node1Index, int node2Index)
        {
            Node node1 = Nodes[node1Index]; 
            Node node2 = Nodes[node2Index];
            AddEdgeFromTo(node1, node2);
        }

        public void AddEdgeFromTo(Node node1, Node node2)
        {     
            node1.AddNeighbor(node2);
            Edges.Add(new Node[] { node1, node2 });                     
        }

        public void AddMultiEdge(Node node1, Node node2)
        {
            AddEdgeFromTo(node1, node2);
            AddEdgeFromTo(node2, node1);
        }

        // repeat
        public int Contract()
        {
            Random random = new();

            while (Nodes.Count > 2 && Edges.Count > 0)
            {
                int r = random.Next(Edges.Count);
                Node[] e = Edges[r];
                Contract(e);
            }

            return Nodes[0].Neighbors.Count;
        }

        public void Contract(Node[] edge)
        {
            Node mergedNode = AddNode(-1);
            mergedNode.SuperVertexNodes.RemoveAt(0);
            
            Node node1 = edge[0];
            Node node2 = edge[1];
            mergedNode.SuperVertexNodes.AddRange(node1.SuperVertexNodes);
            mergedNode.SuperVertexNodes.AddRange(node2.SuperVertexNodes);

            Merge(mergedNode, node1, node2);
            Merge(mergedNode, node2, node1);

            int removed = Edges.RemoveAll(e => e[1] == node1 || e[1] == node2
                                            || e[0] == node1 || e[0] == node2);

            removed = Nodes.RemoveAll(n => n.Equals(node1) || n.Equals(node2));
            
            //

            void Merge(Node mergedNode, Node node1, Node skipNode)
            {             
                var neighbors = node1.Neighbors;
                foreach (Node neighbor in neighbors)
                {
                    if (neighbor == node1 || neighbor == skipNode)
                    {
                        continue;
                    }

                    AddMultiEdge(mergedNode, neighbor);                    
                    neighbor.RemoveNeighbor(node1);
                }            
            }
        }
        
    }    
}
