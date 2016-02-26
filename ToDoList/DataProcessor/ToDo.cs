using System;

namespace DataProcessor
{
    /// <summary>
    /// Class object for to do items.
    /// </summary>
    public class ToDo
    {
        /// <summary>
        /// Item Name.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Item Description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Item Due Date.
        /// </summary>
        public DateTime? DueDate { get; set; }
        
        /// <summary>
        /// Item Complete status.
        /// </summary>
        public bool Completed { get; set; }
        
        /// <summary>
        /// Item Id.
        /// </summary>
        public Guid Id { get; set; }
    }
}