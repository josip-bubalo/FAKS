using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Postavke kretanja")]
    public float moveSpeed = 5f;
    public float jumpForce = 0.5f;
    public float crouchSpeed = 2f;

    private bool isCrouching = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Rigidbody: ", rb);
    }

    void Update()
    {
        // Kretanje WASD u smjeru igrača
        HandleMovement();

        // Skakanje
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Čučanje
        HandleCrouch();
        UpdatePlayerState();
    }

    void HandleMovement()
    {
        // Dohvati input za kretanje (WASD ili strelice)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Kreiraj vektor kretanja u lokalnim koordinatama
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Transformiraj vektor kretanja u globalne koordinate prema trenutnoj rotaciji igrača
        movement = transform.TransformDirection(movement);

        // Primijeni brzinu na Rigidbody
        if (isCrouching)
        {
            rb.velocity = new Vector3(movement.x * crouchSpeed, rb.velocity.y, movement.z * crouchSpeed);
        }
        else
        {
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouching = false;
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private string currentState = "Stoji";

    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude;
    }

    public string GetPlayerState()
    {
        return currentState;
    }




    void UpdatePlayerState()
    {
        if (!IsGrounded())
        {
            currentState = "Skače";
        }
        else if (isCrouching)
        {
            currentState = "Čuči";
        }
        else if (rb.velocity.magnitude > moveSpeed * 0.9f)
        {
            currentState = "Trči";
        }
        else if (rb.velocity.magnitude > 0.1f)
        {
            currentState = "Hoda";
        }
        else
        {
            currentState = "Stoji";
        }
    }
}
