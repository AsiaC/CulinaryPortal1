using CulinaryPortal.Application.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.DeleteCookbook
{
    public class DeleteCookbookCommandHandler : IRequestHandler<DeleteCookbookCommand>
    {
        private readonly ICookbookRepository _cookbookRepository;

        public DeleteCookbookCommandHandler(ICookbookRepository cookbookRepository)
        {
            _cookbookRepository = cookbookRepository;
        }
        public async Task<Unit> Handle(DeleteCookbookCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _cookbookRepository.GetByIdAsync(request.Id);
            if (objectToDelete == null)
            {
                throw new Exception("Server error while removing the cookbook");
            }
            await _cookbookRepository.DeleteAsync(objectToDelete);
            return Unit.Value;
        }
    }
}
