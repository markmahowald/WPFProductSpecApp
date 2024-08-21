using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductSpecApp.Wpf.ViewModels.DisplayViewModels;
using ProductSpecificationApp.Data.Repositories;

namespace ProductSpecApp.Wpf.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _productRepository;

        private object selectedViewModel;
        public object SelectedViewModel
        {
            get => selectedViewModel;
            set => SetProperty(ref selectedViewModel, value);
        }

        public ICommand ShowProductsCommand { get; }
        public ICommand ShowMaterialsCommand { get; }
        public ICommand ShowMoldsCommand { get; }
        public ICommand ShowClientsCommand { get; }
        public ICommand ShowBrandingCommand { get; }

        public MainWindowViewModel(IProductSpecAppRepo repo)
        {
            // Initialize commands
            this._productRepository = repo;
            ShowProductsCommand = new RelayCommand(() => SelectedViewModel = new ProductDisplayViewModel(_productRepository));
            ShowMaterialsCommand = new RelayCommand(() => SelectedViewModel = new MaterialDisplayViewModel(_productRepository));
            ShowMoldsCommand = new RelayCommand(() => SelectedViewModel = new MoldDisplayViewModel(_productRepository));
            ShowClientsCommand = new RelayCommand(() => SelectedViewModel = new ClientDisplayViewModel(_productRepository));
            ShowBrandingCommand = new RelayCommand(() => SelectedViewModel = new BrandingDisplayViewModel(_productRepository));

            SelectedViewModel = new ProductDisplayViewModel(_productRepository); // Default to Products
        }

        private void EditProduct()
        {
            // Logic to open a new window for editing the selected product
        }

        private void EditMaterial()
        {
            // Logic to open a new window for editing the selected material
        }

        private void EditMold()
        {
            // Logic to open a new window for editing the selected mold
        }

        private void EditClient()
        {
            // Logic to open a new window for editing the selected client
        }

        private void EditBranding()
        {
            // Logic to open a new window for editing the selected branding
        }
    }
}