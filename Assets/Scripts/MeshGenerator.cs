using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts
{
    [RequireComponent(typeof(MeshFilter))]
    public class MeshGenerator : MonoBehaviour
    {
        private Mesh mesh;

        private Vector3[] vertices;
        private int[] triangles;

        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape();
            UpdateMesh();
        }

        void CreateShape()
        {
            vertices = new Vector3[]
            {
                new Vector3(5,0,0),
                new Vector3(3,0,2),
                new Vector3(0,0,0),
                new Vector3(0,0,4)
            };

            triangles = new int[]
            {
                0, 1, 2,
                2, 3, 0
            };


            vertices = SortVectors(vertices);
        }

        void UpdateMesh()
        {
            mesh.Clear();

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
        }

        private Vector3 GetVectorBetween(Vector3 vector1, Vector3 vector2)
        {
            return vector1 + (vector2 - vector1) / 2;
        }

        private Vector3 GetQuadMiddle(Vector3[] quadVertices)
        {
            var firstMiddle = GetVectorBetween(quadVertices[0], quadVertices[1]);
            var secondMiddle = GetVectorBetween(quadVertices[2], quadVertices[3]);
            return GetVectorBetween(firstMiddle, secondMiddle);
        }

        private float GetVectorAngle(Vector3 middleVector, Vector3 destinationVector)
        {
            var firstVector = Square.GetVector(middleVector, destinationVector);
            var projectVector = Square.GetVector(middleVector, new Vector3(middleVector.x + 2,0,middleVector.z));
            var cos = Square.Cos(firstVector, projectVector);
            var arcCos = Mathf.Acos(cos);
            var degree = arcCos * 180 / Mathf.PI;
            var vectorQuarter = Square.FindVectorQuarter(new UnityEngine.Vector2(middleVector.x, middleVector.z),
                new UnityEngine.Vector2(destinationVector.x, destinationVector.z));
            if (vectorQuarter == Quarter.I || vectorQuarter == Quarter.II)
            {
                degree = 360 - degree;
            }

            return degree;
        }

        private Dictionary<Vector3, float> GetVectorsAndAnglesDict(Vector3[] vectors, Vector3 middleVector)
        {
            Dictionary<Vector3, float> dict = new Dictionary<Vector3, float>();

            foreach (var vector in vectors)
            {
                dict.Add(vector, GetVectorAngle(middleVector, vector));
            }

            return dict;
        }

        private Vector3[] SortVectors(Vector3[] vectors)
        {
            Vector3[] sortedVectors = new Vector3[4];
            var middleVector = GetQuadMiddle(vectors);
            var dict = GetVectorsAndAnglesDict(vectors, middleVector);
            for(var i = 0; i < sortedVectors.Length; i++)
            {
                var minValue = dict.Min(d => d.Value);
                var vector = dict.Where(v => v.Value.Equals(minValue)).Select(d => d.Key).First();
                sortedVectors[i] = vector;
                dict.Remove(vector);
            }

            return sortedVectors;
        }
    }
}
