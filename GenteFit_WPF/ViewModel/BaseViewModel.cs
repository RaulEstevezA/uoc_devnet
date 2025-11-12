using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GenteFit_WPF.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private string _titulo = "Título de la vista";
        public string Titulo
        {
            get => _titulo;
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}