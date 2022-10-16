using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int life;
    public int apple;
    public Rigidbody2D rig;
    public Animator anim;
    private Vector2 direction;
    private bool isGrounded;
    public SpriteRenderer sprite;
    private bool recovery;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
        PlayAnim();
    }

    // é usado para fisica
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement() 
    {
        rig.velocity = new Vector2(direction.x * speed, rig.velocity.y);
    }

    void Jump() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            anim.SetInteger("transition", 2);
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Death() 
    {

    }

    void PlayAnim() 
    {
        if (direction.x > 0)
        {
            if(isGrounded)
                anim.SetInteger("transition", 1);
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0)
        {
            if (isGrounded)
                anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0, 180);
        }
        else 
        {
            if (isGrounded)
                anim.SetInteger("transition", 0);
        }
    }

    public void Hit() 
    {
        if (!recovery)
        {         
            StartCoroutine(Flick());
        }
    }

    IEnumerator Flick() 
    {
        recovery = true;
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        life--;
        recovery = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
