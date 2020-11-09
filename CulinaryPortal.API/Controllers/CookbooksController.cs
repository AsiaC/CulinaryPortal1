﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CulinaryPortal.API.Entities;
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
        public ActionResult<IEnumerable<Cookbook>> GetCookbooks()
        {
            var cookbooksFromRepo = _culinaryPortalRepository.GetCookbooks();
            return Ok(cookbooksFromRepo);
        }

        // GET: api/cookbooks/5
        [HttpGet("{cookbookId}", Name = "GetCookbook")]
        public ActionResult<Cookbook> GetCookbook(int cookbookId)
        {
            var checkIfCookbookExists = _culinaryPortalRepository.CookbookExists(cookbookId);

            if (checkIfCookbookExists == null)
            {
                return NotFound();
            }
            var cookbookFromRepo = _culinaryPortalRepository.GetCookbook(cookbookId);
            return Ok(cookbookFromRepo);
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
        public ActionResult DeleteCookbook(int cookbookId)
        {
            var cookbookFromRepo = _culinaryPortalRepository.GetCookbook(cookbookId);
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