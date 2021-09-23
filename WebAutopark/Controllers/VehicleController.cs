using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.Core.Enums;
using WebAutopark.DataAccess.Repositories.Base;
using WebAutopark.DataAccess.Repositories.ExtendRepository;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(VehicleSortCriteria? criteria, bool? isAscending)
        {
            IEnumerable<Vehicle> vehicles = null;
            // check nullable value
            if (!criteria.HasValue)
            {
                vehicles = await _vehicleRepository.GetAll();
                return View(_mapper.Map<IEnumerable<VehicleViewModel>>(vehicles));
            }

            isAscending ??= false;
            // !check nullable value

            vehicles = await _vehicleRepository.GetAll(criteria.Value, isAscending.Value);

            return View(_mapper.Map<IEnumerable<VehicleViewModel>>(vehicles));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleInfo(int id)
        {
            var getModel = await _vehicleRepository.Get(id);

            if (getModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleViewModel>(getModel));
        }

        [HttpGet]
        public IActionResult VehicleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleCreate(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehicleRepository.Create(_mapper.Map<Vehicle>(vehicle));
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VehicleUpdate(int id)
        {
            var updateModel = await _vehicleRepository.Get(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleViewModel>(updateModel));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleUpdate(VehicleViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehicleRepository.Update(_mapper.Map<Vehicle>(vehicle));
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpGet]
        [ActionName("VehicleDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _vehicleRepository.Get(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDelete(int id)
        {
            await _vehicleRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}