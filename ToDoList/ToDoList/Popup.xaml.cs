using System;
using System.Windows;
using DataProcessor;

namespace ToDoList
{
    /// <summary>
    /// InterUpdate logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
       /// <summary>
       /// Confirm if complete or delete should be processed or not.
       /// </summary>
        public bool Confirm = false;
    
        /// <summary>
        /// Constructor for deleting or completing a selected item.
        /// </summary>
        /// <param name="tag">selected item id</param>
        /// <param name="Update">delete or edit enumeration</param>
        public Popup(FileDataSaver.Update action)
        {
            InitializeComponent();
            SetControls(action);
            
        }

        /// <summary>
        /// Set warning label and title depending on selected item enumeration.
        /// </summary>
        /// <param name="Update">delete or edit enumeration</param>
        public void SetControls(FileDataSaver.Update action)
        {
            Title = action.ToString();      
            WarningLabel.Content = "Are you sure you want to " + action.ToString() + " the selected item?";    
        }

        /// <summary>
        /// Update selected item in local list according to enumeration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Confirm = true;
            Close();
        }

        /// <summary>
        /// Close window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
