
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x=>x.Category).NotEmpty().WithMessage("Product Category is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required.");
            RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("Product Image is required.");
            RuleFor(x=>x.Price).GreaterThan(0).WithMessage("Product Price must be greater than zero.");
        }
    }

    internal class CreateProductCommandHandler (IDocumentSession  session, ILogger<CreateProductCommandHandler> logger) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        //public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            logger.LogInformation("Handling CreateProductCommand for product: {ProductName}", command.Name);

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

            //Save to database
            
            session.Store(prod);
            await session.SaveChangesAsync(cancellationToken);


            return new CreateProductResult(prod.Id);

        }

    }
}
