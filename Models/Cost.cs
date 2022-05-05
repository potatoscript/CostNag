using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CostNag.Models
{
    public class Cost
    {

        public int CostId { get; set; }

        public string issue_date { get; set; }

        public string doc_no { get; set; }

        public string wr_no { get; set; }

        public string sales { get; set; }

       
        public string approved_by { get; set; }

        public string expired_by { get; set; }

        public string parts_code { get; set; }

        public string product { get; set; }

        public List<Cost> data = new List<Cost>();

        public double target_price_sgd { get; set; }
        public double tooling_list_total_amount_sgd { get; set; }

        ///<summary>
        /// Gets or sets Costs.
        ///</summary>
        public List<Cost> CostData { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }


    }
}





