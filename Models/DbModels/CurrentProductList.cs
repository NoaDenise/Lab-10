using System;
using System.Collections.Generic;

namespace Lab_10.Models.DbModels
{
    public partial class CurrentProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}
