using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    { 
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            await using var session = store.LightweightSession();

           if( await session.Query<Product>().AnyAsync()) 
                return;

            session.Store(GetPreConfiguredProducts());

            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung 10",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Huawei Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur.",
                ImageFile = "product-3.png",
                Price = 650.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "HTC U11+ Plus",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = new List<string> { "Smart Phone" }
                },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "LG G7 ThinQ",
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-6.png",
                Price = 240.00M,
                Category = new List<string> { "Home Kitchen" }
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Panasonic Lumix",
                Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-6.png",
                Price = 240.00M,
                Category = new List<string> { "Camera" }
            }
        };
         
    }
}
