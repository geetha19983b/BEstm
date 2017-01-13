using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestEstimateModels
{
    public class RevenueData
    {
        public string MasterCustomerCode { get; set; }
        public long RowDup { get; set; }
        public long BVFR { get; set; }
        public string PU { get; set; }
        public Nullable<double> PQAM1 { get; set; }
        public string NC { get; set; }
        public Nullable<double> ER { get; set; }
        public Nullable<double> CQM1ActuaLs { get; set; }
        public Nullable<double> CQM2ActuaLs { get; set; }
        public Nullable<double> CQM3ActuaLs { get; set; }
        public double CQM1NATIVE { get; set; }
        public double CQM1USD { get; set; }
        public double CQM2NATIVE { get; set; }
        public double CQM2USD { get; set; }
        public double CQM3NATIVE { get; set; }
        public double CQM3USD { get; set; }
        public string CQREMARKS { get; set; }
        public double CQREVNATIVE { get; set; }
        public double CQREVUSD { get; set; }
        public Nullable<decimal> CQRTBRNATIVE { get; set; }
        public Nullable<double> CQMCOBE { get; set; }
        public Nullable<double> CQDHBE { get; set; }
        public double NQM1NATIVE { get; set; }
        public double NQM1USD { get; set; }
        public double NQM2NATIVE { get; set; }
        public double NQM2USD { get; set; }
        public double NQM3NATIVE { get; set; }
        public double NQM3USD { get; set; }
        public string NQREMARKS { get; set; }
        public double NQREVNATIVE { get; set; }
        public double NQREVUSD { get; set; }
        public Nullable<decimal> NQRTBRNATIVE { get; set; }
        public decimal NQRTBRUSD { get; set; }
        public Nullable<double> NQMCOBE { get; set; }
        public Nullable<double> NQDHBE { get; set; }
        public Nullable<double> CQRTBRUSDACTUALS { get; set; }
    }
  }
