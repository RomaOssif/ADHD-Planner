using ADHDPlanner.View.UserControls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using UserControls = ADHDPlanner.View.UserControls;

namespace ADHDPlanner
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<UserControls.Task> tasks = new ObservableCollection<UserControls.Task>();

        public ObservableCollection<UserControls.Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private string timerString;

        public string TimerString
        {
            get { return timerString; }
            set 
            { 
                timerString = value; 
                OnPropertyChanged(); 
            }
        }

        private System.Timers.Timer pomodoroTimer;
        private TimeSpan timeout;
        private bool? isRunning;
        private int sessionFinished = 0;

        private void SetTimer(int miliseconds = 1000)
        {
            borderTimer.Visibility = Visibility.Visible;

            pomodoroTimer = new System.Timers.Timer(miliseconds);
            timeout = new TimeSpan(0, 25, 1);

            pomodoroTimer.AutoReset = true;
            pomodoroTimer.Elapsed += OnTimeElapsed;

            pomodoroTimer.Enabled = true;
            isRunning = true;
        }

        private void OnTimeElapsed(object? source, ElapsedEventArgs e)
        {
            timeout -= new TimeSpan(0, 0, 1);

            if (timeout > TimeSpan.Zero)
            {
                TimerString = timeout.ToString(@"mm\:ss");

                Application.Current.Dispatcher.BeginInvoke(() => Title = "ADHD Planner -- " + TimerString);
            }
            else
            {
                TimerString = "Finished";
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    btnStartTimer.Content = "Start";
                    
                    Title = "ADHD Planner -- session finished!";
                });

                isRunning = null;

                pomodoroTimer.Stop();
                pomodoroTimer.Dispose();

                sessionFinished++;
                Motivate();
            }
        }

        private void btnStartTimer_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning == null)
            {
                SetTimer();

                btnStartTimer.Content = "Stop";
            }
            else if (isRunning == true)
            {
                pomodoroTimer.Stop();

                btnStartTimer.Content = "Resume";
                isRunning = false;
            }
            else
            {
                pomodoroTimer.Start();

                btnStartTimer.Content = "Stop";
                isRunning = true;
            }
        }

        private void Motivate()
        {
            string[] firstSentences =
            {
                "Good job! First session has been finished.",
                "You've already finished your first session, nice!",
                "You're doing great! Wanna start a second session?",
                "To start - is the hardest part. Be proud of yourself, you've already did!",
                "How did your first session go? I know you did pretty well!",
            };

            string[] sentences =
            {
                "Great! You've finished {0} sessions already.",
                "Well done :D {0} sessions are finished.",
                "Keep going! You have completed {0} sessions so far!",
                "I'm proud of you :) You've finished a total of {0} sessions!",
                "Don't forget to take breaks :) It's been {0} sessions already.",
                "Nice, {0} finished sessions already! You can do it!"
            };

            var random = new Random();

            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                if (sessionFinished == 1)
                    tbMotivation.Text = string.Format(firstSentences[random.Next(firstSentences.Length)]);
                else
                    tbMotivation.Text = string.Format(sentences[random.Next(sentences.Length)], sessionFinished);
            });
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string  propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }
    }
}
