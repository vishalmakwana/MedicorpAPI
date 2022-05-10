﻿using Dapper;
using Medicorp.Core;
using Medicorp.Core.CustomException;
using Medicorp.Core.Entity.Master;
using Medicorp.Data.InterFace;
using Medicorp.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Medicorp.Services
{
    public class ProductMasterService : IProductMasterService
    {
        private readonly IDapperHelper _dapperHelper;
        public ProductMasterService(IDapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        async Task<ApiResponse<int>> IProductMasterService.CreateAsync(ProductMaster productMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (string.IsNullOrEmpty(productMaster.ProductName))
                        throw new OperationExecutionException("Product Name is not valid");
                    bool validationResponse = await ValidateNameAsync(productMaster.ProductName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Product Name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@ProductName", productMaster.ProductName, DbType.String);
                    dbPara.Add("@LongDescription", productMaster.LongDescription, DbType.String);
                    dbPara.Add("@ShortDescription", productMaster.ShortDescription, DbType.String);
                    dbPara.Add("@MRP", productMaster.MRP, DbType.String);
                    dbPara.Add("@OrganizationId", productMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", productMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@InsertBy", productMaster.InsertdBy, DbType.String);
                    dbPara.Add("@InsertDate", productMaster.InsertedDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.ProductMasterInsert,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("ProductMasterService CreateAsync", ex.Message);
                }
            }
            return response;
        }

        async Task<ApiResponse<int>> IProductMasterService.DeleteAsync(int ProductId)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@ProductId", ProductId, DbType.Int32);
                    response.Result = await _dapperHelper.ExecuteAsync(sp: SqlObjectName.ProductMasterDelete,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("ProductMasterService DeleteAsync", ex.Message);
                }

                return response;
            }
        }

        async Task<ApiResponse<List<ProductMaster>>> IProductMasterService.GetProductAsync(ProductMasterFilter filter)
        {
            return await GetProductAsync<ProductMaster>(filter);
        }

        async Task<ApiResponse<List<T>>> GetProductAsync<T>(ProductMasterFilter filter)
        {
            ApiResponse<List<T>> response = new ApiResponse<List<T>>() { Success = true };
            try
            {
                DynamicParameters dbPara = new DynamicParameters();
                dbPara.Add("@ProductName", filter.ProductName, DbType.String);
                dbPara.Add("@LongDescription", filter.LongDescription, DbType.String);
                dbPara.Add("@ShortDescription", filter.ShortDescription, DbType.String);
                dbPara.Add("@MRP", filter.MRP, DbType.String);
                dbPara.Add("@OrganizationId", filter.OrganizationId, DbType.Int32);
                dbPara.Add("@IsActive", filter.IsActive, DbType.Boolean);
                dbPara.Add("@OrganizationName", filter.OrganizationName, DbType.Boolean);
                response.Result = await _dapperHelper.GetAllAsync<T>(sp: SqlObjectName.ProductMasterSelect,
                                              parms: dbPara,
                                              commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                response.ConstructErrorResponse("ProductMasterService GetProductAsync", ex.Message);
            }
            return response;
        }

        async Task<ApiResponse<int>> IProductMasterService.UpdateAsync(ProductMaster productMaster)
        {
            ApiResponse<int> response = new ApiResponse<int>() { Success = true };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (productMaster.ProductId == 0)
                        throw new OperationExecutionException("Product Id is not valid");
                    if (string.IsNullOrEmpty(productMaster.ProductName))
                        throw new OperationExecutionException("Product name is not valid");
                    bool validationResponse = await ValidateAsync(productMaster.ProductId, productMaster.ProductName);
                    if (!validationResponse)
                        throw new OperationExecutionException("Product name is already exists");

                    DynamicParameters dbPara = new DynamicParameters();
                    dbPara.Add("@ProductName", productMaster.ProductName, DbType.String);
                    dbPara.Add("@LongDescription", productMaster.LongDescription, DbType.String);
                    dbPara.Add("@ShortDescription", productMaster.ShortDescription, DbType.String);
                    dbPara.Add("@MRP", productMaster.MRP, DbType.String);
                    dbPara.Add("@OrganizationId", productMaster.OrganizationId, DbType.Int32);
                    dbPara.Add("@IsActive", productMaster.IsActive, DbType.Boolean);
                    dbPara.Add("@UpdateBy", productMaster.UpdatedBy, DbType.String);
                    dbPara.Add("@UpdatedDate", productMaster.UpdateDate, DbType.DateTime);
                    response.Result = await _dapperHelper.GetAsync<int>(sp: SqlObjectName.ProductMasterUpdate,
                                                  parms: dbPara,
                                                  commandType: CommandType.StoredProcedure);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    response.ConstructErrorResponse("ProductMasterService UpdateAsync", ex.Message);
                }

            }
            return response;
        }

        async Task<bool> ValidateAsync(int ProductId, string ProductName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@ProductId", ProductId, DbType.Int32);
            dbPara.Add("@ProductName", ProductName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.ProductMasterValidateProductName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }

        async Task<bool> ValidateNameAsync(string ProductName)
        {
            DynamicParameters dbPara = new DynamicParameters();
            dbPara.Add("@ProductName", ProductName, DbType.String);
            bool response = await _dapperHelper.GetAsync<bool>(sp: SqlObjectName.ProductMasterExistProductName,
                                            parms: dbPara,
                                            commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
