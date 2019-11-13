using eRoom.Shared.CoreLib.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eRoom.Shared.Api.Infrastructure.Utils
{
    public class WebApiWrapper
    {
        public delegate T GenericWebApi<T>(params object[] inputs);
        public delegate (DefaultMetaResult, Object) MethodWithMetaResult(params object[] inputs);
        public delegate Task<(DefaultMetaResult, T)> AsyncMethodWithMetaResult<T>(params object[] inputs);

        public static TResult Call<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return func(inputs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<TResult> CallAsync<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return await Task.Factory.StartNew<TResult>(() => { return func(inputs); });
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Returns an object result after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static ObjectResult CallWithObjResult<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(func(inputs));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithObjResultAsync<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return await Task.Factory.StartNew<ObjectResult>(() => new ObjectResult(func(inputs)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithObjResultValidationErrorAsync<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return await Task.Factory.StartNew<ObjectResult>(() => new ObjectResult(func(inputs)) { StatusCode = (int)HttpStatusCode.BadRequest });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithObjResultInternalErrorAsync<TResult>(GenericWebApi<TResult> func, params object[] inputs)
        {
            try
            {
                return await Task.Factory.StartNew<ObjectResult>(() => new ObjectResult(func(inputs)) { StatusCode = (int)HttpStatusCode.InternalServerError });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result after executing the func passed
        /// </summary>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static ObjectResult CallWithApiOkResponse(MethodWithMetaResult func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<Object>(func(inputs))) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result with 200 status code after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithApiOkResponseAsync<TResult>(AsyncMethodWithMetaResult<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<TResult>(await func(inputs)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result with 201 status code after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithApiCreateResponseAsync<TResult>(AsyncMethodWithMetaResult<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<TResult>(await func(inputs))) { StatusCode = (int)HttpStatusCode.Created };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result with 204 status code after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithApiUpdateResponseAsync<TResult>(AsyncMethodWithMetaResult<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<TResult>(await func(inputs))) { StatusCode = (int)HttpStatusCode.NoContent };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result with 500 status code after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithApiInternalErrorResponseAsync<TResult>(AsyncMethodWithMetaResult<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<TResult>(await func(inputs))) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns an object result with 500 status code after executing the func passed
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static async Task<ObjectResult> CallWithApiValidationlErrorResponseAsync<TResult>(AsyncMethodWithMetaResult<TResult> func, params object[] inputs)
        {
            try
            {
                return new ObjectResult(new ApiOkResponse<TResult>(await func(inputs))) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
