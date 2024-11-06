using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

#if ENABLE_VR || (UNITY_GAMECORE && INPUT_SYSTEM_1_4_OR_NEWER)
using UnityEngine.InputSystem.XR;
#endif
namespace UnityEngine.XR.Interaction.Toolkit
{

    public class ExplainationSwitch : MonoBehaviour
    {
        [SerializeField]
        InputActionProperty m_ControlInfoToggleAction = new InputActionProperty(new InputAction("ControlInfoToggle", type: InputActionType.Button));

        public GameObject uiSample;
        public GameObject leftControllerInfo;
        public GameObject leftControllerPoke;
        public GameObject leftControllerRay;
        public GameObject leftControllerDirect;
        public GameObject rightControllerInfo;
        public GameObject rightControllerPoke;
        public GameObject rightControllerRay;
        public GameObject rightControllerDirect;

        // Start is called before the first frame update
        void Start()
        {
            uiSample.SetActive(false);
            /*
            leftControllerPoke.SetActive(true);
            leftControllerRay.SetActive(true);
            leftControllerDirect.SetActive(true);
            rightControllerPoke.SetActive(true);
            rightControllerRay.SetActive(true);
            rightControllerDirect.SetActive(true);

            leftControllerInfo.SetActive(false);
            rightControllerInfo.SetActive(false);
            */

        }

        // Update is called once per frame
        void Update()
        {
            if(m_ControlInfoToggleAction.action.triggered){
                //uiSample.SetActive(!uiSample.activeSelf);
                leftControllerInfo.SetActive(!leftControllerInfo.activeSelf);
                leftControllerPoke.SetActive(!leftControllerPoke.activeSelf);
                leftControllerRay.SetActive(!leftControllerRay.activeSelf);
                leftControllerDirect.SetActive(!leftControllerDirect.activeSelf);
                rightControllerInfo.SetActive(!rightControllerInfo.activeSelf);
                rightControllerPoke.SetActive(!rightControllerPoke.activeSelf);
                rightControllerRay.SetActive(!rightControllerRay.activeSelf);
                rightControllerDirect.SetActive(!rightControllerDirect.activeSelf);
            }

            /*if(m_ControlInfoToggleAction.action.performed != null){
                if(controlInfo.activeSelf){
                    controlInfo.SetActive(false);
                }else{
                    controlInfo.SetActive(true);
                }
            }*/
        }
    }
}