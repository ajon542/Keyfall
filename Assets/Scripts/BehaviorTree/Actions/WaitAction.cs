using UnityEngine;

public class WaitAction : ITask
{
    private float _seconds;
    private float _currentTime;
    
    public WaitAction(float seconds)
    {
        _seconds = seconds;
    }

    public TaskStatus Tick()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _seconds)
            return TaskStatus.Success;
        
        return TaskStatus.Incomplete;
    }
}