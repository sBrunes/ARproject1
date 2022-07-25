using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow, reticle;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float force;

    [SerializeField]
    private Transform bowString, castTarget;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject[] arrows;

    [SerializeField]
    private AnimationCurve curve;

    private int index = 1;

    private bool drawing;

    private float targetTime = .4f;
    private float percent;
    private float elapsedTime;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private int score;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private ReticlePiece[] rPieces;

    public bool placing = true;

    [SerializeField]
    private ARRaycastManager raycastMan;
    [SerializeField]
    private ARPlaneManager planeMan;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Vector2 touchPos;

    public GameObject galleryObject;
    [SerializeField]
    private GameObject placementButton;
    
    private Vector3 establishedPosition;

    void Update()
    {
        if(drawing)
        {
            percent = elapsedTime / targetTime;

            force = Mathf.Lerp(0, 20f, curve.Evaluate(percent));

            foreach(ReticlePiece piece in rPieces)
            {
                piece.Move(percent);
            }

            elapsedTime += Time.deltaTime;
        }

        Ray ray = new Ray(castTarget.position, castTarget.forward);

        if(Physics.Raycast(ray, out RaycastHit info, 500f, layer))
        {
            reticle.transform.position = info.point;
        }

        float step = 1f * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(reticle.transform.position, transform.position, step, 0f);
        reticle.transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void IncreaseScore(int value)
    {
        score += value;

        scoreText.text = score.ToString();
    }

    public void Tap(InputAction.CallbackContext context)
    {
        if(!placing)
        {
            if(context.performed)
            {
                Reload();
            } else if(context.canceled)
            {
                Fire();
            }
        }
    }

    public void Place(InputAction.CallbackContext context)
    {
        if(!placing) return;
        if(context.performed)
        {
            touchPos = context.ReadValue<Vector2>();
            if(touchPos.y < 350f) return;
            if(raycastMan.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                var hitPose = hits[0].pose;

                foreach(var plane in planeMan.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                planeMan.enabled = false;

                galleryObject.transform.position = hitPose.position;
                establishedPosition = hitPose.position;
            }
        }
    }


    public void RotateGalleryLeft()
    {
        galleryObject.transform.Rotate(0,15f,0);
        galleryObject.transform.position = establishedPosition;
    }

    public void RotateGalleryRight()
    {
        galleryObject.transform.Rotate(0,-15f,0);
        galleryObject.transform.position = establishedPosition;
    }


    public void FinishedPlacing()
    {
        placing = false;
        placementButton.SetActive(false);
    }

    public void FullyCharged()
    {
        drawing = false;
        elapsedTime = 0f;
    }

    public void Fire()
    {
        foreach(ReticlePiece piece in rPieces)
        {
            piece.Reset();
        }
        drawing = false;
        arrow.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(arrow.transform.forward * force, ForceMode.Impulse);
        anim.SetTrigger("Fire");
    }

    private void Reload()
    {
        drawing = true;
        elapsedTime = 0f;
        arrow = arrows[index];
        rb = arrows[index].GetComponent<Rigidbody>();
        anim.SetTrigger("Draw");       
        arrow.SetActive(true);
        rb.isKinematic = true;
        arrow.transform.parent = bowString;
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.rotation = bowString.rotation;
        arrow.transform.localRotation = Quaternion.Euler(90,0,0);

        if((index + 1) == arrows.Length)
        {
            index = 0;
        } else {
            index++;
        }

    }
}
