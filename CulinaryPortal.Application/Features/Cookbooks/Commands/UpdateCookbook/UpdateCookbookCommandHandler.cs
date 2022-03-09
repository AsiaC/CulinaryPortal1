using AutoMapper;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.UpdateCookbook
{
    public class UpdateCookbookCommandHandler : IRequestHandler<UpdateCookbookCommand>
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;
        public UpdateCookbookCommandHandler(IMapper mapper, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _cookbookRepository = cookbookRepository;
        }

        public async Task<Unit> Handle(UpdateCookbookCommand request, CancellationToken cancellationToken)
        {
            var cookbookRecipe = _mapper.Map<CookbookRecipe>(request);
            var cookbook = cookbookRecipe.Cookbook;
            if (request.IsRecipeAdded) //add recipe to the cookbook
            {                
                await _cookbookRepository.AddRecipeToCookbookAsync(cookbookRecipe, cookbook);
            }
            else //remove recipe from cookbook
            {
                await _cookbookRepository.RemoveRecipeFromCookbookAsync(cookbookRecipe, cookbook);
            }
            return Unit.Value;
        }
    }
}
