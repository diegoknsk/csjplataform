using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [Header("Atributtes")]
    public float speed;
    public float jumpForce;
    public int life;
    public int apple;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    private Vector2 direction;
    private bool isGrounded;
    public SpriteRenderer sprite;
    private bool recovery;

    [Header("UI")]
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI lifeText;
    public GameObject gameOver;



    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = life.ToString();
        DontDestroyOnLoad(gameObject);
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
        if (life <= 0) 
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
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
            DecreaseLife();
            StartCoroutine(Flick());
            Death();
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
     
        recovery = false;
    }

    private void DecreaseLife()
    {
        life--;
        lifeText.text = life.ToString();
    }

    public void IncreaseScore() 
    {
        apple++;
        appleText.text = apple.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
