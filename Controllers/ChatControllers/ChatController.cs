using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using petder.data;
using System.Linq;
using petder.Messages;
using Microsoft.EntityFrameworkCore;
using petder.Models.ViewModels;
using petder.Models.ViewModels.Requests;
using petder.Models.DataModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using petder.Providers;
using petder.Models.ViewModels.Chats;

namespace petder.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class ChatController : BaseController
    {
        public ChatController(ApplicationDbContext _db, IMapper _mapper, IDateTimeProvider dateTimeProvider) : base(_db, _mapper, dateTimeProvider) {}

        [HttpGet("pets/{petId}/sessions")]
        public IActionResult Sessions(long petId, int pageNumber = 1, int pageSize = 7)
        {
            try
            {
                var sessions = _db.Sessions.Include(x => x.RequestList)
                                                .ThenInclude(x => x.RequestedPet)
                                                    .ThenInclude(x => x.PetImages)
                                            .Include(x => x.RequestList)
                                                .ThenInclude(x => x.RequesterPet)
                                                    .ThenInclude(x => x.PetImages)
                                            .Include(x => x.Messages)
                                            .Where(x => x.RequestList.pet_id == petId
                                                    || x.RequestList.requested_pet_id == petId)
                                            .OrderByDescending(x => x.create_datetime)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList()
                                            .Select(x => new SessionViewModel
                                            {
                                                SessionId = x.session_id,
                                                ReceiverPetId = x.RequestList.pet_id == petId ? x.RequestList.requested_pet_id 
                                                        : x.RequestList.pet_id,
                                                Name = x.RequestList.pet_id == petId ? 
                                                x.RequestList.RequestedPet.name :
                                                x.RequestList.RequesterPet.name,

                                                ImageUrl = x.RequestList.pet_id == petId ?
                                                x.RequestList.RequestedPet.PetImages.FirstOrDefault(x => x.is_profile).image_url :
                                                x.RequestList.RequesterPet.PetImages.FirstOrDefault(x => x.is_profile).image_url,

                                                Message = x.Messages.LastOrDefault().message,         
                                                SentDateTime = _dateTimeProvider.SpecifyUtcKind(x.Messages.LastOrDefault().sent_datetime)                                        
                                            });                  
                return Json(sessions);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }

        [HttpGet("sessions/{sessionId}/{petId}/{anotherPetId}")]
        public IActionResult Sessions(long sessionId, long petId, long anotherPetId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var messages = _db.Messages.Include(x => x.Session)
                                                .ThenInclude(x => x.RequestList)
                                                    .ThenInclude(x => x.RequestedPet)
                                                        .ThenInclude(x => x.PetImages)
                                           .Include(x => x.Session)
                                                .ThenInclude(x => x.RequestList)
                                                    .ThenInclude(x => x.RequesterPet)
                                                        .ThenInclude(x => x.PetImages)
                                           .Where(x => x.session_id == sessionId)
                                           .OrderByDescending(x => x.sent_datetime)
                                           .Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .Select(x => new ChatViewModel
                                           {
                                                MessageId = x.message_id,
                                                Message = x.message,
                                                IsUnsent = x.is_unsent,
                                                IsSystemMessage = x.is_system_message,
                                                SendDateTime = _dateTimeProvider.SpecifyUtcKind(x.sent_datetime),
                                                SenderId = x.sender_pet_id,
                                                SenderName = x.sender_pet_id == x.Session.RequestList.requested_pet_id ?
                                                        x.Session.RequestList.RequestedPet.name : 
                                                        x.Session.RequestList.RequesterPet.name,

                                                SenderImageUrl = x.sender_pet_id == x.Session.RequestList.requested_pet_id ? 
                                                        x.Session.RequestList.RequestedPet.PetImages.FirstOrDefault(x => x.is_profile).image_url : 
                                                        x.Session.RequestList.RequesterPet.PetImages.FirstOrDefault(x => x.is_profile).image_url
                                           });
                return Json(messages);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }

        [HttpPost("messages")]
        public async Task<IActionResult> Messages(MessageViewModel model)
        {
            try
            {
                var session = _db.Sessions
                                .Include(x => x.RequestList)
                                .FirstOrDefault(x => x.session_id == model.SessionId);

                if (session == null) return NotFound(ErrorMessage.SessionNotFound());

                if (session.RequestList.pet_id == model.SenderId || session.RequestList.requested_pet_id == model.SenderId) {
                    var message = _mapper.Map<MessageViewModel, Message>(model);
                    _db.Add(message);
                    await _db.SaveChangesAsync();
                    return Json(message);
                }
                return BadRequest(ErrorMessage.PetNotBelongToSession());
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }

        [HttpPost("unsent")]
        public async Task<IActionResult> Unsent(UnsentViewModel model)
        {
            try
            {
                var message = _db.Messages.Find(model.MessageId);
                if (message == null) return NotFound(ErrorMessage.MessageNotFound());

                message.is_unsent = true;
                message.unsent_datetime = _dateTimeProvider.DateTimeNow();
                await _db.SaveChangesAsync();
                return Json(message);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }
    }
}