using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// A collection of common geometric functions
    /// </summary>
    public struct Square
    {
        /// <summary>
        /// Calculate figure square by vertices
        /// <br>Returns square</br>
        /// </summary>
        /// <param name="points">figure vertices in 3D</param>
        public static float GetSquare(Vector3[] points)
        {
            var triangles = TriangulateConvex(points);
            return triangles.Sum(GetTriangleSquare);
        }

        #region GEOMETRIC_CALCULATIONS
        /// <summary>
        /// Calculate triangle square by vertices
        /// <br>Returns square</br>
        /// </summary>
        /// <param name="triangle">triangle object</param>
        public static float GetTriangleSquare(Triangle triangle)
        {
            var vectorA = GetVector(triangle.VertA, triangle.VertB);
            var vectorB = GetVector(triangle.VertA, triangle.VertC);
            var sideA = GetVectorLength(vectorA);
            var sideB = GetVectorLength(vectorB);
            var sinA = Sin(vectorA, vectorB);
            return .5f * sideA * sideB * sinA;
        }
        /// <summary>
        /// Returns directional vector
        /// </summary>
        /// <param name="point0">vector start point</param>
        /// <param name="point1">vector end point</param>
        public static Vector3 GetVector(Vector3 point0, Vector3 point1)
        {
            return new Vector3(point1.x - point0.x, point1.y - point0.y, point1.z - point0.z);
        }
        /// <summary>
        /// Return vector length
        /// </summary>
        /// <param name="vector"></param>
        public static float GetVectorLength(Vector3 vector)
        {
            return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
        }
        /// <summary>
        /// Triangulate figure by vertices.
        /// <br>Assume that vertices 3 and more</br>
        /// </summary>
        /// <param name="points">figure vertices</param>
        public static List<Triangle> TriangulateConvex(Vector3[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("argument length less than 3", nameof(points));
            
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
        /// <summary>
        /// Returns scalar product of two directional vectors
        /// </summary>
        /// <param name="vector0">directional vector</param>
        /// <param name="vector1">directional vector</param>
        public static float ScalarProduct(Vector3 vector0, Vector3 vector1)
        {
            return (vector0.x * vector1.x) + (vector0.y * vector1.y) + (vector0.z * vector1.z);
        }
        /// <summary>
        /// Finds the cosine of the angle between two directional vectors
        /// <br>Returns cosine of the angle</br>
        /// </summary>
        /// <param name="vector0">directional vector</param>
        /// <param name="vector1">directional vector</param>
        public static float Cos(Vector3 vector0, Vector3 vector1)
        {
            var product = ScalarProduct(vector0, vector1);
            var sideA = GetVectorLength(vector0);
            var sideB = GetVectorLength(vector1);
            return product / (sideA * sideB);
        }
        /// <summary>
        /// Finds the sine of the angle between two directional vectors
        /// <br>Returns sine of the angle</br>
        /// </summary>
        /// <param name="vector0">directional vector</param>
        /// <param name="vector1">directional vector</param>
        public static float Sin(Vector3 vector0, Vector3 vector1)
        {
            var cos = Cos(vector0, vector1);
            return Mathf.Sqrt(1 - Mathf.Pow(cos, 2));
        }
        /// <summary>
        /// Finds the tangent of the angle between two directional vectors
        /// <br>Returns tangent of the angle</br>
        /// </summary>
        /// <param name="vector0">directional vector</param>
        /// <param name="vector1">directional vector</param>
        public static float Tan(Vector3 vector0, Vector3 vector1)
        {
            return Sin(vector0, vector1) / Cos(vector0, vector1);
        }
        /// <summary>
        /// Finds the cotangent of the angle between two directional vectors
        /// <br>Returns cotangent of the angle</br>
        /// </summary>
        /// <param name="vector0">directional vector</param>
        /// <param name="vector1">directional vector</param>
        public static float Cot(Vector3 vector0, Vector3 vector1)
        {
            return Cos(vector0, vector1) / Sin(vector0, vector1);
        }
        /// <summary>
        /// Returns vertex coordinate quarter 
        /// </summary>
        /// <param name="planeCenter">coordinate plane center</param>
        /// <param name="vertex">vertex on coordinate plane</param>
        public static Quarter FindVectorQuarter(Vector2 planeCenter, Vector2 vertex)
        {
            if (planeCenter.x < vertex.x && planeCenter.y < vertex.y)
                return Quarter.I;
            if (planeCenter.x > vertex.x && planeCenter.y < vertex.y)
                return Quarter.II;
            if (planeCenter.x > vertex.x && planeCenter.y > vertex.y)
                return Quarter.III;
            if (planeCenter.x < vertex.x && planeCenter.y > vertex.y)
                return Quarter.IV;

            throw new ArgumentOutOfRangeException(nameof(vertex), null, nameof(vertex));
        }
        #endregion
    }
}
