﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare_API.RequestModel
{
   public class PINUpdateRequestModel
    {
        public int UserId { get; set; }
        public int UserPin { get; set; }
    }
}