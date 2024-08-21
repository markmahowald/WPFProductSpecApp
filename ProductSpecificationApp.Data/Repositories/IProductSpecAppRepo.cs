using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.EntityFrameworkClasses;
using System.Collections.Generic;

namespace ProductSpecificationApp.Data.Repositories
{
    public interface IProductSpecAppRepo
    {
        IEnumerable<Branding> GetAllBrandings();
        Branding GetBrandingById(int id);
        bool AddBranding(TblBranding branding);

        IEnumerable<Client> GetAllClient();
        Client GetClientById(int id);
        bool AddClient(TblClient client);

        IEnumerable<Material> GetAllMaterial();
        Material GetMaterialById(int id);
        bool AddMaterial(TblMaterial material);

        IEnumerable<Mold> GetAllMold();
        Mold GetMoldById(int id);
        bool AddMold(TblMold mold);

        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        bool AddProduct(TblProduct product);
       
        public bool SaveChanges();
    }
}
