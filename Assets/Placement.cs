using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Placement : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastMan;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
