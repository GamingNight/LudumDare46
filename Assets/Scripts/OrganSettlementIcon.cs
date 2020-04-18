using UnityEngine;
using UnityEngine.UI;

public class OrganSettlementIcon : MonoBehaviour {

    public Sprite deselected;
    public Sprite selected;

    private Image image;
    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }

    void Start() {
        image = GetComponent<Image>();
        isSelected = false;
    }

    public void PointerEnter() {

        image.sprite = selected;
        isSelected = true;
    }

    public void PointerExit() {

        image.sprite = deselected;
        isSelected = false;
    }
}
