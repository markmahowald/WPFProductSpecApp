using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using ProductSpecificationApp.Data.Repositories;
using System.Windows.Input;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using CommunityToolkit.Mvvm.Messaging;
using ProductSpecificationApp.Data.Messages;

namespace ProductSpecApp.Wpf.ViewModels
{
    public class ProductEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;

        public ProductEditViewModel(IProductSpecAppRepo repository, Product product)
        {
            _repository = repository;
            FocusedProduct = product;
        }

        public Product FocusedProduct { get; }

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            bool success = false;
            // Update or add the product in the repository
            try
            {
                if (FocusedProduct.ProductId == 0)
                {
                    // TODO: Add validation here
                    FocusedProduct.UpdateDBObject(FocusedProduct.TblProduct);
                    success = _repository.AddProduct(FocusedProduct.TblProduct);
                    
                }
                else
                {
                    // TODO: Add validation here
                    FocusedProduct.UpdateDBObject(FocusedProduct.TblProduct);
                    success= _repository.SaveChanges();  
                }
            }
            catch (Exception)
            {

                success = false;
            }
            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Product), FocusedProduct.ProductId, success));

        }
    }
}
