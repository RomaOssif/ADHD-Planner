using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace ADHDPlanner.View.UserControls
{
    public partial class Workspace : UserControl, INotifyPropertyChanged
    {
        private Task currentTask;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task CurrentTask
        {
            get { return currentTask; }
            set 
            { 
                currentTask = value;
                Set(currentTask.CurrentStage);

                OnPropertyChanged();
            }
        }

        public Workspace()
        {
            DataContext = this;

            InitializeComponent();
        }

        public void Set(Task.Stage stage)
        {
            Title.Text = CurrentTask.Title;

            descriptionTBox.Text = CurrentTask.Description;
            listTBox.Text = CurrentTask.OrderedDescription;

            if (currentTask.EstimatedTime != "hh:mm:ss")
                timeTB.Text = CurrentTask.EstimatedTime;

            stageCB.SelectedIndex = (int)currentTask.CurrentStage;
            stateCB.SelectedIndex = (int)currentTask.TaskState;

            if (stage == Task.Stage.Undefined)
                btnUpdate.Content = "Define";
            else if (stage == Task.Stage.Defined)
                btnUpdate.Content = "Finish";
            else
                btnUpdate.Content = "Well done!";
        }

        public void SaveTask()
        {
            if (CurrentTask != null)
            {
                currentTask.Title = Title.Text;

                currentTask.Description = descriptionTBox.Text;
                currentTask.OrderedDescription = listTBox.Text;

                currentTask.EstimatedTime = timeTB.Text;

                currentTask.CurrentStage = (Task.Stage)stageCB.SelectedIndex;
                currentTask.TaskState = (Task.State)stateCB.SelectedIndex;
            }
        }

        public void SetTask(Task task)
        {
            CurrentTask = task;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
