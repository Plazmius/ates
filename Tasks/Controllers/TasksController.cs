using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Models;
using Tasks.Persistence;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private TasksContext _context;
        private IMapper _mapper;

        public TasksController(TasksContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,worker,manager")]
        [HttpGet("")]
        public async Task<IActionResult> Dashboard()
        {
            var tasks = _mapper.Map<List<DashboardTask>>(
                await _context.Tasks.ToListAsync());
            if (tasks.Count > 0)
                return Json(tasks);
            return NoContent();
        }
    }
}