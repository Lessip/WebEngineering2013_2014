//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSharing
{
    using System;
    using System.Collections.Generic;
    
    public partial class contract
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int car_id { get; set; }
        public System.DateTime pick_up_date { get; set; }
        public System.DateTime return_date { get; set; }
        public string start_location { get; set; }
        public string end_location { get; set; }
        public int distance { get; set; }
        public decimal price { get; set; }
    
        public virtual car car { get; set; }
        public virtual user user { get; set; }
    }
}
