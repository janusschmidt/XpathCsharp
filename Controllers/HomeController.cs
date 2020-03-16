using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using xpathCsharp.Models;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.XPath;

namespace xpathCsharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(HomeViewModel vm)
        {
            try{
                var x = new XPathDocument(new StringReader(vm.xml));
                var nav = x.CreateNavigator();
                nav.MoveToFollowing(XPathNodeType.Element);

                var nsmgr = new XmlNamespaceManager(nav.NameTable);

                var namespaces = nav.GetNamespacesInScope(XmlNamespaceScope.All);
                foreach(var ns in namespaces)
                    nsmgr.AddNamespace(ns.Key, ns.Value);
         
                vm.result = nav.Select(vm.xpath, nsmgr).Cast<XPathNavigator>();

                vm.debug = "";
                foreach(var n in namespaces)
                    vm.debug += $"Key:{n.Key}, value:{n.Value}\r\n";
            }

            catch(Exception ex)
            {
                vm.debug = ex.Message;
            }

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
