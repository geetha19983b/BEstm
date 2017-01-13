using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestEstimateBusiness;
using BestEstimateModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BestEstimateMVC.Controllers
{
    public class EstimateController : Controller
    {
        public BEBusiness be_busobj;
        public EstimateController()
        {
            be_busobj = new BEBusiness();

        }
        public ActionResult Revenue()
        {
            return View();
        }
        public JsonResult GetQtrDetails()
        {
            var qtrdetails = be_busobj.GetQtrDetails();

            return Json(qtrdetails, JsonRequestBehavior.AllowGet);
        }
        public string UpdateRevenue(IEnumerable<RevenueData> lstRevData)
        {

            var rc = be_busobj.UpdateRevenue(lstRevData);
            if (rc == 1)
            {
                return "Data saved successfully";
            }
            else
            {
                return "Data Save Failed";
            }
        }
        // GET: Estimate
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public async Task<JsonResult> GetRevenueData()
        {
            string strlogin = User.Identity.Name.Replace("ITLINFOSYS\\", String.Empty);
            strlogin = "Archana_Ravikumar";
            //Stopwatch sp = new Stopwatch();
            //sp.Start();
            var revdata = be_busobj.GetRevenueData(strlogin);
            //sp.Stop();
            string strPU;

            strPU = "ADM";
            var admtotal = await DoStuff(revdata, strPU);
            strPU = "BPO";
            var bpototal = await DoStuff(revdata, strPU);
            //string strPU;

            //strPU = "ADM";

            //var admtotal = GetTotals(revdata, strPU);
            //strPU = "BPO";
            //var bpototal = GetTotals(revdata, strPU);



            var resut = new
            {
                revdata = revdata,
                admtotal = admtotal,
                bpototal = bpototal

            };
            return Json(resut, JsonRequestBehavior.AllowGet);

        }
        public async Task<RevenueTotals> DoStuff(IEnumerable<RevenueData> revdata, string strPU)
        {
            //string strPU;

            //strPU = "ADM";
            return await GetTotalsAsync(revdata, strPU);


            //Task<RevenueTotals> task1 = Task.Run(async() => await GetTotalsAsync(revdata, strPU));
            //strPU = "BPO";
            //Task<RevenueTotals> task2 = Task.Run(async () => await GetTotalsAsync(revdata, strPU));

            // var rc =  await Task.WhenAll(task1, task2);






        }
        public async Task<RevenueTotals> GetTotalsAsync(IEnumerable<RevenueData> revdata, string strPU)
        {

            var recrdswithPU = revdata.Where(x => x.PU.Contains(strPU));

            //var total_wthbvfr = recrdswithPU.Where(x => x.BVFR == 1)
            //           .GroupBy(x => 1)
            //           .Select(x => new RevenueTotals
            //           {
            //               PQAM1_totl = x.Sum(k => k.PQAM1),
            //               CQMCOBE_totl = x.Sum(k => k.CQMCOBE),
            //               CQDHBE_totl = x.Sum(k => k.CQDHBE),
            //               NQMCOBE_totl = x.Sum(k => k.NQMCOBE),
            //               NQDHBE_totl = x.Sum(k => k.NQDHBE)

            //           }).AsParallel().FirstOrDefault();


            var total_wthutbvfr = recrdswithPU
                        .GroupBy(x => 1)
                        .Select(x => new RevenueTotals
                        {
                            PQAM1_totl = x.Sum(k => k.BVFR == 1  ? k.PQAM1 : 0),
                            CQMCOBE_totl = x.Sum(k => k.BVFR == 1 ? k.CQMCOBE : 0),
                            CQDHBE_totl = x.Sum(k => k.BVFR == 1 ? k.CQDHBE : 0),
                            NQMCOBE_totl = x.Sum(k => k.BVFR == 1 ? k.NQMCOBE : 0),
                            NQDHBE_totl = x.Sum(k => k.BVFR == 1 ? k.NQDHBE : 0),

                            CQM1Actuals_totl = x.Sum(k => k.CQM1ActuaLs),
                            CQM2ActuaLs_totl = x.Sum(k => k.CQM2ActuaLs),
                            CQM3ActuaLs_totl = x.Sum(k => k.CQM3ActuaLs),
                            CQM1NATIVE_totl = x.Sum(k => k.CQM1NATIVE),
                            CQM1USD_totl = x.Sum(k => k.CQM1USD),
                            CQM2NATIVE_totl = x.Sum(k => k.CQM2NATIVE),
                            CQM2USD_totl = x.Sum(k => k.CQM2USD),
                            CQM3NATIVE_totl = x.Sum(k => k.CQM3NATIVE),
                            CQM3USD_totl = x.Sum(k => k.CQM3USD),
                            CQREVNATIVE_totl = x.Sum(k => k.CQREVNATIVE),
                            CQREVUSD_totl = x.Sum(k => k.CQREVUSD),
                            CQRTBRNATIVE_totl = x.Sum(k => k.CQRTBRNATIVE),
                            CQRTBRUSDACTUALS_totl = x.Sum(k => k.CQRTBRUSDACTUALS),
                            NQM1NATIVE_totl = x.Sum(k => k.NQM1NATIVE),
                            NQM1USD_totl = x.Sum(k => k.NQM1USD),
                            NQM2NATIVE_totl = x.Sum(k => k.NQM2NATIVE),
                            NQM2USD_totl = x.Sum(k => k.NQM2USD),
                            NQM3NATIVE_totl = x.Sum(k => k.NQM3NATIVE),
                            NQM3USD_totl = x.Sum(k => k.NQM3USD),
                            NQREVNATIVE_totl = x.Sum(k => k.NQREVNATIVE),
                            NQREVUSD_totl = x.Sum(k => k.NQREVUSD),
                            NQRTBRNATIVE_totl = x.Sum(k => k.NQRTBRNATIVE),
                            NQRTBRUSD_totl = x.Sum(k => k.NQRTBRUSD)

                        }).AsParallel().FirstOrDefault();
            var finaltotal = new RevenueTotals
            {
                //PQAM1_totl = total_wthbvfr.PQAM1_totl,
                //CQMCOBE_totl = total_wthbvfr.CQMCOBE_totl,
                //CQDHBE_totl = total_wthbvfr.CQDHBE_totl,
                //NQMCOBE_totl = total_wthbvfr.NQMCOBE_totl,
                //NQDHBE_totl = total_wthbvfr.NQDHBE_totl,
                PQAM1_totl = total_wthutbvfr.PQAM1_totl,
                CQMCOBE_totl = total_wthutbvfr.CQMCOBE_totl,
                CQDHBE_totl = total_wthutbvfr.CQDHBE_totl,
                NQMCOBE_totl = total_wthutbvfr.NQMCOBE_totl,
                NQDHBE_totl = total_wthutbvfr.NQDHBE_totl,
                CQM1Actuals_totl = total_wthutbvfr.CQM1Actuals_totl,
                CQM2ActuaLs_totl = total_wthutbvfr.CQM2ActuaLs_totl,
                CQM3ActuaLs_totl = total_wthutbvfr.CQM3ActuaLs_totl,
                CQM1NATIVE_totl = total_wthutbvfr.CQM1NATIVE_totl,
                CQM1USD_totl = total_wthutbvfr.CQM1USD_totl,
                CQM2NATIVE_totl = total_wthutbvfr.CQM2NATIVE_totl,
                CQM2USD_totl = total_wthutbvfr.CQM2USD_totl,
                CQM3NATIVE_totl = total_wthutbvfr.CQM3NATIVE_totl,
                CQM3USD_totl = total_wthutbvfr.CQM3USD_totl,
                CQREVNATIVE_totl = total_wthutbvfr.CQREVNATIVE_totl,
                CQREVUSD_totl = total_wthutbvfr.CQREVUSD_totl,
                CQRTBRNATIVE_totl = total_wthutbvfr.CQRTBRNATIVE_totl,
                CQRTBRUSDACTUALS_totl = total_wthutbvfr.CQRTBRUSDACTUALS_totl,
                NQM1NATIVE_totl = total_wthutbvfr.NQM1NATIVE_totl,
                NQM1USD_totl = total_wthutbvfr.NQM1USD_totl,
                NQM2NATIVE_totl = total_wthutbvfr.NQM2NATIVE_totl,
                NQM2USD_totl = total_wthutbvfr.NQM2USD_totl,
                NQM3NATIVE_totl = total_wthutbvfr.NQM3NATIVE_totl,
                NQM3USD_totl = total_wthutbvfr.NQM3USD_totl,
                NQREVNATIVE_totl = total_wthutbvfr.NQREVNATIVE_totl,
                NQREVUSD_totl = total_wthutbvfr.NQREVUSD_totl,
                NQRTBRNATIVE_totl = total_wthutbvfr.NQRTBRNATIVE_totl,
                NQRTBRUSD_totl = total_wthutbvfr.NQRTBRUSD_totl
            };
            return finaltotal;
        }
        public RevenueTotals GetTotals(IEnumerable<RevenueData> revdata, string strPU)
        {
            var total_wthbvfr = revdata.Where(x => x.PU.Contains(strPU) && x.BVFR == 1)
                       .GroupBy(x => 1)
                       .Select(x => new RevenueTotals
                       {
                           PQAM1_totl = x.Sum(k => k.PQAM1),
                           CQMCOBE_totl = x.Sum(k => k.CQMCOBE),
                           CQDHBE_totl = x.Sum(k => k.CQDHBE),
                           NQMCOBE_totl = x.Sum(k => k.NQMCOBE),
                           NQDHBE_totl = x.Sum(k => k.NQDHBE)

                       }).FirstOrDefault();


            var total_wthutbvfr = revdata.Where(x => x.PU.Contains(strPU))
                        .GroupBy(x => 1)
                        .Select(x => new RevenueTotals
                        {
                            CQM1Actuals_totl = x.Sum(k => k.CQM1ActuaLs),
                            CQM2ActuaLs_totl = x.Sum(k => k.CQM2ActuaLs),
                            CQM3ActuaLs_totl = x.Sum(k => k.CQM3ActuaLs),
                            CQM1NATIVE_totl = x.Sum(k => k.CQM1NATIVE),
                            CQM1USD_totl = x.Sum(k => k.CQM1USD),
                            CQM2NATIVE_totl = x.Sum(k => k.CQM2NATIVE),
                            CQM2USD_totl = x.Sum(k => k.CQM2USD),
                            CQM3NATIVE_totl = x.Sum(k => k.CQM3NATIVE),
                            CQM3USD_totl = x.Sum(k => k.CQM3USD),
                            CQREVNATIVE_totl = x.Sum(k => k.CQREVNATIVE),
                            CQREVUSD_totl = x.Sum(k => k.CQREVUSD),
                            CQRTBRNATIVE_totl = x.Sum(k => k.CQRTBRNATIVE),
                            CQRTBRUSDACTUALS_totl = x.Sum(k => k.CQRTBRUSDACTUALS),
                            NQM1NATIVE_totl = x.Sum(k => k.NQM1NATIVE),
                            NQM1USD_totl = x.Sum(k => k.NQM1USD),
                            NQM2NATIVE_totl = x.Sum(k => k.NQM2NATIVE),
                            NQM2USD_totl = x.Sum(k => k.NQM2USD),
                            NQM3NATIVE_totl = x.Sum(k => k.NQM3NATIVE),
                            NQM3USD_totl = x.Sum(k => k.NQM3USD),
                            NQREVNATIVE_totl = x.Sum(k => k.NQREVNATIVE),
                            NQREVUSD_totl = x.Sum(k => k.NQREVUSD),
                            NQRTBRNATIVE_totl = x.Sum(k => k.NQRTBRNATIVE),
                            NQRTBRUSD_totl = x.Sum(k => k.NQRTBRUSD)

                        }).FirstOrDefault();
            var finaltotal = new RevenueTotals
            {
                PQAM1_totl = total_wthbvfr.PQAM1_totl,
                CQMCOBE_totl = total_wthbvfr.CQMCOBE_totl,
                CQDHBE_totl = total_wthbvfr.CQDHBE_totl,
                NQMCOBE_totl = total_wthbvfr.NQMCOBE_totl,
                NQDHBE_totl = total_wthbvfr.NQDHBE_totl,
                CQM1Actuals_totl = total_wthutbvfr.CQM1Actuals_totl,
                CQM2ActuaLs_totl = total_wthutbvfr.CQM2ActuaLs_totl,
                CQM3ActuaLs_totl = total_wthutbvfr.CQM3ActuaLs_totl,
                CQM1NATIVE_totl = total_wthutbvfr.CQM1NATIVE_totl,
                CQM1USD_totl = total_wthutbvfr.CQM1USD_totl,
                CQM2NATIVE_totl = total_wthutbvfr.CQM2NATIVE_totl,
                CQM2USD_totl = total_wthutbvfr.CQM2USD_totl,
                CQM3NATIVE_totl = total_wthutbvfr.CQM3NATIVE_totl,
                CQM3USD_totl = total_wthutbvfr.CQM3USD_totl,
                CQREVNATIVE_totl = total_wthutbvfr.CQREVNATIVE_totl,
                CQREVUSD_totl = total_wthutbvfr.CQREVUSD_totl,
                CQRTBRNATIVE_totl = total_wthutbvfr.CQRTBRNATIVE_totl,
                CQRTBRUSDACTUALS_totl = total_wthutbvfr.CQRTBRUSDACTUALS_totl,
                NQM1NATIVE_totl = total_wthutbvfr.NQM1NATIVE_totl,
                NQM1USD_totl = total_wthutbvfr.NQM1USD_totl,
                NQM2NATIVE_totl = total_wthutbvfr.NQM2NATIVE_totl,
                NQM2USD_totl = total_wthutbvfr.NQM2USD_totl,
                NQM3NATIVE_totl = total_wthutbvfr.NQM3NATIVE_totl,
                NQM3USD_totl = total_wthutbvfr.NQM3USD_totl,
                NQREVNATIVE_totl = total_wthutbvfr.NQREVNATIVE_totl,
                NQREVUSD_totl = total_wthutbvfr.NQREVUSD_totl,
                NQRTBRNATIVE_totl = total_wthutbvfr.NQRTBRNATIVE_totl,
                NQRTBRUSD_totl = total_wthutbvfr.NQRTBRUSD_totl
            };
            return finaltotal;
        }
        public ActionResult Volume()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }
    }
}