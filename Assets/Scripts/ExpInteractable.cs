using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ExpInteractable : MonoBehaviour
{

    public string objectName;

    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject correctTransformObject;
    [SerializeField] Material defaultMat;
    [SerializeField] Material markerMat;


    public void ChangeMat(Material pMat) => meshRenderer.material = pMat;

    private void Start() {

        grabInteractable.selectEntered.AddListener(delegate { ObjectSelected( ); });
        grabInteractable.selectExited.AddListener(delegate { ObjectDeselected( ); });

        correctTransformObject.SetActive(false);
    }

    // Called when the gameobject is grabbed 
    private void ObjectSelected(){
        if(particles) particles.Play();
        correctTransformObject.SetActive(true);
        meshRenderer.material = defaultMat;
        GameController.current.OnGrabbedObject(gameObject);
    }

    // called when the gamobject is dropped 
    private void ObjectDeselected(){
        if(particles) particles.Stop();
        meshRenderer.material = markerMat;
        correctTransformObject.SetActive(false);
        GameController.current.OnDroppedObject();
    }

    // when all of gameobject task is been completed
    // Task - correct position placement


    public void TaskCompleted(){
        grabInteractable.interactionLayerMask = 0;
        gameObject.transform.position = correctTransformObject.transform.position;
        gameObject.transform.rotation = correctTransformObject.transform.rotation;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        DeactivateObject();
    }

    public void ActivateObject(){
        meshRenderer.material = markerMat;
        grabInteractable.interactionLayerMask = ~0;
    }

    public void DeactivateObject(){
        meshRenderer.material = defaultMat;
        grabInteractable.interactionLayerMask = 0;
    }

}
