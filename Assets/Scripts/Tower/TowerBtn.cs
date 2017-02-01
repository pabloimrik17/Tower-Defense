using UnityEngine;

public class TowerBtn : MonoBehaviour {
	[SerializeField]
	private Tower towerObject;
	[SerializeField]
	private Sprite dragSprite;
    [SerializeField]
    private int towerPrice;

	public Tower TowerObject {
		get {
			return this.towerObject;
		}
	}

    public int TowerPrice {
        get {
			return towerPrice;
        }
    }

	public Sprite DragSprite {
		get {
			return this.dragSprite;
		}
	}
}
