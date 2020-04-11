using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Models.HiAccounting;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP_SPARTAN.Controllers
{
    [Authorize(Roles = nameof(RolsAuthorization.HILoans))]
    public class LoanController : BaseController
    {
        private readonly IUnitOfWork _service;
        public LoanController(IUnitOfWork unitOfWork) => _service = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _service.LoanService.GetAllWithRelationShip(GetUserLoggedId()));

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _service.ClientUserService.GetAllWithRelationships(GetUserLoggedId(), null);
            ViewBag.Clients = result.Select(x => new SelectListItem { Text = x.User.FullName, Value = x.Id.ToString() });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Loan model)
        {
            model.UserId = GetUserLoggedId();
            var clients = await _service.ClientUserService.GetAllWithRelationships(GetUserLoggedId(), null);
            ViewBag.Clients = clients.Select(x => new SelectListItem { Text = x.User.FullName, Value = x.Id.ToString() });
            if (!ModelState.IsValid) return View(model);

            var result = await _service.LoanService.Add(model);
            if (!result)
            {
                BasicNotification("Lo sentimos, Intente de nuevo", NotificationType.error);
                return View(model);
            }
            BasicNotification("Agregado correctamente", NotificationType.success);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.LoanService.GetByIdWithRelationships(id);
            if (result == null) new NotFoundView();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _service.LoanService.SoftRemove(id);
            if (!result) return BadRequest();
            return Ok(result);
        }
    }
}