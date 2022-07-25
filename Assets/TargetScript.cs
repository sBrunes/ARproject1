using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField]
    private int directionValue, scoreValue;
    [SerializeField]
    private float speed;
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private Material point1,point5,point10;
    [SerializeField]
    private int chance1,chance2,chance3;
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private GameObject fire,frost;
    [SerializeField]
    private Manager mn;

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.right * speed * Time.deltaTime) * directionValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            mn.Explode(transform.position);
            controller.IncreaseScore(scoreValue);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private int randNum;
    private void OnEnable()
    {
        fire.SetActive(false);
        frost.SetActive(false);
        randNum = Random.Range(1,101);
        if(randNum < chance1)
        {
            mesh.material = point1;
            scoreValue = 1;
        } else if(randNum < chance2)
        {
            mesh.material = point5;
            fire.SetActive(true);
            scoreValue = 5;
        } else {
            mesh.material = point10;
            frost.SetActive(true);
            scoreValue = 10;
        }
    }
}
