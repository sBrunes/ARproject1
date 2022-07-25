using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] topTargets, midTargets, bottomTargets;

    [SerializeField]
    private Transform topStart, midStart, bottomStart;

    [SerializeField]
    private float delay;

    [SerializeField]
    private int index;

    [SerializeField]
    private float deviation;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            thing(topTargets, topStart);
            thing(midTargets, midStart);
            thing(bottomTargets, bottomStart);
            
            if(index == (midTargets.Length - 1))
            {
                index = 0;
            } else {
                index++;
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void thing(GameObject[] targets, Transform start)
    {
        targets[index].transform.position = start.position + new Vector3(0, Random.Range(-deviation,deviation),0);
        targets[index].SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Target")
        {
            other.gameObject.SetActive(false);
        }
    }
}
