//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ibags.API
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderId { get; set; }
        public string AccountNo { get; set; }
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
