using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDemo.Controller;

namespace VRDemo.Classes {

    public abstract class IAmInteractableObject : MonoBehaviour{

        [SerializeField]protected MeshRenderer _meshRender;
        [SerializeField]protected IAmPlacementArea _placementArea;

        protected Material defaultMat;
        protected Material activeMat;

        protected Transform _target;

        protected bool isActive = false;
        protected bool isInPlace = false;

        public virtual void SetInteractableState(bool pValue){
            // ExpController.current.AddDebug("Setting "+this.gameObject.name+" : "+pValue);
            isActive = pValue;
            activeMat = ExpController.current.activeMat;
            if(isActive) UpdateMat(activeMat);
        }

        public virtual void SetPlacemenArea(IAmPlacementArea p)
            => _placementArea = p;

        protected abstract void Start();

        protected virtual void UpdateMat(Material pMat){
            Material[] materials = _meshRender.materials;
            materials[0] = pMat;
            _meshRender.materials = materials;
        }

        public abstract void OnGrabbed();

        public abstract void OnDropped();

        public abstract void OnActivate();

        public virtual void OnDeactivate(){}

        public virtual IAmPlacementArea GetPlacementArea(){
            return _placementArea;
        }

        public virtual void OnEnteredArea(Transform pTarget){
            if(!isActive) return;
            isInPlace = true;
            _target = pTarget;
        }

        public virtual void OnExitedArea(){
            if(!isActive) return;
            isInPlace = false;
        }

        public virtual void OnCompleted()
            => ExpController.current.ActivateNext();

    }

    public abstract class IAmPlacementArea: MonoBehaviour{


        [SerializeField]protected MeshRenderer _meshRender;

        [SerializeField]protected List<Transform> _target;

        protected virtual void Start(){
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        public abstract void OnEnablePlacementArea();

        public abstract void OnDisablePlacementArea(); 

        protected abstract void OnTriggerEnter(Collider other);

        protected abstract void OnTriggerExit(Collider other);

        public Transform GetTarget(int index){
            return _target[index];
        }

    }

}
