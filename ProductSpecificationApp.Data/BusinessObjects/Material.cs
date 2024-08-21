using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.InterfacesAndInheritables;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ProductSpecificationApp.Data.BusinessObjects
{
    public class Material : BusinessObject
    {
        public ProductSpecificationDbContext Context { get; }

        public TblMaterial tblMaterial;

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

        private decimal height;
        public decimal Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        private decimal width;
        public decimal Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        private string unit = null!;
        public string Unit
        {
            get => unit;
            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }

        private string sku = null!;
        public string SKU
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

        public Material()
        {
                
        }
        public Material(TblMaterial tblMaterial, ProductSpecificationDbContext context)
        {
            this.Context = context;
            this.tblMaterial = tblMaterial;
            MaterialId = tblMaterial.MaterialId;
            Name = tblMaterial.Name;
            Description = tblMaterial.Description;
            Height = tblMaterial.Height ?? 0;
            Width = tblMaterial.Width ?? 0;
            Unit = tblMaterial.Unit;
            SKU = tblMaterial.Sku;
            UpdateDate = tblMaterial.UpdateDate;
        }
        public bool SaveDbObject()
        {
            return Save(Context, tblMaterial);
        }
    }
}
