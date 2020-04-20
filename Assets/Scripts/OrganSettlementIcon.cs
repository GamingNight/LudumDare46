using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrganSettlementIcon : MonoBehaviour {

    private Image image;
    private Image imageSelected;
    private Sprite wrongSprite;
    private bool isSelected;
    public bool IsSelected { get { return isSelected; } }

    private Coroutine cannotBuyCoroutine;
    private Sprite initSprite;

    void Start() {
        ProceedImageAttribution();
        isSelected = false;
    }

    private void ProceedImageAttribution() {
        image = GetComponent<Image>();
        foreach (Image i in GetComponentsInChildren<Image>()) {
            if (image != i) {
                imageSelected = i;
            }
        }
    }

    public void PointerEnter() {

        imageSelected.enabled = true;
        isSelected = true;
    }

    public void PointerExit() {

        imageSelected.enabled = false;
        isSelected = false;
    }

    public void UpdateSprites(Sprite main, Sprite selected, Sprite wrong) {
        ProceedImageAttribution();
        image.sprite = main;
        imageSelected.sprite = selected;
        wrongSprite = wrong;
    }

    public void LaunchCannotBuyAnimation() {

        if (cannotBuyCoroutine != null) {
            StopCoroutine(cannotBuyCoroutine);
            image.sprite = initSprite;
        }

        cannotBuyCoroutine = StartCoroutine(LaunchCannotBuyCoroutine());
    }

    private IEnumerator LaunchCannotBuyCoroutine() {

        initSprite = image.sprite;
        image.sprite = wrongSprite;
        yield return new WaitForSeconds(0.5f);
        image.sprite = initSprite;
    }
}
