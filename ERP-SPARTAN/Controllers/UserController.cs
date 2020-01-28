using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Models;

namespace ERP_SPARTAN.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _service;
        public UserController(IUnitOfWork service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return View(await _service.UserService.GetUserAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Update(User model)
        {
            if (ModelState.IsValid)
            {
                if (await _service.UserService.Update(model))
                {
                    BasicNotification("Actualizado Correctamente", NotificationType.success);
                    return RedirectToAction(nameof(Profile));
                }
                BasicNotification("Ha ocurrido un error , una de las razones puede ser que exista un correo igual al ingresado intente con otro", NotificationType.error);
            }
            return View(nameof(Profile), model);
        }
    }
}