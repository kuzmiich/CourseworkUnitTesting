using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    [Route("vehicleTypes/")]
    public class VehicleTypeController : Controller
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IMapper _mapper;

        public VehicleTypeController(IVehicleTypeService vehicleTypeService, IMapper mapper)
        {
            _vehicleTypeService = vehicleTypeService;
            _mapper = mapper;
        }

        [HttpGet("index/")]
        public async Task<IActionResult> Index()
        {
            var models = await _vehicleTypeService.GetAll();

            return View(_mapper.Map<IEnumerable<VehicleTypeViewModel>>(models));
        }

        [HttpGet("info/")]
        public async Task<IActionResult> VehicleTypeInfo(int id)
        {
            var model = await _vehicleTypeService.GetById(id);

            if (model is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(model));
        }

        [HttpGet("create/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public IActionResult VehicleTypeCreate()
        {
            return View();
        }

        [HttpPost("create/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleTypeCreate(VehicleTypeViewModel vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypeService.Create(_mapper.Map<VehicleTypeModel>(vehicleType));
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet("update/")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> VehicleTypeUpdate(int id)
        {
            var updatedModel = await _vehicleTypeService.GetById(id);

            if (updatedModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(updatedModel));
        }

        [HttpPost("/update")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
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

        [HttpGet("/delete")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("VehicleTypeDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deletedModel = await _vehicleTypeService.GetById(id);

            if (deletedModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(deletedModel));
        }

        [HttpPost("/delete")]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> VehicleTypeDelete(int id)
        {
            await _vehicleTypeService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}