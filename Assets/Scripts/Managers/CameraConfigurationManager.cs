using System.Collections;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts.Managers
{
    public class CameraConfigurationManager : MonoBehaviour
    {
        public static CameraConfigurationManager Instance { get; private set; }

        public ARCameraManager CameraManager;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        void Start()
        {
            StartCoroutine(ApplyMaxResolutionCoroutine());
        }

        public XRCameraConfiguration? GetCurrentConfiguration()
        {
            return CameraManager.currentConfiguration;
        }

        public void SetCurrentConfiguration(XRCameraConfiguration configuration)
        {
            CameraManager.currentConfiguration = configuration;
        }

        public NativeArray<XRCameraConfiguration> GetAllAvailableConfigurations()
        {
            return CameraManager.GetConfigurations(Allocator.Persistent);
        }

        public void ApplyMaxCameraResolutionSettings()
        {
            var allConfigurations = GetAllAvailableConfigurations();
            var maxResolutionValue = allConfigurations.Max(t => t.width);
            var maxResolutionConfiguration =
                allConfigurations.Where(t => t.width.Equals(maxResolutionValue))
                    .Select(t => t).First();
            SetCurrentConfiguration(maxResolutionConfiguration);
        }

        private IEnumerator ApplyMaxResolutionCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            ApplyMaxCameraResolutionSettings();
        }
    }
}
