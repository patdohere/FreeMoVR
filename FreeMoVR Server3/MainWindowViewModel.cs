using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMoVR_Server3
{
    class MainWindowViewModel
    {
        public vJoyFeeder vj;

        public string manufacturer;
        public string version;
        public string product;
        public string serial;

        public MainWindowViewModel()
        {
            this.vj = new vJoyFeeder();
            // id is already inputted into the class, it is forced as 1.
            vj.acquire();

            this.manufacturer = vj.getManufacturer();
            this.version = vj.getVersion();
            this.product = vj.getProduct();
            this.serial = vj.getSerialNumber();
        }
    }
}
