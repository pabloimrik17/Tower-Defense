using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField]
	private float navigationUpdateTime;
    [SerializeField]
    private float healthPoints;
	private float navigationTime = 0;

	private int target = 0;

	private Transform enemy;

	private Collider2D enemyCollider;

	private Animator enemyAnimator;

	private bool isDead = false;

    [SerializeField]
    private int rewardAmount;

	public bool IsDead {
		get{
			return isDead;
		}
	}
	
	// Use this for initialization
	void Start () {
		enemy = GetComponent<Transform>();
		enemyCollider = GetComponent<Collider2D> ();
		enemyAnimator = GetComponent<Animator> ();
		GameManager.Instance.RegisterEnemy(this);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnemyPosition();
	}

	void UpdateEnemyPosition() {
		if(GameManager.Instance.WayPoints != null && !isDead) {
			navigationTime += Time.deltaTime;
			if(navigationTime > navigationUpdateTime) {
				if(target < GameManager.Instance.WayPoints.Length) {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.Instance.WayPoints[target].position, navigationTime);
				} else {
					enemy.position = Vector2.MoveTowards(enemy.position, GameManager.Instance.ExitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "CheckPoint") {
			target++;
		} else if(collider.tag == "Finish") {
			GameManager.Instance.UnregisterEnemy(this);
		} else if(collider.tag == "Projectile") {
			Projectile projectile = collider.gameObject.GetComponent<Projectile> ();
			enemyHit (projectile.AttackStrength);

            Destroy(collider.gameObject);
        }
	}

	public void enemyHit(int hitPoints) {
		if ((healthPoints - hitPoints) > 0) {
			healthPoints -= hitPoints;
			enemyAnimator.Play ("Hurt");
		} else {
			enemyAnimator.SetTrigger ("didDie");
			Die ();
		}
	}

	public void Die() {
		isDead = true;
		enemyCollider.enabled = false;
	}

}