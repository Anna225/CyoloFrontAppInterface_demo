using CyoloFrontAppInterface.Data;
using CyoloFrontAppInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace CyoloFrontAppInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = HttpContext.Session.GetString("userinfo");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: HomeController/GetAllTypes
        [HttpGet("Home/GetAllTypes")]
        public async Task<List<CourtDto>> GetAllTypes()
        {
            BackendServerAPI ls = new BackendServerAPI();
            return await ls.GetAllTypes();
        }

        //[HttpPost]
        //public async Task<ResponseDto> GetCourtCaseList()
        //{
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
        //    int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        //    string date = "2022-11-09";
        //    string name = "DAL MURIEL";
        //    BackendServerAPI ls = new BackendServerAPI();
        //    return await ls.GetEmployeeList(
        //            draw,
        //            sortColumn,
        //            sortColumnDirection,
        //            searchValue,
        //            pageSize,
        //            skip,
        //            date,
        //            name
        //        );
        //}
    }

}