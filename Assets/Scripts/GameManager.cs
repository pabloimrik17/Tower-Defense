using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

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

    private void Awake() {
        CheckSingleton();
    }

    void CheckSingleton() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // PERSISTE ENTRE ESCENAS
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
