using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraAdditions : MonoBehaviour
{
    private InputActions inputs = null;

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

    private void Awake()
    {
        inputs = new InputActions();
    }

    private void OnEnable()
    {
        inputs.Player.Enable();
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = (Cinemachine.CinemachineFreeLook)FindObjectOfType(typeof(Cinemachine.CinemachineFreeLook));
        }
        
        fov = mainCamera.m_Lens.FieldOfView;
        //FIXME: test doesn't do anything as of now
        float test = 100;
        Mathf.Clamp(test, 1, 20);
    }

    
    
    private float xPos;
    private float yPos;
    private float tempfov;
    // Update is called once per frame
    void Update()
    { 
        
        //Camera zoom handling'        
        var zoomInput = -inputs.Player.Zoom.ReadValue<float>(); 

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
       
        if (inputs.Player.Run.ReadValue<float>() > 0 && (xPos != transform.position.x || yPos != transform.position.y))
        {
            tempfov = Mathf.Clamp( tempfov += fovChangeSpeed * Time.deltaTime , fov , fov + runFovChange );
        }
        else if (inputs.Player.Sneak.ReadValue<float>() > 0 && (xPos != transform.position.x || yPos != transform.position.y))
        {
            tempfov = Mathf.Clamp( tempfov -= fovChangeSpeed * Time.deltaTime , fov - sneakFovChange , fov );
        }
        else
        {
            //Clamping so tempfov will reach FOV after no input is given
            if (tempfov < fov)
            {
                tempfov = Mathf.Clamp(tempfov += fovChangeSpeed * Time.deltaTime, Mathf.NegativeInfinity, fov);
            }
            else if (tempfov > fov)
            {
                tempfov = Mathf.Clamp(tempfov -= fovChangeSpeed * Time.deltaTime, fov, Mathf.Infinity);
            }
        }

        mainCamera.m_Lens.FieldOfView = tempfov;



        //remember position to detect movement
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
}
