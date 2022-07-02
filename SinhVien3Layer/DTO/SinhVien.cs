using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinhVien3Layer.DTO
{
    internal class SinhVien
    {
        public SinhVien( int maSo,string hoVaTen, int namSinh)
        {
            HoVaTen = hoVaTen;
            MaSo = maSo;
            NamSinh = namSinh;
        }

        public string HoVaTen { get; set; }
        public int MaSo { get; set; }
        public int NamSinh { get; set; }
    }
}
