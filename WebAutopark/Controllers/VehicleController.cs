using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Enums;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(VehicleSortCriteria? criteria, bool? isAscending)
        {
            IEnumerable<VehicleModel> vehicles = null;
            // check nullable value
            if (!criteria.HasValue)
            {
                vehicles = await _vehicleService.GetAll();
                return View(_mapper.Map<IEnumerable<VehicleViewModel>>(vehicles));
            }

            isAscending ??= false;
            // !check nullable value

            vehicles = await _vehicleService.GetAll(criteria.Value, isAscending.Value);

            return View(_mapper.Map<IEnumerable<VehicleViewModel>>(vehicles));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleInfo(int id)
        {
            var getModel = await _vehicleService.GetById(id);

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
                await _vehicleService.Create(_mapper.Map<VehicleModel>(vehicle));
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VehicleUpdate(int id)
        {
            var updateModel = await _vehicleService.GetById(id);

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
                await _vehicleService.Update(_mapper.Map<VehicleModel>(vehicle));
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [HttpGet]
        [ActionName("VehicleDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _vehicleService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> VehicleDelete(int id)
        {
            await _vehicleService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}