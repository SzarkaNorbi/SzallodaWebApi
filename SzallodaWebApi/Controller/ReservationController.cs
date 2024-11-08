using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzallodaWebApi.Context;
using SzallodaWebApi.Entities;

namespace SzallodaWebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(HotelContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Reservation reservation)
        {
            context.Reservations.Add(reservation);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var reservations = await context.Reservations.ToListAsync();
            return Ok(reservations);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var reservation = await context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
            if (reservation is null) { return NotFound(); }
            return Ok(reservation);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Reservation updatedReservation)
        {
            var reservationToUpdate = await context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
            if (reservationToUpdate is null) { return NotFound(); }
            reservationToUpdate.GuestId = updatedReservation.GuestId;
            reservationToUpdate.RoomId = updatedReservation.RoomId;
            reservationToUpdate.ArriveDate = updatedReservation.ArriveDate;
            reservationToUpdate.DepartureDate = updatedReservation.DepartureDate;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var reservationToDelete = await context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
            if (reservationToDelete is null) { return NotFound(); }

            context.Reservations.Remove(reservationToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}