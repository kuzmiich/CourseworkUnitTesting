using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
using WebAutopark.Core.Constants;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class DetailController : Controller
    {
        private readonly IDetailService _detailService;
        private readonly IMapper _mapper;


        public DetailController(IDetailService detailService, IMapper mapper)
        {
            _detailService = detailService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderedEnumerable = await _detailService.GetAll();

            return View(_mapper.Map<IEnumerable<DetailViewModel>>(orderedEnumerable));
        }

        [HttpGet]
        public async Task<IActionResult> DetailInfo(int id)
        {
            var model = await _detailService.GetById(id);

            if (model is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(model));
        }

        [HttpGet]
        public IActionResult DetailCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailCreate(DetailViewModel detail)
        {
            if (ModelState.IsValid)
            {
                await _detailService.Create(_mapper.Map<DetailModel>(detail));
                return RedirectToAction("Index");
            }

            return View(detail);
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> DetailUpdate(int id)
        {
            var updateModel = await _detailService.GetById(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(updateModel));
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailUpdate(DetailViewModel detail)
        {
            if (ModelState.IsValid)
            {
                await _detailService.Update(_mapper.Map<DetailModel>(detail));
                return RedirectToAction("Index");
            }

            return View(detail);
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        [ActionName("DetailDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _detailService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(deleteModel));
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoleConstant.Admin)]
        public async Task<IActionResult> DetailDelete(int id)
        {
            await _detailService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}