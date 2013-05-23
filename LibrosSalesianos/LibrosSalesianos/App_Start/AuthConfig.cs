using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using LibrosSalesianos.Models;

namespace LibrosSalesianos
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Para permitir que los usuarios de este sitio inicien sesión con sus cuentas de otros sitios como, por ejemplo, Microsoft, Facebook y Twitter,
            // es necesario actualizar este sitio. Para obtener más información, visite http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "00000000400F57D9",
                clientSecret: "tw0338ub2UqWQKMNGjFrv3g9iz6MGQGJ");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "475912832481850",
                appSecret: "3479f29ddef3c18656afb8be3e894575");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
