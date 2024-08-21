using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string m_nextScene;

    [ContextMenu("Load")]
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(m_nextScene);
    }
}
