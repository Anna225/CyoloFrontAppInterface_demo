using CyoloFrontAppInterface.Controllers;
using CyoloFrontAppInterface.Data;
using CyoloFrontAppInterface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using Xamarin.Essentials;
using static System.Collections.Specialized.BitVector32;

namespace CyoloFrontAppInterface.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CourtCaseController : Controller
    {
        private readonly ILogger _logger;
        public CourtCaseController(ILogger<CourtCaseController> logger)
        {
            _logger = logger;
        }
        // GET: CourtCaseController
        public async Task<ActionResult> Index()
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string email = HttpContext.Session.GetString("userinfo")!;
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "User", new { area = "" });
            }

            BackendServerAPI ls = new BackendServerAPI();
            try
            {
                ViewBag.Model = await ls.GetCourtCaseByEmailAndDate(email, date);
                ViewBag.Lawyer = await ls.GetLawyerByEmail(email);
            }
            catch(Exception ex)
            {
                ViewBag.Model = null;
                Console.WriteLine(ex);
            }

            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            ViewBag.Today = date;
            ViewBag.Email = email;
            
            return View();
        }

        // GET: CourtCaseController/GetByEmailAndDate
        public async Task<ActionResult> GetByEmailAndDate(string date, string name)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return RedirectToAction("Login", "User", new { area = "" });
            }

            if (date == null)
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            BackendServerAPI ls = new BackendServerAPI();
            ViewBag.Lawyer = await ls.GetLawyerByEmail(HttpContext.Session.GetString("userinfo"));

            try
            {
                ViewBag.Model = await ls.GetCourtCaseByNameAndDate(name, date);
            }
            catch (Exception ex)
            {
                ViewBag.Model = null;
                Console.WriteLine(ex);
            }
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            ViewBag.Today = date;
            ViewBag.Email = name;


            return View();
        }

        // GET: CourtCaseController/AgendasByEmail
        public async Task<ActionResult> AgendasByEmail(string email)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return RedirectToAction("Login", "User", new { area = "" });
            }

            BackendServerAPI ls = new BackendServerAPI();
            ViewBag.Lawyer = await ls.GetLawyerByEmail(HttpContext.Session.GetString("userinfo"));
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");

            try
            {
                ViewBag.Model = await ls.GetAgendasByEmail(email);
            }
            catch (Exception ex)
            {
                ViewBag.Model = null;
                Console.WriteLine(ex);
            }
            
            return View();
        }

        // GET: CourtCaseController/Match
        public async Task<ActionResult> Match(string courtCaseNo)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return RedirectToAction("Login", "User", new { area = "" });
            }

            BackendServerAPI ls = new BackendServerAPI();
            
            ViewBag.CourtCase = await ls.GetCourtCaseByNo(courtCaseNo);
            ViewBag.Lawyer = await ls.GetLawyerByEmail(HttpContext.Session.GetString("userinfo"));
            ViewBag.AvailableModel = await ls.GetAvailableLawyersByCourtCaseNo(courtCaseNo);
            ViewBag.No = courtCaseNo;
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            
            return View();
        }

        [HttpGet]
        // GET: CourtCaseController/Approve
        public async Task<JsonResult?> Approve(string caseno)
        {
            JsonResult? result = null;
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return result;
            }

            BackendServerAPI ls = new BackendServerAPI();
            var lawyer = await ls.GetLawyerByEmail(HttpContext.Session.GetString("userinfo"));
            var response = await ls.SetAvailableCourtCaseNo(caseno, lawyer.id);
            result = new JsonResult(response);
            return new JsonResult(result);
        }

        // POST: CourtCaseController/GetByCourtCase
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetByCourtCase(IFormCollection collection)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("userinfo")))
            {
                return RedirectToAction("Login", "User", new { area = "" });
            }

            BackendServerAPI ls = new BackendServerAPI();
            
            ViewBag.Lawyer = await ls.GetLawyerByEmail(HttpContext.Session.GetString("userinfo"));

            SearchDto retval = new SearchDto {
                CourtCaseNo = collection["courtcaseno"],
                HearingGeneral = collection["jurisdiction"],
                ChamberID = collection["chamberid"],
                HearingDate = collection["hearingdate"],
                HearingTime = collection["hearingtime"]
            };

            ViewBag.CourtCase = retval;
            ViewBag.AvailableModel = await ls.GetByCourtCase(collection);
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            ViewBag.Number = collection["courtcaseno"];
            
            return View();
        }

        // GET: CourtCaseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourtCaseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourtCaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourtCaseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourtCaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourtCaseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourtCaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string makeJuridictionItem(JuridictionTypeDto dto)
        {
            if(dto.TypeJuridiction == null || dto.TypeJuridiction == "")
            {
                return "Rechtscollege";
            }
            string[] res = dto.TypeJuridiction.Split(" - ");
            string lib_juridiction = res[0] + ' ' + dto.Canton;
            if (dto.DivisionId != "000") { 
                lib_juridiction = lib_juridiction + " AFDELING " + dto.Division; 
            }
            if (dto.TypeJuridictionId == 38) { 
                lib_juridiction = lib_juridiction.Replace("DIVISION", "SIEGE"); 
                lib_juridiction = lib_juridiction.Replace("AFDELING", "ZETEL"); 
            }
            string section = (res.Length > 1) ? ", " + res[1] : "";
            lib_juridiction = lib_juridiction + section;
            return lib_juridiction;
        }
    }

}
