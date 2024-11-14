using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backLeftWheelCollider;

    public Transform frontRightWheelTransform;
    public Transform backRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backLeftWheelTransform;

    public Transform carCenterOfMassTransform;
    public Rigidbody rb;

    public float motorForce = 100f;
    public float steeringAngle = 30f;
    public float brakeForce = 1000f;

    float verticalInput;
    float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = carCenterOfMassTransform.localPosition;
    }

    void FixedUpdate()
    {
        GetInput();
        MotorForce();
        Steering();
        ApplyBrakes();
        UpdateWheels();
    }

    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Debug.Log("Vertical Input: " + verticalInput);
        Debug.Log("Horizontal Input: " + horizontalInput);
    }

    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontRightWheelCollider.brakeTorque = brakeForce;
            backRightWheelCollider.brakeTorque = brakeForce;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            backLeftWheelCollider.brakeTorque = brakeForce;

            frontRightWheelCollider.motorTorque = 0f;
            frontLeftWheelCollider.motorTorque = 0f;
        }
        else
        {
            frontRightWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            backLeftWheelCollider.brakeTorque = 0f;
        }
    }

    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }

    void Steering()
    {
        frontRightWheelCollider.steerAngle = steeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steeringAngle * horizontalInput;
    }

    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(backRightWheelCollider, backRightWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(backLeftWheelCollider, backLeftWheelTransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
