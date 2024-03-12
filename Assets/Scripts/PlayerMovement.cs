
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    //https://www.youtube.com/watch?v=TcranVQUQ5U used this tutorial for doing the movement of the player


    private void Awake()
    {
        // grab references for rigidbody and animator object 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //flip player sprite 
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            
            
        }
        else if (horizontalInput < -0.01f)
        {
             transform.localScale = new Vector3(-1, 1, 1);
            
        }



        // wall jump code
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            if (OnWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 1;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }


        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        // print(OnWall());
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("Jump");
        }
        else if (OnWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
            {

                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 7);
                wallJumpCooldown = 0;
            }



        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "Ground")
        // {

        // }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !OnWall();
    }

}
