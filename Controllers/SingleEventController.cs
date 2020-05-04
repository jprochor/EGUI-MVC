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
using System.Text;

namespace CalendarProject.Controllers
{

    public class SingleEventController : Controller
    {
    private readonly ILogger<SingleEventController> _logger;

        public SingleEventController(ILogger<SingleEventController> logger)
        {
            _logger = logger;
        }

    public IActionResult SingleEvent(int day, int month)
        {
            var tst= new CalendarDate();
            tst.Day=day;
            tst.Month=month;
            return View(tst);
        }

        public IActionResult SingleEventEdit(int day, int month, int row)
        {
            var tst= new CalendarDate();
            tst.Day=day;
            tst.Month=month;
            tst.r=row;
            
            return View(tst);
        }

         [HttpPost]
        public ActionResult  SaveEdit(string newtime, string newdescription, CalendarDate model)
        {
            int i=model.r;
            int b=i-1;
            int c=b*2;
            int d=b*2+1;
            var filename= model.Day+ "." + model.Month + ".txt";
            var logFile = System.IO.File.ReadAllLines(filename);
            var logList = new List<string>(logFile);
            model.data=logList;
            model.data[c]=newtime;
            model.data[d]=newdescription;
            model.k=logList.Count;
            using (StreamWriter writer = new StreamWriter(filename))
        {
            for(int j=0;j<model.k;j++)
        writer.WriteLine(model.data[j]);
        }
        
        return RedirectToAction("SingleDay", "SingleDay", new { day = model.Day, month=model.Month});
        }



        public ActionResult  Save()
        {
            return View("~/Views/SingleDay/SingleDay.cshtml") ; 
        }
        
        [HttpPost]
        public ActionResult  Save(string mydate, string mydescription, CalendarDate model)
        {
            var filename= model.Day+ "." + model.Month + ".txt";
            using (StreamWriter writer = new StreamWriter(filename, append: true))
        {
        writer.WriteLine(mydate);
        writer.WriteLine(mydescription);
        }
        
        return RedirectToAction("SingleDay", "SingleDay", new { day = model.Day, month=model.Month});
        }
        



        public IActionResult Cancel(CalendarDate model)
        {   
        return RedirectToAction("SingleDay", "SingleDay", new { day = model.Day, month=model.Month});
        }

        
        


    }
}