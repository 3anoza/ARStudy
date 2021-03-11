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
    }
}