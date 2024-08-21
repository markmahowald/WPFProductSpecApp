using Microsoft.EntityFrameworkCore;
using ProductSpecificationApp.Data.BusinessObjects;
using ProductSpecificationApp.Data.EntityFrameworkClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSpecificationApp.Data.Repositories
{
    public class ProductRepository : IProductSpecAppRepo
    {
        private readonly ProductSpecificationDbContext _context;

        public ProductRepository(ProductSpecificationDbContext context)
        {
            _context = context;
        }

        public bool AddBranding(TblBranding branding)
        {
            _context.TblBrandings.Add(branding);
            _context.SaveChanges();
            return true;
        }

        public bool AddClient(TblClient client)
        {
            _context.TblClients.Add(client);
            _context.SaveChanges();
            return true;
        }

        public bool AddMaterial(TblMaterial material)
        {
            _context.TblMaterials.Add(material);
            _context.SaveChanges();
            return true;
        }

        public bool AddMold(TblMold mold)
        {
            _context.TblMolds.Add(mold);
            _context.SaveChanges();
            return true;
        }

        public bool AddProduct(TblProduct product)
        {
            _context.TblProducts.Add(product);
            _context.SaveChanges();
            return true;    
        }

        public IEnumerable<Branding> GetAllBrandings()
        {
            return _context.TblBrandings.Select(b => new Branding(_context, b)).ToList();
        }

        public IEnumerable<Client> GetAllClient()
        {
            return _context.TblClients.Select(c => new Client(c, _context)).ToList();
        }

        public IEnumerable<Material> GetAllMaterial()
        {
            return _context.TblMaterials.Select(m => new Material(m, _context)).ToList();
        }

        public IEnumerable<Mold> GetAllMold()
        {
            return _context.TblMolds.Select(m => new Mold(_context, m)).ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.TblProducts.Select(p => new Product(_context, p)).ToList();
        }

        public Branding GetBrandingById(int id)
        {
            var brandingEntity = _context.TblBrandings
                .Include(b => b.TblProductbrandings)
                .FirstOrDefault(b => b.BrandingId == id);

            return brandingEntity == null ? null : new Branding(_context, brandingEntity);
        }

        public Client GetClientById(int id)
        {
            var clientEntity = _context.TblClients
                .Include(c => c.TblBrandings)
                .FirstOrDefault(c => c.ClientId == id);

            return clientEntity == null ? null : new Client(clientEntity, _context);
        }

        public Material GetMaterialById(int id)
        {
            var materialEntity = _context.TblMaterials
                .Include(m => m.TblProductmaterials)
                .FirstOrDefault(m => m.MaterialId == id);

            return materialEntity == null ? null : new Material(materialEntity, _context);
        }

        public Mold GetMoldById(int id)
        {
            var moldEntity = _context.TblMolds
                .Include(m => m.TblProductmolds)
                .FirstOrDefault(m => m.MoldId == id);

            return moldEntity == null ? null : new Mold(_context, moldEntity);
        }

        public Product GetProductById(int id)
        {
            var productEntity = _context.TblProducts
                .Include(p => p.TblProductbrandings)
                .Include(p => p.TblProductmaterials)
                .Include(p => p.TblProductmolds)
                .FirstOrDefault(p => p.ProductId == id);

            return productEntity == null ? null : new Product(_context, productEntity);
        }

        public bool SaveChanges()
        {
            bool success = true;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //todo: implement logging. 
                success = false;
            }
            return success;
        }

    }
}
