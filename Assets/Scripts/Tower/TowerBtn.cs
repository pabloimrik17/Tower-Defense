using UnityEngine;

public class TowerBtn : MonoBehaviour {
	[SerializeField]
	private GameObject towerObject;
	[SerializeField]
	private Sprite dragSprite;

	public GameObject TowerObject {
		get {
			return this.towerObject;
		}
	}

	public Sprite DragSprite {
		get {
			return this.dragSprite;
		}
	}
}
