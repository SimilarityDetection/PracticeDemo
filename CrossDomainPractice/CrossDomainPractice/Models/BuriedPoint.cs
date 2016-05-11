using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrossDomainPractice.Models
{
    public class BuriedPoint
    {
        /// <summary>
        /// c/s/d/old_c/old_s/old_d
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// entry
        /// detect/report
        /// detect_clear
        /// detect_exit/report_exit
        /// </summary>
        public string Point { get; set; }
    }
}