using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{

    private LineRenderer lineRend;
    private Transform otherBuilding;



    void Start()
    {

        lineRend = GetComponent<LineRenderer>();
        otherBuilding = GameObject.FindGameObjectWithTag("Building").transform;

    }


    void Update()
    {

        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, otherBuilding.transform.position);

    }
}
