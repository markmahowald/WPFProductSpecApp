using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSpecificationApp.Data.BusinessObjects
{
    public class Branding : BusinessObject
    {
        public ProductSpecificationDbContext Context { get; }

        public TblBranding tblBranding;
        private int brandingId;
        public int BrandingId
        {
            get => brandingId;
            set
            {
                brandingId = value;
                OnPropertyChanged();
            }
        }

        private string name = null!;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string? description;
        public string? Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private string? type;
        public string? Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        private int? clientId;
        public int? ClientId
        {
            get => clientId;
            set
            {
                clientId = value;
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

        private TblClient? tblClient;
        public virtual TblClient? TblClient
        {
            get => tblClient;
            set
            {
                tblClient = value;
                OnPropertyChanged();
            }
        }

        private ICollection<TblProductbranding> tblProductbrandings = new List<TblProductbranding>();
        public virtual ICollection<TblProductbranding> TblProductbrandings
        {
            get => tblProductbrandings;
            set
            {
                tblProductbrandings = value;
                OnPropertyChanged();
            }
        }

        public Branding()
        {
                
        }

        public Branding(ProductSpecificationDbContext context, TblBranding tblBranding)
        {
            Context = context;
            this.tblBranding = tblBranding;
            BrandingId = tblBranding.BrandingId;
            Name = tblBranding.Name;
            Description = tblBranding.Description;
            Type = tblBranding.Type;
            ClientId = tblBranding.ClientId;
            UpdateDate = tblBranding.UpdateDate;
            TblClient = tblBranding.Client;
            TblProductbrandings = tblBranding.TblProductbrandings;
        }

        public bool SaveDbObject()
        {
            return Save(Context, tblBranding);
        }

    }

}
