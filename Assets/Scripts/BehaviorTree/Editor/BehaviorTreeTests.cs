using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class BehaviourTreeTests
    {   
        [TestCase(TaskStatus.Success, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Failure, ExpectedResult = TaskStatus.Failure)]
        [TestCase(TaskStatus.Incomplete, ExpectedResult = TaskStatus.Incomplete)]
        public TaskStatus SelectorWithOneChildAfterOneTickReturns(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SelectorTask(new List<ITask> { task });
            return sequenceTask.Tick();
        }
        
        [Test]
        public void SelectorWithMultipleChildrenReturningSuccessReturnsSuccessImmediately()
        {
            var childTasks = new List<ITask>();
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            
            var selectorTask = new SelectorTask(childTasks);
            Assert.AreEqual(TaskStatus.Success, selectorTask.Tick());
        }
        
        [Test]
        public void SelectorWithChildReturningSuccessReturnsSuccessAfterThatChildIsTicked()
        {
            var childTasks = new List<ITask>();
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            childTasks.Add(new StatusActionTask(TaskStatus.Success));
            
            var selectorTask = new SelectorTask(childTasks);
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Success, selectorTask.Tick());
        }
        
        [Test]
        public void SelectorWithAllChildrenFailingReturnsFailure()
        {
            var childTasks = new List<ITask>();
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            childTasks.Add(new StatusActionTask(TaskStatus.Failure));
            
            var selectorTask = new SelectorTask(childTasks);
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Failure, selectorTask.Tick());
        }
        
        [Test]
        public void SelectorWithAllChildrenIncompleteReturnsIncomplete()
        {
            var childTasks = new List<ITask>();
            childTasks.Add(new StatusActionTask(TaskStatus.Incomplete));
            childTasks.Add(new StatusActionTask(TaskStatus.Incomplete));
            childTasks.Add(new StatusActionTask(TaskStatus.Incomplete));
            
            var selectorTask = new SelectorTask(childTasks);
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
            Assert.AreEqual(TaskStatus.Incomplete, selectorTask.Tick());
        }
        
        [Test]
        public void SelectorContinuesToReturnResultAfterMultipleTicks(
            [Values(TaskStatus.Success, TaskStatus.Failure)]TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SelectorTask(new List<ITask> { task });
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
        }
        
        [TestCase(TaskStatus.Success, ExpectedResult = TaskStatus.Failure)]
        [TestCase(TaskStatus.Failure, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Incomplete, ExpectedResult = TaskStatus.Incomplete)]
        public TaskStatus InverterTaskReturnsInvertedStatusForSuccessAndFailure(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new InverterTask(task);
            return sequenceTask.Tick();
        }
        
        [TestCase(TaskStatus.Success, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Failure, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Incomplete, ExpectedResult = TaskStatus.Incomplete)]
        public TaskStatus SucceederTaskReturnsSuccessStatusForSuccessAndFailure(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SucceederTask(task);
            return sequenceTask.Tick();
        }
    }
}