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
    public class Mold : BusinessObject 
    {
        private ProductSpecificationDbContext context;
        public TblMold TblMold;

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
        public Mold()
        {
               
        }

        public Mold(ProductSpecificationDbContext context, TblMold tblMold)
        {
            this.context = context; 
            this.TblMold = tblMold;
            MoldId = tblMold.MoldId;
            Name = tblMold.Name;
            Description = tblMold.Description;
            UpdateDate = tblMold.UpdateDate;
            TblProductmolds = tblMold.TblProductmolds;
        }

        public bool SaveDbObject()
        {
            return Save(context, TblMold);
        }


    }
}
