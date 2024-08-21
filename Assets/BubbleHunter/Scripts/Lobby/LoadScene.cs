using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string m_nextScene;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            this.NextScene();
    }

    [ContextMenu("Load")]
    public void NextScene()
    {
        SceneManager.LoadSceneAsync(m_nextScene);
    }
}
