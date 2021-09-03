using System;
using System.Collections.Generic;
using VRDemo.Classes;
using UnityEngine;
using TMPro;

namespace VRDemo.Controller{


    public class ExpController : MonoBehaviour{

        #region Singleton
        public static ExpController current;
        private void Awake() {
            if(current == null) current = this;
            else Destroy(this);
        }
        #endregion

        public Material activeMat;

        [SerializeField] TextMeshProUGUI instructionText;
        [SerializeField] TextMeshProUGUI debugText;

        [SerializeField] List<IAmInteractableObject> sequenceList;

        private int _activeIndex;

        private void Start() {
            _activeIndex = 0;
            ToogleState(_activeIndex,true);
        }
  

        public void ActivateNext(){
            _activeIndex += 1;
            ToogleState(_activeIndex,true);
        }


        private void UpdateInstructions(int pIndex){
            if(pIndex == 0){
                instructionText.text = "Put side panel to the side of the fridge.";
            }
            if(pIndex == 1 ){
                instructionText.text = "Fit first screw.";
            }
            if(pIndex == 2){
                instructionText.text = "Fit second screw";
            }
            
        }

        public void ChangeInstruction(string pstr){
            instructionText.text = pstr;
        }

        public void AddDebug(string pStr){
            debugText.text += pStr + "\n";
        }

        private void ToogleState(int pIndex,bool pState){
            
            if(pIndex == 3){
                ChangeInstruction("Task Completed!");
            }

            if(pIndex < 0 || pIndex == sequenceList.Count){
                Debug.LogError("Index not found!");
                return;
            }
            
            UpdateInstructions(pIndex);

            // instructionText.text =
            //     sequenceList[pIndex].gameObject.name+" - "+pState;

            if(pState == true) ClearAll();

            sequenceList[pIndex].SetInteractableState(pState);
            
        }


        private void ClearAll(){
            foreach (IAmInteractableObject obj in sequenceList){
                obj.SetInteractableState(false);
            }
        }

    }


}
