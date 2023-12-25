using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace ADHDPlanner.View.UserControls
{
    public partial class Task : UserControl, INotifyPropertyChanged
    {
        public enum Stage
        {
            Undefined = 0,
            Defined = 1,
            Finished = 2
        }

        public enum State
        {
            ImportantUrgent = 0,
            ImportantNotUrgent = 1,
            NotImportantUrgent = 2,
            NotImportantNotUrgent = 3
        }

        private State taskState;

        public State TaskState
        {
            get { return taskState; }
            set
            {
                taskState = value;
                OnPropertyChanged();
            }
        }

        private Stage currentStage;

        public Stage CurrentStage
        {
            get { return currentStage; }
            set 
            { 
                currentStage = value;
                OnPropertyChanged();

                ChangeColor();
            }
        }

        public Task()
        {
            DataContext = this;

            CurrentStage = Stage.Undefined;
            TaskState = State.NotImportantNotUrgent;

            InitializeComponent();
        }

        public Task(Stage stage)
        {
            DataContext = this;

            currentStage = stage;

            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string title;

        public string Title
        {
            get { return title; }
            set 
            {
                title = value;
                OnPropertyChanged();
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged();
            }
        }

        private void ChangeColor()
        {
            if (currentStage == Stage.Undefined)
            {
                Color = "#FCE09B"; //FCE09B
            }
            else if (currentStage == Stage.Defined)
            {
                Color = "#F39F5A"; //F39F5A
            }
            else if (currentStage == Stage.Finished)
            {
                Color = "#507B58";
            }
        }

        private string estimatedTime;

        public string EstimatedTime
        {
            get { return estimatedTime; }
            set 
            {
                if (TimeOnly.TryParse(value, out TimeOnly time))
                {
                    estimatedTime = time.ToLongTimeString();
                }
                else
                {
                    estimatedTime = "hh:mm:ss";
                }

                OnPropertyChanged();
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set 
            {
                description = value; 
                OnPropertyChanged();
            }
        }

        private string orderedDescription;

        public string OrderedDescription
        {
            get { return orderedDescription; }
            set 
            { 
                orderedDescription = value; 
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
