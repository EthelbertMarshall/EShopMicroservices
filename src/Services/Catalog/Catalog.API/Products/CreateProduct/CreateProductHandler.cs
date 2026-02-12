using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        //public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product Entity from command object

           var prod = new Product
            {
                Category = command.Category,
                Description = command.Description,
                //Id = Guid.NewGuid(),
                ImageFile = command.ImageFile,
                Name = command.Name,
                Price = command.Price

            };


            // new CreateProductResult(Guid.NewGuid()); 

            return new CreateProductResult(Guid.NewGuid());

        }

    }
}
