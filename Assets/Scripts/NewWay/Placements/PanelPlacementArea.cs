using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDemo.Classes;
using VRDemo.Controller;

public class PanelPlacementArea : IAmPlacementArea
{
    public override void OnDisablePlacementArea()
    {
        _meshRender.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void OnEnablePlacementArea()
    {
         _meshRender.enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IAmInteractableObject>() != null){
            ExpController.current.AddDebug("Panel in area!");
            other.gameObject.GetComponent<IAmInteractableObject>().OnEnteredArea(_target[0]);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<IAmInteractableObject>() != null){
            ExpController.current.AddDebug("Panel out area");
             other.gameObject.GetComponent<IAmInteractableObject>().OnExitedArea();
        }
    }
}
