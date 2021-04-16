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

    [SerializeField, Range(0f, 20f)]
    public float runFovChange = 5f;
    [SerializeField, Range(0f, 20f)]
    public float sneakFovChange = 5f;
    [SerializeField, Range(0.1f, 100f)]
    public float fovChangeSpeed = 40f;

    private float fov;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = (Cinemachine.CinemachineFreeLook)FindObjectOfType(typeof(Cinemachine.CinemachineFreeLook));
        }
        fov = mainCamera.m_Lens.FieldOfView;

    }

    
    private float xPos;
    private float yPos;
    private float tempfov;
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


        //change pov only when running/sneaking and moving
        
        if (Input.GetButton("Run") && (xPos != transform.position.x || yPos != transform.position.y))
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Clamp( tempfov += fovChangeSpeed * Time.deltaTime , fov , fov + runFovChange );
        }
        else if (Input.GetButton("Sneak") && (xPos != transform.position.x || yPos != transform.position.y))
        {
            mainCamera.m_Lens.FieldOfView = Mathf.Clamp( tempfov -= fovChangeSpeed * Time.deltaTime , fov - sneakFovChange , fov );
        }
        else
        {
            //TODO implement proper gradual return to nominal state
            mainCamera.m_Lens.FieldOfView = fov;
            tempfov = fov;
        }
        //remember position to detect movement
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
}
