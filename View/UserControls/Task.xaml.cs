using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace ADHDPlanner.View.UserControls
{
    public partial class Task : UserControl, INotifyPropertyChanged
    {
        public Task()
        {
            DataContext = this;

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
                    estimatedTime = "-----";
                }

                OnPropertyChanged();
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //ordered description?

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
