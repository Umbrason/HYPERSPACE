
using UnityEngine;

public class EnemyMaterialAnimator : MonoBehaviour
{
    private MeshRenderer[] cached_MeshRenderers;
    private MeshRenderer[] MeshRenderers => cached_MeshRenderers ??= GetComponentsInChildren<MeshRenderer>();

    [SerializeField] private AnimationCurve EmissionCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public void SetEmissionBrightness(float t)
    {
        var color = Vector3.one * EmissionCurve.Evaluate(t) * 8;
        foreach (var renderer in MeshRenderers)
            renderer.material.SetColor("_EmissionColor", (Vector4)color);
    }

}
