using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ManagementDbContext _dbContext;
        public UserController(ManagementDbContext context)
        {
            _dbContext = context;
        }
        // GET: /<controller>/
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                if(users.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] UserRequest request)
        {
            User user = new User(){
                City = request.City,
                UserName = request.UserName,
                LastName = request.LastName

            };
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] UserRequest request)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
                if(user == null)
                {
                    return StatusCode(400, "User not found");
                }
                user.LastName = request.LastName;
                user.UserName = request.UserName;
                user.City = request.City;
                _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch(Exception)
            {
                return StatusCode(500, "An error occurred");
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return StatusCode(400, "User not found");
                }
                _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
  
    }
}

