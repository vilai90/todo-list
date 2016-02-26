using System;
using System.Windows;
using DataProcessor;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        /// <summary>
        ///Id for assigned ToDo item. Default is random Guid.
        /// </summary>
        private Guid addedId = Guid.NewGuid();

        /// <summary>
        /// ToDo item returned by UpdateWindow.
        /// </summary>
        private ToDo toDo = new ToDo();

        /// <summary>
        /// Get accessor for toDo.
        /// </summary>
        public ToDo UtoDo
        {
            get { return toDo; }
            private set { toDo = value; }
        }

        /// <summary>
        /// Constructor for Edit window
        /// </summary>
        /// <param name="toDo">passed in todo item to be edited</param>
        public UpdateWindow( TDList.Update action, ToDo toDo = null)
        {
            InitializeComponent();
            Title = action.ToString();
            if (toDo != null)
            {
                this.toDo = toDo;
                PopulateEditControls();
            }
        }

        /// <summary>
        /// Process actions for Add or Edit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            toDo = new ToDo()
            {
                Name = NameBox.Text,
                Description = DescriptionBox.Text,
                DueDate = DueDateBox.SelectedDate,
                Completed = CompleteCheckBox.IsChecked.Value,
                Id = addedId
            };

            Close();
        }

        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Fill controls with selected item in the event of Edit window.
        /// </summary>
        /// <param name="toDo">selected item</param>
        private void PopulateEditControls()
        {
            NameBox.Text = toDo.Name;
            DescriptionBox.Text = toDo.Description;
            DueDateBox.SelectedDate = toDo.DueDate;
            CompleteCheckBox.IsChecked = toDo.Completed;
            addedId = toDo.Id;
        }
    }
}
