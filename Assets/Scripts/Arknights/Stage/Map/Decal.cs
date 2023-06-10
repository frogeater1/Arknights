using System;
using Arknights;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Decal : MonoBehaviour
{
    private DecalProjector decalProjector;
    
    public AnimationCurve curve;
    private void Awake()
    {
        decalProjector = GetComponent<DecalProjector>();
    }

    private void OnEnable()
    {
        EventManager.LogicUpdate+=LogicUpdate;
    }

    private void OnDisable()
    {
        EventManager.LogicUpdate-=LogicUpdate;
    }

    private void LogicUpdate()
    {
        decalProjector.fadeFactor = curve.Evaluate(Time.time % 2);
    }
}