using UnityEngine;

public class MotorController : MonoBehaviour
{
    [Header("Postavke vožnje")]
    public float acceleration = 300f;
    public float maxSpeed = 60f;
    public float brakeTorque = 1500f;
    public float motorTorque = 150f;
    public float turnSpeed = 2f;
    public float tiltAngle = 15f;

    [Header("Fizika i vizual")]
    public Vector3 centerOfMassOffset = new Vector3(0, -0.5f, 0);
    public Transform modelTransform; // Vizualni mesh motora
    public Transform frontWheelMesh;
    public Transform rearWheelMesh;

    [Header("Wheel Colliders")]
    public WheelCollider frontWheelCollider;
    public WheelCollider rearWheelCollider;

    [Header("Detekcija")]
    public string obstacleTag = "Obstacle";

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass += centerOfMassOffset;
    }

    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        // Motorna snaga na zadnji kotac
        rearWheelCollider.motorTorque = vInput * motorTorque;

        // Upravljanje – zakret prednjeg kotaca
        frontWheelCollider.steerAngle = hInput * turnSpeed;

        // Rucna kocnica
        if (Input.GetKey(KeyCode.Space))
        {
            rearWheelCollider.brakeTorque = brakeTorque;
        }
        else
        {
            rearWheelCollider.brakeTorque = 0f;
        }

        // Ogranicenje brzine
        if (rb.velocity.magnitude > maxSpeed)
        {
            rearWheelCollider.motorTorque = 0f;
        }

        UpdateWheelVisual(frontWheelCollider, frontWheelMesh);
        UpdateWheelVisual(rearWheelCollider, rearWheelMesh);
    }

    void Update()
    {
        // Naginjanje motora (vizualno)
        float tilt = Input.GetAxis("Horizontal") * -tiltAngle;
        modelTransform.localRotation = Quaternion.Euler(0f, 0f, tilt);
    }

    void UpdateWheelVisual(WheelCollider col, Transform mesh)
    {
        if (mesh == null) return;

        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            // Prevrtanje motora – simulacija
            Vector3 torque = transform.right * 500f + transform.forward * 300f;
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }
}
