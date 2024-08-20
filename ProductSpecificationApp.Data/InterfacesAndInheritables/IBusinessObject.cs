using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSpecificationApp.Data.InterfacesAndInheritables
{
    public interface IBusinessObject : INotifyPropertyChanged
    {

        void AllPropertyChanged();
        bool UpdateDBObject(object dbObject);
    }
}
