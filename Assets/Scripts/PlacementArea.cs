using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){

        GameController.current.OnObjectEnteredArea(other.gameObject);

    }

    private void OnTriggerExit(Collider other){

        GameController.current.OnObjectExitedArea();

    }
}
