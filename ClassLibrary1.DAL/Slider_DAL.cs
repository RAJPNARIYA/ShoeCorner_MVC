using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DAL
{
    public class Slider_DAL
    {
        public String Slider_id { get; set; }
        public String Slider_Name { get; set; }
        public String Slider_img { get; set; }
        public String Slider_line1 { get; set; }
        public String Slider_line2 { get; set; }

        public List<Slider_DAL> SliderList { get; set; }

    }
}
