using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CostNag.Models;
using CostNag.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System;

namespace CostNag.Controllers
{
    public class HomeController : Controller
    {
        CostAPI _api = new CostAPI();

        public async Task<IActionResult> Index(string? search_code)
        {

            Cost list = new Cost();
            List<Cost> cost_ = new List<Cost>();

            HttpClient clientdata = _api.Initial();

            var action = "api/cost/get-all-costs";
            if (search_code!=null)
                action = "api/cost/get-cost-by-search/" + search_code;

            HttpResponseMessage resdata = await clientdata.GetAsync(action);

            resdata.EnsureSuccessStatusCode();

            if (resdata.IsSuccessStatusCode)
            {
                var resultdata = resdata.Content.ReadAsStringAsync().Result;
                cost_ = JsonConvert.DeserializeObject<List<Cost>>(resultdata);


                foreach (var o in cost_)
                {
                    list.data.Add(new Cost
                    {
                        doc_no = o.doc_no,
                        wr_no = o.wr_no,
                        sales = o.sales,
                        parts_code = o.parts_code,
                        product = o.product,
                        issue_date = o.issue_date,
                        expired_by = o.expired_by,
                        approved_by = o.approved_by,
                        CostId = o.CostId

                    });
                }

            }
            List<Cost> model = list.data.ToList();
            ViewData["data"] = model;

            return View();
        }


        public async void Delete(
           Cost model,
           bool confirm,
           int Id
       )
        {
            if (Id == null || Id == 0)  //this is used for the validation as well but in the server side
            {
                //return NotFound();
            }
            else
            {
                if (ModelState.IsValid && confirm == true)
                {
                    HttpClient client = _api.Initial();
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var action = "api/cost/delete-cost-by-id/" + Id;
                    HttpResponseMessage res = await client.PostAsync(action, content).ConfigureAwait(false);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                    }
                }
            }

        }



    }
}
