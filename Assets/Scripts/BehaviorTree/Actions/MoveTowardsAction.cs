using UnityEngine;
using DG.Tweening;

public class MoveTowardsAction : ITask
{
    private readonly Transform _sourceTransform;
    private readonly Transform _targetTransform;
    private readonly float _duration;
    private TaskStatus _taskStatus;

    private bool _firstRun;
    
    public MoveTowardsAction(Transform sourceTransform, Transform targetTransform, float duration)
    {
        _sourceTransform = sourceTransform;
        _targetTransform = targetTransform;
        _duration = duration;
        Reset();
    }

    public TaskStatus Tick()
    {
        if (_firstRun)
            Start();

        if (_taskStatus == TaskStatus.Success)
        {
            Reset();
            return TaskStatus.Success;
        }
        return _taskStatus;
    }

    private void Start()
    {
        _firstRun = false;
        _sourceTransform.DOMove(_targetTransform.position, _duration)
            .OnComplete(() => _taskStatus = TaskStatus.Success);
    }

    private void Reset()
    {
        _firstRun = true;
        _taskStatus = TaskStatus.Incomplete;
    }
}