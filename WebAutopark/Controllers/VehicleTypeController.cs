﻿using System;
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
            var models = await _vehicleTypeService.GetAll();

            return View(_mapper.Map<IEnumerable<VehicleTypeViewModel>>(models));
        }

        [HttpGet]
        public async Task<IActionResult> VehicleTypeInfo(int id)
        {
            var model = await _vehicleTypeService.GetById(id);

            if (model is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(model));
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstants.Admin)]
        public IActionResult VehicleTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VehicleTypeCreate(VehicleTypeViewModel vehicleTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypeService.Create(_mapper.Map<VehicleTypeModel>(vehicleTypeViewModel));
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> VehicleTypeUpdate(int id)
        {
            var updatedModel = await _vehicleTypeService.GetById(id);

            if (updatedModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(updatedModel));
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
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deletedModel = await _vehicleTypeService.GetById(id);

            if (deletedModel is null)
                return NotFound();

            return View(_mapper.Map<VehicleTypeViewModel>(deletedModel));
        }

        [HttpPost]
        public async Task<IActionResult> VehicleTypeDelete(int id)
        {
            await _vehicleTypeService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}