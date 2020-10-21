using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModernArchitectureShop.Store.Application.Persistence;
using ModernArchitectureShop.Store.Infrastructure.Persistence;

namespace ModernArchitectureShop.Store.Infrastructure.UseCases.GetProductsByIds
{
    public class GetProductsByIdsHandler : IRequestHandler<GetProductsByIds, GetProductsByIdsCommandResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByIdsHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<GetProductsByIdsCommandResponse> Handle(GetProductsByIds command, CancellationToken cancellationToken)
        {
            var products = await _productRepository
                    .GetByIdsQuery(command.ProductIds)
                            .Select(x => new GetProductsByIdsCommandResponse.ProductResult
                            {
                                Id = x.ProductId,
                                Code = x.Code,
                                Stores = x.ProductStores.Select(i => new GetProductsByIdsCommandResponse.ProductStoreResult
                                {
                                    StoreId = i.Store.StoreId,
                                    Name = i.Store.Name,
                                    Location = i.Store.Location,
                                    CanPurchase = i.CanPurchase,
                                    Quantity = i.Quantity
                                })
                            }).ToListAsync(cancellationToken);

            var response = new GetProductsByIdsCommandResponse
            {
                Products = products
            };

            return response;
        }

    }
}