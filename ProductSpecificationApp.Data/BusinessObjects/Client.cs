using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using System.Collections.Generic;
using System.Linq;

namespace ProductSpecificationApp.Data.BusinessObjects
{
    public class Client : BusinessObject

    {
        public ProductSpecificationDbContext Context { get; }

        public  TblClient tblClient;

        private int clientId;
        public int ClientId
        {
            get => clientId;
            set
            {
                clientId = value;
                OnPropertyChanged();
            }
        }

        private string clientName = null!;
        public string ClientName
        {
            get => clientName;
            set
            {
                clientName = value;
                OnPropertyChanged();
            }
        }

        private DateTime? updateDate;
        public DateTime? UpdateDate
        {
            get => updateDate;
            set
            {
                updateDate = value;
                OnPropertyChanged();
            }
        }

        private List<TblBranding> tblBrandings = new List<TblBranding>();
        public virtual List<TblBranding> TblBrandings
        {
            get => tblBrandings;
            set
            {
                tblBrandings = value;
                OnPropertyChanged();
            }
        }

        public Client()
        {
                
        }
        public Client(TblClient tblClient, ProductSpecificationDbContext context)
        {
            this.Context = context;
            this.tblClient = tblClient;
            ClientId = tblClient.ClientId;
            ClientName = tblClient.ClientName;
            UpdateDate = tblClient.UpdateDate;
            TblBrandings = tblClient.TblBrandings.ToList();
        }

        public bool SaveDbObject()
        {
            return Save(Context, tblClient);
        }

    }
}
