using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _00012072
{
    public class MyForms
    {
        public static M GetForm<M>() where M : class, new()

        {

            return Application.OpenForms.OfType<M>().FirstOrDefault() ?? new M();

        }
    }
}
