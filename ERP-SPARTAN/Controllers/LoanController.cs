using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models.HiAccounting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP_SPARTAN.Controllers
{
    [Authorize]
    public class LoanController : BaseController
    {
        private readonly IUnitOfWork _service;
        public LoanController(IUnitOfWork unitOfWork) => _service = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _service.LoanService.Filter(x => x.UserId == GetUserLoggedId()).ToListAsync());

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Loan model)
        {
            model.UserId = GetUserLoggedId();
            if (!ModelState.IsValid) return View(model);
            var result = await _service.LoanService.Add(model);
            if (!result)
            {
                BasicNotification("Lo sentimos, Intente de nuevo", Models.Enums.NotificationType.error);
                return View(model);
            }
            BasicNotification("Agregado correctamente", Models.Enums.NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

    }
}