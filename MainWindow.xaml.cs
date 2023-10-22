using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace ADHDPlanner
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();

        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();
        }
    }
}
