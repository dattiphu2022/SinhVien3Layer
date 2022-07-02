using SinhVien3Layer.BUS;
using SinhVien3Layer.DTO;
using System.Data;
using System.Linq;

namespace SinhVien3Layer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KhaiBaoThuocTinhCuarDatagrid(dataGridView1);
            BindDuLieuLenDatagrid(dataGridView1, SinhVienService.Instance.SinhVienTable);
        }

        private void KhaiBaoThuocTinhCuarDatagrid(DataGridView dgv)
        {
            dgv.SelectionChanged += DataGridView1_SelectionChanged;
            dgv.AutoGenerateColumns = true;
            dgv.AutoResizeColumns();
        }

        private void BindDuLieuLenDatagrid(DataGridView dgv, DataTable table)
        {
            BindingSource SBind = new BindingSource();
            SBind.DataSource = table;
            dgv.DataSource = null;
            dgv.DataSource = SBind;
            dgv.Refresh();
        }

        private void DataGridView1_SelectionChanged(object? sender, EventArgs e)
        {
            Int32 selectedRowCount =
        dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                try
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    var msSV = selectedRow?.Cells[nameof(SinhVien.MaSo)]?.Value;
                    if (msSV == null)
                    {
                        return;
                    }
                    var svInDB = SinhVienService.Instance.TimSinhVien((int)msSV);
                    if (svInDB != null)
                    {

                        textBox1.Text = svInDB.MaSo.ToString();
                        textBox2.Text = svInDB.HoVaTen;
                        textBox3.Text = svInDB.NamSinh.ToString();
                    }
                }
                catch (Exception) { }
            }
        }

        //Thêm
        private void button1_Click(object sender, EventArgs e)
        {
            var parseMSSV = int.TryParse(textBox1.Text, out int msSV);
            if (!parseMSSV)
            {
                MessageBox.Show("MSSV phải là một số INT");
                return;
            }
            var parseNS = int.TryParse(textBox3.Text, out int nsSV);
            if (!parseNS)
            {
                MessageBox.Show("Năm sinh không hợp lệ");
                return;
            }
            var newSV = new SinhVien(
                maSo: int.Parse(textBox1.Text),
                hoVaTen: textBox2.Text,
                namSinh: int.Parse(textBox3.Text)
                );
            var ketQuaTaoMoi = SinhVienService.Instance.TaoMoiSinhVien(newSV);
            if (!ketQuaTaoMoi)
            {
                MessageBox.Show("Thêm KHÔNG thành công, vui lòng kiểm tra lại dữ liệu");
            }
            else
            {
                MessageBox.Show("Thêm thành công!");
            }
            BindDuLieuLenDatagrid(dataGridView1, SinhVienService.Instance.SinhVienTable);
            
        }

        //Sửa
        private void button2_Click(object sender, EventArgs e)
        {
            var parseMSSV = int.TryParse(textBox1.Text, out int msSV);
            if (!parseMSSV)
            {
                MessageBox.Show("MSSV phải là một số INT");
                return;
            }
            var parseNS = int.TryParse(textBox3.Text, out int nsSV);
            if (!parseNS)
            {
                MessageBox.Show("Năm sinh không hợp lệ");
                return;
            }

            var newSV = new SinhVien(
                maSo: int.Parse(textBox1.Text),
                hoVaTen: textBox2.Text,
                namSinh: int.Parse(textBox3.Text)
                );

            var ketQuaSua = SinhVienService.Instance.SuaSinhVien(newSV);
            if (!ketQuaSua)
            {
                MessageBox.Show("Sửa KHÔNG thành công, vui lòng kiểm tra lại dữ liệu");
            }
            else
            {
                MessageBox.Show("Sửa thành công!");
            }
            BindDuLieuLenDatagrid(dataGridView1, SinhVienService.Instance.SinhVienTable);
        }

        //xóa
        private void button4_Click(object sender, EventArgs e)
        {
            var parseMSSV = int.TryParse(textBox1.Text, out int msSV);
            if (!parseMSSV)
            {
                MessageBox.Show("MSSV phải là một số INT");
                return;
            }
            var parseNS = int.TryParse(textBox3.Text, out int nsSV);
            if (!parseNS)
            {
                MessageBox.Show("Năm sinh không hợp lệ");
                return;
            }

            var newSV = new SinhVien(
                maSo: int.Parse(textBox1.Text),
                hoVaTen: textBox2.Text,
                namSinh: int.Parse(textBox3.Text)
                );

            var ketQuaXoa = SinhVienService.Instance.XoaSinhVien(newSV);
            if (!ketQuaXoa)
            {
                MessageBox.Show("Xóa KHÔNG thành công, vui lòng kiểm tra lại dữ liệu");
            }
            else
            {
                MessageBox.Show("Xóa thành công!");
            }
            BindDuLieuLenDatagrid(dataGridView1, SinhVienService.Instance.SinhVienTable);
        }
    }
}