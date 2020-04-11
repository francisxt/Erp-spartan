using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Models;

namespace ERP_SPARTAN.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _service;
        private UserManager<User> _userManager;
        public UserController(IUnitOfWork service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

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

        [Authorize(Roles = nameof(RolsAuthorization.Admin))]
        [HttpGet]
        public async Task<IActionResult> AddToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return new NotFoundView();
            var result = await _service.UserService.GetUserAsync(id);
            ViewBag.UserRols = await _userManager.GetRolesAsync(result);
            ViewBag.Rols = await _service.RoleService.GetListItem();
            if (result == null) return new NotFoundView();
            return View(result);
        }

        [Authorize(Roles = nameof(RolsAuthorization.Admin))]
        [HttpPost]
        public async Task<IActionResult> AddToRole(string id, string RoleId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(RoleId)) return new NotFoundView();
            var user = await _service.UserService.GetUserAsync(id);
            if (user == null) return new NotFoundView();
            var result = await _service.UserService.AddToRole(id, RoleId);
            if (!result) BasicNotification("Intente Nuevamente", NotificationType.error, "Error");
            BasicNotification("Cliente agregado al nuevo rol, puede continuar agregando mas roles", NotificationType.success, "Listo");
            return RedirectToAction(nameof(AddToRole), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserRole(string id,string roleName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(roleName)) return BadRequest();
            return Ok(await _service.UserService.RemoveUserRoleAsync(id,roleName));
        }

    }
}