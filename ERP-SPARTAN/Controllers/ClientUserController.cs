using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinesLogic.Interfaces;
using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Enums;
using Models.Models;
using Models.Settings;
using Models.ViewModels.ClientUsers;

namespace ERP_SPARTAN.Controllers
{
    [Authorize]
    public class ClientUserController : BaseController
    {
        private readonly IUnitOfWork _service;
        private readonly DefaultValue _settings;
        private readonly UserManager<User> _userManager;
        public ClientUserController(IUnitOfWork clientUserService, UserManager<User> userManager, IOptions<DefaultValue> options)
        {
            _service = clientUserService;
            _userManager = userManager;
            _settings = options.Value;
        }
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        public async Task<IActionResult> Index()
            => View(await _service.ClientUserService
                .GetAllWithRelationships(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        [HttpGet]
        public IActionResult Create() => View();
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel client)
        {
            client.Rol = User.IsInRole(nameof(RolsAuthorization.ClientsUser)) ? RolsAuthorization.Client : client.Rol;
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Name = client.Name,
                    UserName = client.Email,
                    LastName = client.LastName,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber
                }, _settings.Password);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(client.Email);
                    if (await _service.ClientUserService.Add(new ClientUser
                    {
                        UserId = user.Id,
                        CreatedBy = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value
                    }))
                    {
                        var resultRol = await _userManager.AddToRoleAsync(user, client.Rol.ToString());
                        if (resultRol.Succeeded)
                        {
                            BasicNotification("Cliente Agregado!", NotificationType.success);
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    BasicNotification("Ocurrió un error, no se pudo guardar el cliente", NotificationType.error);
                    return View(client);
                }
                else
                {
                    ModelState.AddModelError(nameof(client.Email), "Error ya existe un cliente con el correo " + client.Email);
                }
            }

            return View(client);
        }

        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser) + "," + nameof(RolsAuthorization.Client))]

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)   
        {
            var model = await _service.ClientUserService.GetByIdWithRelationships(id);
            if (model != null) return View(model);
            return new NotFoundView();
        }
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        [HttpPost]
        public async Task<IActionResult> Update(ClientUser model)
        {
            if (ModelState.IsValid)
            {
                if (await _service.UserService.Update(model.User))
                {
                    BasicNotification("Cliente actualizado", NotificationType.success);
                    return RedirectToAction(nameof(Index));
                }
                BasicNotification("Intente de nuevo, una de las causas es que ya exista alguien con este correo intente con otro",
                         NotificationType.error, "Lo sentimos no se pudo actualizar");
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(GetById), model);
        }
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _service.ClientUserService.GetById(id);
            if (user == null) return NotFound();
            if (await _service.UserService.LockAndUnlockUser(user.UserId))
            {
                if (await _service.ClientUserService.SoftRemove(id)) return Ok(true);
            }
            return BadRequest();
        }
        [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]

        [HttpPost]
        public async Task<IActionResult> LockOrUnlockUser(string id)
        {
            if (await _service.UserService.LockAndUnlockUser(id)) return Ok(true);
            return BadRequest();
        }

        [Authorize(Roles = nameof(RolsAuthorization.Client))]
        [HttpGet]
        public async Task<IActionResult> GetMyClientAccount(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null) return NotFound();
            var client = await _service.ClientUserService.GetClientByUserId(user.Id);
            if (client == null) return NotFound();
            return RedirectToAction(nameof(MovementController.GetByClientUser),"Movement", new { Id = client.Id });
        }
    }
}