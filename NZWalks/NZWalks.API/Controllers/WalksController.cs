using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Identity.Client;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //  /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //create walks
        //post: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
           // if(ModelState.IsValid)
            //{
                //map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //map domain model to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));



           // }
           // return BadRequest(ModelState);
        }





        //get walks
        //get: / api/walks?filteron=name&filterQuery=Track&sortBy=name&isAscending=true&حشلثآعةلاثق=1&حشلثٍهئث=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery]string? sortBy, [FromQuery]bool? isAscending,
             [FromQuery]int pageNumber = 1, [FromQuery]int pageSize=1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn , filterQuery,sortBy, 
                isAscending??true,pageNumber,pageSize );

            //map domain model to dto
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));



        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //update walksby id
        //put: /api/walks/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
           // if(ModelState.IsValid)
           // {
                //map dto to domain model 
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //map domin mode to dto
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
          //  }
            // else  return BadRequest(ModelState);
           
        }

        //delete walk by id
        //delete: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
          var deletedDomainModel=  await walkRepository.DeleteAsync(id);

            if(deletedDomainModel == null)
            { return NotFound(); }

            //map domain to dto
            return Ok(mapper.Map<WalkDto>(deletedDomainModel));
        }

    
    }
}
