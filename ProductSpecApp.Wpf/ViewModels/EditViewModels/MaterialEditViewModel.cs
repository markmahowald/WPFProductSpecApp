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
    public class MaterialEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;

        public MaterialEditViewModel(IProductSpecAppRepo repository, Material material)
        {
            _repository = repository;
            FocusedMaterial = material;
        }

        public Material FocusedMaterial { get; }

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            bool success = false;
            // Update or add the material in the repository
            try
            {
                if (FocusedMaterial.MaterialId == 0)
                {
                    //todo: validation goes here
                    FocusedMaterial.UpdateDBObject(FocusedMaterial.tblMaterial);
                    success = _repository.AddMaterial(FocusedMaterial.tblMaterial);
                    
                }
                else
                {
                    //todo: validation goes here
                    FocusedMaterial.UpdateDBObject(FocusedMaterial.tblMaterial);
                    success = _repository.SaveChanges();
                }
            }
            catch (Exception)
            {

                success = false;
            }

            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Material), FocusedMaterial.MaterialId, success));

        }
    }
}