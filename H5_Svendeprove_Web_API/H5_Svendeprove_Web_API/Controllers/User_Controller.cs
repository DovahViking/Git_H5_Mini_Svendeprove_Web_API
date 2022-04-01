#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using H5_Svendeprove_Web_API.Models;
using H5_Svendeprove_Web_API.DTOs;
using Mapster;
using H5_Svendeprove_Web_API.Game_Folder;
using H5_Svendeprove_Web_API.Singletons;

namespace H5_Svendeprove_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_Controller : ControllerBase
    {
        private readonly Customer_Context _context;
        private Singleton_Game singleton;

        public User_Controller(Customer_Context context)
        {
            _context = context;
            singleton = Singleton_Game.init;
        }

        // GET: api/User_
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_DTO>>> Getuser()
        {
            List<User> user_list = await _context.user.Include(s => s.score).Include(d => d.device).ToListAsync();
            return user_list.Adapt<User_DTO[]>();
        }

        // GET: api/User_/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_DTO>> GetUser(int id)
        {
            User user = await _context.user.Include(s => s.score).Include(d => d.device).SingleOrDefaultAsync(u => u.id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user.Adapt<User_DTO>();
        }

        // PUT: api/User_/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] User_DTO userdto)
        {
            if (id != userdto.id)
            {
                return BadRequest();
            }

            var user_object = _context.user.Include(d => d.device).Include(s => s.score).FirstOrDefault(updated_user => updated_user.id == id);

            if (user_object == null)
            {
                return NotFound();
            }

            userdto.Adapt(user_object);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/User_
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User_DTO userdto)
        {
            _context.user.Add(userdto.Adapt<User>());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = userdto.Adapt<User>().id }, userdto);
        }

        // DELETE: api/User_/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.user.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool UserExists(int id)
        {
            return _context.user.Any(e => e.id == id);
        }

        // GET: api/User_/sfsjj83hd8
        [HttpGet("user/{device_id}")]
        public async Task<ActionResult<User_DTO>> get_user_id(string device_id)
        {
            User user = await _context.user.Include(s => s.score).Include(d => d.device).SingleOrDefaultAsync(u => u.device.device_id == device_id);

            if (user == null)
            {
                return NotFound();
            }

            return user.Adapt<User_DTO>();
        }

        // POST: api/User_/user/game
        [HttpPost("user/game/{user_id}")]
        public async Task<IActionResult> post_start_game_notification(int user_id)
        {
            int sequences = 8; // the amount of light sequences to remember, the more, the harder the game will be
            Game game = new Game();
            singleton.pool_array = await game.on_ready(sequences); // singletons are definitely not a good solution to this, but it works for this concept
            singleton.user_id = user_id;                           // the right thing to do would have been to use a socket connection to the NodeMCU

            return Ok();
        }

        // GET: api/User_/user/game/game_data
        [HttpGet("user/game/game_data")]
        public async Task<ActionResult<List<int[]>>> get_game_data()
        {
            int[][] tmp = new int[singleton.pool_array.Count][]; // use of singletons cause it's the easiest way to keep the data without it getting wiped out, it's not the right thing to do though
            singleton.pool_array.CopyTo(tmp, 0);
            singleton.pool_array.Clear();
            List<int[]> game_data = new(tmp);

            return await Task.FromResult(game_data);
        }

        // GET: api/User_/user/game/game_user_id
        [HttpGet("user/game/game_user_id")]
        public async Task<ActionResult<int>> get_game_user_id()
        {
            return await Task.FromResult(singleton.user_id);
        }

        // POST: api/User_/user/game/result
        [HttpPost("user/game/result")]
        public async Task<IActionResult> post_game_result(int id, int score)
        {
            User user = await _context.user.Include(s => s.score).Include(d => d.device).SingleOrDefaultAsync(u => u.id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.score.recent_score = score;

            if (user.score.recent_score > user.score.highest_score)
            {
                user.score.highest_score = user.score.recent_score;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
