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
    public  class ProductMaterial : BusinessObject
    {
        public ProductSpecificationDbContext Context { get; }

        private TblProductmaterial TblProductmaterial;

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

        private int materialId;
        public int MaterialId
        {
            get => materialId;
            set
            {
                materialId = value;
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

        private TblMaterial material = null!;
        public virtual TblMaterial Material
        {
            get => material;
            set
            {
                material = value;
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

        public ProductMaterial()
        {
        }
        public ProductMaterial(ProductSpecificationDbContext context, TblProductmaterial tblProductmaterial)
        {
            Context = context;
            TblProductmaterial = tblProductmaterial;
            ProductId = tblProductmaterial.ProductId;
            MaterialId = tblProductmaterial.MaterialId;
            UpdateDate = tblProductmaterial.UpdateDate;
            Material = tblProductmaterial.Material;
            Product = tblProductmaterial.Product;
        }


        public bool SaveDbObject()
        {
            return Save(Context, this.TblProductmaterial);
        }

    }
}
