public class InverterTask : ITask
{
    private readonly ITask _child;

    public InverterTask(ITask child)
    {
        _child = child;
    }

    public TaskStatus Tick()
    {
        var childStatus = _child.Tick();

        if (childStatus == TaskStatus.Success)
        {
            return TaskStatus.Failure;
        }

        if (childStatus == TaskStatus.Failure)
        {
            return TaskStatus.Success;
        }

        return childStatus;
    }
}
