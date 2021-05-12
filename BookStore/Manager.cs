using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace BookStore
{
    class Manager
    {
        public static Frame FrameMainWindow { get; set; }
        public static SqlConnection connection { get; set; }
    }
}
