using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainTheme : MonoBehaviour
{
    private static MainTheme Instance;

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
}
