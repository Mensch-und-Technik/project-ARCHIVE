using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepPlane : MonoBehaviour
{
    public GameObject referencePlane;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = referencePlane.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
	Debug.Log(transform.rotation);
        //transform.rotation = Quaternion.identity;
        transform.rotation = referencePlane.transform.rotation;
	Debug.Log(transform.rotation);

    }
}
