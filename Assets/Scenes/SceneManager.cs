using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private int GAMEPLAY_SCENE_INDEX = 1;

    public static SceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameplayScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(GAMEPLAY_SCENE_INDEX);

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
