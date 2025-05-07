using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float acceleration = 10f;       // Brzina ubrzanja
    public float steering = 5f;           // Brzina skretanja
    public float lift = 5f;               // Pove?anje uzgona
    public float descentRate = 2f;        // Stopa pada
    public float rollSpeed = 1f;          // Brzina rolanja (bo?no okretanje)
    public float pitchSpeed = 2f;         // Brzina podizanja/spuštanja nosa aviona
    private float currentSpeed = 0f;      // Trenutna brzina aviona
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Preuzimanje Rigidbody komponente
    }

    void Update()
    {
        // Kontrole za ubrzanje, podizanje i padanje
        float throttle = Input.GetAxis("Vertical");  // W/S ili gore/dolje tipke
        float steer = Input.GetAxis("Horizontal");   // A/D ili lijevo/desno tipke
        float pitch = Input.GetAxis("Pitch");        // Za podizanje/spuštanje nosa (tipke Z/X ili tipke gore/dolje)
        float roll = Input.GetAxis("Roll");          // Za rolanje (tipke Q/E)

        // Ubrzanje aviona
        currentSpeed += throttle * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, 100f);  // Ograni?enje brzine

        // Skretanje
        transform.Rotate(0, steer * steering * Time.deltaTime, 0);  // Rotacija za skretanje

        // Podesite vertikalnu rotaciju za podizanje i spuštanje
        transform.Rotate(pitch * pitchSpeed * Time.deltaTime, 0, 0);  // Podizanje/spuštanje nosa

        // Rolanjem aviona kontroliramo bo?ne pomake
        transform.Rotate(0, 0, roll * rollSpeed * Time.deltaTime);

        // Dodavanje uzgona (lift) i padanja
        if (currentSpeed > 0)
        {
            rb.AddForce(Vector3.up * lift * currentSpeed, ForceMode.Force);  // Uzgon
        }
        else
        {
            rb.AddForce(Vector3.down * descentRate, ForceMode.Force);  // Padanje
        }

        // Kretanje aviona prema naprijed
        rb.AddForce(transform.forward * currentSpeed, ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Efekt sudara - Simulacija gubitka kontrole ili pada
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * 100f, ForceMode.Impulse);
        }
    }
}
