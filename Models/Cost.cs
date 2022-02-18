﻿using System;
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



    }
}





