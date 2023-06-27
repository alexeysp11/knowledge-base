using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Concepts.Models;
using Concepts.Core.Middlewares;

namespace Concepts.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConceptController : ControllerBase
    {
        private readonly ILogger<ConceptController> _logger;

        public ConceptController(ILogger<ConceptController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Concept Get()
        {
            return new Concept
            {
                Name = "Name", 
                Family = "Family",
                ExecSummary = "Summary"
            };
        }

        [HttpGet("{name}")]
        public Concept Get(string name)
        {
            return new Concept
            {
                Family = name,
                IsExecuted = true,
                Result = "result for " + name,
                ExecSummary = "Summary " + name
            };
        }

        [HttpGet("solid/{name}")]
        public Concept GetSolid(string name)
        {
            return new Concept
            {
                Name = name, 
                Family = "SOLID",
                IsExecuted = true,
                Result = "result for " + name,
                ExecSummary = "Executed"
            };
        }

        [HttpGet("grasp/{name}")]
        public Concept GetGrasp(string name)
        {
            return new Concept
            {
                Name = name, 
                Family = "GRASP",
                IsExecuted = true,
                Result = "result for " + name,
                ExecSummary = "Executed"
            };
        }

        [HttpGet("design/{name}")]
        public Concept GetDesign(string name)
        {
            return new Concept
            {
                Name = name, 
                Family = "Design patterns",
                IsExecuted = true,
                Result = "result for " + name,
                ExecSummary = "Executed"
            };
        }

        [HttpGet("middlewares/{name}")]
        public Concept GetMiddleware(string name)
        {
            string result = string.Empty; 
            IMiddlewarePipe pipe = 
                name == "MiddlewarePipeFunc" ? (IMiddlewarePipe)(new MiddlewarePipeFunc()) 
                    : (IMiddlewarePipe)(new MiddlewarePipeDI()); 
            try 
            {
                result = pipe.Execute();
            }
            catch (System.Exception ex)
            {
                result = "Unable to execute: " + ex.Message; 
            }
            return new Concept
            {
                Name = name, 
                Family = "Middlewares",
                IsExecuted = true,
                Result = "result for " + name + ": " + result,
                ExecSummary = "Executed"
            };
        }

        [HttpPost]
        public Concept Post(Concept concept)
        {
            return concept;
        }
    }
}
