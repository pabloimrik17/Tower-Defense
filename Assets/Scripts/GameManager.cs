using System.Collections;
using System.Collections.Generic;
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

    private const float spawnDelay = 0.6f;

    public List<Enemy> EnemyList = new List<Enemy>();

	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn() {
        if(enemiesPerSpawn > 0 && EnemyList.Count < enemiesPerWave) {
            for(int i = 0; i < enemiesPerSpawn; i++) {
                if(EnemyList.Count < maxEnemiesOnScreen) {
                    GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
             yield return new WaitForSeconds(spawnDelay);
             StartCoroutine(Spawn());
        }
       
    }

    public void RegisterEnemy(Enemy enemy) {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy) {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies() {
        foreach(Enemy enemy in EnemyList) {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
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
