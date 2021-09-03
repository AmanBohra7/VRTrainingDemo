using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDemo.Classes;
using VRDemo.Controller;


// this script is accessed by ScrewInteractable
public class DrillController : MonoBehaviour{
   
    // After placement of every screw we have to perform
    // addition steps of 2 Activate Drill to finish that screw task
    #region Singleton
    public static DrillController current;
    private void Awake() {
        if(current == null) current = this;
        Destroy(this);
    }
    #endregion


    [SerializeField] GameObject Drill;

    private IAmPlacementArea currentDrillPlacement;
    private IAmInteractableObject currentScrew;

    private int drillingCount = 0;

    // activate drill
    public void OnActivateDrill(IAmPlacementArea pPlacement,IAmInteractableObject pScrew){
        currentDrillPlacement = pPlacement;
        currentScrew = pScrew;
        drillingCount = 0;

        ExpController.current.ChangeInstruction("Drive screw with drill.");

        Drill.GetComponent<IAmInteractableObject>().SetPlacemenArea(currentDrillPlacement);
        Drill.GetComponent<IAmInteractableObject>().SetInteractableState(true);

        // ExpController.current.UpdateInstructions("Activating drill for "+pPlacement.gameObject.name+" of"+currentScrew.gameObject.name);
    }


    public void OnDrilledScrew(){
        
        drillingCount += 1;
        if(drillingCount == 1){
            IAmPlacementArea screwPlacementArea =  currentScrew.GetPlacementArea();
            currentScrew.gameObject.transform.position = screwPlacementArea.GetTarget(1).position;
        } 
        if(drillingCount == 2){
            IAmPlacementArea screwPlacementArea =  currentScrew.GetPlacementArea();
            currentScrew.gameObject.transform.position = screwPlacementArea.GetTarget(2).position;
            currentScrew.OnCompleted();
            Drill.GetComponent<IAmInteractableObject>().SetInteractableState(false);
            Drill.GetComponent<DrillInteractable>().OnScrewFitted();
            ExpController.current.AddDebug("Completed - "+currentScrew.gameObject.name);
        } 
                
       
    }

    public void OnDeactivateDrill(){
        Drill.GetComponent<IAmInteractableObject>().SetInteractableState(false);
        currentDrillPlacement = null;
    }

    // check if drill is in position
    public void OnDrillEntered(GameObject placementObject){
        ExpController.current.AddDebug("Entered area - "+currentDrillPlacement.gameObject.name);
        Drill.GetComponent<IAmInteractableObject>().OnEnteredArea(placementObject.transform);
        // ExpController.current.UpdateInstructions("Drill entered in correct Area");
    }

    public void OnDrillExited(){
        ExpController.current.AddDebug("Exited area - "+currentDrillPlacement.gameObject.name);
        Drill.GetComponent<IAmInteractableObject>().OnExitedArea();
        // ExpController.current.UpdateInstructions("Drill entered in exited Area");
    }

    // if drill is in position wait for two activation 




}
