using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WebAutopark.BusinessLogic.Models;
using WebAutopark.Core.Entities;
using WebAutopark.DataAccess.Repositories.Base;
using DetailViewModel = WebAutopark.Models.DetailViewModel;

namespace WebAutopark.Controllers
{
    public class DetailController : Controller
    {
        private readonly IRepository<Detail> _detailRepository;
        private readonly IMapper _mapper;


        public DetailController(IRepository<Detail> detailRepository, IMapper mapper)
        {
            _detailRepository = detailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderedEnumerable = await _detailRepository.GetAll();

            return View(_mapper.Map<IEnumerable<DetailViewModel>>(orderedEnumerable));
        }

        [HttpGet]
        public async Task<IActionResult> DetailInfo(int id)
        {
            var getModel = await _detailRepository.GetById(id);

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
                await _detailRepository.Create(_mapper.Map<Detail>(detail));
                return RedirectToAction("Index");
            }

            return View(detail);
        }

        [HttpGet]
        public async Task<IActionResult> DetailUpdate(int id)
        {
            var updateModel = await _detailRepository.GetById(id);

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
                await _detailRepository.Update(_mapper.Map<Detail>(detail));
                return RedirectToAction("Index");
            }

            return View(detail);
        }

        [HttpGet]
        [ActionName("DetailDelete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var deleteModel = await _detailRepository.GetById(id);

            if (deleteModel is null)
                return NotFound();

            return View(_mapper.Map<DetailViewModel>(deleteModel));
        }

        [HttpPost]
        public async Task<IActionResult> DetailDelete(int id)
        {
            await _detailRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}