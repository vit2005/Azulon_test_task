using UnityEngine;

public class RunnerHelper : MonoBehaviour 
{
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
