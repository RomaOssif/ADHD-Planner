using System.Windows.Controls;

namespace ADHDPlanner.View.UserControls
{
    public partial class Workspace : UserControl
    {
        private Task currentTask;

        public Workspace()
        {
            DataContext = this;

            InitializeComponent();
        }

        //private void SetTask()
        //{
        //    //currentTask = taskView //??
        //}
    }
}
