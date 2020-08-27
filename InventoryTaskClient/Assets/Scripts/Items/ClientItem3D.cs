using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ClientItem3D : ClientItem
{
    public Mesh Mesh                 { get; private set; }
    public Material MeshMaterial     { get; private set; }
    public Vector3 BoxColliderSize   { get; private set; }
    public Vector3 BoxColliderCenter { get; private set; }

    protected override void UpdateData()
    {
        base.UpdateData();
        var boxCollider   = GetComponent<BoxCollider>();
        BoxColliderSize   = boxCollider.size;
        BoxColliderCenter = boxCollider.center;
        Mesh              = GetComponent<MeshFilter>().mesh;
        MeshMaterial      = GetComponent<MeshRenderer>().material;
    }

    public override GameObject CreateNewItem()
    {
        var newItemObject = new GameObject(Item.Name);
        newItemObject.transform.SetParent(gameRoot.transform);
        newItemObject.transform.localScale = LocalScale;
        var boxCollider2D     = newItemObject.AddComponent<BoxCollider>();
        boxCollider2D.size    = BoxColliderSize;
        boxCollider2D.center  = BoxColliderCenter;
        var clientItem        = newItemObject.AddComponent<ClientItem3D>();
        clientItem.Item       = Item;
        clientItem.Weight     = Weight;
        clientItem.ItemSprite = ItemSprite;
        var draggableObject   = newItemObject.AddComponent<DraggableObject>();
        draggableObject.ResetPositionToOriginal = ResetPositionToOriginal;
        draggableObject.AllowMouseInteraction   = AllowMouseInteraction;
        newItemObject.AddComponent<Rigidbody>();
        var meshFilter        = newItemObject.AddComponent<MeshFilter>();
        meshFilter.mesh       = Mesh;
        var meshRenderer      = newItemObject.AddComponent<MeshRenderer>();
        meshRenderer.material = MeshMaterial;
        
        clientItem.UpdateData();
        return newItemObject;
    }
}
