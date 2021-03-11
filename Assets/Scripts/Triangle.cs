using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public struct Triangle
    {
        public Vector3 vertA;
        public Vector3 vertB;
        public Vector3 vertC;

        public Triangle(Vector3 vertA, Vector3 vertB, Vector3 vertC)
        {
            this.vertA = vertA;
            this.vertB = vertB;
            this.vertC = vertC;
        }
        public Triangle(Vector3[] vertices)
        {
            if (vertices.Length < 3)
                throw new System.IndexOutOfRangeException($"{nameof(vertices)} length less than 3");
            vertA = vertices[0];
            vertB = vertices[1];
            vertC = vertices[3];
        }
    }
}