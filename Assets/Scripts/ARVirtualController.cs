using UnityEngine;
using UnityEngine.EventSystems;

namespace WebXR.Interactions
{
    public class ARVirtualController : MonoBehaviour, IPointerDownHandler
  {
    private Camera m_currentCamera;
    private Vector3 m_screenPoint;
    private Vector3 m_offset;
    private Vector3 m_currentVelocity;
    private Vector3 m_previousPos;
    float gravity;
    float forwardVelocity, sidewaysVelocity, verticalVelocity;
    CharacterController controller;

    void Awake()
    {
      Debug.Log("ARVIrtualController online");
      Debug.Log(gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
      Debug.Log(gameObject.name);
      gameObject.SetActive(false);

      m_currentCamera = FindCamera();
      if (m_currentCamera != null)
      {
        /*
        //m_screenPoint = m_currentCamera.WorldToScreenPoint(gameObject.transform.position);
        //m_offset = gameObject.transform.position - m_currentCamera.ScreenToWorldPoint(GetMousePosWithScreenZ(m_screenPoint.z));

        //if (gameObject.name == "forward") {
            forwardVelocity = 1 * movementSpeed;
        }
        //if (gameObject.name == "backward") {
            forwardVelocity = -1 * movementSpeed;
        }
        //if (gameObject.name == "left") {
        sidewaysVelocity = 1 * movementSpeed;
        }
        //if (gameObject.name == "right") {
        sidewaysVelocity = -1 * movementSpeed;
        }
        Vector3 direction = new Vector3(sidewaysVelocity, 0 , forwardVelocity);
        direction = Vector3.ClampMagnitude(direction, 5f);

        verticalVelocity -= gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        //childObject.transform.parent.gameObject

        direction = transform.parent.transform.TransformDirection(direction);

        controller.Move(direction * Time.deltaTime);
*/
      }
    }

    void OnMouseUp()
    {
      //m_currentCamera = null;
    }

    void FixedUpdate()
    {
      if (m_currentCamera != null)
      {
        /*
        Vector3 currentScreenPoint = GetMousePosWithScreenZ(m_screenPoint.z);
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.MovePosition(m_currentCamera.ScreenToWorldPoint(currentScreenPoint) + m_offset);
        m_currentVelocity = (transform.position - m_previousPos) / Time.deltaTime;
        m_previousPos = transform.position;
        */
        //Movement(forwardVelocity, sidewaysVelocity)
      }
    }
    void Movement(float forwardVelocity, float sidewaysVelocity) {

    Vector3 direction = new Vector3(sidewaysVelocity, 0 , forwardVelocity);
    direction = Vector3.ClampMagnitude(direction, 5f);

    verticalVelocity -= gravity * Time.deltaTime;
    direction.y = verticalVelocity;

    //childObject.transform.parent.gameObject

    direction = transform.parent.transform.TransformDirection(direction);

    controller.Move(direction * Time.deltaTime);
}

    Vector3 GetMousePosWithScreenZ(float screenZ)
    {
      return new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenZ);
    }

    Camera FindCamera()
    {
#if UNITY_2023_1_OR_NEWER
      Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
#else
      Camera[] cameras = FindObjectsOfType<Camera>();
#endif
      Camera result = null;
      int camerasSum = 0;
      foreach (var camera in cameras)
      {
        if (camera.enabled)
        {
          result = camera;
          camerasSum++;
        }
      }
      if (camerasSum > 1)
      {
        result = null;
      }
      return result;
    }
  }
}
