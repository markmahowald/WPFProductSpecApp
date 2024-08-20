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
    public class ProductMold: BusinessObject
    {
        private ProductSpecificationDbContext context { get; set; }
        private TblProductmold tblProductmold { get; set; }
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

        private int moldId;
        public int MoldId
        {
            get => moldId;
            set
            {
                moldId = value;
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

        private TblMold mold = null!;
        public virtual TblMold Mold
        {
            get => mold;
            set
            {
                mold = value;
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

        public ProductMold()
        {
                
        }

        public ProductMold(TblProductmold tblProductmold, ProductSpecificationDbContext context)
        {
            this.tblProductmold = tblProductmold;
            ProductId = tblProductmold.ProductId;
            MoldId = tblProductmold.MoldId;
            UpdateDate = tblProductmold.UpdateDate;
            Mold = tblProductmold.Mold;
            Product = tblProductmold.Product;
            this.context = context;
        }

        public bool SaveDbObject()
        {
            return Save(context, this.tblProductmold);
        }

    }
}
