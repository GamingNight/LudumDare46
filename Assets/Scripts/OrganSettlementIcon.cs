using UnityEngine;
using UnityEngine.UI;

public class OrganSettlementIcon : MonoBehaviour {

    private Image image;
    private Image imageSelected;
    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }

    void Start() {
        image = GetComponent<Image>();
        foreach (Image i in GetComponentsInChildren<Image>()) {
            if (image != i) {
                imageSelected = i;
            }
        }
        isSelected = false;
    }

    public void PointerEnter() {

        imageSelected.enabled = true;
        isSelected = true;
    }

    public void PointerExit() {

        imageSelected.enabled = false;
        isSelected = false;
    }

    public void UpdateSprites(Sprite main, Sprite selected) {

        GetComponent<Image>().sprite = selected;
        GetComponentInChildren<Image>().sprite = main;
    }
}
