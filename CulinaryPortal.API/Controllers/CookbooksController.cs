﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
using CulinaryPortal.API.Models;
using CulinaryPortal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryPortal.API.Controllers
{
    [Route("api/cookbooks")]
    [ApiController]
    public class CookbooksController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public CookbooksController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/cookbooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookbookDto>>> GetCookbooks()
        {
            var cookbooksFromRepo = await _culinaryPortalRepository.GetCookbooksAsync();
            return Ok(_mapper.Map<IEnumerable<Models.CookbookDto>>(cookbooksFromRepo));
        }

        // GET: api/cookbooks/5
        [HttpGet("{cookbookId}", Name = "GetCookbook")]
        public async Task<ActionResult<CookbookDto>> GetCookbook(int cookbookId)
        {
            var checkIfCookbookExists = await _culinaryPortalRepository.CookbookExistsAsync(cookbookId);

            if (checkIfCookbookExists == false)
            {
                return NotFound();
            }
            var cookbookFromRepo = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
            var cookbook = _mapper.Map<Models.CookbookDto>(cookbookFromRepo);
            var recipesFromRepo = cookbookFromRepo.CookbookRecipes.Select(x => x.Recipe);
            
            

            if (recipesFromRepo.Any() && !cookbook.Recipes.Any())
            {
                var recipeDtosFromRepo = _mapper.Map<IEnumerable<RecipeDto>>(recipesFromRepo);
                //var a = cookbook.Recipes.ToList();
                //a.AddRange(recipeDtosFromRepo);
                //cookbook.Recipes.AddRange(recipeDtosFromRepo);

                foreach (var recipe in recipeDtosFromRepo)
                {
                    cookbook.Recipes.Add(recipe);
                }
            }
            //.ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            return Ok(cookbook);
        }

        [HttpPost]
        public ActionResult<Cookbook> CreateIngredient(Cookbook cookbook)
        {
            _culinaryPortalRepository.AddCookbook(cookbook);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetCookbook", new { cookbookId = cookbook.Id }, cookbook);
        }

        // DELETE: api/cookbooks/5
        [HttpDelete("{cookbookId}")]
        public async Task<ActionResult> DeleteCookbook(int cookbookId)
        {
            var cookbookFromRepo = await _culinaryPortalRepository.GetCookbookAsync(cookbookId);
            if (cookbookFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteCookbook(cookbookFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }
    }
}
