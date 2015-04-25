using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AzureCards.WindowsStoreApp
{
    public class CardDatabinderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cardViewModel = value as CardViewModel;

            if(cardViewModel != null)
                return string.Format(@"Assets/{0}_of_{1}.png", cardViewModel.Face, cardViewModel.Suit);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
