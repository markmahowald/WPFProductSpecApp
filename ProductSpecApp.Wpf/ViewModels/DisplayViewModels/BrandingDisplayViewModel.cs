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
    public class BrandingDisplayViewModel
    {
        public ObservableCollection<Branding> Brandings { get; set; } = new ObservableCollection<Branding>();
        private readonly IProductSpecAppRepo _productRepository;

        public BrandingDisplayViewModel(IProductSpecAppRepo repo)
        {
            this._productRepository = repo;
            LoadBranding();

            WeakReferenceMessenger.Default.Register<SaveSuccessfulMessage>(this, (r, m) =>
            {
                if (m.Value.ObjectType == typeof(Branding))
                {
                    // Refresh the materials list
                    LoadBranding();
                }
            });
        }
        public ICommand EditBrandingCommand => new RelayCommand<object>(EditBranding);

        public void EditBranding(object brandingFromGrid)
        {
            Branding branding = brandingFromGrid as Branding;
            var editViewModel = new BrandingEditViewModel(this._productRepository, branding);
            var editView = new BrandingEditView(branding, _productRepository);
            editView.Show();


        }

        internal void LoadBranding()
        {
            Brandings.Clear();
            foreach (var brd in _productRepository.GetAllBrandings())
            {
                Brandings.Add(brd);
            }
        }
    }
}
