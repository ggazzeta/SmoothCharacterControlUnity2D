using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class SmoothCharacterControl : MonoBehaviour
{
    private bool onfloor = false;
    private bool facingRight;
    private float JumpPressedTimer = .3f;
    private bool isJumping;
    private float jumpPressedTime;
    private float lastGroundedTime;
    private float Jumpforce = 15;
    private bool jumpConsumed;

    Rigidbody2D MyRigidbody;

    public MovementSettings movementSettings;
    public JumpSettings jumpSettings;
    private Vector2 currentVelocity = Vector2.zero;

    private void Awake()
    {
        MyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        MyRigidbody.gravityScale = 3.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        onfloor = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float XAxis = MyRigidbody.velocity.x;
        XAxis = Input.GetAxis("Horizontal");

        Movement(XAxis, movementSettings.acceleration);
        Rotate(facingRight);
    }

    private void ValidateJump()
    {
        jumpPressedTime -= Time.deltaTime;

        if (onfloor)
        {
            lastGroundedTime = Time.time;
            jumpConsumed = false;
        }

        if (Input.GetButtonDown("Jump") && !jumpConsumed)
        {
            jumpConsumed = true;
            jumpPressedTime = JumpPressedTimer;
        }

        if (!jumpConsumed && jumpPressedTime > 0 && (onfloor || Time.time - lastGroundedTime <= jumpSettings.CoyoteTime))
        {
            Jump();
            jumpPressedTime = 0;
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump") && isJumping)
        {
            if (MyRigidbody.velocity.y > 0)
            {
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, MyRigidbody.velocity.y * jumpSettings.MinJumpMultiplier);
            }
            isJumping = false;
        }
    }

    private void ApplyVariableJumpHeight()
    {
        if (MyRigidbody.velocity.y > 0)
        {
            if (!Input.GetButton("Jump"))
            {
                MyRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (jumpSettings.MinHeightToJumpShortClick - 1) * Time.deltaTime;
            }
        }
        else if (MyRigidbody.velocity.y < 0)
        {
            MyRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (jumpSettings.SpeedToFallOnceReachedMaxHeight - 1) * Time.deltaTime;
        }
    }

    private void Update()
    {
        ValidateJump();
        ApplyVariableJumpHeight();
    }

    IEnumerator WaitToLeave()
    {
        yield return new WaitForSeconds(.1f);
        onfloor = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onfloor = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(WaitToLeave());
        }
    }

    public void Movement(float direction, float speed)
    {
        float targetSpeed = direction * speed;
        float newX = Mathf.SmoothDamp(MyRigidbody.velocity.x, targetSpeed, ref currentVelocity.x, movementSettings.horizontalDamping);

        MyRigidbody.velocity = new Vector2(newX, MyRigidbody.velocity.y);

        if (newX > 0)
            facingRight = true;
        else if (newX < 0)
            facingRight = false;

    }

    private void Jump()
    {
        MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, Jumpforce);
    }

    public void Rotate(bool facingRight)
    {
        if (facingRight)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

    }

}

[System.Serializable]
public class MovementSettings
{
    [Tooltip("Time in milliseconds for the damping of the horizontal movement.")]
    [Range(0, 1f)]
    public float horizontalDamping = 0.1f;

    [Tooltip("Maximum movement speed.")]
    public float acceleration = 10f;
}


[System.Serializable]
public class JumpSettings
{
    [HideInInspector]
    public float MinJumpMultiplier = 0.5f;

    [Tooltip("Time in milliseconds that allows the player to jump (in milliseconds) before reaching the ground")]
    public float CoyoteTime = 0.15f;


    public float SpeedToFallOnceReachedMaxHeight = 2.5f;
    [Tooltip("Default jump height if player do not hold the jump button")]
    [Range(0, 10)]
    public float MinHeightToJumpShortClick = 5.0f;
}
