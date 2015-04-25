using AzureCards.WindowsStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzureCards.WindowsStoreApp
{
    public static class CardExtension
    {
        public static CardViewModel CreateViewModel(this Card card)
        {
            return new CardViewModel
            {
                Face = card.Face,
                Suit = card.Suit
            };
        }
    }

    public class CardViewModel : ViewModelBase
    {
        private string _suit;

        public string Suit
        {
            get { return _suit; }
            set
            {
                _suit = value;
                OnPropertyChanged("Suit");
            }
        }

        private string _face;

        public string Face
        {
            get { return _face; }
            set
            {
                _face = value;
                OnPropertyChanged("Face");
            }
        }
    }

    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            this.Cards = new ObservableCollection<CardViewModel>();
        }

        private ObservableCollection<CardViewModel> _cards;

        public ObservableCollection<CardViewModel> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                OnPropertyChanged("Cards");
            }
        }

    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            var property = (MemberExpression)expression.Body;
            this.OnPropertyChanged(property.Member.Name);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
