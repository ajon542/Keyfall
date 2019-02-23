using System.Collections.Generic;

public class SequenceTask : ITask
{
    private readonly List<ITask> _children;
    private int _currentChildIndex;
    private TaskStatus _currentStatus;

    public SequenceTask(List<ITask> children)
    {
        _children = children;
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
                return TaskStatus.Success;
            }

            return TaskStatus.Incomplete;
        }

        return childStatus;
    }
}