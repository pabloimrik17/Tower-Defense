using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {
    [SerializeField]
    private TowerBtn towerBtnPresssed;
	private SpriteRenderer spriteRenderer;

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
	}
	
	// Update is called once per frame
	void Update () {
		OnBuildSiteClicked();
	}

	public void PlaceTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPresssed != null) {
			
			GameObject newTower = Instantiate(towerBtnPresssed.TowerObject);
			newTower.transform.position = hit.transform.position;
			disableDragSprite();
		}
	}

	public void SelectedTower(TowerBtn towerSelected) {
		this.towerBtnPresssed = towerSelected;
		enableDragSprite(towerBtnPresssed.DragSprite);
	}

	public void OnBuildSiteClicked() {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Obtenemos la posicion en coordenadas del mundo del click 
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider.tag == "BuildSite") {
				hit.collider.tag = "BuildSiteFull";
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
}
