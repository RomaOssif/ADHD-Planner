using ADHDPlanner.View.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

            doStuff();

            InitializeComponent();
        }

        private void doStuff()
        {
            tasks.Add(new UserControls.Task() { Title = "Task number 1", Color = "#BFA8BB", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1 +ask number 1 +ask number 1 +ask number 1 + Task number 1 + Task number 1",
                Color = "#FFA836", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", CurrentStage = Task.Stage.Finished, Color = "#90B494", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });
            tasks.Add(new UserControls.Task() { Title = "Task number 1", EstimatedTime = "1:25:33" });

            tasks.Add(new UserControls.Task() { Title = "Task number 1",
                EstimatedTime = "1:25:33", TaskState = Task.State.ImportantNotUrgent, CurrentStage = Task.Stage.Defined });
        }

        private void taskView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (taskView.SelectedItem != null)
            {
                workspace.CurrentTask = (Task)taskView.SelectedItem;
            }
        }
    }
}
