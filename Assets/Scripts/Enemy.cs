using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] int maxhealth = 50;
    [SerializeField] float speed = 2f;

    private int currenthealth;

    [Header("Charger")]
    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge = 5f;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;

    bool isCharging = false;
    bool isPreparingCharge = false;

    Animator anim;
    Transform target;
    void Start()
    {
        currenthealth = maxhealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();    
    }
    void Update()
    {
        if (!WaveManager.Instance.waveRunning()) return;
        if (isPreparingCharge) return;

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerTotheright = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerTotheright ? -1 : 1, 1);

            if(isCharger && !isCharging && Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                isPreparingCharge=true;
                Invoke("StartCharging", prepareTime);
            }
        }  
    }

    void StartCharging()
    {
        isPreparingCharge = false;
        isCharging=true;
        speed = chargeSpeed;
    }
    public void Hit(int damage)
    {
        currenthealth -= damage;
        anim.SetTrigger("Hit");

        if (currenthealth <= 0)
            Destroy(gameObject);
    }
}//class