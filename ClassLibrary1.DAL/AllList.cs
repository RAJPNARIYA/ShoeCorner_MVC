using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.DAL;

namespace ClassLibrary1.DAL
{
	public class AllList
	{
        public IEnumerable<ProductDAL> ProductViewList { get; set; }
        public IEnumerable<Slider_DAL> SliderViewList { get; set; }

    }
}
