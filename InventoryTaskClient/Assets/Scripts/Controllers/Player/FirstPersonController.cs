using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float Speed = 10.0f;
    
    private float translation;
    private float straffe;
    
    void Update()
    {
        if (InteractionManager.Obj.IsLockedControls) return;
        translation = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        straffe     = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
        
        if (InteractionManager.Obj.SelectedObject != null && Vector3.Distance(InteractionManager.Obj.SelectedObject.transform.position, transform.position) > InteractionManager.Obj.InteractionDistance)
            InteractionManager.Obj.SelectedObject = null;
    }
}
