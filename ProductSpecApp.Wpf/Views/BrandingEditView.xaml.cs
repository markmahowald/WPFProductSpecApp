using ProductSpecApp.Wpf.ViewModels;
using ProductSpecApp.Wpf.ViewModels.DisplayViewModels;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace ProductSpecApp.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BrandingEditView.xaml
    /// </summary>
    public partial class BrandingEditView : Window
    {
        public BrandingEditView(Branding branding, IProductSpecAppRepo repo)
        {
            BrandingEditViewModel viewModel = new BrandingEditViewModel(repo, branding);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
