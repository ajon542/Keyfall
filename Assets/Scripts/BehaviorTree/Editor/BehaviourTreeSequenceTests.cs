using NUnit.Framework;
using System.Collections.Generic;
using RangeAttribute = NUnit.Framework.RangeAttribute;

namespace Tests
{
    public class BehaviourTreeSequenceTests
    {
        [Test]
        public void BehaviourTreeReturnsTaskStatus(
            [Values(TaskStatus.Success, TaskStatus.Failure)] TaskStatus taskStatus)
        {
            var bt = new BehaviourTree(new StatusActionTask(taskStatus));
            Assert.AreEqual(TaskStatus.Incomplete, bt.CurrentStatus);
            Assert.AreEqual(taskStatus, bt.Tick());
            Assert.AreEqual(taskStatus, bt.CurrentStatus);
        }

        [TestCase(TaskStatus.Success, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Failure, ExpectedResult = TaskStatus.Failure)]
        [TestCase(TaskStatus.Incomplete, ExpectedResult = TaskStatus.Incomplete)]
        public TaskStatus SequenceWithOneChildAfterOneTickReturns(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SequenceTask(new List<ITask> {task});
            return sequenceTask.Tick();
        }

        [Test]
        public void SequenceContinuesToReturnResultAfterMultipleTicks(
            [Values(TaskStatus.Success, TaskStatus.Failure)] TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SequenceTask(new List<ITask> {task});
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
        }

        [Test]
        public void SequenceWithSuccessChildrenReturnsIncompleteUntilAllChildrenSucceed(
            [Range(1, 10)] int iterations)
        {
            var childTasks = new List<ITask>();
            for (int i = 0; i < iterations; ++i)
            {
                childTasks.Add(new StatusActionTask(TaskStatus.Success));
            }

            var sequenceTask = new SequenceTask(childTasks);

            for (int i = 0; i < iterations; ++i)
            {
                var taskStatus = sequenceTask.Tick();

                if (i < iterations - 1)
                {
                    Assert.AreEqual(TaskStatus.Incomplete, taskStatus);
                }
                else
                {
                    Assert.AreEqual(TaskStatus.Success, taskStatus);
                }
            }
        }

        [Test]
        public void SequenceWithChildFailureReturnsFailure(
            [Range(1, 10)] int iterations)
        {
            var childTasks = new List<ITask>();
            for (int i = 0; i < iterations; ++i)
            {
                childTasks.Add(i < iterations - 1
                    ? new StatusActionTask(TaskStatus.Success)
                    : new StatusActionTask(TaskStatus.Failure));
            }

            var sequenceTask = new SequenceTask(childTasks);

            for (int i = 0; i < iterations; ++i)
            {
                var taskStatus = sequenceTask.Tick();

                if (i < iterations - 1)
                {
                    Assert.AreEqual(TaskStatus.Incomplete, taskStatus);
                }
                else
                {
                    Assert.AreEqual(TaskStatus.Failure, taskStatus);
                }
            }
        }

        [Test]
        public void SequenceWithChildrenIncompleteReturnsIncomplete(
            [Range(1, 10)] int iterations)
        {
            var childTasks = new List<ITask>();
            for (int i = 0; i < iterations; ++i)
            {
                childTasks.Add(new StatusActionTask(TaskStatus.Incomplete));
            }

            var sequenceTask = new SequenceTask(childTasks);

            for (int i = 0; i < 1000; ++i)
            {
                Assert.AreEqual(TaskStatus.Incomplete, sequenceTask.Tick());
            }
        }
    }
}