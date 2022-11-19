#nullable disable
using CyoloFrontAppInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CyoloFrontAppInterface.Data
{
    public class BackendServerAPI
    {
        private RestClient _client;
        private string _ocp_apim_subscription_key;
        public BackendServerAPI()
        {
            _client = new RestClient("https://cyoloapidemo.azurewebsites.net");
            // _client = new RestClient("https://lawyerapi.azure-api.net");
            _ocp_apim_subscription_key = "f05341ebf7244456b8c3bed62c5795a0";
            // or _ocp_apim_subscription_key = "f32b5ffa39a54d50b82dd80b7fd06bc6";
        }

        public void Dispose()
        {

        }
        
        public async Task<dynamic> UploadAgenda(AgendaDto agenda)
        {
            var request = new RestRequest($"/api/Agenda");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(agenda);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<LawyerDto> GetLawyerByEmail(string email)
        {
            var request = new RestRequest($"/api/Custom/GetlawyerByEmail/{email}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<LawyerDto>(response.Content);
        }
        public async Task<CourtCaseAgendaDto> GetCourtCaseByNo(string courtCaseNo)
        {
            var request = new RestRequest($"/api/CourtCaseAgendas/GetCourtCaseByNo/{courtCaseNo}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<CourtCaseAgendaDto>(response.Content);
        }
        public async Task<dynamic> GetCourtCaseByEmailAndDate(string email, string date)
        {
            var request = new RestRequest($"/api/Custom/CourtCaseByDateAndEmail/{date}/{email}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> GetAllJurisdictions()
        {
            var request = new RestRequest($"/api/Custom/AllJurisdictions");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        
        public async Task<dynamic> GetAgendasByEmail(string email)
        {
            var request = new RestRequest($"/api/Custom/GetAgendasByEmail/{email}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> Login(UserDto userdto)
        {
            var request = new RestRequest($"/api/Auth/Login");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(userdto);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> GetByCourtCase(IFormCollection collection)
        {
            var request = new RestRequest($"/api/Lawyers/GetByCourtCase");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddHeader("Accept", "application/json");
            SearchDto searchdto = new SearchDto
            {
                CourtCaseNo = collection["courtcaseno"].ToString(),
                HearingGeneral = collection["jurisdiction"].ToString(),
                ChamberID = collection["chamberid"].ToString(),
                HearingDate = collection["hearingdate"].ToString(),
                HearingTime = collection["hearingtime"].ToString()
            };
            request.AddJsonBody(searchdto);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> IsExist(UserDto userdto)
        {
            var request = new RestRequest($"/api/Auth/IsExist");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(userdto);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> Register(UserDto userdto)
        {
            var request = new RestRequest($"/api/Auth/Register");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(userdto);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public async Task<dynamic> GetCourtCaseByNameAndDate(string lawyername, string date)
        {
            var request = new RestRequest($"/api/Custom/CourtCaseByDateAndName/{date}/{lawyername}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        
        public async Task<dynamic> GetAvailableLawyersByCourtCaseNo(string courtcaseno)
        {
            var request = new RestRequest($"/api/Presentation/GetByCourtCaseNo/{courtcaseno}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> SetAvailableCourtCaseNo(string courtcaseno, int lawyerid)
        {
            PresentationDto presentationdto = new PresentationDto
            {
                ID = 0,
                CourtCaseNo = courtcaseno,
                LawyerId = lawyerid,
                Available = 1
            };
            var request = new RestRequest($"/api/Presentation");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddJsonBody(presentationdto);
            var response = await _client.PostAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> GetlawyerByEmail(string lawyeremail)
        {
            var request = new RestRequest($"/api/Lawyers/ByEmail/{lawyeremail}");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public async Task<dynamic> GetAllCourtTypes()
        {
            var request = new RestRequest($"/api/Custom/AllCourtTypes");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }

        public async Task<dynamic> GetAllCourtLocations()
        {
            var request = new RestRequest($"/api/Custom/AllCourtLocations");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<dynamic> GetAllChamberIDs()
        {
            var request = new RestRequest($"/api/Custom/AllChamberIDs");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<dynamic>(response.Content);
        }
        public async Task<IEnumerable<JuridictionTypeDto>> GetAllJurisdictionTypes()
        {
            var request = new RestRequest($"/api/Courts/JuridictionTypes");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            var response = await _client.GetAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<JuridictionTypeDto>>(response.Content);
        }
        public async Task<List<CourtDto>> GetAllTypes()
        {
            RestClient __client = new RestClient("https://dossier.just.fgov.be");
            var request = new RestRequest("cgi-main/ajax-request-json.pl");
            request.AddParameter("requete", "json_list");
            request.AddParameter("lg", "nl");
            request.AddParameter("liste", "juridiction");
            request.AddParameter("backend", "N");
            var response = await __client.GetAsync(request);
            var results = JsonConvert.DeserializeObject<CourtResultDto>(response.Content);
            if (results != null && results.Data.Count > 0)
            {
                return results.Data;
            }
            return null;
        }

        public async Task<ResponseDto> GetEmployeeList(
                string _draw,
                string _sortColumn,
                string _sortColumnDirection,
                string _searchValue,
                int _pageSize,
                int _skip,
                string date,
                string lawyername
            )
        {
            var draw =_draw;
            var sortColumn = _sortColumn;
            var sortColumnDirection = _sortColumnDirection;
            var searchValue = _searchValue;
            int pageSize = _pageSize;
            int skip = _skip;

            var request = new RestRequest($"/api/Custom/Test");
            request.AddHeader("ocp-apim-subscription-key", _ocp_apim_subscription_key);
            request.AddParameter("draw", draw);
            request.AddParameter("date", date);
            request.AddParameter("lawyername", lawyername);
            request.AddParameter("sortColumn", sortColumn);
            request.AddParameter("sortColumnDirection", sortColumnDirection);
            request.AddParameter("searchValue", searchValue);
            request.AddParameter("pageSize", pageSize);
            request.AddParameter("skip", skip);

            var response = await _client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<ResponseDto>(response.Content);
            return await Task.FromResult( new ResponseDto
            {
                draw = data.draw,
                recordsTotal = data.recordsTotal,
                recordsFiltered = data.recordsFiltered,
                data = data.data
            });
        }

    }
}
