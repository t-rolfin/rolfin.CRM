﻿using Ardalis.ApiEndpoints;
using crm.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace crm.api.EndPoints.UpdateNote
{
    public class UpdateNoteEndPoint : BaseAsyncEndpoint
        .WithRequest<UpdateNoteModel>
        .WithoutResponse
    {

        private readonly ILeadRepository _leadRepository;

        public UpdateNoteEndPoint(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        [Authorize]
        [HttpPatch("leads/{leadid}/notes/update/{noteid}")]
        [SwaggerOperation(
            Summary = "Update an existin' note for a specific lead.",
            Tags = new[] { "LeadEndpoint" }
        )]
        public override async Task<ActionResult> HandleAsync([FromRoute] UpdateNoteModel request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lead = await _leadRepository.GetAsync(request.LeadId);

            if (lead is null)
                return NotFound();

            var result = lead.UpdateNoteContent(request.NoteId ,request.NewContent);

            if (!result.IsSuccess)
                return BadRequest(result.MetaResult.Message);

            await _leadRepository.UpdateAsync(lead, cancellationToken);
            return Ok();
        }
    }
}
