using ADHDPlanner.View.UserControls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UserControls = ADHDPlanner.View.UserControls;

namespace ADHDPlanner
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<UserControls.Task> tasks = new ObservableCollection<UserControls.Task>();

        public ObservableCollection<UserControls.Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public MainWindow()
        {
            DataContext = this;

            Setup();

            InitializeComponent();
        }

        private void Setup()
        {
            tasks.Add(new UserControls.Task() { Title = "Task #1", EstimatedTime = "hh:mm:ss" });

            deleteTask.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control)); //CTRL + W = DELETE
            saveTask.InputGestures  .Add(new KeyGesture(Key.S, ModifierKeys.Control)); //CTRL + S = SAVE
            createTask.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control)); //CTRL + T = CREATE
        }

        private void taskView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (taskView.SelectedItem != null)
            {
                workspace.SaveTask();
                workspace.CurrentTask = (Task)taskView.SelectedItem;
            }
        }

        private void workspace_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: X")
                DeleteTask();
        }

        private void AddTab_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void UpdateProgressBar()
        {
            progressBar.Text = $"{Tasks.Where(e => e.CurrentStage == Task.Stage.Finished).Count()} / {Tasks.Count}";
        }

        private void AddTask()
        {
            Tasks.Add(new Task() { Title = "New task" });

            UpdateProgressBar();

            AllTab.Focus();
            taskView.SelectedIndex = Tasks.Count - 1;

            workspace.Title.Focus();
        }

        private void DeleteTask()
        {
            if (taskView.SelectedItem != null)
            {
                Tasks.RemoveAt(taskView.SelectedIndex);

                workspace.Set();
                UpdateProgressBar();
            }
        }

        private void taskView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (taskView.SelectedItem != null)
            {
                workspace.UpdateTask();

                UpdateProgressBar();
            }
        }

        private void DeleteTabCtrlW(object sender, RoutedEventArgs e)
        {
            DeleteTask();
        }

        private void CreateTaskCtrlT(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void SaveTaskCtrlS(object sender, RoutedEventArgs e)
        {
            if (taskView.SelectedItem != null)
            {
                workspace.SaveTask();
            }
        }

        public static RoutedCommand deleteTask = new RoutedCommand();
        public static RoutedCommand saveTask = new RoutedCommand();
        public static RoutedCommand createTask = new RoutedCommand();
    }
}
