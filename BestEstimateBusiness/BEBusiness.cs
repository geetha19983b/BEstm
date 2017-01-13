using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestEstimateDataAccess;
using BestEstimateModels;

namespace BestEstimateBusiness
{
    public class BEBusiness
    {
        public BEDataAccess be_dataaccessbj;
        public BEBusiness()
        {
            be_dataaccessbj = new BEDataAccess();
        }
        public CurrNxtQtrDetails GetQtrDetails()
        {
            try
            {
                var qtrdata = be_dataaccessbj.GetQtrDetails();
                return qtrdata;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateRevenue(IEnumerable<RevenueData> lstRevData)
        {
            try
            {
                var rc = be_dataaccessbj.UpdateRevenue(lstRevData);
                return rc;
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
                var revdata = be_dataaccessbj.GetRevenueData(strlogin);
                return revdata;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
