using UnityEngine;

public class TowerManager : Singleton<TowerManager> {
	[SerializeField]
	private TowerBtn towerBtnPresssed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectedTower(TowerBtn towerSelected) {
		this.towerBtnPresssed = towerSelected;
	}
}
