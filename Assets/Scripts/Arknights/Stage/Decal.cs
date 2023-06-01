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

    private void Update()
    {
        decalProjector.fadeFactor = curve.Evaluate(Time.time % 2);
    }
}