using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace xpathCsharp.Models
{
    public class HomeViewModel
    {
        string _xml = @"
            <xml>
            <a>hej</a>
            <a>med</a>
            </xml>";
        
        public string xml {get => _xml; set{_xml=value;}}

        public string xpath {get;set;}

        public IEnumerable<XPathNavigator> result {get;set;}

        public string debug {get;set;}
    }
}
