using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_21032019.Models
{
    public class passwordResetModel
    {
        public string adresEmail { get; set; }

        public string guid { get; set; }

        public static void setResetCode (string adresEmail, string Guid)
        {

            testowaEntities ent = new testowaEntities();
            loginy user =  ent.loginies.Where(x => x.email == adresEmail).FirstOrDefault();
           
            if (user != null)
            {
                user.guid = Guid;
                ent.SaveChanges();
            }
            else
            {
                ///
            }
            
        }

        public static bool checkGuid (string guid , string email)
        {
            testowaEntities ent = new testowaEntities();
            int Count =  ent.loginies.Where(x => x.email == email && x.guid == guid).Count();

            if (Count != 0)
            {
                return true;
            }
            return false;
        }

        public static bool resetPassword (string email, string guid, string newPassword)
        {
            testowaEntities ent = new testowaEntities();
            var user = ent.loginies.Where(x => x.email == email && x.guid == guid).FirstOrDefault();
            if (user != null)
            {
                user.password = Cryptop.Hash(newPassword);
                ent.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

            

    }

   

}