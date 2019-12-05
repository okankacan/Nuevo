using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuevoInterView.CMS.Models;
using NuevoInterView.CMS.Repository.Interface;

namespace NuevoInterView.CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPingControlRepository _pingControlRepository;
        private readonly IPingCheckRepository _pingCheckRepository;
        private readonly IEmailService _emailService;

        public HomeController(IPingControlRepository pingControlRepository, IPingCheckRepository pingCheckRepository, IEmailService emailService)
        {
            _pingControlRepository = pingControlRepository;
            _pingCheckRepository = pingCheckRepository;
            _emailService = emailService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]

        public async Task<IActionResult> PingManagement()
        {
            var result = await _pingControlRepository.GetListAsync();

            return View(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PingControl model)
        {
            var result = await _pingControlRepository.SaveOrUpdateAsync(model);
            if (result.success)
                return RedirectToAction("PingManagement");

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _pingControlRepository.FindAsync(id);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PingControl model)
        {
            var result = await _pingControlRepository.SaveOrUpdateAsync(model);
            if (result.success)
                return RedirectToAction("PingManagement");

            return View(model);
        }

        public async Task<IActionResult> PinControl(int id)
        {
            var result = await _pingControlRepository.FindAsync(id);
            var pingResult = await _pingCheckRepository.Check(result.Url);
            if (pingResult.Status == HttpStatusCode.OK)
            {
                result.IsActive = true;
                await _pingControlRepository.SaveOrUpdateAsync(result);
            }
            else
            {
                result.IsActive = false;
                await _pingControlRepository.SaveOrUpdateAsync(result);
                string body = ($"{result.Url} site kapalı durumda. Status : {pingResult.Status}");
                await _emailService.SendEmailAsync(result.Email, body, "Site Ping Sonucu");
            }

            return RedirectToAction("PingManagement");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
