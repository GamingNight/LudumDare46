using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{

    private LineRenderer lineRend;


    void Start()
    {

        lineRend = GetComponent<LineRenderer>();

    }


    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Organ")
        {
            lineRend.enabled = true;
            lineRend.startColor = Color.black;
            lineRend.endColor = Color.black;
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, other.gameObject.transform.position);
        }
    }

    void OnTriggerExit(Collider other) {

        if(other.gameObject.tag == "Organ") {
            lineRend.enabled = false;
        }
    }
}
