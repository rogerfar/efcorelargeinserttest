using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }
        
        [HttpGet]
        [Route("Insert")]
        public async Task<ActionResult> Insert()
        {
            var sw = new Stopwatch();

            await using var db = new DataContext();

            var all = await db.Blogs.ToListAsync();
            db.Blogs.RemoveRange(all);
            await db.SaveChangesAsync();

            var b1 = new Blog
            {
                Data = RandomString(1024 * 1024 * 10) // Insert 10MB of data
            };

            await db.Blogs.AddAsync(b1);

            await db.SaveChangesAsync();

            return Ok(sw.Elapsed);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> Get()
        {
            var sw = new Stopwatch();
            await using var db = new DataContext();

            sw.Start();
            var a = await db.Blogs.FirstOrDefaultAsync();
            sw.Stop();
            
            return Ok(sw.Elapsed);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
