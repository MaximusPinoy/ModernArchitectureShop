using FluentValidation;

namespace ModernArchitectureShop.Store.Infrastructure.UseCases.CreateStore
{
    public class CreateStoreValidator : AbstractValidator<CreateStore>
    {
        public CreateStoreValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Location)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
        }
    }
}