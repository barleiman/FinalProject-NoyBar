using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TryAgain.Models
{
    public abstract class ViewModelBase
    {

        public string geoIP { get; set; }
        public static User logedonUser { get; set; }


    }
}
