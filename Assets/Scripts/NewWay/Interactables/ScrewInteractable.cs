using VRDemo.Controller;
using UnityEngine;
using VRDemo.Classes;

public class ScrewInteractable : IAmInteractableObject{

    protected override void Start() {
        defaultMat = _meshRender.materials[0];
        activeMat = ExpController.current.activeMat;
    }


    // perform action on triggerd
    public override void OnActivate(){

        if(!isActive) return;

    }


    public override void OnGrabbed(){

        if(!isActive) return;
        
        UpdateMat(defaultMat);
        _placementArea.OnEnablePlacementArea();

    }

    public override void OnDropped(){

        if(!isActive) return;

        if(!isInPlace){
            UpdateMat(activeMat);
            _placementArea.OnDisablePlacementArea();
        }else{
            
            PlaceToPostion();
            _placementArea.OnDisablePlacementArea();

            if(gameObject.GetComponentInChildren<IAmPlacementArea>() != null){
                // ExpController.current.UpdateInstructions("Activating drilling . .");
                DrillController.current.OnActivateDrill(
                    gameObject.GetComponentInChildren<IAmPlacementArea>(),
                    gameObject.GetComponent<IAmInteractableObject>()
                );
            }

            // OnCompleted();
        }

    }

    public override void OnCompleted(){
        gameObject.GetComponentInChildren<IAmPlacementArea>().OnDisablePlacementArea();
        ExpController.current.ActivateNext();
    }

    private void PlaceToPostion(){
        if(_target != null){
            gameObject.transform.position = _target.position;
            gameObject.transform.rotation = _target.rotation;
        }
    }

}
