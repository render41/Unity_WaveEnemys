using UnityEngine;

#region Waves Class
[System.Serializable]
public class Wave
{
    public string waveName;
    public int numberOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}
#endregion

public class WaveSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    private Wave currentWave;
    private int checkWaveNumber;
    private float callNextSpawnTime;

    private bool canSpawn = true;
    #endregion

    #region Mono
    private void Update()
    {
        this.currentWave = this.waves[this.checkWaveNumber];
        SpawnWave();
        GameObject[] totalEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemys.Length == 0 && !this.canSpawn && this.checkWaveNumber + 1 != this.waves.Length)
        {
            SpawnNextWave();
        }
    }
    #endregion

    #region Spawn Next Wave
    private void SpawnNextWave()
    {
        this.checkWaveNumber++;
        this.canSpawn = true;
    }
    #endregion

    #region Spawn Wave
    private void SpawnWave()
    {
        if (this.canSpawn && this.callNextSpawnTime < Time.time)
        {
            GameObject randomEnemy = this.currentWave.typeOfEnemies[Random.Range(0, this.currentWave.typeOfEnemies.Length)];
            Transform randomPoint = this.spawnPoints[Random.Range(0, this.spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            this.callNextSpawnTime = Time.time + this.currentWave.spawnInterval;

            this.currentWave.numberOfEnemies--;
            if (this.currentWave.numberOfEnemies == 0)
            {
                this.canSpawn = false;
            }
        }
    }
    #endregion
}
