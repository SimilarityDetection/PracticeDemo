using CrossDomainPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrossDomainPractice.Controllers
{
    public class BuriedPointController : ApiController
    {
        public object Get(string site, string point)
        {
            // 日志操作


            return site + ',' + point;
        }
    }
}
