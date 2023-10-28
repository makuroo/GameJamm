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
            anim.SetBool("isRunningLeft", true);
        }

        bool isMoving = movement.magnitude > 0.1f;
        anim.SetBool("isRunning", isMoving);
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
