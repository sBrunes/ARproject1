using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticlePiece : MonoBehaviour
{
    [SerializeField]
    private Vector3 Start, Target;
    [SerializeField]
    private AnimationCurve curve;

    public void Move(float percent)
    {
        transform.localPosition = Vector3.Lerp(Start, Target, curve.Evaluate(percent));
    }

    public void Reset()
    {
        transform.localPosition = Start;
    }
}
