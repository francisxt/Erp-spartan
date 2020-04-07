using BusinesLogic.UnitOfWork;
using ERP_SPARTAN.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ERP_SPARTAN.Controllers
{
    [Authorize]
    public class AlertController : BaseController
    {
        private readonly IUnitOfWork _services;
        public AlertController(IUnitOfWork service) => _services = service;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyAlerts()
        {
            var result =  _services.AlertService.Filter(x => x.UserId == GetUserLoggedId());
            return View(await result.ToListAsync());
        }
    }
}