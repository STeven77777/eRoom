using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using MetroOil.LoyaltyOps.Models;
using MetroOil.LoyaltyOps.Helpers;
using Newtonsoft.Json;

namespace MetroOil.LoyaltyOps.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = Url.Action("Index", "Home");
            }
            return View(new LoginModel { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel _Login)
        {
            if (string.IsNullOrEmpty(_Login.LoginID) || string.IsNullOrEmpty(_Login.Password))
            {
                return Json(new { desp = "Username or password is invalid" }, JsonRequestBehavior.AllowGet);
            }

            if (_Login.LoginID == "Admin" && _Login.Password == "58rn87")
            {
                var Claims = new List<Claim>{
                     new Claim(ClaimTypes.Name,"Admin"),
                     new Claim(ClaimTypes.Role,"Admin")
                    };
                AuthenticationManager.SignOut(AuthenticationTypes.ApplicationCookie);
                ClaimsIdentity _AdminIdentity = new ClaimsIdentity(Claims, AuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = Url.Action("Index", "UserAccess") }, _AdminIdentity);
                return Json(new { Url = Url.Action("Index", "UserAccess") });
            }

            // CALL API
            _Login.UserType = Enums.USER_TYPE_OPS;
            _Login.DeviceType = Enums.DEVICE_TYPE;
            _Login.DeviceId = Enums.DEVICE_ID;
            _Login.AppsVersionCd = Enums.APPS_VERSION_CD;
            _Login.AppsVersionName = Enums.APPS_VERSION_NAME;

            LoyaltyLogger.Info("LOGIN - Username: " + _Login.LoginID);
            var rpUser = await ApiClient.PostJsonAsync<APIResponseModel<IdentityUserModel>>
                ("Auth/login", JsonConvert.SerializeObject(_Login), true);
            
            if (rpUser != null)
            {
                if (rpUser.ResponseCode == 0 && rpUser.Result != null)
                {
                    LoyaltyLogger.Info("LOGIN SUCCESS - Username: " + _Login.LoginID);

                    var _Claims = new List<Claim>(){
                         new Claim(ClaimTypes.Name, _Login.LoginID),
                         new Claim(ClaimTypes.Role, "Internal"),
                         new Claim(Enums.ACCESS_TOKEN, rpUser.Result.AccessToken),
                         new Claim(Enums.REFRESH_TOKEN, rpUser.Result.RefreshToken),
                         new Claim(Enums.EXPIRY_TOKEN, rpUser.Result.Expiry.ToString()),
                    };

                    AuthenticationManager.SignOut(AuthenticationTypes.ApplicationCookie);
                    ClaimsIdentity _Identity = new ClaimsIdentity(_Claims, AuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                    AuthenticationManager.SignIn(
                        new AuthenticationProperties()
                        {
                            //ExpiresUtc = user.Expiry,
                            IsPersistent = false,
                            RedirectUri = Url.Action("Index", "Home")
                        },
                        _Identity);

                    //var _userAccessIndex = _userAccessService.UserIndexAccess(_Login.LoginID);
                    //Session["UserModules"] = null;

                    if (string.IsNullOrEmpty(_Login.ReturnUrl))
                    {
                        _Login.ReturnUrl = Url.Action("Index", "Home");
                    }
                    return Json(new { Url = _Login.ReturnUrl });
                }
                else
                {
                    LoyaltyLogger.Error("LOGIN FAIL - Username: " + _Login.LoginID + ", Error: " + rpUser.ResponseDesc);
                    ModelState.AddModelError("Error", rpUser.ResponseDesc);
                    return Json(new { desp = rpUser.ResponseDesc }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LoyaltyLogger.Error("LOGIN FAIL - Username: " + _Login.LoginID + ", Error: " + "Username or password is invalid");
                return Json(new { desp = "Username or password is invalid" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(LoginModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<LoginModel>>
                ("Ops/ForgotPassword", JsonConvert.SerializeObject(model));

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(LoginModel model)
        {
            var rp = await ApiClient.PostJsonAsync<APIResponseModel<LoginModel>>
                ("Ops/ResetPassword", JsonConvert.SerializeObject(model));

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            HttpContext.Session["Accessibility"] = null;
            return RedirectToAction("Login", "Auth");
        }
	}
}