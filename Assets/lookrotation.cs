using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookrotation : MonoBehaviour
{
    [SerializeField] Transform point;
    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cam.LookAt(point);
    }
}
