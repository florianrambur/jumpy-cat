using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Speed")]
    public float speed = 5f;
    public float countdown = 20f;

    [Header("Jump")]
    public bool isGrounded;
    [SerializeField] private float forceJump = 10f;
    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float fallingGravityScale = 8f;
    [SerializeField] private float buttonTime = 0.3f;
    [SerializeField] private float jumpTime;
    [SerializeField] private bool isJumping;

    [Header("Animation")]
    public Animator animator;
    public Animator speedTextAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.instance.state == EnumManager.GameState.START)
        {
            float step = speed * Time.deltaTime;

            transform.position = new Vector2(transform.position.x + step, transform.position.y);

            animator.SetBool("IsRunning", true);

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                SoundManager.StartJumpSFX();

                isJumping = true;
                jumpTime = 0;
            }

            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, forceJump);

                jumpTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTime)
            {
                isJumping = false;
            }

            if (rb.velocity.y >= 0)
            {
                rb.gravityScale = gravityScale;
            }

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallingGravityScale;
            }

            IncreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            speedTextAnimator.SetTrigger("SpeedUp");

            StartCoroutine(IncreaseSpeedCoroutine());

            countdown = 20f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    IEnumerator IncreaseSpeedCoroutine()
    {
        yield return new WaitForSeconds(1);

        float increasingTime = 0f;
        float targetedSpeed = speed + 2f;

        while (increasingTime < 2f && speed < targetedSpeed)
        {
            increasingTime += Time.deltaTime * 0.5f;

            speed += increasingTime;

            yield return null;
        }

        speed = Mathf.FloorToInt(speed);

        yield return null;
    }
}
