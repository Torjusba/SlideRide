using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [Header("Jetpack")]

    [SerializeField]
    ParticleSystem[] emitters;

    public float maxFuel;
    public float fuel;
    public float fuelRefillRate;

    [Header("Physics")]

    [SerializeField]
    PhysicMaterial frictionMaterial;

    [SerializeField]
    PhysicMaterial noFrictionMaterial;

    [SerializeField]
    private float camLimit = 80f;

    [Header("Objects")]

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform head;

    public bool BoostGravity = false;

    [SerializeField]
    float GravityBoost = 200f;
    Vector3 gravityForce = Vector3.down * 9.81f;

    Vector3 movementForce = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    Vector3 jumpForce = Vector3.zero;

    float camRotationX = 0f;

    public float maxVelocity;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuel = maxFuel;
    }

    void FixedUpdate()
    {
        ApplyGravity();
        ApplyJetpack();
        PerformMovement();
        PerformRotation();
    }

    void Update()
    {
        if (jumpForce != Vector3.zero && fuel > 0)
        {
            //Generate jetpack particles
            foreach (ParticleSystem e in emitters)
            {
                e.Emit(100);
            }
        }
    }

    public void Move(Vector3 _movement)
    {
        movementForce = _movement;
    }

    void ApplyGravity()
    {
        //Boost gravity
        if (BoostGravity)
        {
            rb.AddForce(gravityForce * GravityBoost, ForceMode.Force);
        }
    }

    void ApplyJetpack()
    {
        if (jumpForce != Vector3.zero && fuel > 0)
        {
            //Generate force and draw fuel
            if (rb.velocity.y < maxVelocity)
            {
                rb.AddForce(jumpForce, ForceMode.Force); // Add jetpack force since we're below maximum velocity
            }
            else
            {
                rb.AddForce(-gravityForce, ForceMode.Force); // Add just enough to keep us at same velocity in air
            }
            fuel -= 2f;
        }
        else if (fuel <= maxFuel)
        {
            fuel += fuelRefillRate;
        }
    }

    void PerformMovement()
    {
        if (movementForce != Vector3.zero)
        {
            float remaingingVelocity = maxVelocity - rb.velocity.magnitude;
            if (remaingingVelocity >= 0)
            {
                // We can still accelerate, so lets do so
                float velocityMultiplier = remaingingVelocity / maxVelocity; //As velocity->maxVelocity the multiplier->0 and no force is added
                rb.AddForce(movementForce * velocityMultiplier, ForceMode.Force);
            }
            else // Max velocity
            {
                // We can't accelerate any more, but we should still be able to change direction

                // Calculate the difference in angle between the movement direction and current velocity direction
                Vector3 forward = rb.velocity;
                Vector3 direction = movementForce.normalized;
                float angleDiff = Mathf.Atan2(Vector3.Dot(Vector3.up, Vector3.Cross(forward, direction)), Vector3.Dot(forward, direction));

                // If we add force in the same way we're already traveling (at max velocity), this multiplier is zero.
                // Else, this goes gradually from 0 to 1 as the angle goes from forward to straight backwards. I think.
                float angleMultiplier = Mathf.Abs(angleDiff / Mathf.PI);

                rb.AddForce(movementForce * angleMultiplier, ForceMode.Force);
            }
        }
    }

    public void Rotate(Vector3 _rotation)
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
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
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
