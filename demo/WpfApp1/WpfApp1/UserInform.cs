using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class UserInform
    {

        public int id;
        public string name;
        public string gender;
        public string specialty;
        public string brith;
        public string mail;
        public string phone;

        public UserInform(int id, string name, string gender, string specialty,
                         string brith, string mail, string phone)
            {
                this.id = id;
                this.name = name;
                this.gender = gender;
                this.specialty = specialty;
                this.brith = brith;
                this.mail = mail;
                this.phone = phone;

            }
       
    }
}
