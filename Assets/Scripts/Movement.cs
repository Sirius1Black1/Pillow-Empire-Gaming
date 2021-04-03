using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Movement : MonoBehaviour
{

    [SerializeField]
    Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = (Camera)gameObject.GetComponentInChildren(typeof(Camera),true);
        if (cam == null)
            Debug.LogError("Camera not found!");
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
