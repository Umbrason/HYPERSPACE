
using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageReciever
{
    [field: SerializeField] public int MaxHP { get; set; } = 10;
    [field: SerializeField] public int HP { get; private set; }
    [SerializeField] private AudioClip OnHitSFX;
    [SerializeField] private AudioClip OnDeathSFX;
    [SerializeField] private AudioSource SFXSource;
    private void Start() => HP = MaxHP;

    bool takenHitThisFrame = false;
    void FixedUpdate()
    {
        takenHitThisFrame = false;
    }

    public void OnHit(int damage)
    {
        HP -= damage;
        OnHealthChanged?.Invoke(HP);
        if (HP <= 0)
        {
            if (!takenHitThisFrame) SFXSource.PlayOneShot(OnDeathSFX);
            Die?.Invoke();
            takenHitThisFrame = true;
            return;
        }
        if (!takenHitThisFrame) SFXSource.PlayOneShot(OnHitSFX);
        takenHitThisFrame = true;
    }

    public void Heal(int amount)
    {
        this.HP += amount;
        this.HP = Mathf.Clamp(this.HP, 0, MaxHP);
        OnHealthChanged?.Invoke(HP);
    }

    public event Action<int> OnHealthChanged;
    public event Action Die;

}