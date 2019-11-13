using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;

namespace eRoom.Shared.CoreLib.DAL
{
    public interface IBaseDAL
    {
    }

    public class BaseDAL : IBaseDAL
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<BaseDAL> logger;
        private readonly string ConnectionString;
        private string ModuleName = "eRoom.DAL";// System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        protected BaseDAL(IConfiguration _configuration, ILogger<BaseDAL> _logger)
        {
            configuration = _configuration;
            logger = _logger;
            ConnectionString = configuration.GetConnectionString("db_context");
        }

        /// <summary>
        /// Opens and returns the sql connection to db using the connection string in app settings file
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Opens and returns the sql connection asynchronously to db using the connection string in app settings file
        /// </summary>
        /// <returns></returns>
        protected async Task<SqlConnection> OpenConnectionAsync()
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T QueryWithResult<T>(string sql, CommandType commandType, object parameters = null)
        {
            try
            {
                using (var connection = OpenConnection())
                {
                    return connection.QueryFirst<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryWithResultList<T>(string sql, CommandType commandType, object parameters = null)
        {
            try
            {
                using (var connection = OpenConnection())
                {
                    return connection.Query<T>(sql: sql, param: parameters, commandType: commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<DynamicParameters> ExecuteSPWithParams(string sql, DynamicParameters param)
        {
            try
            {
                using (var connection = OpenConnection())
                {
                    connection.Execute(sql: sql, param: param, commandType: CommandType.StoredProcedure);
                    return param;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes sp with params passed and returns single record result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecSPWithReturnAsync<T>(string sp, object param)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    var result = await connection.QueryFirstOrDefaultAsync<T>(sql: sp, param: dParam, commandType: CommandType.StoredProcedure);
                    return dParam.Get<T>("RETURN_VALUE");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        /// <summary>
        /// Executes sp with params passed and returns single record result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecSPWithResultAsync<T>(string sp, object param)
        {
            try
            {
                using (var connection = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    return await connection.QueryFirstOrDefaultAsync<T>(sql: sp, param: dParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes sp with params passed and returns only meta result.
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<TMetaResult> ExecSPForMetaResultAsync<TMetaResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                using (var conn = await OpenConnectionAsync())
                {
                    DynamicParameters dParam = new DynamicParameters();
                    if (param is DynamicParameters)
                    {
                        dParam = (DynamicParameters)param;
                    }
                    else
                    {
                        dParam = new DynamicParameters(param);
                    }

                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: dParam, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.ReadFirstOrDefault<TMetaResult>();
                        //second = resultSet.IsConsumed ? default(TResult) : resultSet.ReadFirstOrDefault<TResult>();
                    }
                }
                return first;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TResult, T2, T3, IEnumerable<T4>, TMetaResult)> ExecSPForComplexItemResultAsync<TResult, T2, T3, T4, TMetaResult>(string sp, object param)
        {
            try
            {                
                TResult first;
                T2 second;
                T3 third;
                IEnumerable<T4> fourth;
                TMetaResult fifth;
                using (var conn = await OpenConnectionAsync())
                {
                    DynamicParameters dParam = new DynamicParameters();
                    if (param is DynamicParameters)
                    {
                        dParam = (DynamicParameters)param;
                    }
                    else
                    {
                        dParam = new DynamicParameters(param);
                    }

                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: dParam, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.ReadFirstOrDefault<TResult>();
                        second = resultSet.IsConsumed ? default(T2) : resultSet.ReadFirstOrDefault<T2>();
                        third = resultSet.IsConsumed ? default(T3) : resultSet.ReadFirstOrDefault<T3>();
                        fourth = resultSet.IsConsumed ? default(IEnumerable<T4>) : resultSet.Read<T4>();                        
                        fifth = resultSet.IsConsumed ? default(TMetaResult) : resultSet.ReadFirstOrDefault<TMetaResult>();
                    }
                }
                return (first, second, third, fourth, fifth);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Executes sp with params passed and returns single record with meta result.
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, TResult)> ExecSPForItemResultAsync<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                TResult second;
                using (var conn = await OpenConnectionAsync())
                {
                    DynamicParameters dParam = new DynamicParameters();
                    if (param is DynamicParameters)
                    {
                        dParam = (DynamicParameters)param;
                    }
                    else
                    {
                        dParam = new DynamicParameters(param);
                    }

                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: dParam, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.ReadFirstOrDefault<TMetaResult>();
                        second = resultSet.IsConsumed ? default(TResult) : resultSet.ReadFirstOrDefault<TResult>();
                    }
                }
                return (first, second);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes sp with params passed and returns single record with meta result.
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, object)> ExecSPOutPutForItemResultAsync<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                object second;
                using (var conn = await OpenConnectionAsync())
                {
                    DynamicParameters dParam = new DynamicParameters();
                    if (param is DynamicParameters)
                    {
                        dParam = (DynamicParameters)param;
                    }
                    else
                    {
                        dParam = new DynamicParameters(param);
                    }

                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    dParam.Add("CardAvailableBalance", 0, DbType.Double, ParameterDirection.Output);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: dParam, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.ReadFirstOrDefault<TMetaResult>();
                        second = dParam.Get<double>("CardAvailableBalance");
                    }
                }
                return (first, second);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes sp with params passed and returns list of records with meta result.
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, IEnumerable<TResult>)> ExecSPForListResultAsync<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                IEnumerable<TResult> second;
                using (var conn = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: param, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.Read<TMetaResult>().FirstOrDefault();
                        second = resultSet.IsConsumed ? default(IEnumerable<TResult>) : resultSet.Read<TResult>();
                    }
                }
                return (first, second);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, PagingResult<TResult>)> ExecSPReturnListWithAsync<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                PagingResult<TResult> pagingResult = new PagingResult<TResult>();
                using (var conn = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: param, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.Read<TMetaResult>().FirstOrDefault();
                        pagingResult.RecordList = resultSet.IsConsumed ? default(List<TResult>) : resultSet.Read<TResult>().ToList();
                        pagingResult.RecordCount = resultSet.IsConsumed ? 0 : resultSet.Read<int>().FirstOrDefault();
                    }
                }
                return (first, pagingResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<SqlMapper.GridReader> ExecSPForGridReaderResult(string sp, object param)
        {
            try
            {
                using (var conn = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    return await conn.QueryMultipleAsync(sql: sp, param: param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // <summary>
        // Executes sp with params passed and returns list of records with meta result.
        // </summary>
        // <typeparam name = "TMetaResult" ></ typeparam >
        // < typeparam name="TResult"></typeparam>
        // <param name = "sp" ></ param >
        // < param name="param"></param>
        // <returns></returns>
        public async Task<(TMetaResult, List<TResult>)> ExecSPForListResultAsync2<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                List<TResult> second;
                using (var conn = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: param, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.Read<TMetaResult>().FirstOrDefault();
                        second = resultSet.IsConsumed ? default(List<TResult>) : resultSet.Read<TResult>().ToList();
                    }
                }
                return (first, second);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, PagingResult<TResult>)> ExecSPForPageListResultAsync<TMetaResult, TResult>(string sp, object param)
        {
            try
            {
                TMetaResult first;
                PagingResult<TResult> pagingResult = new PagingResult<TResult>();
                using (var conn = await OpenConnectionAsync())
                {
                    var dParam = new DynamicParameters(param);
                    dParam.Add("RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);
                    using (var resultSet = await conn.QueryMultipleAsync(sql: sp, param: param, commandType: CommandType.StoredProcedure))
                    {
                        first = resultSet.Read<TMetaResult>().FirstOrDefault();
                        pagingResult.RecordList = resultSet.IsConsumed ? default(IEnumerable<TResult>) : resultSet.Read<TResult>();
                        pagingResult.RecordCount = resultSet.IsConsumed ? 0 : resultSet.Read<int>().FirstOrDefault();
                    }
                }
                return (first, pagingResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes query with params passed and returns list of records with meta result.
        /// </summary>
        /// <typeparam name="TMetaResult"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sp"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<(TMetaResult, List<TResult>)> ExecQueryForListResultAsync<TMetaResult, TResult>(string query, CommandType commandType)
        {
            try
            {
                TMetaResult first;
                List<TResult> second;
                using (var conn = await OpenConnectionAsync())
                {
                    using (var resultSet = await conn.QueryMultipleAsync(sql: query, commandType: commandType))
                    {
                        first = resultSet.Read<TMetaResult>().FirstOrDefault();
                        second = resultSet.IsConsumed ? default(List<TResult>) : resultSet.Read<TResult>().ToList();
                    }
                }
                return (first, second);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

    public class PagingResult<T>
    {
        public int RecordCount { get; set; }
        public IEnumerable<T> RecordList { get; set; }
    }
}
