using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BookPresentation> books;
        private readonly Model.Model model;
        public ICommand BookButtonClick { get; set; }

        public ViewModel()
        {
            this.model = new Model.Model(null);
            this.books = new ObservableCollection<BookPresentation>(); 
            foreach (BookPresentation book in model.storagePresentation.GetBooks())
            {
                Books.Add(book);
            }
            Console.WriteLine(Books.Count);
        }

        public ObservableCollection<BookPresentation> Books
        {
            get
            {
                return books;
            }
            set
            {
                if (value.Equals(books))
                    return;
                books = value;
                OnPropertyChanged("Books");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
