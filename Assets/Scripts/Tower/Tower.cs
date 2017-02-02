using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	[SerializeField]
	private float timeBetweenAttacks;
	private float attackCounter;
	[SerializeField]
	private float attackRadius;

	[SerializeField]
	private Projectile projectile;
	private Enemy targetEnemy = null;

	private bool isAttacking = false;

	private const float _SHOOT_PRECISION_ = 0.20f;
	private const float _SHOOT_SPEED_ = 5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		attackCounter -= Time.deltaTime;
		if(targetEnemy == null || targetEnemy.IsDead) {
			SetTargetEnemy();
		} else {
			if (attackCounter <= 0) {
				isAttacking = true;
				attackCounter = timeBetweenAttacks;
			} else {
				isAttacking = false;
			}

			if (Vector2.Distance (transform.localPosition, targetEnemy.transform.localPosition) > attackRadius) { //TODO ESTO NO ES ASI ORIGINALMENTE, POR SI CAUSA BUG
				ClearTargetEnemy ();
			}
		}


	}

	void FixedUpdate() {
		if (isAttacking) {
			Attack ();
		}
	}

	private List<Enemy> GetAllEnemiesInRange() {
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList) {
			if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius) {
				enemiesInRange.Add(enemy);
			}
		}

		return enemiesInRange;
	}

	private Enemy GetNearestEnemyInRange() {
		Enemy nearestEnemy = null;
		float smallestDistance = float.PositiveInfinity;

		foreach(Enemy enemy in GetAllEnemiesInRange()) {
			float currentDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
			if(currentDistance < smallestDistance) {
				nearestEnemy = enemy;
				smallestDistance = currentDistance;
			}
		}

		return nearestEnemy;
	}

	private void SetTargetEnemy() {
		Enemy nearestEney = GetNearestEnemyInRange();
		if(nearestEney != null && Vector2.Distance(transform.localPosition, nearestEney.transform.localPosition) <= attackRadius) {
			targetEnemy = nearestEney;
		}
	}

	private void ClearTargetEnemy() {
		targetEnemy = null;
	}

	public void Attack() {
		isAttacking = false;
		if (targetEnemy != null) {
			Projectile newProjectile = Instantiate (projectile) as Projectile;	
			projectile.transform.localPosition = transform.localPosition;
			switch (projectile.ProjectileType) {
				case projectileType.arrow:
					GameManager.Instance.AudioSource.PlayOneShot (SoundManager.Instance.Arrow);
					break;
				case projectileType.fireball:
					GameManager.Instance.AudioSource.PlayOneShot (SoundManager.Instance.Fireball);
					break;
				case projectileType.rock:
					GameManager.Instance.AudioSource.PlayOneShot (SoundManager.Instance.Rock);
					break;
			}
			StartCoroutine(ShootProjectile(newProjectile));
		}
	}

	IEnumerator ShootProjectile(Projectile projectile) {
		while(getTargetDistance(this.targetEnemy) > _SHOOT_PRECISION_ && projectile != null && this.targetEnemy != null) {
			Vector2 dir = targetEnemy.transform.localPosition - transform.localPosition;
			float angleDirection = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			projectile.transform.rotation = Quaternion.AngleAxis (angleDirection, Vector3.forward);
			projectile.transform.localPosition = Vector2.MoveTowards (projectile.transform.localPosition, targetEnemy.transform.localPosition, _SHOOT_SPEED_ * Time.deltaTime); 
			yield return null;
		}

		if (projectile != null || targetEnemy == null) {
			Destroy(projectile);
		}
	}

	private float getTargetDistance(Enemy enemy) {
		if (enemy == null) {
			enemy = GetNearestEnemyInRange ();
			if (enemy == null) {
				return 0.0f;
			}
		} 

		return Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));
	}
}
