using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{

    public float speedRotationMin;
    public float speedRotationDelta;
    private float speedRotation;

    public bool clockwise;

    void Awake()
    {
        speedRotation = Random.Range(0f, speedRotationDelta) + speedRotationMin;
        clockwise = (Random.Range(0, 2) == 0);
    }

    void Update()
    {
        transform.Rotate(0, 0, speedRotation * Time.deltaTime * (clockwise ? 1 : -1));
    }
}
