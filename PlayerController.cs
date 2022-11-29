using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontalInput;
    private float speed = 10;
    private float jumpForce = 13;
    private bool facingRight = true;

    public bool canDoubleJump;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if(Input.GetKeyDown("space"))
        {
            if (isOnGround())
            {
                Jump();
                canDoubleJump = true;
            }
            else if(canDoubleJump == true)
            {
                Jump();
                canDoubleJump = false;
            }                                            
        }

        if(isOnGround() && !Input.GetKey("space"))
        {
            canDoubleJump = false;
        }

        ChangeLookDirection();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool isOnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
    }

    private void ChangeLookDirection()
    {
        if(facingRight && horizontalInput <0 || !facingRight && horizontalInput > 0)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
