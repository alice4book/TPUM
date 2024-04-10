using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        private Model.Model model;
        private ObservableCollection<ViewModelBook> books;
        private string b_mainViewVisibility;
        private string b_cartViewVisibility;
        private CartPresentation cartPresentation;
        private float cartSum;

        public ViewModel()
        {
            this.model = new Model.Model(null);
            this.model.Refresh += RefreshBooks;
            this.model.StoragePresentation.PriceChanged += HandlePriceChanged;
            this.books = new ObservableCollection<ViewModelBook>();
            MainViewVisibility = this.model.MainViewVisibility;
            CartViewVisibility = this.model.CartViewVisibility;
            cartPresentation = this.model.CartPresentation;
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                books.Add(new ViewModelBook (book));
            }

            HorrorButtonClick = new RelayCommand(HorrorButtonClickHandler);
            ComedyButtonClick = new RelayCommand(ComedyButtonClickHandler);
            CriminalButtonClick = new RelayCommand(CriminalButtonClickHandler);
            RomanceButtonClick = new RelayCommand(RomanceButtonClickHandler);
            FantasyButtonClick = new RelayCommand(FantasyButtonClickHandler);
            AdventureButtonClick = new RelayCommand(AdventureButtonClickHandler);
            CartButtonClick = new RelayCommand(CartButtonClickHandler);
            MainPageButtonClick = new RelayCommand(MainPageButtonClickHandler);
            BuyButtonClick = new RelayCommand(BuyButtonClickHandler);

            ConnectButtonClick = new RelayCommand(ConnectButtonClickHandler);

            BookButtonClick = new ParameterCommand<Guid>(BookButtonClickHandler);
        }

        public ObservableCollection<ViewModelBook> Books
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
        public ICommand CartButtonClick { get; set; }
        public ICommand BookButtonClick { get; set; }
        public ICommand ConnectButtonClick { get; set; }
        public ICommand MainPageButtonClick { get; set; }
        public ICommand BuyButtonClick { get; set; }

        private void ConnectButtonClickHandler()
        {
            this.model.Connect();
        }
        private void CartButtonClickHandler()
        {
            CartViewVisibility = "Visible";
            MainViewVisibility = "Hidden";
        }
        private void MainPageButtonClickHandler()
        {
            CartViewVisibility = "Hidden";
            MainViewVisibility = "Visible";
        }
        private void HorrorButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Horror"))
                    books.Add(new ViewModelBook(book));
            }
        }
        private void ComedyButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Comedy"))
                    books.Add(new ViewModelBook(book));
            }
        }
        private void CriminalButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Criminal"))
                    books.Add(new ViewModelBook(book));
            }
        }
        private void RomanceButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Romance"))
                    books.Add(new ViewModelBook(book));
            }
        }
        private void FantasyButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Fantasy"))
                    books.Add(new ViewModelBook(book));
            }
        }
        private void AdventureButtonClickHandler()
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Type.Equals("Adventure"))
                    books.Add(new ViewModelBook(book));
            }
        }
        public void BookButtonClickHandler(Guid id)
        {
            foreach (BookPresentation book in this.cartPresentation.Books)
            {
                if (book.Id.Equals(id))
                {
                    return;
                }
            }
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                if (book.Id.Equals(id))
                {
                    cartPresentation.Add(book);
                    CartSum = cartPresentation.Sum();
                }
            }
        }
        public void BuyButtonClickHandler()
        {
            CartPresentation.Buy();
            CartSum = cartPresentation.Sum();

            books.Clear();
            foreach (BookPresentation book in model.StoragePresentation.GetBooks())
            {
                books.Add(new ViewModelBook(book));
            }
            if(books.Count == 0)
            {
                OnPropertyChanged("Books");
            }
        }

        public CartPresentation CartPresentation
        {
            get
            {
                return cartPresentation;
            }
            set
            {
                if (value.Equals(cartPresentation))
                    return;
                cartPresentation = value;
                OnPropertyChanged("CartPresentation");
            }
        }
        public string MainViewVisibility
        {
            get
            {
                return b_mainViewVisibility;
            }
            set
            {
                if (value.Equals(b_mainViewVisibility))
                    return;
                b_mainViewVisibility = value;
                OnPropertyChanged("MainViewVisibility");
            }
        }
        public string CartViewVisibility
        {
            get
            {
                return b_cartViewVisibility;
            }
            set
            {
                if (value.Equals(b_cartViewVisibility))
                    return;
                b_cartViewVisibility = value;
                OnPropertyChanged("CartViewVisibility");
            }
        }

        public float CartSum
        {
            get
            {
                return cartSum;
            }
            set
            {
                if (value.Equals(cartSum))
                    return;
                cartSum = value;
                OnPropertyChanged("CartSum");
            }
        }

        private void HandlePriceChanged(object sender, PriceChangeEventArgs args)
        {
            RefreshBooks();
        }

        public void RefreshBooks( )
        {
            books.Clear();
            foreach (BookPresentation book in this.model.StoragePresentation.GetBooks())
            {
                books.Add(new ViewModelBook(book));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
