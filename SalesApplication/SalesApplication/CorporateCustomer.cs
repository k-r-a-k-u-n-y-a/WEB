using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApplication
{
    public class CorporateCustomer : Customer
    {
        private decimal _projectedSpend;
        public CorporateCustomer(string name, string address, string region, decimal projectedSpend) : base(name, address, region)
        { _projectedSpend = projectedSpend; }
        public override string Details
        {
            get
            {
                return string.Format
                 ("[Corporate Customer] Total spend: {0:c2}, Projected spend: {1:c2}",
                    TotalSpend, _projectedSpend);
            }
        }
    }

}
