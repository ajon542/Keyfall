using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class BehaviourTreeTests
    {
        [Test]
        public void BehaviourTreeWithNoTasksReturnsSuccessAfterFirstTick()
        {
            var bt = new BehaviourTree();
            Assert.AreEqual(TaskStatus.Incomplete, bt.CurrentStatus);
            Assert.AreEqual(TaskStatus.Success, bt.Tick());
            Assert.AreEqual(TaskStatus.Success, bt.CurrentStatus);
        }
        
        [TestCase(TaskStatus.Success, ExpectedResult = TaskStatus.Success)]
        [TestCase(TaskStatus.Failure, ExpectedResult = TaskStatus.Failure)]
        [TestCase(TaskStatus.Incomplete, ExpectedResult = TaskStatus.Incomplete)]
        public TaskStatus SequenceWithOneChildAfterOneTickReturns(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SequenceTask(new List<ITask> { task });
            return sequenceTask.Tick();
        }
        
        [TestCase(TaskStatus.Success)]
        [TestCase(TaskStatus.Failure)]
        public void SequenceContinuesToReturnResultAfterMultipleTicks(TaskStatus taskStatus)
        {
            var task = new StatusActionTask(taskStatus);
            var sequenceTask = new SequenceTask(new List<ITask> { task });
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
            Assert.AreEqual(taskStatus, sequenceTask.Tick());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void SequenceWithSuccessChildrenReturnsIncompleteUntilAllChildrenSucceed(int iterations)
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
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void SequenceWithChildFailureReturnsFailure(int iterations)
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
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void SequenceWithChildrenIncompleteReturnsIncomplete(int iterations)
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
        
        [TestCase(TaskStatus.Success)]
        [TestCase(TaskStatus.Failure)]
        public void SelectorContinuesToReturnResultAfterMultipleTicks(TaskStatus taskStatus)
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