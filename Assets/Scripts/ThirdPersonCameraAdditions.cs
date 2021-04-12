using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraAdditions : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook mainCamera;

    [SerializeField, Range(0.1f,0.9f)]
    public float scrollSensititvity = 0.5f;    

    public float minDistance = 1f;
    public float maxDistance = 20f;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = (Cinemachine.CinemachineFreeLook)FindObjectOfType(typeof(Cinemachine.CinemachineFreeLook));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Camera zoom handling
        var zoomInput = -Input.mouseScrollDelta.y;
        if (zoomInput > 0 && mainCamera.m_Orbits[2].m_Radius < maxDistance)
        {
            mainCamera.m_Orbits[0].m_Radius += scrollSensititvity;
            mainCamera.m_Orbits[1].m_Radius += scrollSensititvity;
            mainCamera.m_Orbits[2].m_Radius += scrollSensititvity;
        }
        else if (zoomInput < 0 && mainCamera.m_Orbits[2].m_Radius > minDistance)
        {
            mainCamera.m_Orbits[0].m_Radius -= scrollSensititvity;
            mainCamera.m_Orbits[1].m_Radius -= scrollSensititvity;
            mainCamera.m_Orbits[2].m_Radius -= scrollSensititvity;
        }

    }
}
