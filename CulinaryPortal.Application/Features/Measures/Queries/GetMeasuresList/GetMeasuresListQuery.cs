﻿using CulinaryPortal.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryPortal.Application.Features.Measures.Queries.GetMeasuresList
{
    public class GetMeasuresListQuery :IRequest<List<MeasureDto>>
    {
    }
}
