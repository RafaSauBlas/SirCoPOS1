using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SirCoPOS.Web
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
                {
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                            ctx.Response.Redirect(ctx.RedirectUri);
                    }
                }
            });

            //app.UseFileServer(MapPath("wwwroot", null));
            //app.UseFileServer(MapPath("Scripts", "/Scripts"));
            //app.UseFileServer(MapPath("Content", "/Content"));
            //app.UseFileServer(MapPath("Areas/HelpPage", "/Areas/HelpPage", false));
        }
        private FileServerOptions MapPath(string path, string request, bool unknownFiles = true)
        {
            var requestPath = request == null ?
                PathString.Empty : new PathString(request);
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var physicalFileSystem = new PhysicalFileSystem(Path.Combine(root, path));
            var options = new FileServerOptions
            {
                RequestPath = requestPath,
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = unknownFiles;
            return options;
        }
        private bool IsAjaxRequest(IOwinRequest request)
        {
            var apiPath = VirtualPathUtility.ToAbsolute("~/api/");
            if (request.Uri.LocalPath.StartsWith(apiPath, StringComparison.InvariantCultureIgnoreCase))
                return true;

            var query = request.Query;
            if (query != null && query["X-Requested-With"] == "XMLHttpRequest")
            {
                return true;
            }

            var headers = request.Headers;
            return headers != null && headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }

    //public static class WCFAppBuilderExtensions
    //{
    //    public static IAppBuilder IgnoreWCFRequests(this IAppBuilder builder)
    //    {
    //        return builder.MapWhen(context => IsWCFRequest(context), appBuilder =>
    //        {
    //            // Do nothing and allow the IIS ASP.NET pipeline to process the request
    //        });
    //    }

    //    private static bool IsWCFRequest(IOwinContext context)
    //    {
    //        // Determine whether the request is to a WCF endpoint
    //        return context.Request.Path.Value.EndsWith(".svc", StringComparison.OrdinalIgnoreCase);
    //    }
    //}
}