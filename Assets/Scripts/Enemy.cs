using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int target = 0;
	
	
	
	public float navigationUpdateTime;

	private Transform enemy;
	
	private float navigationTime = 0;
	// Use this for initialization
	void Start () {
		enemy = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnemyPosition();
	}

	void UpdateEnemyPosition() {
		if(GameManager.instance.wayPoints != null) {
			navigationTime += Time.deltaTime;
			if(navigationTime > navigationUpdateTime) {
				if(target < GameManager.instance.wayPoints.Length) {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.instance.wayPoints[target].position, navigationTime);
				} else {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.instance.exitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "CheckPoint") {
			target++;
		} else if(collider.tag == "Finish") {
			Destroy(gameObject);
			GameManager.instance.removeEnemyFromScreen();
		}
	}

}