using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodFest
{
    public class Restaurant
    {
        public int RestaurantId { set; get; }
        public string RestaurantName { set; get; }
        public string RestaurantAddress { set; get; }
        public string RestaurantPhone { set; get; }
        public string RestaurantMinOrder { set; get; }
        public DateTime OpenHour { set; get; }
        public DateTime CLoseHour { set; get; }       
        public int DeliveryFee { set; get; }

    }
}