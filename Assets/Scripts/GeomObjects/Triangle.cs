using UnityEngine;

namespace Assets.Scripts.GeomObjects
{
    /// <summary>
    /// Representations of a triangle in three-dimensional space in the form of a plane
    /// </summary>
    public struct Triangle
    {
        /// <summary>
        /// Vertex A of triangle
        /// </summary>
        public Vector3 VertA;
        /// <summary>
        /// Vertex B of triangle
        /// </summary>
        public Vector3 VertB;
        /// <summary>
        /// Vertex C of triangle
        /// </summary>
        public Vector3 VertC;

        public Triangle(Vector3 vertA, Vector3 vertB, Vector3 vertC)
        {
            VertA = vertA;
            VertB = vertB;
            VertC = vertC;
        }
        public Triangle(Vector3[] vertices)
        {
            if (vertices.Length < 3)
                throw new System.IndexOutOfRangeException($"{nameof(vertices)} length less than 3");
            VertA = vertices[0];
            VertB = vertices[1];
            VertC = vertices[3];
        }
    }
}