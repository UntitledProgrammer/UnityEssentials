using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Attributes:
    public float sensitivity;
    public float recentre_sensitivity;
    private Vector3 previous_mousePosition;
    public Vector3 origin;
    public bool recentre = false;
    private const float obtuse = 180.0f;
    private const float toleration = 0.25f;

    //Properties:
    public Vector3 mouseDelta { get { return Input.mousePosition - previous_mousePosition; } }
    public float horizontal { get { return transform.localEulerAngles.y; } }
    public float difference { get { return (origin - transform.localEulerAngles).normalized.y; } }

    //Methods:
    private void Start() { origin = transform.localEulerAngles; }

    private void Update()
    {
        //Vertical rotation.
        transform.eulerAngles += mouseDelta.normalized.y * sensitivity * Time.deltaTime * -Vector3.right;

        //Horizontal rotation.
        if (Input.GetKey(KeyCode.LeftShift)) { transform.eulerAngles += mouseDelta.normalized.x * sensitivity * Time.deltaTime * Vector3.up; recentre = true; }

        //Keep the camera centred.
        else if (!Input.GetKey(KeyCode.LeftShift) && recentre)
        {
            transform.localEulerAngles += horizontal < obtuse ? Vector3.up * difference * recentre_sensitivity *  Time.deltaTime : -Vector3.up * difference * recentre_sensitivity * Time.deltaTime;
            recentre = Mathf.Abs(origin.y - transform.localEulerAngles.y) > toleration; //If rotation is within toleration, stop centring the camera.
        }

    }

    private void LateUpdate() { previous_mousePosition = Input.mousePosition; }
}
