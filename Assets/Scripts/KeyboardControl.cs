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
    /*
    if (Input.GetKeyDown("d")){
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    if (Input.GetKeyDown("a")){
        transform.position += Vector3.left* speed * Time.deltaTime;
    }
    if (Input.GetKeyDown("w")){
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
    if (Input.GetKeyDown("s")){
        transform.position += Vector3.back* speed * Time.deltaTime;
    }
    */

    sidewaysVelocity += Input.GetAxis("Horizontal") * .5f;
    forwardVelocity += Input.GetAxis("Vertical") * .5f;

    Vector3 direction = new Vector3(sidewaysVelocity, transform.position.y , forwardVelocity);

    transform.localPosition = direction;

}

void Movement() {
    forwardVelocity = Input.GetAxis("Horizontal") * 5f;
    sidewaysVelocity = Input.GetAxis("Vertical") * 5f;

    Vector3 direction = new Vector3(sidewaysVelocity, 0 , forwardVelocity);

    Debug.Log(direction);

    direction = Vector3.ClampMagnitude(direction, 5f);

    direction = transform.TransformDirection(direction);
}

}
