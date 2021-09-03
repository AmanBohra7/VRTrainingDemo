using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip drillClip;

    #region Singleton
    public static AudioController current;
    private void Awake() {
        if(current == null) current = this;
        else Destroy(this);
    }
    #endregion

    public void PlayDrillSound(){
        audioSource.PlayOneShot(drillClip,.5f);
    }

}
