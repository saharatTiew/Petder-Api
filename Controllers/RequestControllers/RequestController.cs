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

namespace petder.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class RequestController : BaseController
    {
        public RequestController(ApplicationDbContext _db, IMapper _mapper, IDateTimeProvider dateTimeProvider) : base(_db, _mapper, dateTimeProvider) {}

        [HttpGet("filters")]
        public IActionResult Filters()
        {
            try
            {
               List<string> genders = new List<string> {"M","F"};
               var breedings = _db.Breed.Select(x => new {Breed = x.name, BreedingId = x.breed_id}).ToList();
               var ages = _db.Pet.Select(x =>_dateTimeProvider.DateTimeNow().Year - x.birth_datetime.Year).Distinct().ToList();
               var addresses = _db.Addresses.Select(x => x.name).ToList();
               return Json(new {ages, breedings, genders, addresses});
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }  
        }

        [HttpGet("pets/{userId}/{petId}")]
        public IActionResult Pets(long userId, long petId, string gender, long breedId, int age, string address, 
                                  int pageNumber = 1, int pageSize = 7)
        {
            try 
            {
                if (_db.Pet.Find(petId) == null) return NotFound(ErrorMessage.PetNotFound());

                var pet = _db.Pet.Include(x => x.RequestedLists)
                                 .Include(x => x.RequesterLists)
                                 .Include(x => x.BlockedLists)
                                 .Include(x => x.BlockerLists)
                                 .Include(x => x.User)
                                 .Include(x => x.Breed)
                                 .Include(x => x.PetImages)
                                 .Where(x => x.user_id != userId
                                          && x.pet_id != petId
                                          && !x.RequestedLists.Any(y => y.pet_id == petId)
                                          && !x.RequesterLists.Any(y => y.requested_pet_id == petId)
                                          && !x.BlockedLists.Any(y => y.pet_id == petId)
                                          && !x.BlockerLists.Any(y => y.blocked_pet_id == petId))
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize);

                pet = DynamicFilterPet(pet, gender, breedId, age, address);    
                                 
                var petProfile = pet.Select(x => new PetRequestViewModel
                                              {
                                                Name = x.name,
                                                Address = x.User.Address.name,
                                                PetId = x.pet_id,
                                                Breed = x.Breed.name,
                                                ImageUrls = x.PetImages.Select(x => x.image_url).ToList()
                                              });

                if (!petProfile.Any()) return NotFound(ErrorMessage.RequestNotFound());
                return Json(petProfile);  
            } 
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }  
        }

        private IQueryable<Pet> DynamicFilterPet(IQueryable<Pet> pet, string gender, long breedId, int age, string address)
        {
            var query = pet;
            if (gender != null) query = query.Where(x => x.gender == gender);
            if (breedId != 0) query = query.Where(x => x.breed_id == breedId);
            if (address != null) query = query.Where(x => x.User.Address.name == address);
            if (age != 0) {
                int birthYear = _dateTimeProvider.DateTimeNow().Year - age;
                query = query.Where(x => x.birth_datetime.Year == birthYear);
            }
            return query;
        }

        [HttpPost("blocks")]
        public async Task<IActionResult> Blocks(BlockViewModel model) 
        {
            try
            {
                var block = _mapper.Map<BlockViewModel, BlockList>(model);
                _db.BlockList.Add(block);
                await _db.SaveChangesAsync();
                return Json(block);
            }
            
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }  
        }

        [HttpPost("matches")]
        public async Task<IActionResult> Matches(RequestViewModel model) 
        {
            try
            {
                var request = _mapper.Map<RequestViewModel, RequestList>(model);
                request.status_id = 1;
                _db.RequestList.Add(request);
                await _db.SaveChangesAsync();
                return Json(request);
            }
            
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }  
        }

        [HttpPost("likes")]
        public async Task<IActionResult> Likes(RequestViewModel model)
        {
            try
            {
                var requested = _db.Pet.Find(model.RequestedPetId);

                if (requested == null) return NotFound(ErrorMessage.RequestedPetNotFound());

                ++requested.number_of_like;
                var request = _mapper.Map<RequestViewModel, RequestList>(model);
                request.status_id = 1;
                _db.RequestList.Add(request);
                await _db.SaveChangesAsync();
                return Json(request);
            }

            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }  
        }

        [HttpGet("pending-requests/{requestedPetId}")]
        public IActionResult Requests(long requestedPetId)
        {
            try 
            {
                var requests = _db.RequestList.Include(x => x.RequestedPet)
                                                .ThenInclude(x => x.PetImages)
                                              .Include(x => x.RequestedPet)
                                                .ThenInclude(x => x.Breed)
                                              .Where(x => x.requested_pet_id == requestedPetId
                                                       && x.status_id == 1)
                                              .OrderByDescending(x => x.request_datetime)
                                              .Select(x => new PendingRequestViewModel
                                              {
                                                RequestListId = x.request_list_id,
                                                Name = x.RequesterPet.name,
                                                Breed = x.RequesterPet.Breed.name,
                                                RequesterPetId = x.pet_id,
                                                ImageUrl = x.RequesterPet.PetImages.FirstOrDefault(x => x.is_profile).image_url
                                              });

                if (!requests.Any()) return NotFound(ErrorMessage.PendingRequestNotFound());
                return Json(requests);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }

        [HttpPost("requests")]
        public async Task<IActionResult> Requests(AcceptRequestViewModel model)
        {
            try 
            {
                var request = _db.RequestList
                                .Include(x => x.RequestedPet)
                                .FirstOrDefault(x => x.request_list_id == model.RequestListId);
                if (request == null) return NotFound(ErrorMessage.PendingRequestNotFound());

                request.status_id = 2;
                var session = _mapper.Map<AcceptRequestViewModel, Session>(model);
                _db.Sessions.Add(session);
                await _db.SaveChangesAsync();
                await AddCongratMessage(session, request);
                return Json(new { SessionId = session.session_id, CreateDateTime = session.create_datetime });
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }   
        }

        private async Task<bool> AddCongratMessage(Session session, RequestList request)
        {
            var message = new Message
            {
                session_id = session.session_id,
                sender_pet_id = request.requested_pet_id,
                message = request.RequestedPet.name + " accepted request.",
                is_system_message = true
            };
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            return true;
        }

        [HttpGet("ranks")]
        public IActionResult Ranks(int pageSize = 7, int pageNumber = 1)
        {
            try
            {            
                var ranks = _db.Pet.Include(x => x.PetImages)
                                   .OrderByDescending(x => x.number_of_like)
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .Select(x => new RankViewModel
                                   {
                                       Name = x.name,
                                       NumberOfLikes = x.number_of_like,
                                       ImageUrl = x.PetImages
                                                   .FirstOrDefault(x => x.is_profile)
                                                   .image_url
                                   });
                return Json(ranks);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }

        [HttpDelete("pets/{petId}")]
        public async Task<IActionResult> Pets(long petId)
        {
            var pet = _db.Pet.Find(petId);
            if (pet == null) return NotFound(ErrorMessage.PetNotFound());
            var requests = _db.RequestList.Where(x => x.pet_id == petId || x.requested_pet_id == petId);
            var blocks = _db.BlockList.Where(x => x.pet_id == petId || x.blocked_pet_id == petId);

            _db.RequestList.RemoveRange(requests);
            _db.BlockList.RemoveRange(blocks);
            _db.Pet.Remove(pet);
            await _db.SaveChangesAsync();
            return Json(ErrorMessage.DeletePet());
        }
    }
}