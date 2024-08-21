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
    public class Product : BusinessObject
    {
        public ProductSpecificationDbContext Context { get; }

        public TblProduct TblProduct;

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

        private decimal? height;
        public decimal? Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        private decimal? width;
        public decimal? Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        private string? unit;
        public string? Unit
        {
            get => unit;
            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }

        private string? sku;
        public string? Sku
        {
            get => sku;
            set
            {
                sku = value;
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

        private ICollection<TblProductmaterial> tblProductmaterials = new List<TblProductmaterial>();
        public virtual ICollection<TblProductmaterial> TblProductmaterials
        {
            get => tblProductmaterials;
            set
            {
                tblProductmaterials = value;
                OnPropertyChanged();
            }
        }

        private ICollection<TblProductmold> tblProductmolds = new List<TblProductmold>();
        public virtual ICollection<TblProductmold> TblProductmolds
        {
            get => tblProductmolds;
            set
            {
                tblProductmolds = value;
                OnPropertyChanged();
            }
        }


        public Product()
        {
                
        }

        public Product(ProductSpecificationDbContext context, TblProduct tblProduct)
        {
            Context = context;
            TblProduct = tblProduct;
            ProductId = tblProduct.ProductId;
            Name = tblProduct.Name;
            Description = tblProduct.Description;
            Height = tblProduct.Height;
            Width = tblProduct.Width;
            Unit = tblProduct.Unit;
            Sku = tblProduct.Sku;
            UpdateDate = tblProduct.UpdateDate;
            TblProductbrandings = tblProduct.TblProductbrandings;
            TblProductmaterials = tblProduct.TblProductmaterials;
            TblProductmolds = tblProduct.TblProductmolds;
        }

        public bool SaveDbObject()
        {
            return Save(Context, TblProduct);
        }
    }
}
