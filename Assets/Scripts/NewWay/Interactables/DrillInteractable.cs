using VRDemo.Controller;
using VRDemo.Classes;
using UnityEngine;

public class DrillInteractable : IAmInteractableObject
{

    // private int drillingCount = 0;

    protected override void Start() {
        defaultMat = _meshRender.materials[2];
        activeMat = ExpController.current.activeMat;
        // drillingCount = 0;
    }


    [SerializeField] Animator TriggerAnimator;
    [SerializeField] Animator NeedleAnimator;

    public override void OnActivate()
    {
        AudioController.current.PlayDrillSound();
        NeedleAnimator.SetBool("PlayNeedleAnim",true);
        TriggerAnimator.SetFloat("Trigger",1);
        if(isActive && isInPlace){
           DrillController.current.OnDrilledScrew();
        }
    }

    public override void OnDeactivate()
    {
        TriggerAnimator.SetFloat("Trigger",0);
        NeedleAnimator.SetBool("PlayNeedleAnim",false);
    }

    protected override void UpdateMat(Material pMat)
    {
        Material[] mats = _meshRender.materials;
        mats[2] = pMat;
        _meshRender.materials = mats;
    }

    public override void OnDropped()
    {
        if(!isActive) return;

        if(!isInPlace){
            UpdateMat(activeMat);
            _placementArea.OnDisablePlacementArea();
        }else{
            // PERFORM LOGIC FOR 2 TIMES DRILL
        }

    }

    public void OnScrewFitted()
        => isInPlace = false;


    public override void OnGrabbed() 
    {
        if(!isActive) return;

        UpdateMat(defaultMat);
        _placementArea.OnEnablePlacementArea();


    }
}
