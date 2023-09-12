using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject.Utility
{
    public static class SD
    {
        //Stored Procedure
        public const string Proc_CoverType_Create = "SP_CreateCoverType";

        public const string Proc_CoverType_Update = "SP_UpdateCoverType";

        public const string Proc_CoverType_Delete = "SP_DeleteCoverType";

        public const string Proc_GetCoverTypes = "SP_GetCoverTypes";

        public const string Proc_GetCoverType = "SP_GetCoverType";
    }
}
