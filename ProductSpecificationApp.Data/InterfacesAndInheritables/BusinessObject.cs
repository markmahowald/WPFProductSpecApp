using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProductSpecificationApp.Data.InterfacesAndInheritables
{
    public abstract class  BusinessObject: IBusinessObject
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AllPropertyChanged()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead);

            foreach (var property in properties)
            {
                OnPropertyChanged(property.Name);
            }
        }

        public bool UpdateDBObject(object dbObject)
        {
            try
            {
                var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite);

                foreach (var property in properties)
                {
                    var dbProperty = dbObject.GetType().GetProperty(property.Name);
                    if (dbProperty != null && dbProperty.CanWrite)
                    {
                        var value = property.GetValue(this);
                        dbProperty.SetValue(dbObject, value);
                    }
                }

                var updateDateProperty = dbObject.GetType().GetProperty("UpdateDate");
                if (updateDateProperty != null && updateDateProperty.CanWrite)
                {
                    updateDateProperty.SetValue(dbObject, DateTime.Now);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating DB object: {ex.Message}");
                return false;
            }
        }
        public bool Save<TDbContext, TEntity>(TDbContext context, TEntity dbObject)
                where TDbContext : DbContext
                where TEntity : class
        {
            try
            {
                var dbSet = context.Set<TEntity>();

                var idProperty = typeof(TEntity).GetProperty("Id");
                if (idProperty == null)
                {
                    throw new InvalidOperationException("The database object does not have an 'Id' property.");
                }

                var idValue = idProperty.GetValue(dbObject);
                if (idValue == null || Convert.ToInt32(idValue) == 0)
                {
                    // It's a new entity
                    UpdateDBObject(dbObject);
                    dbSet.Add(dbObject);
                }
                else
                {
                    // Existing entity
                    UpdateDBObject(dbObject);
                    dbSet.Update(dbObject);
                }

                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving object: {ex.Message}");
                return false;
            }
        }
    }
}