using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	public GameObject spawnPoint;
	public GameObject[] enemies;
	
	public int maxEnemiesOnScreen;
	public int enemiesPerWave;
	public int enemiesPerSpawn;

    public Transform exitPoint;
	public Transform[] wayPoints;

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
}
