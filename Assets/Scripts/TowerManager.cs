using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager> {
    [SerializeField]
    private TowerBtn towerBtnPresssed;
	private SpriteRenderer spriteRenderer;
	private List<Tower> TowerList = new List<Tower>();
	private List<Collider2D> BuildList = new List<Collider2D>();
	private Collider2D buildTile;

    public TowerBtn TowerBtnPressed {
        get {
            return towerBtnPresssed;
        }
        set {
            towerBtnPresssed = value;
        }
    }
	
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildTile = GetComponent<Collider2D> ();
		//spriteRenderer.enabled = false; //NO DEBERIA HACER FALTA
	}
	
	// Update is called once per frame
	void Update () {
		OnBuildSiteClicked();
	}

	public void RegisterBuildSite(Collider2D buildTag) {
		BuildList.Add (buildTag);
	}

	public void RegisterTower(Tower tower) {
		TowerList.Add (tower);
	}

	public void RenameTagsBuildSites() {
		foreach (Collider2D buildtag in BuildList) {
			buildtag.tag = "BuildSite";
		}

		BuildList.Clear ();
	}

	public void DestroyAllTower() {
		foreach (Tower tower in TowerList) {
			Destroy (tower.gameObject);
		}

		TowerList.Clear ();
	}

	public void PlaceTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPresssed != null) {
			
			Tower newTower = Instantiate(towerBtnPresssed.TowerObject);
			newTower.transform.position = hit.transform.position;
			BuyTower (towerBtnPresssed.TowerPrice);
			GameManager.Instance.AudioSource.PlayOneShot (SoundManager.Instance.TowerBuilt);
			RegisterTower (newTower);
			disableDragSprite();
		}
	}

	public void SelectedTower(TowerBtn towerSelected) {
		if (GameManager.Instance.TotalMoney >= towerSelected.TowerPrice) { 
			this.towerBtnPresssed = towerSelected;
			enableDragSprite (towerBtnPresssed.DragSprite);
		}
	}

	public void OnBuildSiteClicked() {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Obtenemos la posicion en coordenadas del mundo del click 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider.tag == "BuildSite") {
				buildTile = hit.collider;
				buildTile.tag = "BuildSiteFull";
				RegisterBuildSite (buildTile);
				PlaceTower(hit);
			}
		}
		
		if(spriteRenderer.enabled) {
			TowerFollowMouse();
		}
	}

	public void TowerFollowMouse() {
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void enableDragSprite(Sprite sprite) {
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;
	}

	public void disableDragSprite() {
		spriteRenderer.enabled = false;
		spriteRenderer.sprite = null;
	}

	public void BuyTower(int price) {
		GameManager.Instance.SubtractMoney (price);
	}
}
