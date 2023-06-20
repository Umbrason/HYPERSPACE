using System;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBeamTarget : MonoBehaviour
{
    private static List<ShieldBeamTarget> m_AvailableTargets = new();
    public static IReadOnlyList<ShieldBeamTarget> AvailableTargets => m_AvailableTargets;
    public event Action OnRecieveShield;
    public event Action OnLoseShield;
    private HashSet<Guid> ShieldSources = new();


    void OnEnable()
    {
        m_AvailableTargets.Add(this);
    }

    void OnDisable()
    {
        m_AvailableTargets.Remove(this);
    }

    public void AddShieldSource(Guid guid)
    {
        ShieldSources.Add(guid);
        if (ShieldSources.Count == 1) OnRecieveShield?.Invoke();
    }

    public void RemoveShieldSource(Guid guid)
    {
        ShieldSources.Remove(guid);
        if (ShieldSources.Count == 0) OnLoseShield?.Invoke();
    }


}