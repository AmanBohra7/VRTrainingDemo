using TMPro;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class HandPresence : MonoBehaviour
{

    [SerializeField] InputDeviceCharacteristics selectedDeviceCharacteristic;
    [SerializeField] GameObject handPrefab; 

    private InputDevice targetDevice;
    private GameObject spawnedObject;
    private Animator animator;

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize(){
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(selectedDeviceCharacteristic,devices);

        if(devices.Count > 0){
            targetDevice = devices[0];
            spawnedObject = Instantiate(handPrefab,transform);
            animator = spawnedObject.GetComponent<Animator>();
        }

        // Debug.Log("In hand presence!");
    }

    void UpdateHandAnimation(){
        if( targetDevice.TryGetFeatureValue(CommonUsages.trigger,out float triggerValue) ){
            animator.SetFloat("Trigger",triggerValue);
            // targetDevice.SendHapticImpulse();
        }else animator.SetFloat("Trigger",0);

         if( targetDevice.TryGetFeatureValue(CommonUsages.grip,out float gripValue) ){
            animator.SetFloat("Grip",gripValue);
        }else animator.SetFloat("Grip",0);
        
    }

    
    void Update(){

        if(!targetDevice.isValid) TryInitialize();
        else UpdateHandAnimation();

    }
}
