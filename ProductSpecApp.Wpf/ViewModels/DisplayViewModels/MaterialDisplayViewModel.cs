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
    public class MaterialDisplayViewModel
    {
        public ObservableCollection<Material> Materials { get; set; }
        IProductSpecAppRepo _productRepository { get; set; }

        public MaterialDisplayViewModel(IProductSpecAppRepo repo)
        {
            this._productRepository = repo;
            Materials = new ObservableCollection<Material>(_productRepository.GetAllMaterial());


            WeakReferenceMessenger.Default.Register<SaveSuccessfulMessage>(this, (r, m) =>
            {
                if (m.Value.ObjectType == typeof(Material))
                {
                    // Refresh the materials list
                    LoadMaterials();
                }
            });
        }
        public ICommand EditMaterialCommand => new RelayCommand<object>(EditMaterial);

        public void EditMaterial(object materialFromGrid)
        {
            Material material = materialFromGrid as Material;    
            var editViewModel = new MaterialEditViewModel(this._productRepository, material);
            var editView = new MaterialEditView( material, _productRepository);
            editView.Show();

            // Refresh the materials list after editing

        }

        public void LoadMaterials()
        {
            Materials.Clear();
            foreach (var mat in _productRepository.GetAllMaterial())
            {
                Materials.Add(mat);
            }
        }
    }
}