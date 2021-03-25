using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeviceInfoUIDisplay : MonoBehaviour
    {
        public TextMeshProUGUI Text;

        void Update()
        {
            var resolution = CameraConfigurationManager.Instance.GetCurrentConfiguration().Value.resolution.ToString();
            Text.text = "Resolution: " + resolution;
        }
    }
}
