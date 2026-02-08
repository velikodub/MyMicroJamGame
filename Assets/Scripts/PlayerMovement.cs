using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 50f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float fallMultiplier = 3f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;

    [Header("Feel")]
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float minFallSpeedForSound = 1f;

    [Header("Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 checkBoxSize = new Vector2(0.8f, 0.2f);
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool wasGrounded;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float lastVerticalVelocity;

    private bool isFacingRight = true;
    private RobotSounds sounds;
    private EchoPulse pulse;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sounds = GetComponent<RobotSounds>();
        pulse = GetComponent<EchoPulse>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sounds.PlayScan();
            pulse.EmitWave();
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(horizontalInput < 0 & isFacingRight)
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapBox(groundCheck.position, checkBoxSize, 0f, groundLayer);
        if (isGrounded && !wasGrounded)
        {
            if(lastVerticalVelocity < -minFallSpeedForSound)
            {
                sounds.PlayLand();
            }
            coyoteTimeCounter = coyoteTime;
        }
        wasGrounded = isGrounded;
        if (!isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            ExecuteJump();
        }

        if (Mathf.Abs(horizontalInput) > 0.01f && isGrounded)
        {
            sounds.SetMoving(true);
        }
        else
        {
            sounds.SetMoving(false);
        }
    }
    private void FixedUpdate()
    {
        lastVerticalVelocity = rb.linearVelocity.y;
        ApplyMovement();
        ApplyBetterFall();
    }
    private void ApplyMovement()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRat = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = speedDif * accelRat;

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    private void ExecuteJump()
    {
        sounds.PlayJump();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }
    private void ApplyBetterFall()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if(rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaler = transform.localScale;

        scaler.x *= -1;

        transform.localScale = scaler;
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, checkBoxSize);
        }
    }
}
