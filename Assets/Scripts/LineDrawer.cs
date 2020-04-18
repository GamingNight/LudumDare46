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


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Organ")
        {
            Debug.Log("Triggé");
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, other.gameObject.transform.position);
        } else {

        }

    }
}
