using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class MostrarSesionesView : UserControl
    {
        public MostrarSesionesView()
        {
            InitializeComponent();
            SesionesDataGrid.ItemsSource = GestionSesion.ObtenerSesiones();
        }
    }
}



