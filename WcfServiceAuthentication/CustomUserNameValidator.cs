using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfServiceAuthentication
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (null == userName || null == password)
            {
                throw new ArgumentNullException();
            }

            DBConnector dbc = new DBConnector();
            if (!dbc.ValidateLogin(userName, password))
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}