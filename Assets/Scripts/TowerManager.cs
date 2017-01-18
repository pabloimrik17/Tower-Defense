using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {
	[SerializeField]
	private TowerBtn towerBtnPresssed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Obtenemos la posicion en coordenadas del mundo del click 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider.tag == "BuildSite") {
				PlaceTower(hit);
			}
		}
	}

	public void PlaceTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPresssed != null) {
			
			GameObject newTower = Instantiate(towerBtnPresssed.TowerObject);
			newTower.transform.position = hit.transform.position;
		}
	}

	public void SelectedTower(TowerBtn towerSelected) {
		this.towerBtnPresssed = towerSelected;
	}
}
