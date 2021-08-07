using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APSystem.Models.Appointment;
using APSystem.Services.Appointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace APSystem.Core.Controllers.Appointment
{
    [Route("api/v1/Auth")]
    [ApiController]
    public class AppointmentController : BaseController<AppointmentController>
    {
        IAppointmentService _appointmentService;
        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService appointmentService) : base(logger)
        {
            _appointmentService = appointmentService;
        }
        [HttpPost("AddSlots")]
        public async Task<ActionResult<AppointmentSlots>> AddApSlots([FromBody] AppointmentSlots apslots)
        {
            var addslots = _appointmentService.AddApSlots(apslots);
            return await addslots;
        }

        [HttpGet("ApSlots")]
        public async Task<ActionResult<List<AppointmentSlots>>> AppointmentSlots()
        {
            var result = _appointmentService.AllApSlots();
            return await result;
        }

        [HttpPost("ApSlotsbyid")]
        public async Task<ActionResult<AppointmentSlots>> AppointmentSlotById(int id)
        {
            AppointmentSlots apSlotid = new AppointmentSlots() { AppointmentSlotID = id };
            var apSlotbyid = _appointmentService.ApSlotbyId(apSlotid);
            return await apSlotbyid;
        }

        [HttpPut("UpdateApSlot")]
        public async Task<ActionResult<AppointmentSlots>> UpdateAppointmentSlot([FromQuery] int id, AppointmentSlots apSlots)
        {
            if (id != apSlots.AppointmentSlotID)
            {
                return BadRequest();
            }
            var updatedslot = _appointmentService.UpdateApSlot(apSlots);
            return await updatedslot;
        }

        [HttpDelete("DeleteSlot")]
        public ActionResult DeleteSlot([FromBody] int id)
        {

            _appointmentService.DeleteApSlots(id);
            return Ok();
        }

        [HttpPost("CreateAp")]
        public async Task<ActionResult<Appointments>> CreateAppointment([FromBody] Appointments aps)
        {
            var appointment = _appointmentService.CreateAppointments(aps);
            return await appointment;
        }

        [HttpGet("GetAppointment")]
        public async Task<ActionResult<List<AppointmentwithSlotsjoin>>> GetAppointment()
        {
            var listap = _appointmentService.ListAppointments();
            return await listap;
        }

        [HttpPost("ApbyDocId")]
        public async Task<ActionResult<List<AppointmentwithSlotsjoin>>> GetAppointmentbyDocId([FromBody] Appointments apdocid)
        {
            var _apbydocid = _appointmentService.AppointmentbyDocId(apdocid);
            return await _apbydocid;
        }

        [HttpPost("AppointmentById")]
        public async Task<ActionResult<AppointmentwithSlotsjoin>> GetAppointmentById([FromBody] Appointments apdocid)
        {
            var _apbydocid = _appointmentService.AppointmentById(apdocid);
            return await _apbydocid;
        }

        [HttpPut("EditAppointment")]
        public async Task<ActionResult<Appointments>> UpdateAppointment([FromQuery] int id, Appointments updateAppointment)
        {
            if (id != updateAppointment.AppointmentID)
            {
                return BadRequest();
            }
            var updatedap = await _appointmentService.UpdateApService(updateAppointment);
            return NoContent();
        }

        [HttpDelete("DeleteAppointment")]
        public ActionResult DeleteAppointment(int id)
        {

            if (id != 0)
            {
                _appointmentService.DeleteApService(id);
            }
            return Ok();
        }


    }
}