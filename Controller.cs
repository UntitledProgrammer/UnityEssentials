using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    //Attributes:
    [Header("Main Settings:")]
    public float walk_speed, sprint_speed, jump_force;
    [Header("Controller Settings:")]
    public bool displayCursor = false;
    public KeyCode jump_key;
    public float sensitivity, raycast_length;
    private Rigidbody rigidbody;
    private const string horizontal = "Horizontal", vertical = "Vertical";
    private Vector3 previous_mousePosition;
    private RaycastHit hit;

    //Properties:
    public int IsJumping { get { return Input.GetKeyDown(jump_key) && Physics.Raycast(transform.position, -transform.up, out hit, raycast_length) ? 1 : 0; } }
    public Vector3 mouseDelta { get { return Input.mousePosition - previous_mousePosition; } }

    //Methods:
    private void Start() { rigidbody = gameObject.GetComponent<Rigidbody>(); } //Because we require the rigidbody component, we do not need to check if it exists. 

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift)) return;

        //Movement:
        rigidbody.MovePosition(transform.position + (Time.deltaTime * walk_speed * (transform.forward * Input.GetAxis(vertical) + transform.right * Input.GetAxis(horizontal))));
    }

    private void Update()
    {
        Cursor.visible = displayCursor;

        if (Input.GetKey(KeyCode.LeftShift)) return;

        //Jump:
        rigidbody.AddForce(transform.up * IsJumping * jump_force, ForceMode.Impulse);

        //Rotation: (Normalise for smoother rotation per frame).
        transform.eulerAngles += mouseDelta.normalized.x * sensitivity * Time.deltaTime * transform.up;
    }

    private void LateUpdate() { previous_mousePosition = Input.mousePosition; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * raycast_length);
        Gizmos.color = Color.white;
    }
}
