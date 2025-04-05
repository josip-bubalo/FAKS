using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float mouseSensitivity = 100f; // Osjetljivost misa

    private float rotationY = 0f; // Trenutna rotacija oko X osi (za vertikalnu rotaciju)

    void Start()
    {
        // Zakljucavanje kursora unutar prozora igre
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Uzimanje inputa misa
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotacija igraca lijevo-desno (horizontalna rotacija)
        transform.Rotate(0, mouseX, 0);

        // Ogranicavanje vertikalne rotacije kako bi se sprijecilo "preokretanje" kamere
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Ogranicenje na -90 do 90 stupnjeva

        // Primjena vertikalne rotacije (ako je kamera dijete igraca)
        if (Camera.main != null)
        {
            Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        }
    }
}
