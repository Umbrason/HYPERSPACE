
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public Sprite Icon;
    public string Text;
    public abstract void Apply();
}
