using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private void Awake()
    {
        var runner = new GameObject("CoroutinesRunner");
        var instance = Instantiate(runner);
        var component = instance.AddComponent<RunnerHelper>();
        DontDestroyOnLoad(instance);

        var gameController = new GameController(OnInitialized, component);
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene("Main");
    }
}
