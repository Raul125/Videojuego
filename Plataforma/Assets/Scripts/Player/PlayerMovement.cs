using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float rollForce = 6.0f;
    [SerializeField] private GameObject slideDust;

    private Animator animator;
    private Rigidbody2D body2d;
    private SpriteRenderer spriteRenderer;

    private CollideSensor groundSensor;
    private CollideSensor wallSensorR1;
    private CollideSensor wallSensorR2;
    private CollideSensor wallSensorL1;
    private CollideSensor wallSensorL2;

    private bool isWallSliding = false;
    private bool grounded = false;
    public bool Rolling = false;

    public int FacingDirection { get; private set; } = 1;

    private float delayToIdle = 0.0f;
    private float rollCurrentTime = 0f;
    private readonly float rollDuration = 8.0f / 14.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        groundSensor = transform.Find("GroundSensor").GetComponent<CollideSensor>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<CollideSensor>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<CollideSensor>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<CollideSensor>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<CollideSensor>();
    }

    private void Update()
    {
        UpdateTimers();
        CheckGroundStatus();
        HandleInputs();

        HandleMovement();
        HandleWallSliding();
    }

    private void UpdateTimers()
    {
        if (Rolling)
        {
            rollCurrentTime += Time.deltaTime;
        }

        if (rollCurrentTime > rollDuration)
        {
            Rolling = false;
        }
    }

    private void CheckGroundStatus()
    {
        bool sensorState = groundSensor.State();
        if (!grounded && sensorState)
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
        else if (grounded && !sensorState)
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
    }

    private void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        if (inputX != 0)
        {
            spriteRenderer.flipX = inputX < 0;
            FacingDirection = inputX < 0 ? -1 : 1;
        }

        if (!Rolling)
        {
            body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);
        }

        animator.SetFloat("AirSpeedY", body2d.velocity.y);
    }

    private void HandleWallSliding()
    {
        isWallSliding = (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State());
        animator.SetBool("WallSlide", isWallSliding);
    }

    private void HandleInputs()
    {
        if (Input.GetButtonDown("Jump") && grounded && !Rolling)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Fire3") && !Rolling && !isWallSliding)
        {
            StartRolling();
        }
        else
        {
            UpdateIdleState();
        }
    }

    private void StartRolling()
    {
        Rolling = true;
        animator.SetTrigger("Roll");
        body2d.velocity = new Vector2(FacingDirection * rollForce, body2d.velocity.y);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        grounded = false;
        animator.SetBool("Grounded", grounded);
        body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
        groundSensor.Disable(0.2f);
    }

    private void UpdateIdleState()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
        {
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
    }

    private void AE_SlideDust()
    {
        Vector3 spawnPosition = FacingDirection == 1 ? wallSensorR2.transform.position : wallSensorL2.transform.position;
        if (slideDust != null)
        {
            GameObject dust = Instantiate(slideDust, spawnPosition, transform.localRotation);
            dust.transform.localScale = new Vector3(FacingDirection, 1, 1);
        }
    }
}
