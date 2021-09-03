using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRDemo.Controller;
using VRDemo.Classes;

public class PanelInteractable : IAmInteractableObject
{
    [SerializeField] Transform target_test;

    [SerializeField] Material panelDefaulMat;


    public override void OnActivate()
    {
        // throw new System.NotImplementedException();
    }

    public override void OnDropped()
    {
        // throw new System.NotImplementedException();
        if(!isActive) return;

        if(isInPlace){
            try{
                isActive = false;
                UpdateMat(defaultMat);
                _placementArea.OnDisablePlacementArea();
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                if(_target != null){
                    ExpController.current.AddDebug("Placing at transform of "+_target.gameObject.name);
                    gameObject.transform.position = _target.position;
                    gameObject.transform.rotation = _target.rotation;
                }
                ExpController.current.ActivateNext();
            }catch(Exception e){
                ExpController.current.AddDebug(e.Message);
            }
        }else{
            _placementArea.OnDisablePlacementArea();
            UpdateMat(activeMat);
        }

        
    }

    public override void OnGrabbed()
    {
        if(!isActive) return;

        UpdateMat(defaultMat);
        _placementArea.OnEnablePlacementArea();
    }



    protected override void Start()
    {
        // throw new System.NotImplementedException();
        Debug.Log("testing!");
        defaultMat = panelDefaulMat;
        
        // gameObject.transform.position = target_test.position;
        // gameObject.transform.rotation = target_test.rotation;
        // UpdateMat(default);
    }
}
