using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class UserInputManager : MonoBehaviour
    {
        public GameObject ObjectToSpawn;

        private PlacementIndicator _placementIndicator;
        private MeshGenerator _meshGenerator;
        private List<Vector3> _vertices = new List<Vector3>();

        private bool IsMeshCreated = false;

        void Start()
        {
            _placementIndicator = FindObjectOfType<PlacementIndicator>();
            _meshGenerator = FindObjectOfType<MeshGenerator>();
        }

        void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began 
                                     && _vertices.Count < 4)
            {
                GameObject instance = Instantiate(ObjectToSpawn,
                    _placementIndicator.transform.position, _placementIndicator.transform.rotation);
                _vertices.Add(instance.transform.position);
            }

            if (!IsMeshCreated && _vertices.Count == 4)
            {
                IsMeshCreated = true;
                _meshGenerator.GenerateMesh(_vertices.ToArray());
            }
        }
    }
}
