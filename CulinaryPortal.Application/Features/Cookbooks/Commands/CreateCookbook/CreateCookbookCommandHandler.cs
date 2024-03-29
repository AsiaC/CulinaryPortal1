﻿using AutoMapper;
using CulinaryPortal.Application.Models;
using CulinaryPortal.Application.Persistence;
using CulinaryPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Cookbooks.Commands.CreateCookbook
{
    public class CreateCookbookCommandHandler : IRequestHandler<CreateCookbookCommand, CookbookDto>
    {
        private readonly ICookbookRepository _cookbookRepository;
        private readonly IMapper _mapper;

        public CreateCookbookCommandHandler(IMapper mapper, ICookbookRepository cookbookRepository)
        {
            _mapper = mapper;
            _cookbookRepository = cookbookRepository;
        }
        public async Task<CookbookDto> Handle(CreateCookbookCommand request, CancellationToken cancellationToken)
        {
            var cookbook = _mapper.Map<Cookbook>(request);

            cookbook = await _cookbookRepository.AddAsync(cookbook);

            var objectToReturn = _mapper.Map<CookbookDto>(cookbook);

            return objectToReturn;
        }
    }
}
