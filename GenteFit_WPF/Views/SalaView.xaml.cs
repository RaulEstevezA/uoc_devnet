using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GenteFit_WPF.ViewModel;

namespace GenteFit_WPF.Views
{
    public partial class SalaView : UserControl
    {
        public SalaView()
        {
            InitializeComponent();
            DataContext = new SalaViewModel();
        }
    }
}
