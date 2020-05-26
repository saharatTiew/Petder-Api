using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using petder.data;
using System.Linq;
using petder.Messages;
using Microsoft.EntityFrameworkCore;
using petder.Models.ViewModels;
using petder.Models.DataModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using petder.Providers;

namespace petder.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class ProfileController : BaseController
    {
        public ProfileController(ApplicationDbContext _db, IMapper _mapper) : base(_db, _mapper) {}
        
        [HttpGet("users/{userId}/current-pet-profiles")]
        public IActionResult CurrentProfiles(long userId)
        {
            try 
            {
                var pet = _db.Pet.Include(x => x.Breed)
                                 .Include(x => x.PetImages)
                                 .FirstOrDefault(x => x.user_id == userId
                                                   && x.is_current);
            
                if (pet == null) return NotFound(ErrorMessage.PetNotFound());

                var address = _db.User.Include(x => x.Address)
                                      .FirstOrDefault(x => x.user_id == userId)
                                      .Address
                                      .name;
                
                var petProfile = _mapper.Map<Pet, PetProfileViewModel>(pet);
                var petImg = pet.PetImages.FirstOrDefault(x => x.pet_id == pet.pet_id && x.is_profile);
                petProfile.Address = address;
                petProfile.ImageUrl = petImg.image_url;
                petProfile.ImageId = petImg.image_id;
                
                return Json(petProfile);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }

        [HttpGet("breeds")]
        public IActionResult Breeds()
        {
            var breed = _db.Breed.Select(x => new 
                                        {
                                            BreedId = x.breed_id,
                                            Name = x.name
                                        });
            return Json(breed);
        }

        //yyyy-MM-dd
        [HttpPost("users/{userId}/pets")]
        public async Task<IActionResult> Pets(long userId, CreatePetViewModel model)
        {
            try
            {
                if (_db.User.Find(userId) == null) return NotFound(ErrorMessage.UserNotFound());
                if (_db.Breed.Find(model.BreedId) == null) return NotFound(ErrorMessage.BreedNotFound());

                DateTime birthDatetime;
                if (!DateTime.TryParse(model.BirthDatetime, out birthDatetime)) return BadRequest(ErrorMessage.DateTimeWrongFormat());

                var pet = _mapper.Map<Pet>(model);
                pet.birth_datetime = birthDatetime;
                pet.user_id = userId;

                if (!_db.Pet.Any(x => x.user_id == userId)) {pet.is_current = true;}

                _db.Pet.Add(pet);
                await _db.SaveChangesAsync();
                await Images(pet.pet_id, model.PetImages);
                return Json(pet);
            }
            catch (TimeoutException e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }

        [HttpPost("pets/{petId}/images")]
        private async Task<bool> Images(long petId, List<CreatePetImagesViewModel> model)
        {
            try
            {
                foreach (var img in model)
                {
                    _db.PetImage.Add(new PetImage {pet_id = petId, image_url = img.ImageUrls, is_profile = img.IsProfile});
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false;} 
        }
        //will fix address
        [HttpGet("users/{userId}/current-pets")]
        public IActionResult CurrentPets(long userId)
        {
            try 
            {
                var pet = _db.Pet.Include(x => x.PetImages)
                                 .FirstOrDefault(x => x.user_id == userId && x.is_current);

                var petImages = pet.PetImages
                                   .OrderByDescending(y => y.is_profile)
                                   .Select(z => new EditImageViewModel
                                             {
                                                ImageId = z.image_id,
                                                ImageUrl = z.image_url,
                                                IsProfile = z.is_profile
                                             })
                                   .ToList();
                                                                  
                var petProfile = _mapper.Map<Pet, EditProfileViewModel>(pet);
                
                var address = _db.User.Include(x => x.Address)
                                      .FirstOrDefault(x => x.user_id == userId)
                                      .Address;

                petProfile.Address = address.name;
                petProfile.AddressId = address.address_id;
                petProfile.PetImages = petImages;

                return Json(petProfile);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }

        //will fix address
        [HttpPost("users/{userId}/edit-pets/{petId}")]
        public async Task<IActionResult> EditPets(long userId, long petId, EditProfileViewModel model)
        {
            try
            {
                var user = _db.User.Find(userId);
                if (user == null) return NotFound(ErrorMessage.UserNotFound());
                user.address_id = _db.Addresses.FirstOrDefault(x => x.name == model.Address).address_id;

                var pet = _db.Pet.Find(petId);
                if (pet == null) return NotFound(ErrorMessage.PetNotFound());

                pet.name = model.Name;
                pet.breed_id = model.BreedId;
                pet.gender = model.Gender;
                pet.birth_datetime = model.BirthDateTime;
                pet.description = model.Description;
                pet.is_current = model.IsCurrent;

                foreach (var imgModel in model.PetImages) {

                    if (imgModel.ImageId == 0) {
                        _db.PetImage.Add(new PetImage
                        {
                            image_url = imgModel.ImageUrl,
                            pet_id = petId,
                            is_profile = imgModel.IsProfile
                        });
                        
                    } else {
                        var petImage = _db.PetImage.Find(imgModel.ImageId);
                        petImage.image_url = imgModel.ImageUrl;
                        petImage.is_profile = imgModel.IsProfile;
                    }
                }
                await _db.SaveChangesAsync();
                return Json(pet);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }
        
        [HttpPost("images/{imageId}")]
        public async Task<IActionResult> EditProfileImg(long imageId, EditPetCurrentImgViewModel model)
        {
            try 
            {
                var petImage = _db.PetImage.Find(imageId);
                petImage.image_url = model.ImageUrl;
                await _db.SaveChangesAsync();
                return Json(petImage);
            }
            catch (NullReferenceException) { return NotFound(ErrorMessage.ImageNotFound()); }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }

        [HttpGet("users/{userId}/pets")]
        public IActionResult Pets(long userId)
        {
            try 
            {   
                var pets = _db.Pet.Include(x => x.Breed)
                                .Include(x => x.PetImages)
                                .Where(x => x.user_id == userId && !x.is_current)
                                .Select(x => new
                                {
                                    ImageUrl = x.PetImages.FirstOrDefault(x => x.is_profile).image_url,
                                    Name = x.name,
                                    Breed = x.Breed.name,
                                    PetId = x.pet_id
                                });

                if (!pets.Any()) return NotFound(ErrorMessage.PetNotFound());
                return Json(pets);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); } 
        }

        [HttpPost("pets/edit-pets")]
        public async Task<IActionResult> EditCurrentPet(EditCurrentPetViewModel model)
        {
            try
            {
                _db.Pet.Find(model.OldPetId).is_current = false;
                var currentPet = _db.Pet.Find(model.NewPetId);
                currentPet.is_current = true;
                await _db.SaveChangesAsync();
                return Json(currentPet);
            }
            catch (Exception e) { return BadRequest(ErrorMessage.Exception(e.Message)); }
        }
    }
}