using System;
using DataProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoListTest
{
    [TestClass]
    public class TDListTest
    {
        [TestMethod]
        public void SaveTest()
        {
            ToDo testToDo = new ToDo();
            TDList testList = new TDList();
            testToDo.Id = Guid.NewGuid();
            testList.Add(testToDo);
            testList.Save();
            testList.Load();
            Assert.IsNotNull(testList.GetList(), "GetList was null.");
        }

        [TestMethod]
        public void CompleteTest()
        {
            ToDo testToDo = new ToDo();
            TDList testList = new TDList();
            testToDo.Id = Guid.NewGuid();
            testList.Add(testToDo);
            testList.Complete(testToDo.Id);
            Assert.IsTrue(testList.GetList().Count == 1, "ToDo item was not added.");
            Assert.IsTrue(testList.GetList()[testToDo.Id].Completed, "Status was not marked complete.");
        }

        [TestMethod]
        public void AddTest()
        {
            ToDo testToDo = new ToDo();
            TDList testList = new TDList();
            testToDo.Id = Guid.NewGuid();
            testList.Add(testToDo);
            Assert.IsTrue(testList.GetList().Count == 1, "ToDo item was not added.");
        }

        [TestMethod]
        public void DeleteTest()
        {
            ToDo testToDo = new ToDo();
            TDList testList = new TDList();
            testToDo.Id = Guid.NewGuid();
            testList.Add(testToDo);
            Assert.IsTrue(testList.GetList().Count == 1, "ToDo item was not added.");
            testList.Delete(testToDo.Id);
            Assert.IsTrue(testList.GetList().Count == 0, "ToDo item was not deleted.");
        }

        [TestMethod]
        public void EditTest()
        {
            ToDo testToDo = new ToDo();
            TDList testList = new TDList();
            testToDo.Id = Guid.NewGuid();
            testList.Add(testToDo);
            Assert.IsTrue(testList.GetList().Count == 1, "ToDo item was not added.");
            testToDo.Name = "test";
            testToDo.Description = "test";
            testList.Edit(testToDo);
            Assert.IsTrue(testList.GetList()[testToDo.Id].Name == "test", "Name was not edited.");
            Assert.IsTrue(testList.GetList()[testToDo.Id].Description == "test", "Description was not edited.");

        }

        [TestMethod]
        public void GetListTest()
        {
            TDList testList = new TDList();
            testList.Load();
            Assert.IsNotNull(testList.GetList(), "GetList was null.");
        }
    }
}
