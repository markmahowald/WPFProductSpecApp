using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProductSpecificationApp.Data.BusinessObjects
{
    public  class ProductBranding: BusinessObject
    {
        public ProductSpecificationDbContext Context { get; }

        private TblProductbranding TblProductbranding;

        private int productId;
        public int ProductId
        {
            get => productId;
            set
            {
                productId = value;
                OnPropertyChanged();
            }
        }

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

        private TblBranding branding = null!;
        public virtual TblBranding Branding
        {
            get => branding;
            set
            {
                branding = value;
                OnPropertyChanged();
            }
        }

        private TblProduct product = null!;


        public virtual TblProduct Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }


        public ProductBranding()
        {
                
        }
        public ProductBranding(ProductSpecificationDbContext context, TblProductbranding tblProductbranding)
        {
            Context = context;
            TblProductbranding = tblProductbranding;
            ProductId = tblProductbranding.ProductId;
            BrandingId = tblProductbranding.BrandingId;
            UpdateDate = tblProductbranding.UpdateDate;
            Branding = tblProductbranding.Branding;
            Product = tblProductbranding.Product;
        }

        public bool SaveDbObject()
        {
            return Save(Context, TblProductbranding);
        }

    }
}
