using CoaxarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoaxarApp
{
    public partial class ListPage : ContentPage
    {
        private readonly CoaxarViewModel _viewModel;

        public ListPage()
        {
            InitializeComponent();

            _viewModel = new CoaxarViewModel();
            BindingContext = _viewModel;

            var listView = new ListView()
            {
                ItemsSource = _viewModel.Families,
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, ".");
                    return textCell;
                })
            };

            Content = listView;
        }
    }
}
