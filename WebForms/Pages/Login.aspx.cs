using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //control de licencias
            //DealerList dealers = DealerMapper.Instance().GetAll();
            //foreach (Dealer dealer in dealers)
            //{
            //    if (string.IsNullOrEmpty(dealer.InternalInfo))
            //    {
            //        CompaniesList companiesByDealer = CompaniesMapper.Instance().GetByDealer(dealer.DealerId);
            //        if (companiesByDealer.Count > 0)
            //        {
            //            LicensingRules.InitializeDealerLicense(companiesByDealer[0].ConnectionString, dealer.DealerId);
            //        }
            //    }
            //}
            // set focus to UserName if blank, otherwise Password
            // If "remember me" is implemented, do this after UserName is populated
            // or just set focus to Password instead


            //TextBox userName = Login1.FindControl("UserName") as TextBox;
            //TextBox password = Login1.FindControl("Password") as TextBox;
            //if (userName != null && password != null)
            //{
            //    if (userName.Text != string.Empty)
            //        password.Focus();
            //    else
            //        userName.Focus();
            //}
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = emailInput.Text;
            string password = passwordInput.Text;

            // Aquí debes incluir la lógica de autenticación
            if (email == "usuario@example.com" && password == "password123")
            {
                // Autenticación exitosa
                Response.Redirect("HomePage.aspx"); // Redirigir a la página principal
            }
            else
            {
                // Fallo en la autenticación
                // Mostrar mensaje de error
            }
        }
    }
}