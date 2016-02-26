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
        private bool confirm = false;
    
        /// <summary>
        /// Get accessor for confirm.
        /// </summary>
        public bool Confirm
        {
            get { return confirm; }
            private set { confirm = value; }
        }

        /// <summary>
        /// Constructor for deleting or completing a selected item.
        /// </summary>
        /// <param name="tag">selected item id</param>
        /// <param name="Update">delete or edit enumeration</param>
        public Popup(TDList.Update action)
        {
            InitializeComponent();
            SetControls(action);
        }

        /// <summary>
        /// Set warning label and title depending on selected item enumeration.
        /// </summary>
        /// <param name="Update">delete or edit enumeration</param>
        public void SetControls(TDList.Update action)
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
            confirm = true;
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
