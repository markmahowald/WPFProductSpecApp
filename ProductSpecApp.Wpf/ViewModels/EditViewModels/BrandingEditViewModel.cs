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
    public class BrandingEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;

        public BrandingEditViewModel(IProductSpecAppRepo repository, Branding branding)
        {
            _repository = repository;
            FocusedBranding = branding;
        }

        public Branding FocusedBranding { get; }

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            bool success = false;
            // Update or add the branding in the repository
            try
            {
                if (FocusedBranding.BrandingId == 0)
                {
                    // TODO: Add validation here
                    FocusedBranding.UpdateDBObject(FocusedBranding.tblBranding);
                    success = _repository.AddBranding(FocusedBranding.tblBranding);
                }
                else
                {
                    // TODO: Add validation here
                    FocusedBranding.UpdateDBObject(FocusedBranding.tblBranding);
                    success = _repository.SaveChanges();
                }
            }
            catch (Exception)
            {

                success = false;
            }

            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Branding), FocusedBranding.BrandingId, success));


        }
    }
}
