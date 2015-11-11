using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace shipmentsApi.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }

        public static List<Shipment> Create()
        {
            List<Shipment> List = new List<Shipment>();
            Shipment s1 = new Shipment() { Destination = "Medellin", Id = 123, Origin = "ny" };
            List.Add(s1);

            return List;
        }
    }
}