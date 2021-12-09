using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IMapper _mapper;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService, IMapper mapper)
        {
            _vehicleTypeService = vehicleTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicleTypes = await _vehicleTypeService.GetAll();

            return View(_mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleTypes));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleTypeInfo(Guid id)
        {
            var getModel = await _vehicleTypeService.GetById(id);

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
                await _vehicleTypeService.Create(_mapper.Map<VehicleTypeModel>(detail));
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> VehicleTypeUpdate(Guid id)
        {
            var updateModel = await _vehicleTypeService.GetById(id);

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
                await _vehicleTypeService.Update(_mapper.Map<VehicleTypeModel>(vehicleType));
                return RedirectToAction("Index");
            }

            return View(vehicleType);
        }

        [HttpGet]
        [ActionName("VehicleTypeDelete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var deleteModel = await _vehicleTypeService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> VehicleTypeDelete(Guid id)
        {
            await _vehicleTypeService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}