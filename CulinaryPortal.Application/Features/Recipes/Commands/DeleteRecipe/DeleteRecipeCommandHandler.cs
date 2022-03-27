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
        private readonly IRecipeRepository _recipeRepository;        
        public DeleteRecipeCommandHandler(IRecipeRepository recipeRepository)
        {            
            _recipeRepository = recipeRepository;
        }
        public async Task<Unit> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var objectToDelete = await _recipeRepository.GetByIdAsync(request.Id);
            if (objectToDelete == null)
            {
                throw new Exception("Server error while removing the recipe");
            }
            await _recipeRepository.DeleteAsync(objectToDelete);
            return Unit.Value;
        }
    }
}
