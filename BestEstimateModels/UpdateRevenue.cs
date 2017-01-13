using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestEstimateModels
{
    public class UpdateRevenue
    {
        public string acctName { get; set; }
        public string pU { get; set; }
        public string curr_Code { get; set; }
        public Nullable<double> cQM1 { get; set; }
        public Nullable<double> cQM2 { get; set; }
        public Nullable<double> cQM3 { get; set; }
        public Nullable<double> nQM1 { get; set; }
        public Nullable<double> nQM2 { get; set; }
        public Nullable<double> nQM3 { get; set; }
        public string cQ_Remarks { get; set; }
        public string nQ_Remarks { get; set; }
        public string modified_By { get; set; }
        public Nullable<int> actuals_Count { get; set; }
    }
}
