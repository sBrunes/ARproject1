using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] explosions;

    private int index;

    public void Explode(Vector3 point)
    {
        explosions[index].SetActive(true);
        explosions[index].transform.position = point;
        StartCoroutine(delay(explosions[index]));

        if((index + 1) == explosions.Length)
        {
            index = 0;
        } else {
            index++;
        }
    }

    private IEnumerator delay(GameObject thing)
    {
        yield return new WaitForSeconds(.3f);
        thing.SetActive(false);
    }
}
