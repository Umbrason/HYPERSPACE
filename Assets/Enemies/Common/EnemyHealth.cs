
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageReciever
{
    [SerializeField] private int MaxHP = 10;
    [SerializeField] private AudioClip OnHitSFX;
    [SerializeField] private AudioClip OnDeathSFX;
    [SerializeField] private AudioSource SFXSource;
    private int HP;

    private void Start() => HP = MaxHP;

    public void OnHit(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            SFXSource.PlayOneShot(OnDeathSFX);
            Die?.Invoke();
        }
        else SFXSource.PlayOneShot(OnHitSFX);
    }

    public void Heal(int amount)
    {
        this.HP += amount;
        this.HP = Mathf.Clamp(this.HP, 0, MaxHP);
    }

    public event Action Die;

}