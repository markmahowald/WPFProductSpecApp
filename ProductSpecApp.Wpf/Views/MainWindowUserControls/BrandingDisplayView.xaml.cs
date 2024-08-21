using ProductSpecApp.Wpf.ViewModels.DisplayViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Messaging;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.Messages;

namespace ProductSpecApp.Wpf.Views.MainWindowUserControls
{
    /// <summary>
    /// Interaction logic for BrandingDisplayView.xaml
    /// </summary>
    public partial class BrandingDisplayView : UserControl
    {
        public BrandingDisplayViewModel ViewModel{ get; set; }
        public BrandingDisplayView()
        {
       
            InitializeComponent();
   
        }
    }
}
