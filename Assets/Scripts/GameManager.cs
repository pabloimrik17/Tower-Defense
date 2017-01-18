using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] // MUESTRA EN LA UI DE UNITY UNA VARIABLE PRIVADA
	private GameObject spawnPoint;
    [SerializeField]
	private GameObject[] enemies;
    [SerializeField]
	private int maxEnemiesOnScreen;
    [SerializeField]
	private int enemiesPerWave;
    [SerializeField]
	private int enemiesPerSpawn;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
	private Transform[] wayPoints;

	private int enemiesOnScreen = 0;

    private const float spawnDelay = 0.6f;

	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn() {
        if(enemiesPerSpawn > 0 && enemiesOnScreen < enemiesPerWave) {
            for(int i = 0; i < enemiesPerSpawn; i++) {
                if(enemiesOnScreen < maxEnemiesOnScreen) {
                    GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen++;
                }
            }
             yield return new WaitForSeconds(spawnDelay);
             StartCoroutine(Spawn());
        }
       
    }

    public void removeEnemyFromScreen() {
        if(enemiesOnScreen > 0) {
            enemiesOnScreen--;
        }
    }

    public Transform[] WayPoints {
        get{
            return this.wayPoints;
        }
    }

    public Transform ExitPoint {
        get{
            return this.exitPoint;
        }
    }
}
