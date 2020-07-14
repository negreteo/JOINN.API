using System;
using JOINN.API.RequestModels;
using JOINN.Data.Models;
using JOINN.Services;
using Microsoft.AspNetCore.Mvc;

namespace JOINN.API.Controllers
{
   [ApiController]
   public class SolutionController : ControllerBase
   {
      private readonly ISolutionService _solutionService;

      public SolutionController (ISolutionService solutionService)
      {
         _solutionService = solutionService;
      }

      [HttpGet ("/api/solutions")]
      public ActionResult GetSolutions ()
      {
         var solutions = _solutionService.GetSolutions ();
         return Ok (solutions);
      }

      [HttpGet ("/api/solutions/{id}")]
      public ActionResult GetSolution (int id)
      {
         var solution = _solutionService.GetSolution (id);
         return Ok (solution);
      }

      [HttpPost ("/api/solutions")]
      public ActionResult CreateSolution ([FromBody] SolutionRequest item)
      {

         if (item == null || !ModelState.IsValid)
         {
            return BadRequest ("Model state not valid.");
         }

         if (_solutionService.SolutionExists (item.Title))
         {
            return BadRequest ("Solution already exists.");
         }

         var now = DateTime.UtcNow;

         var solution = new Solution
         {
            Title = item.Title,
            UpdatedBy = "Oscar Negrete",
            LastUpdate = now
         };

         try
         {
            _solutionService.AddSolution (solution);
         }
         catch (Exception)
         {
            return BadRequest ("Could not create solution");
         }

         return Ok ($"Solution created: {item.Title}");
      }

      [HttpPut ("/api/solutions/{id}")]
      public ActionResult UpdateSolution (int id, [FromBody] SolutionRequest item)
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

         var solution = new Solution
         {
            ID = id,
            Title = item.Title,
            UpdatedBy = "Oscar Negrete",
            LastUpdate = now
         };

         try
         {
            _solutionService.UpdateSolution (solution);
         }
         catch (Exception e)
         {
            if (!_solutionService.SolutionExists (id))
            {
               return NotFound ();
            }
            else
            {
               return BadRequest (e.Message);
            }
         }

         return Ok ($"Solution updated: {solution.ID} - {solution.Title}");
      }
   }
}
