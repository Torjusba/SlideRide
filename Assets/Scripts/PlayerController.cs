using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    private PlayerMotor Motor;

    [SerializeField]
    private float acceleration = 1f;

    [SerializeField]
    private float _JumpForce = 50f;

    private float xAxis;
    private float zAxis;
    private float yRotation;
    private float xRotation;

    void Start()
    {
        Motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            switch (PlayerPrefs.GetInt("Perspective"))
            {
                case 1:
                    PlayerPrefs.SetInt("Perspective", 3);
                    break;
                case 3:
                    PlayerPrefs.SetInt("Perspective", 1);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Motor.DeactivateFriction();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Motor.ActivateFriction();
        }

        //Boost gravity?
        Motor.BoostGravity = Input.GetButton("Boost Gravity");




        //Move player
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");
        Vector3 MovX = transform.right * xAxis;
        Vector3 MovY = transform.forward * zAxis;
        Vector3 mov = (MovX + MovY).normalized * acceleration;
        Motor.Move(mov);

        //Rotate around the y axis
        yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rot = new Vector3(0f, yRotation, 0f) * PlayerPrefs.GetFloat("LookSensitivity");

        Motor.Rotate(rot);

        //Rotate camera around the y axis
        xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRot = xRotation * PlayerPrefs.GetFloat("LookSensitivity");

        Motor.RotateCamera(cameraRot);


        //Jetpack
        float JumpForce = 0f;
        if (Input.GetButton("Jump"))
        {
            JumpForce = _JumpForce;
        }
        Motor.Jump(JumpForce);
    }
}
