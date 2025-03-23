using UnityEngine;

public class Fps : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = int.MaxValue;
        Application.runInBackground = true;
        Time.timeScale = 1;
    }
}
