using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviorTree : MonoBehaviour
{
    private BehaviourTree _behaviorTree;
    void Start()
    {
        var sequenceTask = new SequenceTask(new List<ITask>
        {
            new LogAction("Child 1"),
            new WaitAction(1),
            new LogAction("Child 2"),
            new WaitAction(1),
            new LogAction("Child 3"),
            new LogAction("Child 4"),
            new LogAction("Child 5"),
            new WaitAction(1),
            new LogAction("Child 6"),
            new LogAction("Child 7"),
            new LogAction("Child 8"),
            new WaitAction(1),
            new LogAction("Child 9"),
        });
        _behaviorTree = new BehaviourTree(sequenceTask);
    }

    private void Update()
    {
        if (_behaviorTree.CurrentStatus == TaskStatus.Incomplete)
            _behaviorTree.Tick();
    }
}
