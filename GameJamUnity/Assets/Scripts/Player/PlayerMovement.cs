using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private float lastRollTime = 0f;
    public bool isRolling = false;
    [SerializeField] private float rollCooldown = 0f;
    [SerializeField] private Rigidbody2D rb;
    public float movementSpeed = 5;
    [SerializeField] private Camera cam;
    private ItemStatus item;
    private bool facingRight = true;
    public AudioSource jalan;
    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        anim = GetComponent<Animator>();
        item = GetComponent<ItemStatus>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        float currentTime = Time.time;

        if (currentTime - lastRollTime >= rollCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isRolling && !item.holdingBall)
            {
                StartRoll();
                lastRollTime = currentTime;
                isRolling = false;
            }
        }
        if ((movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }

        bool isMoving = movement.magnitude > 0.1f;
        anim.SetBool("isRunning", isMoving);

        bool isPressedA = Input.GetKey(KeyCode.A);
        bool isPressedD = Input.GetKey(KeyCode.D);
        bool isPressedW = Input.GetKey(KeyCode.W);
        bool isPressedS = Input.GetKey(KeyCode.S);

        anim.SetBool("isRunningUp", isPressedW && !isPressedA && !isPressedD);
        anim.SetBool("isRunningDown", isPressedS && !isPressedA && !isPressedD);
        anim.SetBool("isRunningDiagonal", isPressedW && (isPressedA || isPressedD));
        anim.SetBool("isRunningDiagonalDown", isPressedS && (isPressedA || isPressedD));

        if (!isPressedA && !isPressedD && !isPressedW && !isPressedS)
        {
            anim.SetBool("isRunningUp", false);
            anim.SetBool("isRunningDown", false);
            anim.SetBool("isRunningDiagonal", false);
            anim.SetBool("isRunningDiagonalDown", false);

            if (isMoving)
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {
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
        movementSpeed += 3;
    }

    public void EndDashRoll()
    {
        movementSpeed -= 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void walksound()
    {
        jalan.Play();
    }

    private void StartRoll()
    {
        anim.SetTrigger("isRolling");
        isRolling = true;
    }
}