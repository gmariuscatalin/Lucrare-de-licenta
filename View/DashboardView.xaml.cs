using NewBank2.ViewModel;
using System.Windows.Controls;

namespace NewBank2.View
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
