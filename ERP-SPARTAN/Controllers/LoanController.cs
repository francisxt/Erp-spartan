using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Models.HiAccounting;
using Models.ViewModels.HiLoans.Loans;
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
        public async Task<IActionResult> Index(Guid? idEnterprise = null)
        {
            var userId = GetUserLoggedId();
            ViewBag.Enterprises = await _service.EnterpriseService.GetListItem(x => x.UserId == userId);
            return View(await _service.LoanService.GetAllWithRelationShip(userId,idEnterprise));
        }

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
        public async Task<IActionResult> GetById(Guid id, State stateDeb = State.All)
        {
            ViewBag.Selected = stateDeb;
            ViewBag.Action = nameof(GetById);
            ViewBag.AccessUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/{nameof(Loan)}/{nameof(GetMyLoan)}";
            var result = await _service.LoanService.GetByIdWithRelationships(id, stateDeb);
            if (result == null) new NotFoundView();
            return View(result);
        }

        [AllowAnonymous]
        [HttpGet]   
        public async Task<IActionResult> GetMyLoan(Guid id, State stateDeb = State.All)
        {
            ViewBag.Selected = stateDeb;
            ViewBag.Action = nameof(GetMyLoan);
            var result = await _service.LoanService.GetByIdWithRelationships(id, stateDeb);
            if (result == null) new NotFoundView();
            return View(nameof(GetById),result);
        }


        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _service.LoanService.SoftRemove(id);
            if (!result) return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PaymentDeb(PaymentLoanVM model)
        {
            var total =Math.Abs( model.AmortizationTotal - model.Amortization);
            if (model.ExtraMount > total)
            {
                BasicNotification("El monto a abonar es mayor que el capital actual, intente abonar un monto menor", NotificationType.warning, "Error");
                return RedirectToAction(nameof(GetById), new { id = model.IdLoan });
            }
            var result = await _service.LoanService.PaymentDeb(model.IdDeb, model.IdLoan, model.ExtraMount);
            if (!result) BasicNotification("Error intente de nuevo", NotificationType.error);
            BasicNotification("Acción Realizada", NotificationType.success);
            return RedirectToAction(nameof(GetById), new { id = model.IdLoan });
        }

        [HttpGet]
        public IActionResult GetAmortization(Loan model)
            => PartialView("_GetAmortizationPartial", _service.LoanService.GetAmortization(model));
        
    }
}