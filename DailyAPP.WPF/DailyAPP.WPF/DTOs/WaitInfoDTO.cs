using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.DTOs
{
    class WaitInfoDTO
    {
        public int WaitID { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public int Status { get; set; }

        public string BackColor 
        {
            get 
            {
                return Status == 0 ? "#1E90FF" : "#3CB371";
            }

          }
    }
}
