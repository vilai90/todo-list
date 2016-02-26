using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    /// <summary>
    /// Class that encapsulates ToDoList functionality.
    /// </summary>
    public class TDList : IToDoList
    {
        /// <summary>
        /// Dictionary from which the listbox will read.
        /// </summary>
        private Dictionary<Guid, ToDo> fileDict = new Dictionary<Guid, ToDo>();

        /// <summary>
        /// Save the list to csv file.
        /// </summary>
        public void Save()
        {
            FileDataSaver.SaveToFile(fileDict);
        }

        /// <summary>
        /// Load the list from csv file.
        /// </summary>
        public void Load()
        {
            fileDict = FileDataSaver.ReadFile();
        }

        /// <summary>
        /// Mark a todo item as complete based on passed in id.
        /// </summary>
        /// <param name="id">guid of todo item</param>
        public void Complete(Guid id)
        {
            fileDict[id].Completed = true;
        }

        /// <summary>
        /// Add passed in todo item to list.
        /// </summary>
        /// <param name="toDo">passed in todo item</param>
        public void Add(ToDo toDo)
        {
            fileDict.Add(toDo.Id, toDo);
        }

        /// <summary>
        /// Delete todo item from list based on passed in guid.
        /// </summary>
        /// <param name="id">passed in guid</param>
        public void Delete(Guid id)
        {
            fileDict.Remove(id);
        }

        /// <summary>
        /// Edit existing todo item based on passed in todo item.
        /// </summary>
        /// <param name="toDo">passed in todo item</param>
        public void Edit(ToDo toDo)
        {
            fileDict[toDo.Id] = toDo;
        }

        /// <summary>
        /// Get the todo item list.
        /// </summary>
        /// <returns>todo item list</returns>
        public Dictionary<Guid, ToDo> GetList()
        {
            return fileDict;
        }

    }
}
