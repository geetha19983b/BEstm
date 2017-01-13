using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestEstimateModels;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BestEstimateDataAccess
{
    public class BEDataAccess
    {
        public HIL_BETrackerEntities be_dbcontextobj;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HILBETrackerDBConnection"].ConnectionString);
        public BEDataAccess()
        {
            be_dbcontextobj = new HIL_BETrackerEntities();
        }
        #region todelete
        public DataTable GetRevenueData_Optimize(string strLogin)
        {
            try
            {
                DataTable dtRevenue = new DataTable();

                SqlCommand cmd = new SqlCommand("sp_GetRevenue_Optimize", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mail_ID", strLogin);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtRevenue);
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                cmd.Dispose();
                return dtRevenue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //    con.Dispose();
            }
        }
        #endregion

        public CurrNxtQtrDetails GetQtrDetails()
        {
            try
            {
                var qtrdetails = be_dbcontextobj.fn_GetCurrNextQtr().Select(x =>
                new CurrNxtQtrDetails
                {
                    PrevQtr = x.PrevQtr,
                    CurrQtr = x.CurrQtr,
                    NextQtr = x.NextQtr,
                    FutQtr = x.FutQtr,
                    PrevM1 = x.PrevM1,
                    PrevM2 = x.PrevM2,
                    PrevM3 = x.PrevM3,
                    CurrM1 = x.CurrM1,
                    CurrM2 = x.CurrM2,
                    CurrM3 = x.CurrM3,
                    NxtM1 = x.NxtM1,
                    NxtM2 = x.NxtM2,
                    NxtM3 = x.NxtM3,
                    FutM1 = x.FutM1,
                    FutM2 = x.FutM2,
                    FutM3 = x.FutM3


                }).FirstOrDefault();
                return qtrdetails;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateRevenue(IEnumerable<RevenueData> lstrevData)
        {
            int val = 0;
            UpdateRevenue updtobj = new UpdateRevenue();
            try
            {
                foreach (RevenueData revdata in lstrevData)
                {
                    updtobj.acctName = revdata.MasterCustomerCode;
                    updtobj.pU = revdata.PU;
                    updtobj.curr_Code = revdata.NC;
                    updtobj.cQM1 = revdata.CQM1NATIVE;
                    updtobj.cQM2 = revdata.CQM2NATIVE;
                    updtobj.cQM3 = revdata.CQM3NATIVE;
                    updtobj.nQM1 = revdata.NQM1NATIVE;
                    updtobj.nQM2 = revdata.NQM2NATIVE;
                    updtobj.nQM3 = revdata.NQM3NATIVE;
                    updtobj.cQ_Remarks = revdata.CQREMARKS;
                    updtobj.nQ_Remarks = revdata.NQREMARKS;
                    updtobj.modified_By = " ";
                    updtobj.actuals_Count = 0;
                    
                    var rc = be_dbcontextobj.sp_HIL_UpdateRevenue(updtobj.acctName, updtobj.pU, updtobj.curr_Code, updtobj.cQM1, updtobj.cQM2, updtobj.cQM3, updtobj.nQM1, updtobj.nQM2, updtobj.nQM3, updtobj.cQ_Remarks, updtobj.nQ_Remarks, updtobj.modified_By, updtobj.actuals_Count);

                    if (rc < 0)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = 0;
                    }
                }
                return val;
                
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<RevenueData> GetRevenueData(string strlogin)
        {
            try
            {

                // GetRevenueData_Optimize(strlogin);
                // var revdata = new List<RevenueData>();

                return be_dbcontextobj.Database.SqlQuery<RevenueData>("sp_GetRevenue_Optimize @Mail_ID  = {0}", strlogin);
                //var revdata = be_dbcontextobj.sp_GetRevenue_Optimize(strlogin).Select(x => new RevenueData
                //{
                //    MasterCustomerCode = x.MasterCustomerCode,
                //    RowDup = x.RowDup,
                //    BVFR = x.BVFR,

                //    PU = x.PU,
                //    PQAM1 = x.PQAM1,

                //    NC = x.NC,

                //    ER = x.ER,

                //    CQM1ActuaLs = x.CQM1ActuaLs,

                //    CQM2ActuaLs = x.CQM2ActuaLs,

                //    CQM3ActuaLs = x.CQM3ActuaLs,

                //    CQM1NATIVE = x.CQM1NATIVE,

                //    CQM1USD = x.CQM1USD,

                //    CQM2NATIVE = x.CQM2NATIVE,

                //    CQM2USD = x.CQM2USD,

                //    CQM3NATIVE = x.CQM3NATIVE,
                //    CQM3USD = x.CQM3USD,
                //    CQREMARKS = x.CQREMARKS,

                //    CQREVNATIVE = x.CQREVNATIVE,
                //    CQREVUSD = x.CQREVUSD,
                //    CQRTBRNATIVE = x.CQRTBRNATIVE,
                //    CQMCOBE = x.CQMCOBE,

                //    CQDHBE = x.CQDHBE,
                //    NQM1NATIVE = x.NQM1NATIVE,

                //    NQM1USD = x.NQM1USD,
                //    NQM2NATIVE = x.NQM2NATIVE,
                //    NQM2USD = x.NQM2USD,
                //    NQM3NATIVE = x.NQM3NATIVE,
                //    NQM3USD = x.NQM3USD,
                //    NQREMARKS = x.NQREMARKS,
                //    NQREVNATIVE = x.NQREVNATIVE,
                //    NQREVUSD = x.NQREVUSD,
                //    NQRTBRNATIVE = x.NQRTBRNATIVE,
                //    NQRTBRUSD = x.NQRTBRUSD,
                //    NQMCOBE = x.NQMCOBE,
                //    NQDHBE = x.NQDHBE,
                //    CQRTBRUSDACTUALS = x.CQRTBRUSDACTUALS


                //}).AsParallel();
              //  return revdata;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
