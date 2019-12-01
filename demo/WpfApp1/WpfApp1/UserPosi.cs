using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class UserPosi
    {
        public int id;
        public string name;
        public string position;
        public decimal salary;
        public UserPosi(int id, string name, string position, decimal salary)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.salary = salary;
            
        }
    }
}
