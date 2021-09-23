using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _vehicleTypeRepository;
        private readonly IMapper _mapper;

        public VehicleTypeController(IRepository<VehicleType> vehicleTypeRepository, IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAll();

            return View(_mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleTypes));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleTypeInfo(int id)
        {
            var getModel = await _vehicleTypeRepository.Get(id);

            if (getModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(getModel));
        }

        [HttpGet]
        public IActionResult VehicleTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleTypeCreate(VehicleTypeViewModel detail)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypeRepository.Create(_mapper.Map<VehicleType>(detail));
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> VehicleTypeUpdate(int id)
        {
            var updateModel = await _vehicleTypeRepository.Get(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(updateModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleTypeUpdate(VehicleTypeViewModel vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypeRepository.Update(_mapper.Map<VehicleType>(vehicleType));
                return RedirectToAction("Index");
            }

            return View(vehicleType);
        }

        [HttpGet]
        [ActionName("VehicleTypeDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _vehicleTypeRepository.Get(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> VehicleTypeDelete(int id)
        {
            await _vehicleTypeRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}