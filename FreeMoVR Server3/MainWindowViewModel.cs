using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMoVR_Server3
{
    class MainWindowViewModel
    {
        private vJoyFeeder vj;

        public MainWindowViewModel()
        {
            vj = new vJoyFeeder();
            vj.acquire(1);
        }
    }
}
