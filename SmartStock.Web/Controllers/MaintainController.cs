using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Service.Maintain;

namespace SmartStock.Web.Controllers
{
    public class MaintainController : Controller
    {
        private IMaintainService maintainService;

        public MaintainController(IMaintainService maintainService)
        {
            this.maintainService = maintainService;
        }

        // GET: /Maintain/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void InitialAllStocksHistory()
        {
            this.maintainService.InitialAllStocksHistory(); 
        }

        public void UpdateAllStocksHistory(string startDate, string endDate)
        {
            // this.maintainService.UpdateAllStocksHistory();
        }

        public void UpdateStockHistory(string strockCode, string startDate, string endStart)
        {
            
        }
     
    }
}
