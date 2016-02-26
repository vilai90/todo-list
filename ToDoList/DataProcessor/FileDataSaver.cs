using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessor
{
    /// <summary>
    /// Class that saves to and reads from the csv file.
    /// </summary>
    public class FileDataSaver
    {
        /// <summary>
        /// Local file path where csv file is stored.
        /// </summary>
        private static string filePath = @"C:/ToDoData/ToDoList.csv";
        
        /// <summary>
        /// File info object for creating the directory.
        /// </summary>
        private static FileInfo file = new FileInfo(filePath);

        /// <summary>
        /// Read from csv file.
        /// </summary>
        public static Dictionary<Guid, ToDo> ReadFile()
        {
            Dictionary<Guid, ToDo> dict = new Dictionary<Guid, ToDo>();

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            StreamReader reader = new StreamReader(File.OpenRead(filePath));

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                ToDo toDo = new ToDo
                {
                    Name = values[0],
                    Description = values[1],
                    DueDate = string.IsNullOrEmpty(values[2]) ? (DateTime?)null : DateTime.Parse(values[2]),
                    Completed = bool.Parse(values[3]),
                    Id = Guid.Parse(values[4])
                };

                dict.Add(toDo.Id, toDo);
            }
            
            return dict;  
        }

        /// <summary>
        /// Save to csv file.
        /// </summary>
        public static void SaveToFile(Dictionary<Guid, ToDo> fileDict)
        {
            int rowcount = 0;
            string[] rows = new string[fileDict.Count];
            foreach (KeyValuePair<Guid, ToDo> kvp in fileDict)
            {
                string row = kvp.Value.Name + "," + kvp.Value.Description + "," + kvp.Value.DueDate + "," + kvp.Value.Completed + "," + kvp.Key;
                rows[rowcount] = row;
                rowcount++;
            }
            File.WriteAllLines(file.FullName, rows);
        }

    }
}