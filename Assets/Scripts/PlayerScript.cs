using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public Text winText;
    public Text loseText;
    public Text lives;
    private int livesValue = 3;
    Animator anim;
    private bool facingRight = true;




    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();


        winText.text = "";
        loseText.text = "";

        musicSource.clip = musicClipOne;
        musicSource.Play();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
         {
             Flip();
        }
    }
    
    void Update()

  {     if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

          if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

           if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
         if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Jump", true);
        }
         if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Jump", false);
        }
  }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
            transform.position = new Vector2(34.34f, 0.0f);
            livesValue = 3;
            lives.text = livesValue.ToString();
            }
        }
        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Cooper Wilson!";

            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            musicSource.volume = 0.2f;
            Destroy (this);

        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (livesValue <= 0)
        {
            loseText.text = "You lose!";
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

}
