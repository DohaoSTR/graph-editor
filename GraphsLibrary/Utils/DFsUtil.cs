using GraphsLibrary.GraphElements;
using System;
using System.Collections.Generic;

namespace GraphsLibrary.Utils
{
    public class DFsUtil
    {
        private List<string> _chains = new List<string>();

        public ICollection<string> GetChains(ElementContainer<Vertex> vertices, ElementContainer<Edge> edges, int start, int target)
        {
            _chains.Clear();

            int[] color = new int[vertices.Count];
            for (int i = 0; i < vertices.Count - 1; i++)
            {
                for (int j = i + 1; j < vertices.Count; j++)
                {
                    for (int k = 0; k < vertices.Count; k++)
                    {
                        color[k] = 1;
                    }

                    DFSchain(i, j, color, (i + 1).ToString(), vertices, edges);
                }
            }

            List<string> newChainList = new List<string>();
            foreach (string i in _chains)
            {
                if (i.StartsWith(start.ToString()) && i.EndsWith(target.ToString()))
                {
                    newChainList.Add(i);
                }
                else if (i.EndsWith(start.ToString()) && i.StartsWith(target.ToString()))
                {
                    char[] arr = i.ToCharArray();
                    Array.Reverse(arr);
                    string j = new string(arr);
                    newChainList.Add(j);
                }
            }
            return newChainList;
        }

        private void DFSchain(int u, int numberEndVertex, int[] color, string s, ElementContainer<Vertex> vertices, ElementContainer<Edge> edges)
        {
            if (u != numberEndVertex)
            {
                color[u] = 2;
            }
            else
            {
                _chains.Add(s);
                return;
            }
            foreach (var el in edges)
            {
                if (color[vertices.IndexOf(el.End)] == 1 && vertices.IndexOf(el.Start) == u)
                {
                    AddChain(numberEndVertex, color, s, vertices.IndexOf(el.End), vertices, edges);
                }
                else if (color[vertices.IndexOf(el.Start)] == 1 && vertices.IndexOf(el.End) == u)
                {
                    AddChain(numberEndVertex, color, s, vertices.IndexOf(el.Start), vertices, edges);
                }
            }
        }

        private void AddChain(int endV, int[] color, string s, int numberVertex, ElementContainer<Vertex> vertices, ElementContainer<Edge> edges)
        {
            DFSchain(numberVertex, endV, color, s + (numberVertex + 1).ToString(), vertices, edges);
            color[numberVertex] = 1;
        }
    }
}
