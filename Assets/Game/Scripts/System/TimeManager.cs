using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int _minutes;
    private int _seconds;
    private float _elapsedTime;
    private bool isPaused = false;

    [SerializeField] private Clock clock;
    private void Update()
    {
        if (!isPaused)
        {
            UpdateTime();
        }
    }

    private void UpdateTime()
    {
        _elapsedTime += Time.deltaTime;
        _seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _minutes = Mathf.FloorToInt(_elapsedTime / 60);
        clock.UpdateClock(_minutes, _seconds);
    }
}
