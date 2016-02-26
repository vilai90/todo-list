using System;
using System.Collections.Generic;

namespace DataProcessor
{
    /// <summary>
    /// Interface for ToDoList.
    /// </summary>
    public interface IToDoList
    {
        void Save();
        void Load();
        void Complete(Guid id);
        void Add(ToDo toDo);
        void Delete(Guid id);
        void Edit(ToDo toDo);
        Dictionary<Guid, ToDo> GetList();
    }
}
