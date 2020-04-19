using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFooter : MonoBehaviour {

    void Start() {
        UpdateText();
    }

    void Update() {
        UpdateText();
    }

    private void UpdateText() {
        int a = GameManager.GetInstance().GetResources(Resources.ResourcesType.A).count;
        int b = GameManager.GetInstance().GetResources(Resources.ResourcesType.B).count;
        int c = GameManager.GetInstance().GetResources(Resources.ResourcesType.C).count;
        int d = GameManager.GetInstance().GetResources(Resources.ResourcesType.D).count;
        Text t = GetComponent<Text>();
        t.text = "A " + a.ToString() + " B " + b.ToString() + " C " + c.ToString() + " D " + d.ToString();
    }
}
