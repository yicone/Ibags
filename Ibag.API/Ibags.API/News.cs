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
    
    public partial class News
    {
        public int NewsId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Order { get; set; }
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
