using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.Repositories;
using CommunityToolkit.Mvvm.Input;
using ProductSpecApp.Wpf.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProductSpecificationApp.Data.Messages;


namespace ProductSpecApp.Wpf.ViewModels.DisplayViewModels
{
    public class ProductDisplayViewModel
    {
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        private readonly IProductSpecAppRepo _productRepository;

        public ProductDisplayViewModel(IProductSpecAppRepo repo)
        {
            this._productRepository = repo;

            LoadProducts();
            WeakReferenceMessenger.Default.Register<SaveSuccessfulMessage>(this, (r, m) =>
            {
                if (m.Value.ObjectType == typeof(Product))
                {
                    // Refresh the materials list
                    LoadProducts();
                }
            });

        }

        public ICommand EditProductCommand => new RelayCommand<object>(EditProduct);

        public void EditProduct(object productFromGrid)
        {
            Product product = productFromGrid as Product;
            var editViewModel = new ProductEditViewModel(this._productRepository, product);
            var editView = new ProductEditView(product, _productRepository);
            editView.Show();


        }

        internal void LoadProducts()
        {
            Products.Clear();
            foreach (var prod in _productRepository.GetAllProducts())
            {
                Products.Add(prod);
            }
        }
    }
}
