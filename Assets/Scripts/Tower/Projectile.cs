using UnityEngine;

public enum projectileType {
	rock, arrow, fireball
};

public class Projectile : MonoBehaviour {

	[SerializeField]
	private int attackStrength;
	[SerializeField]
	private projectileType projectileType;
	// Use this for initialization
	
	public int AttackStrength {
		get {
			return this.attackStrength;
		}
	}

	public projectileType ProjectileType {
		get {
			return this.projectileType;
		}
	}
}
