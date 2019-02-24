using System.Collections.Generic;

public class SelectorTask : ITask
{
    private readonly List<ITask> _children;
    private int _currentChildIndex;
    private TaskStatus _currentStatus;
    private bool _resettable;

    public SelectorTask(List<ITask> children, bool resettable = false)
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

        if (childStatus == TaskStatus.Failure)
        {
            _currentChildIndex++;
            if (_currentChildIndex >= _children.Count)
            {
                Complete();
                return TaskStatus.Failure;
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