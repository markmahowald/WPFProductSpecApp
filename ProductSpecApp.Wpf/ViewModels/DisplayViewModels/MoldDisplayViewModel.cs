using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.Input;
using ProductSpecApp.Wpf.Views;
using System.Windows.Input;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.Repositories;


namespace ProductSpecApp.Wpf.ViewModels.DisplayViewModels
{
    public class MoldDisplayViewModel
    {
        public ObservableCollection<Mold> Molds { get; } = new ObservableCollection<Mold>();
        private readonly IProductSpecAppRepo _productRepository;

        public MoldDisplayViewModel(IProductSpecAppRepo repo)
        {
            this._productRepository = repo;

            LoadMolds();
        }
        public ICommand EditMoldCommand => new RelayCommand<object>(EditMold);

        public void EditMold(object moldFromGrid)
        {
            Mold mold = moldFromGrid as Mold;
            var editViewModel = new MoldEditViewModel(this._productRepository, mold);
            var editView = new MoldEditView(mold, _productRepository);
            editView.Show();



        }
        public void LoadMolds()
        {
            Molds.Clear();
            foreach (var mol in _productRepository.GetAllMold())
            {
                Molds.Add(mol);
            }
        }

    }
}
