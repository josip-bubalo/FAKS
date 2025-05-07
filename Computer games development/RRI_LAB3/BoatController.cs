using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float acceleration = 5f;        // Brzina ubrzanja
    public float steering = 2f;           // Brzina skretanja
    public float brakeForce = 3f;         // Otpornost prilikom kocenja
    public float drag = 2f;               // Sila otpora u vodi
    public float waveAmplitude = 0.5f;    // Amplituda valova
    public float waveSpeed = 0.3f;        // Brzina valova
    private float currentSpeed = 0f;      // Trenutna brzina camca
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Preuzimanje Rigidbody komponente
        rb.drag = drag;                 // Postavljanje otpora
    }

    void Update()
    {
        // Kontrola za ubrzanje i kocenje
        float throttle = Input.GetAxis("Vertical"); // Klasicne tipke W/S ili gore/dolje
        float steer = Input.GetAxis("Horizontal");  // Klasicne tipke A/D ili lijevo/desno

        // Ubrzanje
        currentSpeed += throttle * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, 10f); // Ogranicavanje brzine

        // Skretanje
        transform.Rotate(0, steer * steering * Time.deltaTime, 0); // Rotacija za skretanje

        // Primjena sile na kretanje camca
        rb.AddForce(transform.forward * currentSpeed, ForceMode.Force);

        // Simulacija valova - podizanje i spustanje camca
        float waveHeight = Mathf.Sin(Time.time * waveSpeed) * waveAmplitude;
        transform.position = new Vector3(transform.position.x, waveHeight, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Efekt sudara - Odbijanje camca (simulacija sudara s preprekom)
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * brakeForce, ForceMode.Impulse);
        }
    }
}
