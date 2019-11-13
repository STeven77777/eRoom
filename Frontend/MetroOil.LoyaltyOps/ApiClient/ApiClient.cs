using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Text;
using MetroOil.LoyaltyOps.Helpers;
using MetroOil.LoyaltyOps.Models;
using System.Net;

namespace MetroOil.LoyaltyOps
{
    public class ApiClient
    {
        private static async Task<HttpResponseMessage> PatchAsync(HttpClient client, string requestUri, HttpContent iContent)
        {
            //HttpClient client = new HttpClient();
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                //Debug.WriteLine("ERROR: " + e.ToString());
            }

            return response;
        }

        private static async Task<HttpResponseMessage> DeleteAsync(HttpClient client, string requestUri, HttpContent iContent)
        {
            //HttpClient client = new HttpClient();
            var method = HttpMethod.Delete;
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                //Debug.WriteLine("ERROR: " + e.ToString());
            }

            return response;
        }

        public static async Task<T> PostJsonAsync<T>(string requestUri, string jsonData, bool isAuth = false)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            var apiUrl = WebConfigurationManager.AppSettings["APIBaseUrl"];
            if (isAuth)
            {
                apiUrl = WebConfigurationManager.AppSettings["APIAuthUrl"];
            }

            try
            {
                var strData = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));

                httpClient.DefaultRequestHeaders.Add("app_token", WebConfigurationManager.AppSettings["APP_TOKEN"]);
                //if (isAuth)
                //{
                //    httpClient.DefaultRequestHeaders.Add("app_token", WebConfigurationManager.AppSettings["APP_TOKEN"]);
                //}

                await httpClient.PostAsync(requestUri, strData)
                        .ContinueWith((resMsg) =>
                        {
                            var rp = resMsg.Result.Content.ReadAsStringAsync();
                            response = rp.Result;
                        });

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch(Exception e)
            {
                // DO NOT LOG auth data
                LoyaltyLogger.Error("POST - " + apiUrl + requestUri + (isAuth ? "*****" : jsonData) + Environment.NewLine + "API RESPONSE: " + response, e);
                string strErr = GetErrorResponse(e);
                var _obj = json_serializer.Deserialize<T>(strErr);

                return _obj;
            }
        }

        private static string GetErrorResponse(Exception e)
        {            
            string message = e.Message;
            string innerMessage = e.InnerException == null ? "" : e.InnerException.Message;
            string innerInnerMessage = string.Empty;
            if (e.InnerException != null && e.InnerException.InnerException != null && e.InnerException.InnerException != null)
            {
                innerInnerMessage = e.InnerException.InnerException.Message;
            }
            string stackTrace = e.StackTrace == null ? "" : e.StackTrace.ToString();
            string errorMessage = string.Format("{0} {1} {2} {3}", message, innerMessage, innerInnerMessage, stackTrace);         
            var json_serializer = new JavaScriptSerializer();
            object objResponse = new { ResponseCode = 500, ResponseDesc = errorMessage };
            return json_serializer.Serialize(objResponse);

        }

        public static async Task<T> PutJsonAsync<T>(string requestUri, string jsonData)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            try
            {
                var strData = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIBaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));

                await httpClient.PutAsync(requestUri, strData)
                        .ContinueWith((resMsg) =>
                        {
                            var rp = resMsg.Result.Content.ReadAsStringAsync();
                            response = rp.Result;
                        });

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch (Exception e)
            {
                // Log data
                LoyaltyLogger.Error("PUT - " + WebConfigurationManager.AppSettings["APIBaseUrl"] + requestUri + jsonData + Environment.NewLine + "API RESPONSE: " + response, e);

                var _obj = json_serializer.Deserialize<T>("");
                return _obj;
            }
        }
        public static async Task<T> GetArrayJsonAsync<T>(string requestUri)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            try
            {
                //SetValidToken();

                var httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIBaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));

                await httpClient.GetAsync(requestUri)
                .ContinueWith((resMsg) =>
                {
                    var rp = resMsg.Result.Content.ReadAsStringAsync();
                    response = rp.Result;
                }).ConfigureAwait(false);

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch (Exception e)
            {
                // Log data
                LoyaltyLogger.Error("GET - " + WebConfigurationManager.AppSettings["APIBaseUrl"] + requestUri + Environment.NewLine + "API RESPONSE: " + response, e);

                var _obj = json_serializer.Deserialize<T>("");
                return _obj;
            }
        }
        public static async Task<T> PatchJsonAsync<T>(string requestUri, string jsonData)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            try
            {
                var strData = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIBaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));

                await PatchAsync(httpClient, requestUri, strData)
                        .ContinueWith((resMsg) =>
                        {
                            var rp = resMsg.Result.Content.ReadAsStringAsync();
                            response = rp.Result;
                        });

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch (Exception e)
            {
                // Log data
                LoyaltyLogger.Error("PATCH - " + WebConfigurationManager.AppSettings["APIBaseUrl"] + requestUri + jsonData + Environment.NewLine + "API RESPONSE: " + response, e);

                var _obj = json_serializer.Deserialize<T>("");
                return _obj;
            }
        }

        public static async Task<T> GetJsonAsync<T>(string requestUri)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            try
            {
                //SetValidToken();
                  
                var httpClient = new HttpClient(); 

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIBaseUrl"]);
              //       httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                await httpClient.GetAsync(requestUri)
                .ContinueWith((resMsg) =>
                {
                    var rp = resMsg.Result.Content.ReadAsStringAsync();
                    response = rp.Result;
                }).ConfigureAwait(false);

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch (Exception e)
            {
                // Log data
                LoyaltyLogger.Error("GET - " + WebConfigurationManager.AppSettings["APIBaseUrl"] + requestUri + Environment.NewLine + "API RESPONSE: " + response, e);

                var _obj = json_serializer.Deserialize<T>("");
                return _obj;
            }
        }

        public static async Task<T> DeleteJsonAsync<T>(string requestUri, string jsonData)
        {
            var json_serializer = new JavaScriptSerializer();
            var response = string.Empty;
            try
            {
                var strData = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                httpClient.BaseAddress = new Uri(WebConfigurationManager.AppSettings["APIBaseUrl"]);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Helpers.Helper.GetClaimsInfo(Enums.ACCESS_TOKEN));

                await DeleteAsync(httpClient, requestUri, strData)
                        .ContinueWith((resMsg) =>
                        {
                            var rp = resMsg.Result.Content.ReadAsStringAsync();
                            response = rp.Result;
                        });

                var _obj = json_serializer.Deserialize<T>(response);
                return _obj;
            }
            catch (Exception e)
            {
                // Log data
                LoyaltyLogger.Error("DELETE - " + WebConfigurationManager.AppSettings["APIBaseUrl"] + requestUri + jsonData + Environment.NewLine + "API RESPONSE: " + response, e);

                var _obj = json_serializer.Deserialize<T>("");
                return _obj;
            }
        }

        private static void SetValidToken()
        {
            var Expiry = Helper.GetClaimsInfo(Enums.EXPIRY_TOKEN);
            if(true)//
            {
                // Call refresh token
                var model = new RefreshTokenModel()
                {
                    AppsVersionCd = Enums.APPS_VERSION_CD,
                    AppsVersionName = Enums.APPS_VERSION_NAME,
                    RefreshToken = Helper.GetClaimsInfo(Enums.REFRESH_TOKEN)
                };

                var rpUser = ApiClient.PostJsonAsync<APIResponseModel<IdentityUserModel>>
                ("Auth/refreshLogin", JsonConvert.SerializeObject(model), true);

                if(rpUser != null && rpUser.Result != null && rpUser.Result.Result != null)
                {
                    Helper.SetCliamsTokenInfo(rpUser.Result.Result);
                }
            }
        }
    }
}