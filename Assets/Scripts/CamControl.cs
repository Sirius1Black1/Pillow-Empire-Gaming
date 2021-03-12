
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamControl : MonoBehaviour
{
    [SerializeField, Range(0f, 2f)]
    float sensitivity = 1f;

    [SerializeField, Range(0f, 1f)]
    float scrollSensitivity = 0.1f;

    [SerializeField, Range(1f, 20f)]
    float distance = 5;

    [SerializeField]
    float maxDistance = 20f;

    [SerializeField]
    float minDistance = 1f;

    [Range(0f,90f)]
    public float lowerKeepout = 45;
    
    [Range(91f, 180f)]
    public float upperKeepout = 120;

    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        distance = -gameObject.transform.localPosition.z;
    }

    private float getAdjustedXRotation() 
    {
        return (gameObject.transform.eulerAngles.x + 90) % 360;
    }


    // Update is called once per frame
    void Update()
    {
        var mouseX = Mathf.Clamp(Input.GetAxis("Mouse X") * sensitivity, -distance, distance) ;
        var mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y") * sensitivity, -distance, distance) ;
        if (mouseX != 0 || mouseY != 0)
        {
            gameObject.transform.Translate(-mouseX, 0, 0, Space.Self);
            gameObject.transform.Translate(0, 0, -Mathf.Sqrt( distance*distance - mouseX*mouseX ) + distance, Space.Self);
            
            if ( !(getAdjustedXRotation() < lowerKeepout && mouseY > 0) && !(getAdjustedXRotation() > upperKeepout && mouseY < 0))
            {
                gameObject.transform.Translate(0, -mouseY, 0, Space.Self);
                gameObject.transform.Translate(0, 0, -Mathf.Sqrt(Mathf.Pow(distance, 2) - Mathf.Pow(mouseY, 2)) + distance, Space.Self);
            }

            gameObject.transform.LookAt(gameObject.transform.parent.transform);

            if (debug)
            {
                Debug.Log("X rotation: " + getAdjustedXRotation() + " °");
            }
            
        }

        var mouseS = -Input.mouseScrollDelta.y;
        var distanceDelta = mouseS * scrollSensitivity;
        distance += distanceDelta;
        //ensure distance stays within limits
        if (distance > maxDistance)
        {
            distance = maxDistance;
            distanceDelta = 0;
        }
        if (distance < minDistance)
        {
            distance = minDistance;
            distanceDelta = 0;
        }
        
        if (distanceDelta != 0)
        {
            //set camera position to match distance
            gameObject.transform.Translate(0, 0, -distanceDelta);
        }
        #region debug
        if (debug)
        {
            Debug.Log("local x: " + gameObject.transform.localPosition.x);
            Debug.Log("local y: " + gameObject.transform.localPosition.y);
        }
        #endregion
        
        //gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -distance);

    }
    

}
