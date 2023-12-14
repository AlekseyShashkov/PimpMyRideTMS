using UnityEngine;

namespace Car
{
    public class Lights : MonoBehaviour
    {
        [SerializeField]
        private GameObject tailLights;
        [SerializeField]
        private Material material;

        [SerializeField]
        private GameObject _frontLights;
        [SerializeField]
        private GameObject _lens;
        [SerializeField]
        private GameObject _lowBeam;
        [SerializeField]
        private GameObject _highBeam;
        [SerializeField]
        private Material _lensMaterial;
        [SerializeField]
        private Material _beamsMaterial;
      
        public void TailLights(bool on)
        {
            tailLights.SetActive(on);
            if (on)
            {
                material.EnableKeyword("_EMISSION");
            }
            else
            {
                material.DisableKeyword("_EMISSION");
            }
        }

        public void FrontLights(bool isPressed)
        {   
            if(isPressed) {
                if (!_frontLights.activeSelf) {
                    _frontLights.SetActive(true);
                    _lens.SetActive(true);
                    _lowBeam.SetActive(true);

                    _lensMaterial.EnableKeyword("_EMISSION");
                    _beamsMaterial.EnableKeyword("_EMISSION");
                }
                else if(_lowBeam.activeSelf && !_highBeam.activeSelf) {                
                    _lowBeam.SetActive(false);
                    _highBeam.SetActive(true);
                }
                else if(_highBeam.activeSelf) {
                    _frontLights.SetActive(false);
                    _lens.SetActive(false);
                    _highBeam.SetActive(false);

                    _lensMaterial.DisableKeyword("_EMISSION");
                    _beamsMaterial.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}