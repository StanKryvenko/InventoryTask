using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MouseCameraLook : MonoBehaviour
{
    [SerializeField] public float Sensitivity = 5.0f;
    [SerializeField] public float Smoothing   = 2.0f;
    public GameObject Character; // The chacter is the capsule
    
    private Vector2 mouseLook; // Incremental value of mouse moving
    private Vector2 smoothV;   // smooth the mouse moving
    
    void Start () 
    {
        if (Character == null) Character = this.transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;		
    }
	
    void Update () {
        if (InteractionManager.Obj.IsLockedControls) return;
        #if UNITY_EDITOR
        // Turn on the cursor
        if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
        #endif

        // Calculate mouse delta based on mouse movements
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md     = Vector2.Scale(md, new Vector2(Sensitivity * Smoothing, Sensitivity * Smoothing));
        // Interpolate result between mouse coordinates
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / Smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / Smoothing);
        // Incrementally add to the camera look
        mouseLook += smoothV;

        // Vector3.right means the x-axis
        var resultAngle = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        // Up/down look preventing rotation
        if (resultAngle.x <= 0.6 && resultAngle.x >= -0.6)
            transform.localRotation = resultAngle;
        else 
            mouseLook.y -= smoothV.y;

        Character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Character.transform.up);
    }
}
