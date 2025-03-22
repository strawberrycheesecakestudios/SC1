using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake() //runs at the beginning when we run the script
    {
        //Grabs references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() //runs every frame
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 5);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && grounded)
            Jump();

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
