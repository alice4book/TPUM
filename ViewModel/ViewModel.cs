using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        private Model.Model model;
        private ObservableCollection<BookPresentation> books;

        public ViewModel()
        {
            this.model = new Model.Model(null);
            this.books = new ObservableCollection<BookPresentation>();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                books.Add(book);
            }

            HorrorButtonClick = new RelayCommand(HorrorButtonClickHandler);
            ComedyButtonClick = new RelayCommand(ComedyButtonClickHandler);
            CriminalButtonClick = new RelayCommand(CriminalButtonClickHandler);
            RomanceButtonClick = new RelayCommand(RomanceButtonClickHandler);
            FantasyButtonClick = new RelayCommand(FantasyButtonClickHandler);
            AdventureButtonClick = new RelayCommand(AdventureButtonClickHandler);
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
        public ICommand HorrorButtonClick { get; set; }
        public ICommand ComedyButtonClick { get; set; }
        public ICommand CriminalButtonClick { get; set; }
        public ICommand RomanceButtonClick { get; set; }
        public ICommand FantasyButtonClick { get; set; }
        public ICommand AdventureButtonClick { get; set; }

        private void HorrorButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Horror"))
                    books.Add(book);
            }
        }
        private void ComedyButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Comedy"))
                    books.Add(book);
            }
        }
        private void CriminalButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Criminal"))
                    books.Add(book);
            }
        }
        private void RomanceButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Romance"))
                    books.Add(book);
            }
        }
        private void FantasyButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Fantasy"))
                    books.Add(book);
            }
        }
        private void AdventureButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Adventure"))
                    books.Add(book);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
