using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMoVR_Server3
{
    class MainWindowViewModel
    {
        static private vJoyFeeder vj;
        public MainWindowViewModel()
        {
            vj = new vJoyFeeder();

            // id is already inputted into the class, it is forced as 1.
            //vj.acquire();
            
        }
    }
}
