using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzallodaWebApi.Context;
using SzallodaWebApi.Entities;

namespace SzallodaWebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(HotelContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Room room)
        {
            context.Rooms.Add(room);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id = room.Id}, room);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if (room is null) { return NotFound(); }
            return Ok(room);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Room updatedRoom)
        {
            var roomToUpdate = await context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if (roomToUpdate is null) { return NotFound(); }
            roomToUpdate.Name = updatedRoom.Name;
            roomToUpdate.Description = updatedRoom.Description;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var roomToDelete = await context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if (roomToDelete is null) { return NotFound(); }

            context.Rooms.Remove(roomToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
