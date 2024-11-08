using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzallodaWebApi.Context;
using SzallodaWebApi.Entities;

namespace SzallodaWebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController(HotelContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Guest guest)
        {
            context.Guests.Add(guest);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = guest.Id }, guest);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var guests = await context.Guests.ToListAsync();
            return Ok(guests);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var guest = await context.Guests.FirstOrDefaultAsync(guest => guest.Id == id);
            if (guest is null) { return NotFound(); }
            return Ok(guest);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Guest updatedGuest)
        {
            var guestToUpdate = await context.Guests.FirstOrDefaultAsync(guest => guest.Id == id);
            if (guestToUpdate is null) { return NotFound(); }
            guestToUpdate.Name = updatedGuest.Name;
            guestToUpdate.Email = updatedGuest.Email;
            guestToUpdate.PhoneNumber = updatedGuest.PhoneNumber;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var guestToDelete = await context.Guests.FirstOrDefaultAsync(guest => guest.Id == id);
            if (guestToDelete is null) { return NotFound(); }

            context.Guests.Remove(guestToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}