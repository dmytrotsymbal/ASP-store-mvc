using System;
using System.Collections.Generic;
using Tables.Models.Entities;

namespace Tables.Models
{
    public class ClientDetailsViewModel
    {
        public Client Client { get; set; }
        public List<Car> Cars { get; set; }
    }
}
