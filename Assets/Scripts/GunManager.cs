using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject GunPrefab;

    Transform player;
    List<Vector2> gunpositions = new List<Vector2>();

    int spawnedgun = 0;

    public static GunManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;

        gunpositions.Add(new Vector2(-1.5f, -0.15f));
        gunpositions.Add(new Vector2(1.5f, 0.15f));

        gunpositions.Add(new Vector2(-1.5f, 0f));
        gunpositions.Add(new Vector2(1.5f, 0f));

        gunpositions.Add(new Vector2(-1.5f, -0.45f));
        gunpositions.Add(new Vector2(1.5f, 0.45f));

        AddGun();

    }
     public void AddGun()
    {
        var pos = gunpositions[spawnedgun];
        var newGun = Instantiate(GunPrefab, pos, Quaternion.identity);

        newGun.GetComponent<Gun>().setOffset(pos);
        spawnedgun++;
    }
}