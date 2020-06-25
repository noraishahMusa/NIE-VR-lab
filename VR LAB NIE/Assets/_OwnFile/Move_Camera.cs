using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    public float horizontalSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float speed = 5.0f;
    public float sensitivity = 5.0f;
    public GameObject eye;

    private float xValue;
    private float yValue;
    private float moveFB;
    private float moveLR;

    public Rigidbody rigid;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //get mouse movement to translate to camera rotation
        xValue = Input.GetAxis("Mouse X");
        yValue = Input.GetAxis("Mouse Y");
        moveFB = Input.GetAxis("Vertical");
        moveLR = Input.GetAxis("Horizontal");
        //transform.eulerAngles = new Vector3(yValue, xValue, 0);

        //get keyboard to move camera position
        //Vector3 move = Input.GetAxis("Horizontal") * transform.right;
        //move = Input.GetAxis("Vertical") * transform.forward;
        transform.Rotate(0, xValue * sensitivity, 0);
        Vector3 movement = new Vector3(moveLR * speed * Time.deltaTime, 0,
            moveFB * speed * Time.deltaTime);
        controller.Move(transform.rotation * movement);
        eye.transform.Rotate(-yValue * sensitivity,0,0);
        
    }
}
