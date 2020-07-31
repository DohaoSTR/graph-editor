using GraphModel.Assets.Model.GraphElements;
using System;
using System.Collections.Generic;

namespace GraphsLibrary.Assets.Model.Utils
{
    public class AdjacencyMatrix
    {
        private readonly int[,] _matrix;

        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;

        public int this[int indexVertex, int indexEdge]
        {
            set
            {
                _matrix[indexVertex, indexEdge] = value;
            }
            get
            {
                return _matrix[indexVertex, indexEdge];
            }
        }

        public AdjacencyMatrix(List<Vertex> vertices, List<Edge> edges)
        {
            _vertices = vertices;
            _edges = edges;
            _matrix = new int[vertices.Count, vertices.Count];

            SetInitialValuesInMatrix();
        }

        private void SetInitialValuesInMatrix()
        {
            for (int indexVertex = 0; indexVertex < _vertices.Count; indexVertex++)
            {
                for (int indexEdge = 0; indexEdge < _vertices.Count; indexEdge++)
                {
                    _matrix[indexVertex, indexEdge] = 0;
                }
            }

            foreach (var element in _edges)
            {
                _matrix[element.Start.GetNumber, element.End.GetNumber] = 1;
                _matrix[element.End.GetNumber, element.Start.GetNumber] = 1;
            }
        }

        public int Length => _vertices.Count;

        /// <summary>
        /// Ищет кратчайший путь с помощью волнового алгоритма.
        /// </summary>
        /// <returns>
        /// Минимальное количество шагов между двумя вершинами.
        /// </returns>
        public int SearchShortestWay(int start, int target)
        {
            start -= 1;
            target -= 1;
            int[] p = new int[Length];

            for (int i = 0; i < Length; i++)
            {
                p[i] = -1;
            }

            p[start] = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int k = 0; k < Length; k++)
                {
                    if (p[k] == i)
                    {
                        for (int j = 0; j < Length; j++)
                        {
                            if (p[j] == -1 && this[j, k] == 1)
                            {
                                p[j] = i + 1;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Length; i++)
            {
                if (i == target)
                {
                    return p[i];
                }
            }
            throw new ArgumentNullException("Кратчайший путь не найден!");
        }
    }
}
