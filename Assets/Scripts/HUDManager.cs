﻿using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject defLevel;
    public GameObject defPay;
    public GameObject aCount;
    public GameObject aProd;
    public GameObject bCount;
    public GameObject bProd;
    public GameObject cCount;
    public GameObject cProd;

    // Start is called before the first frame update
    void Start() {
        UpdateData();
    }

    // Update is called once per frame
    void Update() {
        UpdateData();
    }

    private void SetProdValue(GameObject obj, Resources.ResourcesType type) {
        int aProdValue = GameManager.GetInstance().GetSimulation(type).count;
        string aProdString = "";
        if (aProdValue > 0) {
            aProdString = "+" + aProdValue.ToString();
        }
        obj.GetComponent<Text>().text = aProdString;
    }

    private void UpdateData() {
        int defLevelValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.D).count + 1;
        defLevel.GetComponent<Text>().text = defLevelValue.ToString();

        if (defPay) {
            int defPayValue = GameManager.GetInstance().GetDefCost();
            defPay.GetComponent<Text>().text = "Inject " + defPayValue.ToString();
        }

        int aCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.A).count;
        aCount.GetComponent<Text>().text = aCountValue.ToString();
        int bCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.B).count;
        bCount.GetComponent<Text>().text = bCountValue.ToString();
        int cCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.C).count;
        cCount.GetComponent<Text>().text = cCountValue.ToString();

        SetProdValue(aProd, Resources.ResourcesType.A);
        SetProdValue(bProd, Resources.ResourcesType.B);
        SetProdValue(cProd, Resources.ResourcesType.C);
    }
}
