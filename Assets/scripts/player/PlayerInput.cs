using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Movement move;
    public WeaponManager weap;
    public float mouseXSensitivity = 30f;
    public float mouseYSensitivity = 30f;
    public float mouseSnappiness = 100f;
    private float mouseX;
    private float mouseY;
    private float xAccumulator;
    private float yAccumulator;
    void Awake()
    {
        move = this.GetComponent<Movement>();
        weap = this.GetComponent<WeaponManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 inputVector = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            inputVector += new Vector3(0,0,1);
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputVector += new Vector3(0,0,-1);
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputVector += new Vector3(-1,0,0);
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputVector += new Vector3(1,0,0);
        }
        move.inputDir = Vector3.Normalize(transform.TransformVector(inputVector));

        if(Input.GetKeyDown(KeyCode.Space) && !move.isJumping)
        {
            move.isJumping = true;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            weap.ChangeWeapons(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            weap.ChangeWeapons(false);
        }

        mouseX = Input.GetAxis("Mouse X");
        xAccumulator = Mathf.Lerp(xAccumulator, mouseX, mouseSnappiness * Time.deltaTime);
        transform.Rotate(0, xAccumulator, 0, Space.World);
 
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -90, 90);
        yAccumulator = Mathf.Lerp(yAccumulator, mouseY, mouseSnappiness * Time.deltaTime);
        Camera.main.transform.localEulerAngles = new Vector3(-yAccumulator, 0, 0);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            weap.PrimaryDown();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            weap.SecondaryDown();
        }
    }
}
