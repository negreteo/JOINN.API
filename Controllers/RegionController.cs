using System;
using JOINN.API.RequestModels;
using JOINN.Data.Models;
using JOINN.Services;
using Microsoft.AspNetCore.Mvc;

namespace JOINN.API.Controllers
{
   [ApiController]
   public class RegionController : ControllerBase
   {
      private readonly IRegionService _regionService;

      public RegionController (IRegionService regionService)
      {
         _regionService = regionService;
      }

      [HttpGet ("/api/regions")]
      public ActionResult GetRegions ()
      {
         var regions = _regionService.GetRegions ();
         return Ok (regions);
      }

      [HttpGet ("/api/regions/{id}")]
      public ActionResult GetRegion (int id)
      {
         var region = _regionService.GetRegion (id);
         return Ok (region);
      }

      [HttpPost ("/api/regions")]
      public ActionResult CreateRegion ([FromBody] RegionRequest item)
      {

         if (item == null || !ModelState.IsValid)
         {
            return BadRequest ("Model state not valid.");
         }

         if (_regionService.RegionExists (item.Name))
         {
            return BadRequest ("Region already exists.");
         }

         var now = DateTime.UtcNow;

         var region = new Region
         {
            Name = item.Name,
            Active = item.Active,
            UpdatedBy = "Oscar Negrete",
            LastUpdate = now
         };

         try
         {
            _regionService.AddRegion (region);
         }
         catch (Exception)
         {
            return BadRequest ("Could not create region");
         }

         return Ok ($"Region created: {item.Name}");
      }

      [HttpPut ("/api/regions/{id}")]
      public ActionResult UpdateRegion (int id, [FromBody] RegionRequest item)
      {
         if (item == null || !ModelState.IsValid)
         {
            return BadRequest ("Model state not valid.");
         }

         if (id != item.ID)
         {
            return BadRequest ("ID not valid.");
         }

         var now = DateTime.UtcNow;

         var region = new Region
         {
            ID = id,
            Name = item.Name,
            Active = item.Active,
            UpdatedBy = "Oscar Negrete",
            LastUpdate = now
         };

         try
         {
            _regionService.UpdateRegion (region);
         }
         catch (Exception e)
         {
            if (!_regionService.RegionExists (id))
            {
               return NotFound ();
            }
            else
            {
               return BadRequest (e.Message);
            }
         }

         return Ok ($"Region updated: {region.ID} - {region.Name}");
      }

      // [HttpDelete ("/api/regions/{id}")]
      // public ActionResult DeleteRegion (int id)
      // {
      //    if (!_regionService.RegionExists (id))
      //    {
      //       return NotFound ();
      //    }

      //    try
      //    {
      //       _regionService.DeleteRegion (id);
      //    }
      //    catch (Exception)
      //    {
      //       return BadRequest ("Could not delete the region");
      //    }
      //    return Ok ($"region deleted ID: {id}");
      // }
   }
}
