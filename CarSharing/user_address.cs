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
    
    public partial class user_address
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string street { get; set; }
        public string post_code { get; set; }
        public string city { get; set; }
    
        public virtual user user { get; set; }
    }
}
