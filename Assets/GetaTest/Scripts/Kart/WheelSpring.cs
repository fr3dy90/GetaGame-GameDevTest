using System;
using UnityEngine;

public class WheelSpring : MonoBehaviour
{
    [Header("Car RigidBody")] 
    private Rigidbody rb;

    [Header("Suspension Physics")] public float distance = .8f;
    public LayerMask groundMask;
    private float force;

    private float lastHitDist;
    public float strength;
    public float dampening;

    [Header("Car Settings")] 
    public float maxSpeed;
    private float actualSpeed;
    public float driveForce;
    public float brakeForce;
    public float turnForce;
    private float drag;
    public float grip;

    private float h, v;

    [Header("Release Player")] 
    public bool isPlay = false;


    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        drag = driveForce / maxSpeed;
        isPlay = false;
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    public void FixedUpdate()
    {
        actualSpeed = Vector3.Dot(rb.velocity, transform.forward);
        bool isGrounded = (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, distance, groundMask));
        
        if (isGrounded)
        {
            force = HooksLawDampen(hit.distance);
            rb.AddForceAtPosition(transform.up * force, rb.centerOfMass);
        }
        else
        {
            lastHitDist = distance * 1f;
        }

        float sideWaySpeed = Vector3.Dot(rb.velocity, transform.right);
        Vector3 sidewayFriction = -transform.right * (sideWaySpeed / Time.deltaTime);
        rb.AddForce(sidewayFriction*grip, ForceMode.Acceleration);

        if (isPlay)
        {
            TurnCar(h);
            if (isGrounded)
            {
                AccelerateCar(v);
            }
        }
    }

    float HooksLawDampen(float hitDistance)
    {
        float forceAmmount = strength * (distance - hitDistance) + (dampening * (lastHitDist - hitDistance));
        forceAmmount = Mathf.Max(0f, forceAmmount);
        lastHitDist = hitDistance;
        return forceAmmount;
    }

    void TurnCar(float inputForce)
    {
        float rotationTorque = turnForce * inputForce - rb.angularVelocity.y;
        rb.AddRelativeTorque(0f,rotationTorque, 0f, ForceMode.VelocityChange);
    }

    void AccelerateCar(float inputForce)
    {
        float carForce = driveForce * inputForce - drag * Mathf.Clamp(actualSpeed, 0f, maxSpeed);
        rb.AddForce(transform.forward * carForce, ForceMode.Acceleration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.up * distance);
    }
}