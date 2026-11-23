using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveLevel=1;
    public float waveRate=1.5f;

    public Transform spawnPoint;
    public int liveEnemy;
    public GameObject enemyPrefab;


    void Start()
    {
        liveEnemy = 0;
        waveStart();
    }

    // Update is called once per frame
    void Update()
    {
        int enemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemy == 0)
        {
            waveLevel++;
            waveStart();
        }
        
    }

    void waveStart()
    {
        int spawnEnemy = waveLevel * 2;
        for (int i = 0; i < spawnEnemy; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(enemyPrefab,transform.position,Quaternion.identity);
    }

}
