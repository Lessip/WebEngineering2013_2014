//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSharing
{
    using System;
    using System.Collections.Generic;
    
    public partial class car_type
    {
        public car_type()
        {
            this.car = new HashSet<car>();
        }
    
        public int id { get; set; }
        public string type { get; set; }
        public Nullable<int> car_class { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> seat_size { get; set; }
    
        public virtual ICollection<car> car { get; set; }
    }
}
