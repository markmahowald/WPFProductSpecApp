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
    public class ClientEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;

        public ClientEditViewModel(IProductSpecAppRepo repository, Client client)
        {
            _repository = repository;
            FocusedClient = client;
        }

        public Client FocusedClient { get; }

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            bool success = false;

            try
            {
                // Update or add the client in the repository
                if (FocusedClient.ClientId == 0)
                {
                    // TODO: Add validation here
                    FocusedClient.UpdateDBObject(FocusedClient.tblClient);
                   success= _repository.AddClient(FocusedClient.tblClient);
                }
                else
                {
                    // TODO: Add validation here
                    FocusedClient.UpdateDBObject(FocusedClient.tblClient);
                    success = _repository.SaveChanges();
                }
            }
            catch (Exception)
            {

                success = false;
            }
            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Client),FocusedClient.ClientId, success));

        }
    }
}
