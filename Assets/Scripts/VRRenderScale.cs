using UnityEngine.XR;
using UnityEngine;

public class VRRenderScale : MonoBehaviour
{
  private void Start() {
      XRSettings.eyeTextureResolutionScale = 1.5f;
  }
}
