using UnityEngine;

public class TowerBtn : MonoBehaviour {
	[SerializeField]
	private GameObject towerObject;
	[SerializeField]
	private Sprite dragSprite;
    [SerializeField]
    private int towerPrice;

	public GameObject TowerObject {
		get {
			return this.towerObject;
		}
	}

    public int TowerPrice {
        get {
            return TowerPrice;
        }
    }

	public Sprite DragSprite {
		get {
			return this.dragSprite;
		}
	}
}
