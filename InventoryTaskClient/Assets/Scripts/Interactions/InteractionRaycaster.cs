using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRaycaster : MonoBehaviour
{
    public void FixedUpdate()
    {
        if (InteractionManager.Obj.AllowRaycasting)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
                InteractionManager.Obj.InteractionDistance))
            {
                var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                InteractionManager.Obj.SelectedObject = interactable != null ? hit.collider.gameObject : null;
            }
            else InteractionManager.Obj.SelectedObject = null;
        }
    }
}
