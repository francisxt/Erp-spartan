using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinesLogic.Interfaces;
using BusinesLogic.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Enums;

namespace ERP_SPARTAN.Controllers
{
    [Authorize(Roles = nameof(RolsAuthorization.Admin) + "," + nameof(RolsAuthorization.ClientsUser))]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _service;
        public HomeController(IUnitOfWork service) => _service = service;
        public IActionResult Index()
        {
            var user = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return View(_service.HomeService.Get(user));
        }

    }
}
