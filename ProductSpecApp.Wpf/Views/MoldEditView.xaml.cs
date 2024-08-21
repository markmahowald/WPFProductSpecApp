using ProductSpecApp.Wpf.ViewModels;
using ProductSpecificationApp.Data.BusinessObjects;
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

namespace ProductSpecApp.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MoldEditView.xaml
    /// </summary>
    public partial class MoldEditView : Window
    {
        public MoldEditView(Mold mold, IProductSpecAppRepo repo)
        {
            MoldEditViewModel viewModel = new MoldEditViewModel(repo, mold);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
