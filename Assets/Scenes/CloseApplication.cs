
using UnityEngine;

public class CloseApplication : MonoBehaviour
{
    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
