using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalendarProject.Models;
using System.Globalization;

namespace CalendarProject.Controllers
{

    public class CalendarController : Controller
    {
    private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

    public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult CalendarPrev(int month)
        {
            return View(month);
        }

        public IActionResult CalendarNext(int month)
        {
            return View(month);
        }

    }
}