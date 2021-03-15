using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts
{
    public class PlacementIndicator : MonoBehaviour
    {
        private ARRaycastManager _rayManager;

        private GameObject _visual;

        void Start()
        {
            // get the components
            _rayManager = FindObjectOfType<ARRaycastManager>();
            _visual = transform.GetChild(0).gameObject;

            // hide the placements visual
            _visual.SetActive(false);
        }

        void Update()
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            _rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2),
                hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;

                if (!_visual.activeInHierarchy)
                {
                    _visual.SetActive(true);
                }
            }
        }
    }
}
