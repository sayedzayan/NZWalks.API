using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Collections.Generic;

namespace NZWalks.API.Controllers
{
    //https://localhost:00000/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET ALL REGIONS
        //GET: https:// localhost:portnumber/api/regions
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            //get data from database  - domain model
           //var regionsDomain = await dbContext.Regions.ToListAsync();
           var regionsDomain=await regionRepository.GetAllAsync();

            //map domain to dtos
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    });
            //}

            //map domain model to dtos
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            //return DTOs
            return Ok(regionsDto);
        }




        //getbsinglr region (by id)
        //GET: https:// localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task <IActionResult> GetById([FromRoute]Guid id)
        {

         // var region= dbContext.Regions.Find(id);
         //or
            //linq code 


            //gget region domain model from database
        //
        //var regionDomain=await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        
            var regionDomain=await regionRepository.GetByIdAsync(id);
            if(regionDomain == null)
            {
                return NotFound();
            }
            //map/convert region domain model to region dto
            //var regionsDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //};

            //return dto back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }






        //POST to create new region 
        //post: https //localhost : portnum/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task <IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //if(ModelState.IsValid)
            //{
                //map or convert dto to domain model
                //var regionDomainModel = new Region
                //{
                //    Code = addRegionRequestDTO.Code,
                //    Name = addRegionRequestDTO.Name,
                //    RegionImageUrl=addRegionRequestDTO.RegionImageUrl
                //};

                var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);
                //use domain to create region

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //map domain model back to dto 
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl,
                //    Code = regionDomainModel.Code
                //};
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

            //}

            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }
            
            





        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task <IActionResult> Update([FromRoute]Guid id , [FromBody]UpdateRegionRequestDto updateRegionRequestDto) 
        {
           // if(ModelState.IsValid)
            //{

                //map
                //var regionDomainModel = new Region
                //{
                //    Code= updateRegionRequestDto.Code,
                //    Name = updateRegionRequestDto.Name,
                //    RegionImageUrl=updateRegionRequestDto.RegionImageUrl
                //};
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //check if region exists
                //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //map dto to domain model
                // regionDomainModel.Code= updateRegionRequestDto.Code;
                //regionDomainModel.Name= updateRegionRequestDto.Name;
                //regionDomainModel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;

                // await dbContext.SaveChangesAsync();

                //convert domain model to dto
                //var regionDto = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                // var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(mapper.Map<RegionDto>(regionDomainModel));

           //  }
            //  else { return BadRequest(ModelState); }


        }





        //delete region
        //delete: https:// localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task <IActionResult> Delete([FromRoute]Guid id)
        {
           //var regionDomainModel=await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           
            var regionDomainModel=await regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            { return NotFound(); 
            }

            //delete region
            // dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();


            //return the deleted region back
            //map domain model to dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);

        }
    }
}
