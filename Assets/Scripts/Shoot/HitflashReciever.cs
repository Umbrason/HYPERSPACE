
using UnityEngine;

public class HitflashReciever : MonoBehaviour, IDamageReciever
{
    public void OnHit(int damage)
    {
        HitFlashRenderFeature.Flash(gameObject);
    }
}
