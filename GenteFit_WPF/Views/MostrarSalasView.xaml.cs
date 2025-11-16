using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class MostrarSalasView : UserControl
    {
        public MostrarSalasView()
        {
            InitializeComponent();
            SalasDataGrid.ItemsSource = GestionSala.ObtenerSalas();
        }
    }
}
