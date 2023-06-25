
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int buildIndex;
    public void Load()
    {
        SceneManager.LoadScene(buildIndex);
    }
}
