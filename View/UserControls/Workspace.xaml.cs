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
            if (stage == Task.Stage.Undefined)
            {
                Title.Text = "Name...";
                btnUpdate.Content = "Define";
                
                descriptionTBox.Text = "Type here :)";
                listTBox.Text = "Second space for typing. . .";

                timeTB.Text = "hh:mm:ss";
                stageCB.SelectedItem = stageCB.Items[0];
                stateCB.SelectedItem = stateCB.Items[3];
            }
            else if (stage == Task.Stage.Defined)
            {
                Title.Text = CurrentTask.Title;
                btnUpdate.Content = "Finish";

                descriptionTBox.Text = CurrentTask.Description;
                listTBox.Text = CurrentTask.OrderedDescription;

                if (currentTask.EstimatedTime != "hh:mm:ss")
                    timeTB.Text = CurrentTask.EstimatedTime;

                stageCB.SelectedItem = currentTask.CurrentStage; //to fix!!
                stateCB.SelectedItem = currentTask.TaskState; //to fix!!
            }
            else
            {
                Title.Text = CurrentTask.Title;
                btnUpdate.Content = "Well done!";

                descriptionTBox.Text = CurrentTask.Description;
                listTBox.Text = CurrentTask.OrderedDescription;

                if (currentTask.EstimatedTime != "hh:mm:ss")
                    timeTB.Text = CurrentTask.EstimatedTime;

                stageCB.SelectedValue = currentTask.CurrentStage; //to fix!!
                stateCB.SelectedItem = Task.Stage.Finished; //to fix!!
            }


        }

        private void SetTask(Task task)
        {
            CurrentTask = task;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
