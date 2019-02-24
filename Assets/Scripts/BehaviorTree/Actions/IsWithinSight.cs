using UnityEngine;

public class IsWithinSight : ITask
{
    private readonly Transform _sourceTransform;
    private readonly Transform _targetTransform;
    private readonly float _sqrDistance;
    private TaskStatus _taskStatus;

    public IsWithinSight(Transform sourceTransform, Transform targetTransform, float distance)
    {
        _sourceTransform = sourceTransform;
        _targetTransform = targetTransform;
        _sqrDistance = distance * distance;
        _taskStatus = TaskStatus.Incomplete;
    }

    public TaskStatus Tick()
    {
        var v = _sourceTransform.position - _targetTransform.position;
        if (v.sqrMagnitude <= _sqrDistance)
        {
            _taskStatus = TaskStatus.Incomplete;
            return TaskStatus.Success;
        }

        return _taskStatus;
    }
}