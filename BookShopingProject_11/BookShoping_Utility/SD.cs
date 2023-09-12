using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoping_Utility
{
   public static class SD
    {
        public const string Proc_CoverType_Create = "SP_CreateCoverType";

        public const string Proc_CoverType_Update = "SP_UpdateCoverType";

        public const string Proc_CoverType_Delete = "SP_DeleteCoverType";

        public const string Proc_CoverType_GetCoverTypes = "SP_GetCoverTypes";

        public const string Proc_CoverType_GetCoverType = "SP_GetCoverType";


        //Role

        public const string Role_Admin = "Admin";
        public const string Role_Employee = "EmployeeUser";
        public const string Role_Company = "CompanyUser";
        public const string Role_Individual = "IndividualUser";

        //session
        public const string Ss_Session = "Cart Count Session";

        //Price

        public static double GetPriceBasedOnQuantity(double quantity, double Price, double Price50, double Price100)
        {
            if (quantity < 50)
                return Price;
            else if (quantity < 100)
                return Price50;
            else
                return Price100;
        }

        //Order Status

        public const string OrderStatusPending = "Pending";
        public const string OrderStatusApproved = "Approved";
        public const string OrderStatusInProgress = "Processing";
        public const string OrderStatusShipped = "Shipped";
        public const string OrderStatusCancelled = "Cancelled";
        public const string OrderStatusRefunded = "Refunded";

        //Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "PaymentStatusDelay";
        public const string PaymentStatusRejected = "Rejected";



        ////This is instead of @html.Row in C#
        
        public static string ConvertToRowHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            for(int i=0; i<source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;

                }
                if(let=='>')
                {
                    inside = false;
                    continue;
                }
                if(!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

    }
}



