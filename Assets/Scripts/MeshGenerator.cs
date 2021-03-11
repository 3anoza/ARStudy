using System.Linq;
using UnityEngine;

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
                new Vector3(1,0,0),
                new Vector3(2,0,1),
                new Vector3(0,0,0),
                new Vector3(0,0,1)
                
            };

            triangles = new int[]
            {
                0, 1, 2,
                1, 3, 2
            };

            SortVectors();
        }

        void UpdateMesh()
        {
            mesh.Clear();

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
        }

        void SortVectors()
        {
            var xMin = vertices.Min(v => v.x);
            var xMax = vertices.Max(v => v.x);

            var dot1 = vertices.Where(v => v.x.Equals(xMin) && v.z.Equals(vertices.Where(g => g.x.Equals(xMin)).Select(g => g.z).Min())).Select(v => v).First();
            var dot2 = vertices.Where(v => v.x.Equals(xMin) && v.z.Equals(vertices.Where(g => g.x.Equals(xMin)).Select(g => g.z).Max())).Select(v => v).First();
            var dot3 = vertices.Where(v => v.x.Equals(xMax) && v.z.Equals(vertices.Where(g => g.x.Equals(xMax)).Select(g => g.z).Max())).Select(v => v).First();
            var dot4 = vertices.Where(v => v.x.Equals(xMax) && v.z.Equals(vertices.Where(g => g.x.Equals(xMax)).Select(g => g.z).Min())).Select(v => v).First();

            vertices = new Vector3[]
            {
                dot1,
                dot2,
                dot3,
                dot4
            };
        }
    }
}
