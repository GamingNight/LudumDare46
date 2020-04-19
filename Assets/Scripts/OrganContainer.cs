using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganContainer : MonoBehaviour
{

    private static OrganContainer INSTANCE;

    public static OrganContainer GetInstance() {

        return INSTANCE;
    }

    void Awake() {

        if(INSTANCE == null) {
            INSTANCE = this;
        }
    }
}
