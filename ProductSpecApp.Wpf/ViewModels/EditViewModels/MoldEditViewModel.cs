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
    public class MoldEditViewModel : ObservableObject
    {
        private readonly IProductSpecAppRepo _repository;

        public MoldEditViewModel(IProductSpecAppRepo repository, Mold mold)
        {
            _repository = repository;
            FocusedMold = mold;
        }

        public Mold FocusedMold { get; }

        public ICommand SaveCommand => new RelayCommand(Save);

        private void Save()
        {
            bool success = false;
            try
            {
                // Update or add the mold in the repository
                if (FocusedMold.MoldId == 0)
                {
                    // TODO: Add validation here
                    FocusedMold.UpdateDBObject(FocusedMold.TblMold);
                    success=_repository.AddMold(FocusedMold.TblMold);
                }
                else
                {
                    // TODO: Add validation here
                    FocusedMold.UpdateDBObject(FocusedMold.TblMold);
                    success=_repository.SaveChanges();
                }
            }
            catch (Exception)
            {

                success = false;
            }

            WeakReferenceMessenger.Default.Send(new SaveSuccessfulMessage(typeof(Mold), FocusedMold.MoldId, success));

        }
    }
}
