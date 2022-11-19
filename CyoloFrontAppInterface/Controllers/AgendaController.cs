using CyoloFrontAppInterface.Data;
using Microsoft.AspNetCore.Mvc;

namespace CyoloFrontAppInterface.Controllers
{
    public class AgendaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        // POST: AgendaController/Create
        public async Task<JsonResult?> Create(
            string courtcaseno,
            string jurisdiction,
            string uploaderemail,
            string firstname,
            string lastname,
            string chamberid,
            string hearingdate,
            string hearingtime,
            string hearingtype
        )
        {
            AgendaDto agenda = new AgendaDto
            {
                courtCaseNo = courtcaseno,
                jurisdiction = jurisdiction,
                hearingDate = hearingdate,
                hearingTime = hearingtime,
                hearingType = hearingtype,
                uploaderEmail = uploaderemail,
                firstName = firstname,
                lastName = lastname,
                chamberId = chamberid
            };
            BackendServerAPI ls = new BackendServerAPI();
            var response = await ls.UploadAgenda(agenda);
            return new JsonResult(response);
        }
    }
}
