using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts
{
    public class DeviceInfoOnUI : MonoBehaviour
    {
        public ARCameraManager CameraManager;
        void Start()
        {
            var settings = CameraManager.currentConfiguration;
            var framerate = settings.Value.framerate;
            var res = settings.Value.resolution;

        }

        void Update()
        {
            
        }
    }
}
