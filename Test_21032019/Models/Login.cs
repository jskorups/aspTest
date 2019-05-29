using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_21032019.Models
{
    public class Login
    {
        public string login { get; set; }
        public string email { get; set; }
        public string guid { get; set; }

        [Required]
        public string password { get; set; }
        public bool aktywny { get; set; }

        //???
        [Required]
        [Compare("password", ErrorMessage = "Wpisane hasła nie są jednakowe")]
        public string ConfirmPassword { get; set; }

        public void SaveToDatabase()
        {
            testowaEntities ent = new testowaEntities();
            loginy l = new loginy();
            l.login = login;
            //???
            l.password = Cryptop.Hash(password);
            l.email = email;
            l.guid = guid;
            l.aktywny = aktywny;
            ent.loginies.Add(l);
            ent.SaveChanges();
        }

        public bool CheckIfExist(string login, string password)
        {

            testowaEntities ent = new testowaEntities();
            int logins = ent.loginies.Count(x => x.login == login && x.password == password);
            if (logins == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }



}