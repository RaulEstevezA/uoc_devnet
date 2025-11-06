using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GenteFit.src.model.entity;
using GenteFit.src.model.GestionModelo;


namespace GenteFit_WPF.ViewModel
{
    public class SalaViewModel
    {

        public ObservableCollection<Sala> Salas { get; set; }
        public SalaViewModel()
        {
            Salas = new ObservableCollection<Sala>(GestionSala.ObtenerSalas());
        }
    }
}
