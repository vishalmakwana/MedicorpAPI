﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicorp.Core
{
    public static class SqlObjectName
    {
        #region Organization Master Object Name

        public const string OrganizationMasterSelect = "Proc_OrganizationMaster_Select";
        public const string OrganizationMasterInsert = "Proc_OrganizationMaster_Insert";
        public const string OrganizationMasterUpdate = "Proc_OrganizationMaster_Update";
        public const string OrganizationMasterDelete = "Proc_OrganizationMaster_Delete";
        public const string OrganizationMasterExistOrganizationName = "Proc_OrganizationMaster_Exit_OrganizationName";
        public const string OrganizationMasterValidateOrganizationName = "Proc_ OrganizationMaster_Validate_OrganizationName";
        #endregion

        #region Product Master Object Name

        public const string ProductMasterSelect = "Proc_ProductMaster_Select";
        public const string ProductMasterInsert = "Proc_ProductMaster_Insert";
        public const string ProductMasterUpdate = "Proc_ProductMaster_Update";
        public const string ProductMasterDelete = "Proc_ProductMaster_Delete";
        public const string ProductMasterExistProductName = "Proc_ProductMaster_Exit_ProductName";
        public const string ProductMasterValidateProductName = "Proc_ProductMaster_Validate_ProductName";
        #endregion

        #region User Master Object Name

        public const string UserMasterSelect = "Proc_AspNetUsersMaster_Select";
        public const string UserMasterInsert = "Proc_AspNetUsersMaster_Insert";
        public const string UserMasterUpdate = "Proc_AspNetUsersMaster_Update";
        public const string UserMasterDelete = "Proc_AspNetUsersMaster_Delete";
        public const string UserMasterExistUserName = "Proc_AspNetUsersMaster_Exit_UserName";
        public const string UserMasterValidateUserName = "Proc_AspNetUsersMaster_Validate_UserName";
        #endregion

        #region Roles Object Name
        public const string RolesMasterSelect = "Proc_AspNetRoles_Select";
        #endregion

        #region User Roles Object Name
        public const string UserRolesMasterSelect = "Proc_AspNetUsersRoles_Select";
        #endregion
    }
}
