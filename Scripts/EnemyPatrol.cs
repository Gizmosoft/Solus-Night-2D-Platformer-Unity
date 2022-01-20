using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]     // This attached a Rigidbody2d component to any object running this script if its not added before
public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody;
    public float moveSpeed = 200;
    public bool mustFlip;

    public Transform groundCheckPos;
    public LayerMask groundLayer;

    private void Update()
    {
        Patrol();
    }
    private void Start()
    {
        
    }

    private void FixedUpdate()      // modifying physics related things here
    {
        // returns true if overlapcircle contains ground else returns False
        mustFlip = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
    }

    void Patrol()
    {
        if (!mustFlip)
        {
            Flip();
        }

        enemyRigidBody.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
    }

    void Flip()
    {
        // multiplying x component of local scale with -1 to flip it
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        moveSpeed *= -1;
    }


}
