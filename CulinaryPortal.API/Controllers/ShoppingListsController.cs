using System;
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
    [Route("api/shoppingLists")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly ICulinaryPortalRepository _culinaryPortalRepository;
        private readonly IMapper _mapper;

        public ShoppingListsController(ICulinaryPortalRepository culinaryPortalRepository, IMapper mapper)
        {
            _culinaryPortalRepository = culinaryPortalRepository ?? throw new ArgumentNullException(nameof(culinaryPortalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/shoppingLists
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingList>> GetShoppingLists()
        {
            var shoppingListsFromRepo = _culinaryPortalRepository.GetShoppingLists();
            return Ok(shoppingListsFromRepo);
        }

        // GET: api/shoppingLists/5
        [HttpGet("{shoppingListId}", Name = "GetShoppingList")]
        public ActionResult<ShoppingList> GetShoppingList(int shoppingListId)
        {
            var checkIfShoppingListExists = _culinaryPortalRepository.ShoppingListExists(shoppingListId);

            if (checkIfShoppingListExists == null)
            {
                return NotFound();
            }
            var ingredinetFromRepo = _culinaryPortalRepository.GetShoppingList(shoppingListId);
            return Ok(ingredinetFromRepo);
        }

        [HttpPost]
        public ActionResult<ShoppingList> CreateShoppingList(ShoppingList shoppingList)
        {
            _culinaryPortalRepository.AddShoppingList(shoppingList);
            _culinaryPortalRepository.Save();

            return CreatedAtAction("GetShoppingList", new { shoppingListId = shoppingList.Id }, shoppingList);
        }

        // DELETE: api/shoppingLists/5
        [HttpDelete("{shoppingListId}")]
        public ActionResult DeleteShoppingList(int shoppingListId)
        {
            var shoppingListFromRepo = _culinaryPortalRepository.GetShoppingList(shoppingListId);
            if (shoppingListFromRepo == null)
            {
                return NotFound();
            }

            _culinaryPortalRepository.DeleteShoppingList(shoppingListFromRepo);
            _culinaryPortalRepository.Save();

            return NoContent();
        }
    }
}
