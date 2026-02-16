
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile,
        decimal Price)  : ICommand<UpdateProductResult>;
    
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.")
                .Length(2,150).WithMessage("Product name must be between 2 to 150 Characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product Price must be greater than zero.");
            //RuleFor(x => x.Category).NotEmpty().WithMessage("Product Category is required.");
            //RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required.");
            //RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product Image is required.");


        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session , ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommand {command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);                
            }
            
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update<Product>(product);

            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);



        }
    }
}
