using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [Header("Jetpack")]

    [SerializeField]
    ParticleSystem[] emitters;

    public float maxFuel;
    public float fuel;

    [Header ("Physics")]

    [SerializeField]
    PhysicMaterial frictionMaterial;

    [SerializeField]
    PhysicMaterial noFrictionMaterial;
    
    [SerializeField]
    private float camLimit = 80f;
    
    [Header ("Objects")]

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform head;

    public bool BoostGravity = false;

    [SerializeField]
    float GravityBoost = 200f;

    Vector3 movement = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    float camRotationX = 0f;
    Vector3 jumpForce = Vector3.zero;

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        fuel = maxFuel;
	}

    void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
    }

    public void Move (Vector3 _movement)
    {
        movement = _movement * 1.5f;
    }

    void PerformMovement ()
    {
        if (movement != Vector3.zero)
        {
            rb.MovePosition(rb.position +movement * Time.fixedDeltaTime);
        }
        if (jumpForce != Vector3.zero && fuel > 0) {

            //Generate force and draw fuel
            rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            rb.AddForce(movement * 2, ForceMode.Force);
            fuel -= 2f;

            //Generate particles
            foreach (ParticleSystem e in emitters)
            {
                e.Emit(100);
            }
        } else if (fuel <= maxFuel)
        {
            fuel += 0.5f;
        }

        //Boost gravity
        if (BoostGravity)
        {
            rb.AddForce(-transform.up * GravityBoost);
        }
    }	

    public void Rotate (Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _camRotationX)
    {
        camRotationX = _camRotationX;
    }


    float currentCameraRotX;
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
        currentCameraRotX -= camRotationX;
        currentCameraRotX = Mathf.Clamp(currentCameraRotX, -camLimit, camLimit);
        head.transform.localEulerAngles = new Vector3(currentCameraRotX, 0f, 0f);
    }

    public void Jump(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
    }
    
    public void DeactivateFriction()
    {
        rb.drag = 0;
        gameObject.GetComponent<Collider>().material = noFrictionMaterial;
    }


    public void ActivateFriction()
    {
        rb.drag = 0;
        gameObject.GetComponent<Collider>().material = frictionMaterial;
    }


    //Die
    public void die()
    {
        GameManager.UnRegisterPlayer(gameObject.name);
        GameManager.respawn(gameObject);
        //Destroy(gameObject);
    }
}
