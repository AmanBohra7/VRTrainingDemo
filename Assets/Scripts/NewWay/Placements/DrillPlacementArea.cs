using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDemo.Classes;

[RequireComponent(typeof(BoxCollider))]
public class DrillPlacementArea : IAmPlacementArea{

    public override void OnDisablePlacementArea()
    {
        _meshRender.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override void OnEnablePlacementArea()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _meshRender.enabled = true;
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<DrillInteractable>() != null){
            DrillController.current.OnDrillEntered(gameObject);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
         if(other.gameObject.GetComponent<DrillInteractable>() != null){
            DrillController.current.OnDrillExited();
        }
    }

}
