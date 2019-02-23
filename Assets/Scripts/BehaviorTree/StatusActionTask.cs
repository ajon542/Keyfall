public class StatusActionTask : ITask
{
    private readonly TaskStatus _taskStatus;

    public StatusActionTask(TaskStatus taskStatus)
    {
        _taskStatus = taskStatus;
    }

    public TaskStatus Tick()
    {
        return _taskStatus;
    }
}