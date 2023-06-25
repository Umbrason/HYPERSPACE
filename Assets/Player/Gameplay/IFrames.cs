
using System.Linq;
using UnityEngine;

public class IFrames : MonoBehaviour, IDamageReciever
{
    private IDamageReciever[] OtherDamageRecievers;
    void Start()
    {
        OtherDamageRecievers = GetComponents<IDamageReciever>().Where(reciever => (IDamageReciever)reciever != (IDamageReciever)this).ToArray();
    }

    public const float ImmunityDuration = .5f;

    public bool godmode;
    private bool IsImmune => (Time.fixedTime - ImmunityStartTime <= ImmunityDuration) || godmode;
    private float ImmunityStartTime = float.MinValue;
    public void OnHit(int damage)
    {
        if (IsImmune) return;
        ImmunityStartTime = Time.fixedTime;
        SetOthersImmune(true);
    }

    private bool OthersImmune = false;
    void FixedUpdate()
    {
        if (OthersImmune != IsImmune)
            SetOthersImmune(IsImmune);
    }

    private void SetOthersImmune(bool value)
    {
        foreach (var reciever in OtherDamageRecievers)
            reciever.enabled = !value;
        OthersImmune = value;
    }
}
