using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

	public GameObject defLevel;
	public GameObject aCount;
	public GameObject aProd;
	public GameObject bCount;
	public GameObject bProd;
	public GameObject cCount;
	public GameObject cProd;
    // Start is called before the first frame update
    void Start()
    {
        UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();
    }

    private void UpdateData() {
    	int defLevelValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.D).count;
    	defLevel.GetComponent<Text>().text = defLevelValue.ToString();
    	int aCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.A).count;
    	aCount.GetComponent<Text>().text = aCountValue.ToString();
    	int bCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.B).count;
    	bCount.GetComponent<Text>().text = bCountValue.ToString();
    	int cCountValue = GameManager.GetInstance().GetResources(Resources.ResourcesType.C).count;
    	cCount.GetComponent<Text>().text = cCountValue.ToString();
    }
}
