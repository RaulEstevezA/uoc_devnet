using System.Windows.Controls;
using GenteFit.src.model.GestionModelo;

namespace GenteFit_WPF.Views
{
    public partial class MostrarActividadesView : UserControl
    {
        public MostrarActividadesView()
        {
            InitializeComponent();
            ActividadesDataGrid.ItemsSource = GestionActividad.GetAll();
        }
    }
}
