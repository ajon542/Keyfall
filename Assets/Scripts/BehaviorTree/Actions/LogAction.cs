using UnityEngine;

public class LogAction : ITask
{
    private string _message;
    
    public LogAction(string message)
    {
        _message = message;
    }

    public TaskStatus Tick()
    {
        Debug.Log(_message);
        return TaskStatus.Success;
    }
}