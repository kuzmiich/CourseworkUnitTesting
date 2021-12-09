using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebAutopark.BusinessLayer.Interfaces;
using WebAutopark.BusinessLayer.Models;
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
        public async Task<IActionResult> DetailInfo(Guid id)
        {
            var getModel = await _detailService.GetById(id);

            if (getModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(getModel));
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
        public async Task<IActionResult> DetailUpdate(Guid id)
        {
            var updateModel = await _detailService.GetById(id);

            if (updateModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(updateModel));
        }

        [HttpPost]
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
        [ActionName("DetailDelete")]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            var deleteModel = await _detailService.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> DetailDelete(Guid id)
        {
            await _detailService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}