using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataProcessor;

namespace ToDoList
{
    /// <summary>
    /// InterUpdate logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main list that populates TaskBox.
        /// </summary>
        private TDList MainList = new TDList();

        /// <summary>
        /// MainWindow constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates to take when Window has loaded successfully.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainList.Load();
            Refresh(); 
        }

        /// <summary>
        /// Refresh controls.
        /// </summary>
        public void Refresh()
        {
            DateLabel.Content = DateTime.Today;
            PopulateTaskBox();
            BtnCheck();
        }

        /// <summary>
        /// Add new to do item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateWindow UpdateWindow = new UpdateWindow(TDList.Update.Add);
            UpdateWindow.ShowDialog();
            MainList.Add(UpdateWindow.UtoDo);
            Refresh();
        }

        /// <summary>
        /// Delete selected to do item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            Popup deletePopup = new Popup(TDList.Update.Delete);
            deletePopup.ShowDialog();
            if (deletePopup.Confirm)
            {
                ListBoxItem listBoxItem = TaskBox.SelectedItem as ListBoxItem;
                Guid id = Guid.Parse(listBoxItem.Tag.ToString());
                MainList.Delete(id);
            }
            Refresh();
        }

        /// <summary>
        /// Complete selected to do item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            Popup completePopup = new Popup(TDList.Update.Complete);
            completePopup.ShowDialog();
            if (completePopup.Confirm)
            {
                ListBoxItem listBoxItem = TaskBox.SelectedItem as ListBoxItem;
                Guid id = Guid.Parse(listBoxItem.Tag.ToString());
                MainList.Complete(id);
            }
            Refresh();
        }

        /// <summary>
        /// Check to enable or disable complete and delete buttons.
        /// </summary>
        private void BtnCheck()
        {
            bool shouldEnable = true;
            if (TaskBox.Items.Count == 0 || TaskBox.SelectedItem == null)
            {
                shouldEnable = false;
            }

            DeleteTaskBtn.IsEnabled = shouldEnable;
            CompleteTaskBtn.IsEnabled = shouldEnable;
        }

        /// <summary>
        /// Clear and then fill listBox from local dictionary.
        /// </summary>
        private void PopulateTaskBox()
        {
            TaskBox.Items.Clear();
            foreach (KeyValuePair<Guid, ToDo> kvp in MainList.GetList())
            {
                ToDo toDo = new ToDo();
                toDo = kvp.Value;
                
                ListBoxItem item = new ListBoxItem
                {
                    Content = toDo.Name + "|" + toDo.Description + "| Due: " + toDo.DueDate,
                    Tag = kvp.Key
                };

                //Overdue
                if (toDo.DueDate < DateTime.Today)
                {
                    item.Background = Brushes.Yellow;
                }

                //Completed
                if (toDo.Completed == true)
                {
                    item.Background = Brushes.Green;
                }

                item.MouseDoubleClick += item_MouseDoubleClick;

                TaskBox.Items.Add(item);
            }
        }

       /// <summary>
       /// Event handler for when listboxitems are double clicked/edit window trigger.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
       {
            ListBoxItem listBoxItem = sender as ListBoxItem;
            string[] contentStr = listBoxItem.Content.ToString().Split('|');
            ToDo existingToDo = new ToDo();

            //Populate existing to do item with parameters extracted from selected item content
            existingToDo.Name = contentStr[0]; 
            existingToDo.Description = contentStr[1]; 

            if (contentStr[2] == " Due: ")
            {
                contentStr[2] = null;
            }
            else
            {
                existingToDo.DueDate = DateTime.Parse(contentStr[2].Replace("Due:", ""));
            }
            
            if (listBoxItem.Background == Brushes.Green)
            {
                existingToDo.Completed = true;
            }
            else
            {
                existingToDo.Completed = false;
            }
            existingToDo.Id = Guid.Parse(listBoxItem.Tag.ToString());
            UpdateWindow editWindow = new UpdateWindow(TDList.Update.Edit, existingToDo);
            editWindow.ShowDialog();
            MainList.Edit(editWindow.UtoDo);
            Refresh();
        }
        
       /// <summary>
       /// Enable or disable delete and complete buttons.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void TaskBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           BtnCheck();
       }

       /// <summary>
       /// Save local dictionary to csv file upon app close.
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void Window_Closed(object sender, EventArgs e)
       {
           MainList.Save();
       }
    }
}
