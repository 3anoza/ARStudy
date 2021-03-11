using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class Geometric {
        [Obsolete("This method override is deprecated. Use CalculateSquare(Vector3[]) instead.")]
        public static float CalculateSquare(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 dot3){
            var cosAB = Cos(dot0 / 2, dot1 / 2) * Cos(dot2 / 2, dot3 / 2) - Sin(dot0 / 2, dot1 / 2) * Sin(dot2 / 2, dot3 / 2);
            var a = VectorLength(dot0, dot1);
            var b = VectorLength(dot1, dot2);
            var c = VectorLength(dot2, dot3);
            var d = VectorLength(dot0, dot3);
            var p = (a + b + c + d) / 2;
            return Mathf.Sqrt((p - a) * (p - b) * (p - c) * (p - d) - ( a * b * c * d ) * Mathf.Pow(cosAB, 2));
        }
        /// <summary>
        /// Find square for figure by mesh vertices.
        /// <br>Assume that mash contains 3 and more vertices</br>
        /// </summary>
        /// <param name="points">figure vertices in 3D world</param>
        /// <returns>area in square unit-meters (<see cref="float"/>)</returns>
        public static float CalculateSquare(Vector3[] points)
        {
            var triangles = TriangulatePoly(points);
            float square = 0;

            foreach (var triangle in triangles)
            {
                square += CalculateSquare(triangle.vertA, triangle.vertB, triangle.vertC);
            }

            return square;
        }
        /// <summary>
        /// Triangulates a mesh or polygon represented as an array of vertices
        /// <br>and then returns the number of triangles in this mesh or polygon</br>
        /// </summary>
        /// <param name="points">figure vertices in 3D world</param>
        /// <returns>a list of triangles that make up the mesh (<seealso cref="Triangle"/>)</returns>
        public static List<Triangle> TriangulatePoly(Vector3[] points)
        {
            return TriangulateConvexPoly(points);
        }
        public static List<Triangle> TriangulatePoly(Vector3[] points, PolygonType poly)
        {
            switch (poly)
            {
                case PolygonType.Convex:
                    return TriangulateConvexPoly(points);
                case PolygonType.Concave:
                    return TriangulateConcavePoly(points);
                case PolygonType.RandomPoints:
                    return TriangulateRandomPoly(points);
                default:
                    throw new ArgumentOutOfRangeException(nameof(poly), null, nameof(poly));
            }
        }
        private static List<Triangle> TriangulateConvexPoly(Vector3[] points)
        {
            List<Triangle> triangles = new List<Triangle>();
            for (int i = 2; i < points.Length; i++)
            {
                Vector3 a = points[0];
                Vector3 b = points[i - 1];
                Vector3 c = points[i];

                triangles.Add(new Triangle(a, b, c));
            }

            return triangles;
        }
        private static List<Triangle> TriangulateConcavePoly(Vector3[] points)
        {
            throw new NotImplementedException();
        }
        private static List<Triangle> TriangulateRandomPoly(Vector3[] points)
        {
            throw new NotImplementedException();
        }
        public static float CalculateSquare(Vector3 dot0, Vector3 dot1, Vector3 dot2){
            return 0.5f * VectorLength(dot0, dot1) * VectorLength(dot1, dot2) * Sin(dot0, dot2);
        }
        public static float VectorLength(Vector3 dot0, Vector3 dot1){
            return Mathf.Sqrt( Mathf.Pow(dot1.x - dot0.x, 2) + Mathf.Pow(dot1.y - dot0.y, 2) + Mathf.Pow(dot1.z - dot0.z, 2) );
        }
        public static float VectorLength(Vector3 dot0){
            return Mathf.Sqrt( Mathf.Pow(dot0.x, 2) + Mathf.Pow(dot0.y, 2) + Mathf.Pow(dot0.z, 2) );
        }
        public static float Sin(Vector3 dot0, Vector3 dot1){
            return Mathf.Sqrt( 1 - Mathf.Pow(Cos(dot0, dot1), 2) );
        }
        public static float Cos(Vector3 dot0, Vector3 dot1){
            return ScalarProduct(dot0, dot1) / ( VectorLength(dot0) * VectorLength(dot1) );
        }
        public static float ScalarProduct(Vector3 dot0, Vector3 dot1){
            return ( dot0.x * dot1.x ) + ( dot0.y * dot1.y ) + ( dot0.z * dot1.z );
        }

    }
}