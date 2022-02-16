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

namespace CulinaryPortal.Application.Features.Recipes.Commands.DeleteRecipe
{
    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand>
    {
        private readonly IAsyncRepository<Recipe> _recipeRepository;
        private readonly IMapper _mapper;
        public DeleteRecipeCommandHandler(IMapper mapper, IAsyncRepository<Recipe> recipeRepository)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<Unit> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipeToDelete = await _recipeRepository.GetByIdAsync(request.RecipeId);

            //if (recipeToDelete == null)
            //{
            //    //todo
            //    //throw new NotFoundException(nameof(Recipe), request.RecipeId);
            //}

            await _recipeRepository.DeleteAsync(recipeToDelete);

            return Unit.Value;
        }
    }
}
