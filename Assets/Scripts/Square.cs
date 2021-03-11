using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public struct Square
    {
        public static float GetSquare(Vector3[] points)
        {
            float square = 0;
            var triangles = TriangulateConvex(points);
            foreach (var triangle in triangles)
            {
                square += GetTriangleSquare(triangle);
            }

            return square;
        }

        #region GEOMETRIC_CALCULATIONS

        public static float GetTriangleSquare(Triangle triangle)
        {
            var vectorA = GetVector(triangle.vertA, triangle.vertB);
            var vectorB = GetVector(triangle.vertA, triangle.vertC);
            var sideA = GetVectorLength(vectorA);
            var sideB = GetVectorLength(vectorB);
            var sinA = Sin(vectorA, vectorB);
            return .5f * sideA * sideB * sinA;
        }

        public static Vector3 GetVector(Vector3 point0, Vector3 point1)
        {
            return new Vector3(point1.x - point0.x, point1.y - point0.y, point1.z - point0.z);
        }

        public static float GetVectorLength(Vector3 vector)
        {
            return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
        }

        public static List<Triangle> TriangulateConvex(Vector3[] points)
        {
            List<Triangle> triangles = new List<Triangle>();
            
            for (int i = 2; i < points.Length; i++)
            {
                Vector3 sideA = points[0];
                Vector3 sideB = points[i - 1];
                Vector3 sideC = points[i];

                triangles.Add(new Triangle(sideA, sideB, sideC));
            }
            
            return triangles;
        }

        public static float ScalarProduct(Vector3 vector0, Vector3 vector1)
        {
            return (vector0.x * vector1.x) + (vector0.y * vector1.y) + (vector0.z * vector1.z);
        }
        
        public static float Cos(Vector3 vector0, Vector3 vector1)
        {
            var product = ScalarProduct(vector0, vector1);
            var sideA = GetVectorLength(vector0);
            var sideB = GetVectorLength(vector1);
            return product / (sideA * sideB);
        }

        public static float Sin(Vector3 vector0, Vector3 vector1)
        {
            var cos = Cos(vector0, vector1);
            return Mathf.Sqrt(1 - Mathf.Pow(cos, 2));
        }
        #endregion
    }
}
