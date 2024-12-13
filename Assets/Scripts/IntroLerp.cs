// A longer example of Vector3.Lerp usage.
// Drop this script under an object in your scene, and specify 2 other objects in the "startMarker"/"endMarker" variables in the script inspector window.
// At play time, the script will move the object along a path between the position of those two markers.

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    //public Transform startMarker;
    //public Transform endMarker;
    //private Vector3 startPosition = new Vector3(-23f, 13f, -10f);
    private Vector3 startPosition = new Vector3(-30f, 20f, -10f);
    private Vector3 endPosition = new Vector3(0.2f, 2.9f, -4f);
    //private Vector3 endPosition = new Vector3(-100f, 0f, -10f);
    private Vector3 currentPosition;
    public GameObject object2bTransformed;
    public GameObject object2LookAt;

	public Camera m_camera;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float flightLength;

    private Color lerpedColor = Color.white;

    void Start()
    {
        //m_camera.clearFlags = CameraClearFlags.SolidColor;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        //flightLength = Vector3.Distance(startMarker.position, endMarker.position);
        flightLength = Vector3.Distance(startPosition, endPosition);
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / flightLength;

        // Set our position as a fraction of the distance between the markers.
        currentPosition = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
        object2bTransformed.transform.position = currentPosition;

        object2bTransformed.transform.LookAt(object2LookAt.transform);

        if (fractionOfJourney > 0.99f){
            //Debug.Log("terminated");
            
	        //m_camera.clearFlags = CameraClearFlags.Skybox;
            //lerpedColor = Color.Lerp(Color.black, Color.clear, Mathf.PingPong(Time.time, 2));
	        //m_camera.backgroundColor = lerpedColor;
	        //m_camera.backgroundColor = Color.clear;
            object2bTransformed.transform.position = endPosition;
            object2bTransformed.transform.LookAt(object2LookAt.transform);
            Destroy(gameObject);
        }
    }
}