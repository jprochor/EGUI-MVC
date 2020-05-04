using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalendarProject.Models;
using System.Globalization;
using System.IO;

namespace CalendarProject.Controllers
{

    public class SingleDayController : Controller
    {
    private readonly ILogger<SingleDayController> _logger;

        public SingleDayController(ILogger<SingleDayController> logger)
        {
            _logger = logger;
        }

    public IActionResult SingleDay(int day, int month)
        {
            var tst= new CalendarDate();
            tst.Day=day;
            tst.Month=month;
            var filename= tst.Day+ "." + tst.Month + ".txt";
            if (System.IO.File.Exists(filename)) {
            var logFile = System.IO.File.ReadAllLines(filename);
            var logList = new List<string>(logFile);
            tst.data=logList;
            tst.k=logList.Count;
            return View(tst);
            }
            else
            {
                return RedirectToAction("SingleDayDefault", "SingleDay", new { day=tst.Day, month=tst.Month});
            }
        }

        public IActionResult SingleDayDefault(int day, int month)
        {
            var tst2= new CalendarDate();
            tst2.Day=day;
            tst2.Month=month;
            return View(tst2);
        }

        public IActionResult  AddNew(int day, int month)
        {
            return RedirectToAction("SingleEvent", "SingleEvent", new { day = day, month=month}); 
        }

        public IActionResult Close()
        {
        return View("~/Views/Calendar/Calendar.cshtml");
        }

        [HttpPost]
        public ActionResult  Delete(string row, CalendarDate model)
        {
            int i=Int32.Parse(row);
            int b=i-1;
            int c=b*2;
            var filename= model.Day+ "." + model.Month + ".txt";
            var logFile = System.IO.File.ReadAllLines(filename);
            var logList = new List<string>(logFile);
            model.data=logList;
            model.data.RemoveAt(c);
            model.data.RemoveAt(c);
            model.k=logList.Count;
            using (StreamWriter writer = new StreamWriter(filename))
        {
            for(int j=0;j<model.k;j++)
        writer.WriteLine(model.data[j]);
        }
        
        return RedirectToAction("SingleDay", "SingleDay", new { day = model.Day, month=model.Month});
        }

        public ActionResult  Edit(string row, CalendarDate model)
        {
            int i=Int32.Parse(row);
        return RedirectToAction("SingleEventEdit", "SingleEvent", new { day = model.Day, month=model.Month, row=i});
        }


    }
}