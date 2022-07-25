using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject gal;
    public GameObject target;

    public void Update()
    {
        float step = 1f * Time.deltaTime;
        Debug.Log("LSDFJ:");
        Vector3 newDir = Vector3.RotateTowards(gal.transform.position, target.transform.position, step, 0f);
        gal.transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, 0));
    }
}
