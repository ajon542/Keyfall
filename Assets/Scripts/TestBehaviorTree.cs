using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviorTree : MonoBehaviour
{
    private BehaviourTree _behaviorTree;

    public Transform _sourceTransform;
    public Transform _targetTransform;
    
    void Start()
    {
        var sequenceTask = new SequenceTask(new List<ITask>
        {
            new SucceederTask(new IsWithinSight(_sourceTransform, _targetTransform, 5)),
            new MoveTowardsAction(_sourceTransform, _targetTransform, 5)
        }, true);
        _behaviorTree = new BehaviourTree(sequenceTask);
        
        // Idle until player is within sight
    }

    private void Update()
    {
        //if (_behaviorTree.CurrentStatus == TaskStatus.Incomplete)
            _behaviorTree.Tick();
    }
}
