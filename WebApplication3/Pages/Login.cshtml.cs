using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApplication3.ViewModels;
using System.Net;
using System.IO;
//using System.Web.Script.Serialization;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<IdentityUser> signInManager;
		public LoginModel(SignInManager<IdentityUser> signInManager)
		{
			this.signInManager = signInManager;
		}
		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
			   LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, "c@c.com"),
						new Claim(ClaimTypes.Email, "c@c.com")
					};

					var i = new ClaimsIdentity(claims, "MyCookieAuth");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(i);
					await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

					return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}

		//public bool ValidateCaptcha()
		//{
		//	bool result = true;
		//	string captchaResponse = Request.Form["g-recaptcha-response"];
		//	HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.google.com/recaptcha/api/siteverify?secret=6Lf4FmEpAAAAAJxZ7B1xzzpEY1dpIql-iC96LIhN &response=" + captchaResponse);
		//
		//	try
		//	{
		//		using (WebResponse wResponse = req.GetResponse())
		//		{
		//			using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
		//			{
		//				string jsonResponse = readStream.ReadToEnd();
		//
		//				JavaScriptSerializer js = new JavaScriptSerializer();
		//
		//				Login jsonObject = js.Deserialize<Login>(jsonResponse);
		//
		//				result = Convert.ToBoolean(jsonObject.success);
		//			}
		//		}
		//		return result;
		//	}
		//	catch (WebException ex)
		//	{
		//		throw ex;
		//	}
		//}
	}
}
