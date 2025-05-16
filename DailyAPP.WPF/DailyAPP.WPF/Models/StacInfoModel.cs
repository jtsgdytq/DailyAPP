using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.Models
{
    class StacInfoModel:BindableBase
    {
        public string Icon { get; set; }
        public string ItemName { get; set; }

        private string _Result;

        public string Result
        {
            get { return _Result; }
            set { 
                _Result = value;
                RaisePropertyChanged();
            }
        }

       

        public string BackColor { get; set; }
        public string ViewName { get; set; }

        
        public string Hand
        {
            get
            {
                if (ItemName != "比例")
                {
                    return "Hand";
                }
                else
                    return "";
            }
            
        }


    }
}
