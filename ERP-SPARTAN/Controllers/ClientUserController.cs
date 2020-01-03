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
using Models.Enums;
using Models.Models;

namespace ERP_SPARTAN.Controllers
{
    [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]
    public class ClientUserController : BaseController
    {
        private readonly IUnitOfWork _service;
        UserManager<User> _userManager;
        public ClientUserController(IUnitOfWork clientUserService, UserManager<User> userManager)
        {
            _service = clientUserService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index() => View(await _service.ClientUserService.GetAllWithRelationships());

        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(User client)
        {
            //for implements
            var result = await _userManager.CreateAsync(new User { });
            if (result.Succeeded)
            {
                if (ModelState.IsValid)
                {
                    if (await _service.ClientUserService.Add(new ClientUser { }))
                    {
                        BasicNotification("Cliente Agregado!", NotificationType.success);
                        return RedirectToAction(nameof(Index));
                    }
                    BasicNotification("Ocurrió un error, no se pudo guardar el cliente", NotificationType.error);
                    return View(client);
                }
            }           
            return View(client);
        }

    }
}