using ProductSpecificationApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProductSpecApp.Wpf.ViewModels;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.Repositories;

namespace ProductSpecApp.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ClientEditView.xaml
    /// </summary>
    public partial class ClientEditView : Window
    {
        public ClientEditView(Client client, IProductSpecAppRepo repo)
        {
            ClientEditViewModel viewModel = new ClientEditViewModel(repo, client);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
