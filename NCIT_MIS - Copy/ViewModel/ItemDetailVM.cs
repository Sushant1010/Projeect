using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCIT_MIS.ViewModel
{
    public class ItemDetailVM
    {
        public INV_ItemVM Item { get; set; }
        public List<INV_UnitVM> Units
        {
            get; set;
        }
    }
}