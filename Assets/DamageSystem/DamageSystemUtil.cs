using System.Linq;
using UnityEngine;

public static class DamageSystemUtil
{



    public static IDamageReciever[] GetEnabledDamageRecievers(this Collider c) => GetEnabledDamageRecievers(c.gameObject);
    public static IDamageReciever[] GetEnabledDamageRecievers(this GameObject go)
    {
        var parent = go.GetComponentsInParent<IDamageReciever>();
        var child = go.GetComponentsInChildren<IDamageReciever>().Where(r => r.gameObject != go);
        return parent.Concat(child).Where(r => r.enabled).ToArray();
    }
}