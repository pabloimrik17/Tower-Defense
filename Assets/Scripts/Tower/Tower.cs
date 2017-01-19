using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField]
	private float shootCooldown;
	private float lastAttackTime;
	[SerializeField]
	private float attackRadius;
	
	private Projectile projectile;
	private Enemy targetEnemy = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private List<Enemy> GetAllEnemiesInRange() {
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList) {
			if(Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius) {
				enemiesInRange.Add(enemy);
			}
		}

		return enemiesInRange;
	}

	private Enemy GetNearestEnemyInRange() {
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;

		foreach(Enemy enemy in GetAllEnemiesInRange()) {
			float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
			if(currentDistance < smallestDistance) {
				nearestEnemy = enemy;
				smallestDistance = currentDistance;
			}
		}

		return nearestEnemy;
	}
}
