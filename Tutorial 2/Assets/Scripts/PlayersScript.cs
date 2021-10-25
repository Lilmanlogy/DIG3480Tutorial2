using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersScript : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
    public Text countText;
    private int count;
    private int scoreValue = 0;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    private bool facingRight = true;
    public Text hozText;
    public Text jumpText;
    private int lifecount;
    public Text lifeText;
    public Text winText;
    private bool teleport;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();
        winText.text = "";

        rd2d = GetComponent <Rigidbody2D>();
        lifecount = 3;
        SetlifeText();
        winText.text = "";
        
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey("escape"))
        {
          Application.Quit();
        }
    }
    
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));  
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround); 
        
        if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }
        
        if (hozMovement > 0 && facingRight == true)
        {
            Debug.Log ("Facing Right");
            hozText.text = "Facing Right";
        }
        if (hozMovement < 0 && facingRight == false)
        {
            Debug.Log ("Facing Left");
            hozText.text = "Facing Left";
        }
        if (verMovement > 0 && isOnGround == false)
        {
            Debug.Log ("Jumping");
            jumpText.text = "Jumping";
        }
        else if (verMovement == 0 && isOnGround == true)
        {
            Debug.Log ("Not Jumping");
            jumpText.text = "Not Jumping";
        }
        void Flip()
        {
            facingRight = !facingRight;
            Vector2 Scaler = transform.localScale;
            Scaler.x = Scaler.x * -1;
            transform.localScale = Scaler;   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            lifecount = lifecount - 1;
            SetlifeText();
        }
    }

    
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
            }
        }


    }
    void SetlifeText()
    {
        lifeText.text = "Lives: " + lifecount.ToString();
        if (lifecount <= 0)
        {
            winText.text = "You Lose! Game Created By Logan Weber";
            Destroy(Player);
        }
    }
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
         if(count == 4)
        {
            transform.position = new Vector3(60.0f, 0.5f, 0f);
            rd2d = GetComponent <Rigidbody2D>();
        lifecount = 3;
        SetlifeText();
        winText.text = "";
        }
        if (count == 8)
         {
             winText.text = "You Win! Game Created By Logan Weber";
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
        musicSource.Play();
         }

    }


}

