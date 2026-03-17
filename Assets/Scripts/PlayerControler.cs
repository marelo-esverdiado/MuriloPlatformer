using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool isGrounded = true;
    public float jumpForce = 500;
    
    public int coinsCollected = 0;
    public int health =  3;
    public int metal = 0;
    
    public GameObject bullet;
    
    public TMP_Text coinsCollectedText;
    public TMP_Text healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coinsCollectedText.text = coinsCollected.ToString();
        healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed);
        }
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }    
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(bullet, transform.position + Vector3.up * 3, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce( Vector2.right * (jumpForce * 10));
			
        }    
        

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //other.GetComponent<Collectable>())  <- only works for a certain script
        if (other.GetComponent<Collectable>())
        {
            coinsCollected++;
            coinsCollectedText.text = coinsCollected.ToString();
        }
        else if (other.GetComponent<Colleliron>())
        {
            metal = metal + (5);
        }

        if (other.GetComponent<Colledeath>())
        {
            health--;
            healthText.text = health.ToString(); 
            
                if (health <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
        else if (other.GetComponent<Goall>())
        {
            SceneManager.LoadScene(other.GetComponent<Goall>().NextLevel);
        }    

            
        }    
        
    }
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    
}
