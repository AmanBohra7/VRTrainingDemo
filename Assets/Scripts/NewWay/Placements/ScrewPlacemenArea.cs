using UnityEngine;
using VRDemo.Controller;
using VRDemo.Classes;

public class ScrewPlacemenArea : IAmPlacementArea
{



    public override void OnDisablePlacementArea(){
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _meshRender.enabled = false;
    }

    public override void OnEnablePlacementArea(){
        gameObject.GetComponent<BoxCollider>().enabled = true;
        _meshRender.enabled = true;
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IAmInteractableObject>() != null){
            other.gameObject.GetComponent<IAmInteractableObject>().OnEnteredArea(_target[0]);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<IAmInteractableObject>() != null){
            other.gameObject.GetComponent<IAmInteractableObject>().OnExitedArea();
        }
    }


}
