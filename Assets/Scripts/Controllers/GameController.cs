using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class GameController : MonoBehaviour
{

    public static GameController current;

    private void Awake() {
        if(current == null) current = this;
        else Destroy(this);
    }

    [SerializeField] TextMeshProUGUI debugText;

    [SerializeField] Material defualtMat;   
    [SerializeField] Material heighlightMat;


    [SerializeField] List<GameObject> interactableObjects;
    private GameObject currentInteractableObject;   
    private int currentInteractableIndex = 0;
    private bool objectCurrentlyInPose = false;


    private void Start() {
        debugText.text = "Pick up side panel of the fridge!";

        foreach(GameObject obj in interactableObjects){
            obj.GetComponent<ExpInteractable>().DeactivateObject();
        }

        currentInteractableObject =  interactableObjects[currentInteractableIndex];
        currentInteractableObject.GetComponent<ExpInteractable>().ActivateObject();
    }

    private void ActivateNext(){

        if(currentInteractableIndex != interactableObjects.Count-1){
            ++currentInteractableIndex;
            currentInteractableObject =  interactableObjects[currentInteractableIndex];
            currentInteractableObject.GetComponent<ExpInteractable>().ActivateObject();

            if(currentInteractableIndex == 1)
                debugText.text = "Pick up the screw!";
        
            if(currentInteractableIndex == 2)
                debugText.text = "Pick up the screw!";

        }else{
            debugText.text = "Task Completed!";
        }




    }


    public void OnGrabbedObject(GameObject pObject){

        if(currentInteractableIndex == 0)
            debugText.text = "Place side panel at the placement area!";

        if(currentInteractableIndex == 1)
            debugText.text = "Place screw at correct position!";
        
        if(currentInteractableIndex == 2)
            debugText.text = "Place screw at correct position!";

        currentInteractableObject = pObject;
    }

    public void OnDroppedObject(){
        if(currentInteractableObject && objectCurrentlyInPose){
            currentInteractableObject.GetComponent<ExpInteractable>().TaskCompleted();
            ActivateNext();
            objectCurrentlyInPose = false;
        }else{

            if(currentInteractableIndex == 0)
                debugText.text = "Pick up side panel of the fridge!";

            if(currentInteractableIndex == 1)
                debugText.text = "Pick up the screw!";
        
            if(currentInteractableIndex == 2)
                debugText.text = "Pick up the screw!";

            currentInteractableObject = null;
        }
    }

    public void OnObjectEnteredArea(GameObject pObj){
        // debugText.text = "Entered object: "+pObj.name;
        if(currentInteractableObject != null){
            // debugText.text = "Object "+pObj.name+" task completed!";
            if(pObj.name == currentInteractableObject.name){
                objectCurrentlyInPose = true;
            }
        }
    }

    public void OnObjectExitedArea(){
        objectCurrentlyInPose = false;
    }

    private void Update() {
        
    }


}
