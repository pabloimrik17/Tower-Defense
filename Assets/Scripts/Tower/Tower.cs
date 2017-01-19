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
		attackRadius -= Time.deltaTime;
		if(targetEnemy == null) {
			SetTargetEnemy();
		} else if(Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius) { //TODO ESTO NO ES ASI ORIGINALMENTE, POR SI CAUSA BUG
			ClearTargetEney();
		}
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

	private void SetTargetEnemy() {
		Enemy nearestEney = GetNearestEnemyInRange();
		if(nearestEney != null && Vector2.Distance(transform.position, nearestEney.transform.position) <= attackRadius) {
			targetEnemy = nearestEney;
		}
	}

	private void ClearTargetEney() {
		targetEnemy = null;
	}

	public void Attack() {
		if(targetEnemy != null) {
			Projectile newProjectile = Instantiate(projectile) as Projectile;
		}
	}
}
