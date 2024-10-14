using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebXR.InputSystem
{
public class DesktopMovement : MonoBehaviour
{
    WebXRState state;

    [SerializeField]
    float mouseSensitivity = 5f;
    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField]
    float gravity;
    float verticalRotation;
    float minMaxRotation = 45.0f;
    float forwardVelocity, sidewaysVelocity, verticalVelocity;
    CharacterController controller;

    //public GameObject virtualControllerTest;

    WebXRState currentState = WebXRState.NORMAL;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //controller = gameObject.GetComponent<CharacterController>();
        controller = GetComponentInParent<CharacterController>();
        currentState = WebXRManager.Instance.XRState;
        //Debug.Log(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        //called each frame. Probably there is a better way, i.e. by utilising the WebXRModesNotifier (within the editor!)?
        currentState = WebXRManager.Instance.XRState;
        if(currentState == WebXRState.NORMAL){

            Rotation();
            Movement();

            //if(virtualControllerTest.activeSelf){
                //virtualControllerTest.SetActive(false);
            //}        
        } //else if(currentState == WebXRState.AR) {
            //if(!virtualControllerTest.activeSelf){
                //virtualControllerTest.SetActive(true);
            //}
        //}

        //WebXRState.AR:
        //WebXRState.VR:
        //WebXRState.NORMAL:

    }
    void Movement() {

    forwardVelocity = Input.GetAxis("Vertical") * movementSpeed;
    sidewaysVelocity = Input.GetAxis("Horizontal") * movementSpeed;

    Vector3 direction = new Vector3(sidewaysVelocity, 0 , forwardVelocity);
    direction = Vector3.ClampMagnitude(direction, 5f);

    verticalVelocity -= gravity * Time.deltaTime;
    direction.y = verticalVelocity;

    //childObject.transform.parent.gameObject

    direction = transform.parent.transform.TransformDirection(direction);

    controller.Move(direction * Time.deltaTime);
}

void Rotation() {

    float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
    //childObject.transform.parent.gameObject
    transform.parent.transform.Rotate(0, horizontalRotation, 0);

    verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
    verticalRotation = Mathf.Clamp(verticalRotation, -minMaxRotation, minMaxRotation);
    //Camera.main.
    transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
}
}
}