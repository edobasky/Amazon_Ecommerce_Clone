using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers.Read
{
    public class GetAllTypesQueryHandler(ITypeRepository typeRepository) : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
    {
        public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var typesList = await typeRepository.GetAllTypes();
            return typesList.ToResponseList();
        }
    }
}
