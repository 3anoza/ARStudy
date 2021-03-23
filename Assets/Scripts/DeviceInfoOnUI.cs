using System.Collections;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts
{
    public class DeviceInfoOnUI : MonoBehaviour
    {
        public ARCameraManager CameraManager;
        void Start()
        {
            StartCoroutine(SetConfig());

        }

        void Update()
        {
            
        }

        private IEnumerator SetConfig()
        {
            yield return new WaitForSeconds(1);
            var conf = CameraManager.GetConfigurations(Allocator.Persistent);
            CameraManager.currentConfiguration = conf[2];
        }
    }
}
