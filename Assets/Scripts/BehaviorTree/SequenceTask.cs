using System.Collections.Generic;

public class SequenceTask : ITask
{
    private readonly List<ITask> _children;
    private int _currentChildIndex;
    private TaskStatus _currentStatus;
    private bool _resettable;
    
    public SequenceTask(List<ITask> children, bool resettable = false)
    {
        _children = children;
        _resettable = resettable;
    }

    public TaskStatus Tick()
    {
        if (_currentChildIndex >= _children.Count)
            return _currentStatus;

        var childStatus = _children[_currentChildIndex].Tick();
        _currentStatus = childStatus;

        if (childStatus == TaskStatus.Success)
        {
            _currentChildIndex++;
            if (_currentChildIndex >= _children.Count)
            {
                Complete();
                return TaskStatus.Success;
            }

            return TaskStatus.Incomplete;
        }

        return childStatus;
    }

    private void Complete()
    {
        if (!_resettable)
            return;
        _currentChildIndex = 0;
        _currentStatus = TaskStatus.Incomplete;
    }
}