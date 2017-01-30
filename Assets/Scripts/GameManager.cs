using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus {
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    private int totalWaves = 10;
    [SerializeField]
    private Text totalMoneyLbl;
    [SerializeField]
    private Text currentWaveLbl;
    [SerializeField]
    private Text TotalEscapedLbl;

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
    [SerializeField]
    private Text playBtnLbl;
    [SerializeField]
    private Button playBtn;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKill = 0;
    private int whichEnemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;

    private const float spawnDelay = 0.6f;

    public List<Enemy> EnemyList = new List<Enemy>();

    public int TotalMoney {
        get {
            return totalMoney;
        }
        set {
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

	void Start () {
        playBtn.gameObject.SetActive(false);
        ShowMenu();
	}

    void Update() {
        handleEScape();
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

    public void AddMoney(int amount) {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount) {
        TotalMoney -= amount;
    }

    public void ShowMenu() {
        switch(currentState) {
            case gameStatus.gameover:
                playBtnLbl.text = "Play Again!";
                break;
            case gameStatus.next:
                playBtnLbl.text = "Next Wave";
                break;
            case gameStatus.play:
                playBtnLbl.text = "Play";
                break;
            case gameStatus.win:
                playBtnLbl.text = "Play";
                break;
        }

        playBtn.gameObject.SetActive(true);
    }

    private void handleEScape() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TowerManager.Instance.disableDragSprite();
            TowerManager.Instance.TowerBtnPressed = null;
        }
    }
}
