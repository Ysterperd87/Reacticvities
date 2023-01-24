using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseAPIController
    {
        public DataContext Context { get; }
        public ActivitiesController(DataContext context)
        {
            this.Context = context;
        }

        [HttpGet] //api/activites
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await this.Context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] //api/acitivities/
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await this.Context.Activities.FindAsync(id);
        }
    }
}