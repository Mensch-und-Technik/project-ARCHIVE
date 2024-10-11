using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{
private float speed = 2.0f;

float forwardVelocity, sidewaysVelocity;

void Start(){
    
}
void Update () {
    sidewaysVelocity += Input.GetAxis("Horizontal") * .5f;
    forwardVelocity += Input.GetAxis("Vertical") * .5f;

    Vector3 direction = new Vector3(sidewaysVelocity, transform.position.y , forwardVelocity);

    transform.localPosition = direction;

}

void Movement() {
    forwardVelocity = Input.GetAxis("Horizontal") * 5f;
    sidewaysVelocity = Input.GetAxis("Vertical") * 5f;

    Vector3 direction = new Vector3(sidewaysVelocity, 0 , forwardVelocity);

    direction = Vector3.ClampMagnitude(direction, 5f);

    direction = transform.TransformDirection(direction);
}

}
