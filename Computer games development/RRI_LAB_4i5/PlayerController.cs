using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public AudioSource walkAudio;
    public AudioSource runAudio;
    public AudioSource jumpAudio;

    public ParticleSystem dustEffect;


    private bool isPlayingWalkSound = false;
    private bool isPlayingRunSound = false;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    private float currentSpeed => Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Unos
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(moveX, 0, moveZ).normalized;

        // Ako se igrac krece
        if (moveInput.magnitude >= 0.1f)
        {
            // Dohvati smjer kamere
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Okreni igraca
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // Pomak u tom smjeru
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        // Detekcija naglog prestanka trcanja
        bool isRunningNow = Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude >= 0.1f;
        Debug.Log("is playing run sound" + isPlayingRunSound);
        Debug.Log("is running now" + isRunningNow);
        if (isPlayingRunSound && !isRunningNow)
        {
            Debug.Log("play particle");
            // Pokreni prasinu kad trcanje prestane
            if (dustEffect != null)
            {
                Debug.Log("should play particle");
                dustEffect.Play();
            }
        }


        // Animator brzina
        float speed = new Vector2(moveX, moveZ).magnitude;
        float animatorSpeed = speed * (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f);
        animator.SetFloat("Speed", animatorSpeed);
        // Zvuk koraka
        if (isGrounded && moveInput.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isPlayingRunSound)
                {
                    runAudio.loop = true;
                    runAudio.Play();
                    walkAudio.Stop();
                    isPlayingRunSound = true;
                    isPlayingWalkSound = false;
                }
            }
            else
            {
                if (!isPlayingWalkSound)
                {
                    walkAudio.loop = true;
                    walkAudio.Play();
                    runAudio.Stop();
                    isPlayingWalkSound = true;
                    isPlayingRunSound = false;
                }
            }
        }
        else
        {
            if (walkAudio.isPlaying) walkAudio.Stop();
            if (runAudio.isPlaying) runAudio.Stop();
            isPlayingWalkSound = false;
            isPlayingRunSound = false;
        }


        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            StartCoroutine(TriggerJump());
        }

        IEnumerator TriggerJump()
        {
            animator.SetBool("IsJumping", true);
            jumpAudio.Play();
            yield return new WaitForSeconds(0.6f); // trajanje Jump animacije
            animator.SetBool("IsJumping", false);
        }


        // Gravitacija
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Debug
        Debug.Log("Speed: " + animatorSpeed + " | IsJumping: " + animator.GetBool("IsJumping"));
    }

}
