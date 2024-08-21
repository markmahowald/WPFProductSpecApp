using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using ProductSpecApp.Wpf.Views;
using System.Windows.Input;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using ProductSpecificationApp.Data.Messages;

namespace ProductSpecApp.Wpf.ViewModels.DisplayViewModels
{
    public class ClientDisplayViewModel
    {
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        private readonly IProductSpecAppRepo _productRepository;

        public ClientDisplayViewModel(IProductSpecAppRepo repo)
        {
            this._productRepository = repo;
            LoadClients();

            WeakReferenceMessenger.Default.Register<SaveSuccessfulMessage>(this, (r, m) =>
            {
                if (m.Value.ObjectType == typeof(Client))
                {
                    // Refresh the materials list
                    LoadClients();
                }
            });
        }
        public ICommand EditClientCommand => new RelayCommand<object>(EditClient);

        public void EditClient(object clientFromGrid)
        {
            Client client = clientFromGrid as Client;
            var editViewModel = new ClientEditViewModel(this._productRepository, client);
            var editView = new ClientEditView(client, _productRepository);
            editView.Show();


        }

        internal void LoadClients() 
        { 
            Clients.Clear();
            foreach (var cli in _productRepository.GetAllClient())
            {
                Clients.Add(cli);
            }
        }
    }
}
