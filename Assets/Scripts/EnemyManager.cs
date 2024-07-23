
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject Charger;
    [SerializeField] GameObject tuf;
    [SerializeField] float TimeBetweenSpawns = 0.5f;

    float currentTimeBetwwenSpawns;

    Transform EnemiesParent;

    public static EnemyManager Instance;

    private void Awake()
    {
         if(Instance == null) Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemiesParent = GameObject.Find("Enemies").transform;  
    }
    void Update()
    {
       if(!WaveManager.Instance.waveRunning())  return;

        currentTimeBetwwenSpawns -= Time.deltaTime;

        if(currentTimeBetwwenSpawns <= 0 )
        {
            SpwanEnemy();
            currentTimeBetwwenSpawns = TimeBetweenSpawns;
        }   
    }
    Vector2 RandomPostion()
    {
        return new Vector2(Random.Range(-20, 20), Random.Range(-10, 10));
    }
    void SpwanEnemy()
    {
        var roll = Random.Range(0, 100);
        var enemytype = roll < 90 ? enemyPrefab : Charger;
           enemytype = roll <=60 ? enemyPrefab : tuf;

        var e = Instantiate(enemytype, RandomPostion(), Quaternion.identity);
        e.transform.SetParent(EnemiesParent);
    }
    public void DestroyAllEnemies()
    {
        foreach (Transform e in EnemiesParent)
            Destroy(e.gameObject);
    }
}//class