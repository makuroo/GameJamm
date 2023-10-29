using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private float lastRollTime = 0f;
    private bool isRolling = false;
    [SerializeField] private float rollCooldown = 1f;
    [SerializeField] private Rigidbody2D rb;
    public float movementSpeed = 5;
    [SerializeField] private Camera cam;
    private bool facingRight = true;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        float currentTime = Time.time;

        if (currentTime - lastRollTime >= rollCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
            {
                StartRoll();
                lastRollTime = currentTime;
            }
        }
        if ((movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }

        bool isMoving = movement.magnitude > 0.1f;
        anim.SetBool("isRunning", isMoving);
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            anim.SetBool("isRunningUp", false);
            anim.SetBool("isRunningDown", false);
            anim.SetBool("isRunningDiagonal", true);
        }
        else
        {
            if (Mathf.Abs(movement.x) > 0.1f || Mathf.Abs(movement.y) > 0.1f)
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {
                    anim.SetBool("isRunningUp", false);
                    anim.SetBool("isRunningDown", false);
                    anim.SetBool("isRunningDiagonal", false);

                    if (movement.x > 0)
                    {
                        anim.SetBool("isRunningRight", true);
                        anim.SetBool("isRunningLeft", false);
                    }
                    else
                    {
                        anim.SetBool("isRunningRight", false);
                        anim.SetBool("isRunningLeft", true);
                    }
                }
                else
                {
                    anim.SetBool("isRunningRight", false);
                    anim.SetBool("isRunningLeft", false);

                    if (movement.y > 0)
                    {
                        anim.SetBool("isRunningUp", true);
                        anim.SetBool("isRunningDown", false);
                    }
                    else
                    {
                        anim.SetBool("isRunningUp", false);
                        anim.SetBool("isRunningDown", true);
                    }
                }
            }
            else
            {
                anim.SetBool("isRunningUp", false);
                anim.SetBool("isRunningDown", false);
                anim.SetBool("isRunningRight", false);
                anim.SetBool("isRunningLeft", false);
                anim.SetBool("isRunningDiagonal", false);
            }
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void DashRoll()
    {
        movementSpeed += 15;
    }

    public void EndDashRoll()
    {
        movementSpeed -= 15;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void StartRoll()
    {
        anim.SetTrigger("isRolling");
        isRolling = true;
    }
}