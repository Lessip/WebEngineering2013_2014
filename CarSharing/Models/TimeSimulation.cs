using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarSharing.Models
{
    public class TimeSimulation
    {
        public List<CarSharing.Models.UserProfile> userTable;
        public List<CarSharing.Models.CarProfile> carTable;
        public List<CarSharing.contract> contractTable;

        public TimeSimulation()
        {
            userTable = null;
            carTable = null;
            contractTable = null;
        }

        public TimeSimulation(List<CarSharing.Models.UserProfile> userList, List<CarSharing.Models.CarProfile> carList, List<contract> contractList)
        {
            userTable = userList;
            carTable = carList;
            contractTable = contractList;
        }
    }
}