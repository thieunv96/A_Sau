using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace ChinhSuaAnhApp
{
    public partial class Main : Form
    {
        private Image<Bgr, byte> AnhGoc = null;
        private Image<Bgr, byte> AnhXuLy = null;
        public Main()
        {
            InitializeComponent();
            timer1_Tick(null, null);
        }

        private void btTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File | *.png;*.jpg;*.bmp;*.jpeg;*.tiff";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    AnhGoc = new Image<Bgr, byte>(ofd.FileName);
                    Log("Tải ảnh", sw.ElapsedMilliseconds);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (AnhXuLy != null)
                {
                    AnhXuLy.Dispose();
                    AnhXuLy = null;
                }
                AnhXuLy = AnhGoc.Copy();
                imbGoc.Image = AnhGoc;
                imbXuLy.Image = AnhXuLy;
            }
        }
        private void btLuuAnh_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var image = AnhXuLy == null ? AnhGoc : AnhXuLy;
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.DefaultExt = ".png";
            ofd.AddExtension = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    CvInvoke.Imwrite(ofd.FileName, image); 
                    Log("Lưu ảnh", sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            
        }
        private void Log(string Action, long time)
        {
            lbXuLy.Text = $"{Action} thành công trong  {time} ms...";
        }
        private void rbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if(rbZoom.Checked)
            {
                imbGoc.SizeMode = PictureBoxSizeMode.Normal;
                imbXuLy.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
            {
                imbGoc.SetZoomScale(1, new Point());
                imbXuLy.SetZoomScale(1, new Point());
                imbGoc.SizeMode = PictureBoxSizeMode.StretchImage;
                imbXuLy.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            rbZoom_CheckedChanged(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }
        private void BtCvtAnhXam_Click(object sender, EventArgs e)
        {
            if(AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.ChuyenAnhXam(AnhGoc);
            Log("Chuyển ảnh xám", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
            
        }
        private void nThreshold_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btCvtAnhDenTrang_Click(null, null);
        }
        private void btCvtAnhDenTrang_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.ChuyenAnhDenTrang(AnhGoc, Convert.ToInt32(nThreshold.Value));
            Log("Chuyển ảnh đen trắng", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
        private void nGocXoay_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btXoayAnh_Click(null, null);
        }
        private void btXoayAnh_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.XoayAnh(AnhGoc, Convert.ToDouble(nGocXoay.Value));
            Log("Xoay ảnh", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
        private void rbLatAnhX_CheckedChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            rbLatAnh_Click(null, null);
        }
        private void rbLatAnh_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.LatAnh(AnhGoc, rbLatAnhX.Checked, rbLatAnhY.Checked);
            Log("Lật ảnh", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
        private void nKLamMoTrungBinh_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btLamMoTrungBinh_Click(null, null);
        }
        private void btLamMoTrungBinh_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.LamMoTrungBinh(AnhGoc, Convert.ToInt32(nKLamMoTrungBinh.Value));
            Log("Làm mờ trung bình ảnh", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
        private void nKLamMoTrungVi_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btLamMoTrungVi_Click(null, null);
        }
        private void btLamMoTrungVi_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.LamMoTrungVi(AnhGoc, Convert.ToInt32(nKLamMoTrungVi.Value));
            Log("Làm mờ trung vị ảnh", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
        private void nDoTuongPhan_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btTuongPhanSang_Click(null, null);
        }
        private void btTuongPhanSang_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.ChinhDoTuongPhan_DoSang(AnhGoc, Convert.ToDouble(nDoTuongPhan.Value), Convert.ToDouble(nDoSang.Value));
            Log("Chỉnh độ tương phản, độ sáng", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }

        private void nKLaplacian_ValueChanged(object sender, EventArgs e)
        {
            if (AnhGoc == null)
                return;
            btAnhcanhLaplacian_Click(null, null);
        }

        private void btAnhcanhLaplacian_Click(object sender, EventArgs e)
        {
            if (AnhGoc == null)
            {
                MessageBox.Show("Vui lòng tải ảnh trước...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (AnhXuLy != null)
            {
                AnhXuLy.Dispose();
                AnhXuLy = null;
            }
            AnhXuLy = HamXuLy.TimCanhLaplacian(AnhGoc, Convert.ToInt32(nKLaplacian.Value));
            Log("Tìm cạnh Laplacian của ảnh", sw.ElapsedMilliseconds);
            imbGoc.Image = AnhGoc;
            imbXuLy.Image = AnhXuLy;
        }
    }
}
