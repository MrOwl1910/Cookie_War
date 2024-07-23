using UnityEngine;
public class Gun : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject muzzle;
    [SerializeField] Transform muzzleposition;
    [SerializeField] GameObject projectile;

    [Header("config")]
    [SerializeField] float fireDistance ;
    [SerializeField] float fireRate = 0.3f;

    Transform Player;
    Vector2 offset;

    private float timesinceLastShot =0f;
    Transform closestenemy;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timesinceLastShot = fireRate;
        Player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        transform.position = (Vector2)Player.position + offset;

        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }
    void FindClosestEnemy()
    {
        closestenemy = null;
        float closestDistance = Mathf.Infinity;

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= fireDistance)
            {
                closestDistance = distance;
                closestenemy = enemy.transform;
            }
        }
    }
    void AimAtEnemy()
    {
        if (closestenemy != null)
        {
            Vector3 direction = closestenemy.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = (Vector2)Player.position + offset;
            Debug.Log("enemy");
        }
       
    }
    void Shooting()
    {
        if (closestenemy == null) return;

        timesinceLastShot += Time.deltaTime;
        if(timesinceLastShot >= fireRate)
        {
            Shoot();
            timesinceLastShot = 0;
        }  
    }
    void Shoot()
    {
        var muzzleGo = Instantiate(muzzle, muzzleposition.position, transform.rotation);
        muzzleGo.transform.SetParent(transform);
        Destroy(muzzleGo, 0.05f);

        var projectileGo = Instantiate(projectile, muzzleposition.position, transform.rotation);
        Destroy(projectileGo, 3f);
        Debug.Log("Shoot");
    }
    public void setOffset(Vector2 o)
    {
        offset = o;

    }
}//class