using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class MostrarSalasView : UserControl
    {
        public MostrarSalasView()
        {
            InitializeComponent();

            // se puede usar GetAll() o ObtenerSalas(), ambas existen
            SalasDataGrid.ItemsSource = GestionSala.ObtenerSalas();
        }
    }
}

