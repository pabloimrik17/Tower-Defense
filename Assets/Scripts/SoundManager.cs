using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
	[SerializeField]
	private AudioClip arrow;
	[SerializeField]
	private AudioClip death;
	[SerializeField]
	private AudioClip fireball;
	[SerializeField]
	private AudioClip gameover;
	[SerializeField]
	private AudioClip hit;
	[SerializeField]
	private AudioClip level;
	[SerializeField]
	private AudioClip newGame;
	[SerializeField]
	private AudioClip rock;
	[SerializeField]
	private AudioClip towerBuilt;



	public AudioClip Arrow {
		get {
			return this.arrow;
		}
	}

	public AudioClip Death {
		get {
			return this.death;
		}
	}

	public AudioClip Fireball {
		get {
			return this.fireball;
		}
	}

	public AudioClip Gameover {
		get {
			return this.gameover;
		}
	}

	public AudioClip Hit {
		get {
			return this.hit;
		}
	}

	public AudioClip Level {
		get {
			return this.level;
		}
	}

	public AudioClip NewGame {
		get {
			return this.newGame;
		}
	}

	public AudioClip Rock {
		get {
			return this.rock;
		}
	}

	public AudioClip TowerBuilt {
		get {
			return this.towerBuilt;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
