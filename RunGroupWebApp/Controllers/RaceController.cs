using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        
        private readonly IRaceRepository raceRepository;
        private readonly IPhotoService photo;

        public RaceController(IRaceRepository raceRepository, IPhotoService photo)
        {
            this.raceRepository = raceRepository;
            this.photo = photo;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> race=await raceRepository.GetAll();
            return View(race);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Race race = await raceRepository.GetByIdAsync(id);
            return View(race);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await photo.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    }
                };
                raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else 
            {
                ModelState.AddModelError("","Upload Image Failed");

            }
            return View(raceVM);
            
        }
    }
}
