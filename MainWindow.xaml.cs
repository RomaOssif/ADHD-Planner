﻿using ADHDPlanner.View.UserControls;
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

            commandTest.InputGestures.Add(new KeyGesture(Key.W, ModifierKeys.Control));
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
            {
                if (taskView.SelectedItem != null)
                {
                    Tasks.RemoveAt(taskView.SelectedIndex);
                }
            }

            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {
            progressBar.Text = "";

            progressBar.Text += Tasks.Where(e => e.CurrentStage == Task.Stage.Finished).Count();
            progressBar.Text += " / ";
            progressBar.Text += Tasks.Count;
        }

        private void TabItem_Click(object sender, RoutedEventArgs e)
        {
            Tasks.Add(new Task() { Title = "New task" });

            UpdateProgressBar();

            AllTab.Focus();
            taskView.SelectedIndex = Tasks.Count - 1;

            workspace.Title.Focus();
        }

        private void DeleteTabCtrlW(object sender, RoutedEventArgs e)
        {
            if (taskView.SelectedItem != null)
            {
                Tasks.RemoveAt(taskView.SelectedIndex);
                
                workspace.Set();
                UpdateProgressBar();
            }
        }

        public static RoutedCommand commandTest = new RoutedCommand();
    }
}
