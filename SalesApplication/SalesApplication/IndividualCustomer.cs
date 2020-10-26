using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApplication
{
    public class IndividualCustomer : Customer
    {
        private int _numberOfPurchases = 0;
        public IndividualCustomer(string name, string address, string region)
       : base(name, address, region) { }
        public override void MakePurchase(decimal valueOfPurchase)
        {
            base.MakePurchase(valueOfPurchase);
            _numberOfPurchases++;
        }
        public override string Details
        {
            get
            {
                return string.Format("[Individual Customer] Total spend: {0:c2}, Number of purchases: {1}",
       TotalSpend, _numberOfPurchases);
            }
        }
    }

}
