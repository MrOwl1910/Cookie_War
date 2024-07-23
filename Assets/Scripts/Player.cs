using TMPro;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HealthText;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField] float movespeed = 6;
    int maxHealth = 100;
    int currenthealth;

    bool dead = false;

    float moveHorizontal, moveVertical;
    Vector2 movement;

    int facingDirection = 1; // 1 = right , -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currenthealth = maxHealth;
        HealthText.text = maxHealth.ToString();
        
    }
    void Update()
    {
        if(dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        anim.SetFloat("velocity", movement.magnitude);

        if(movement.x != 0)
            facingDirection = movement.x > 0 ? 1 : -1;

        transform.localScale = new Vector2(facingDirection, 1);
    }
    private void FixedUpdate()
    {
        rb.velocity = movement * movespeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
            Hit(10);        
    }
    void Hit(int damage)
    {
        anim.SetTrigger("Hit");
        currenthealth -= damage; 
        HealthText.text = Mathf.Clamp(currenthealth, 0 , maxHealth).ToString();

        if(currenthealth <= 0 )
            Die(); 
    }
    void Die()
    {
        dead = true;
        GameManager.Instance.GameOver();
    }
}//class 