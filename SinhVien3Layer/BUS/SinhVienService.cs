using SinhVien3Layer.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinhVien3Layer.BUS
{
    /// <summary>
    /// xem cách dùng trong Form 1
    /// </summary>
    internal class SinhVienService
    {
        public static readonly SinhVienService Instance = new SinhVienService();
        private static string sinhVienTableName => nameof(SinhVien);

        public DataTable SinhVienTable { get; private set; } = new DataTable(sinhVienTableName);
        public SinhVienService()
        {
            TaoCauTrucChoDB(SinhVienTable);
            KhoiTaoDuLieuSinhVien(SinhVienTable);
        }

        private void KhoiTaoDuLieuSinhVien(DataTable sinhVienTable)
        {
            //TODO: kiểm tra cấu trúc DB
            for (int i = 0; i < 10; i++)
            {
                sinhVienTable.Rows.Add(i,$"Nguyễn Văn {i}", 1990 + i);
            }
        }

        private void TaoCauTrucChoDB(DataTable sinhVienTable)
        {
            DataColumn maSoSV = new DataColumn
            {
                ColumnName = nameof(SinhVien.MaSo),
                DataType = typeof(int),
                Unique = true,
                AllowDBNull = false
            };

            DataColumn hoTen = new DataColumn
            {
                ColumnName = nameof(SinhVien.HoVaTen),
                DataType = typeof(string),
                AllowDBNull = false,
            };

            DataColumn namSinh = new DataColumn
            {
                ColumnName = nameof(SinhVien.NamSinh),
                DataType = typeof(int),
                AllowDBNull = false
            };
            sinhVienTable.Columns.Add(maSoSV);
            sinhVienTable.Columns.Add(hoTen);
            sinhVienTable.Columns.Add(namSinh);

        }
        public bool XoaSinhVien(SinhVien sv)
        {
            try
            {
                var svInDB = TimDongDuLieuCoSinhVien(sv.MaSo);
                if (svInDB != null)
                {
                    SinhVienTable.Rows.Remove(svInDB);
                    return true;
                }
            }
            catch (Exception)
            {
                
            }
            return false;
        }

        private DataRow? TimDongDuLieuCoSinhVien(int maSo)
        {
            foreach (var item in SinhVienTable.Rows)
            {
                var row = item as DataRow;
                if (row != null)
                {
                    var msSV = (int)row[nameof(SinhVien.MaSo)];
                    if (maSo == msSV)
                    {
                        return row;
                    }

                }
            }
            return null;
        }

        public bool SuaSinhVien(SinhVien sv)
        {
            try
            {
                var svInDB = TimDongDuLieuCoSinhVien(sv.MaSo);
                if (svInDB != null)
                {
                    ///Mã số là để quản lý nên không thể sửa.
                    svInDB[nameof(SinhVien.MaSo)] = sv.MaSo;
                    svInDB[nameof(SinhVien.HoVaTen)] = sv.HoVaTen;
                    svInDB[nameof(SinhVien.NamSinh)] = sv.NamSinh;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public SinhVien? TimSinhVien(int masoSV)
        {
            SinhVien? output = null;
            var svInDB = TimDongDuLieuCoSinhVien(masoSV);
            if (svInDB != null)
            {
                output = new SinhVien(
                            maSo: (int)svInDB[nameof(SinhVien.MaSo)],
                            hoVaTen: (string)svInDB[nameof(SinhVien.HoVaTen)],
                            namSinh: (int)svInDB[nameof(SinhVien.NamSinh)]
                            );
            }
            
            return output;
        }
        public bool TaoMoiSinhVien(SinhVien sv)
        {
            try
            {
                SinhVienTable.Rows.Add(sv.MaSo, sv.HoVaTen, sv.NamSinh);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
