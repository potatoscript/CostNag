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

           // var action = "api/cost/get-all-costs";
            var action = "api/cost/get-cost-by-page/"+1;
            if (search_code!=null)
                action = "api/cost/get-cost-by-search/" + search_code;

            HttpResponseMessage resdata = await clientdata.GetAsync(action);

            resdata.EnsureSuccessStatusCode();

            ViewBag.PageCount = 1;
            ViewBag.CurrentPageIndex = 1;

            ViewBag.first = 1;
            ViewBag.last = 10;


            if (resdata.IsSuccessStatusCode)
            {
                // use try catch to prevent zero data error of out of Index range
                try
                {
                    var resultdata = resdata.Content.ReadAsStringAsync().Result;
                    cost_ = JsonConvert.DeserializeObject<List<Cost>>(resultdata);

                    ViewBag.PageCount = cost_[0].PageCount;
                    ViewBag.CurrentPageIndex = cost_[0].CurrentPageIndex;

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
                            target_price_sgd = o.target_price_sgd,
                            tooling_list_total_amount_sgd = o.tooling_list_total_amount_sgd,
                            CostId = o.CostId

                        });
                    }
                }
                catch(Exception msg) { }
            }
            List<Cost> model = list.data.ToList();
            ViewData["data"] = model;

            return View();
        }


        public async Task<IActionResult> IndexPage(int CurrentPage, int first, int last)
        {

            Cost list = new Cost();
            List<Cost> cost_ = new List<Cost>();

            HttpClient clientdata = _api.Initial();

            var action = "api/cost/get-cost-by-page/" + CurrentPage;

            HttpResponseMessage resdata = await clientdata.GetAsync(action);

            resdata.EnsureSuccessStatusCode();

            ViewBag.PageCount = 1;
            ViewBag.CurrentPageIndex = 1;

            ViewBag.first = first;
            ViewBag.last = last;


            if (resdata.IsSuccessStatusCode)
            {
                var resultdata = resdata.Content.ReadAsStringAsync().Result;
                cost_ = JsonConvert.DeserializeObject<List<Cost>>(resultdata);

                ViewBag.PageCount = cost_[0].PageCount;
                ViewBag.CurrentPageIndex = cost_[0].CurrentPageIndex;

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
                        target_price_sgd = o.target_price_sgd,
                        tooling_list_total_amount_sgd = o.tooling_list_total_amount_sgd,
                        CostId = o.CostId

                    });
                }

            }
            List<Cost> model = list.data.ToList();
            ViewData["data"] = model;

            return View("Index");
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
