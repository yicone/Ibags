//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ibags.API
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int rowId { get; set; }
        public string AccountId { get; set; }
        public string OrderNo { get; set; }
        public string FullName { get; set; }
        public string Sex { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string FlightNo { get; set; }
        public Nullable<int> Direction { get; set; }
        public string Airport { get; set; }
        public Nullable<System.DateTime> PlanDate { get; set; }
        public string PlanHour { get; set; }
        public string PlanMinute { get; set; }
        public Nullable<int> PackageCount { get; set; }
        public Nullable<int> SpecialPackageCount { get; set; }
        public Nullable<double> Mileage { get; set; }
        public Nullable<decimal> Fee { get; set; }
        public string Remark { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public Nullable<int> PaymentChannel { get; set; }
        public Nullable<System.DateTime> OrderTime { get; set; }
        public Nullable<System.DateTime> CompleteTime { get; set; }
    }
}
