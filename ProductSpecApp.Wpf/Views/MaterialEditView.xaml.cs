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
    /// Interaction logic for MaterialEditWindow.xaml
    /// </summary>
    public partial class MaterialEditView : Window
    {
        public MaterialEditView(Material material, IProductSpecAppRepo repo)
        {
            MaterialEditViewModel viewModel = new MaterialEditViewModel( repo, material);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
