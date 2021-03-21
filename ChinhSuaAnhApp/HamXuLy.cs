using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.Util;

namespace ChinhSuaAnhApp
{
    class HamXuLy
    {
        public static Image<Bgr, byte>  ChuyenAnhXam(Image<Bgr, byte> image)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            using (Image<Gray, byte> imageGray = new Image<Gray, byte>(image.Size))
            {
                CvInvoke.CvtColor(image, imageGray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                CvInvoke.CvtColor(imageGray, imageOutput, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);
            }
            return imageOutput;
        }
        public static Image<Bgr, byte> ChuyenAnhDenTrang(Image<Bgr, byte> image, int Threshold)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            using (Image<Gray, byte> imageGray = new Image<Gray, byte>(image.Size))
            {
                CvInvoke.CvtColor(image, imageGray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                CvInvoke.Threshold(imageGray, imageGray, Threshold, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
                CvInvoke.CvtColor(imageGray, imageOutput, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);
            }
            return imageOutput;
        }
        public static Image<Bgr, byte> XoayAnh(Image<Bgr, byte> image, double angle)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            Point center = new Point(image.Width / 2, image.Height / 2);
            using (Mat rotMatrix = new Mat())
            {
                CvInvoke.GetRotationMatrix2D(center, -angle, 1, rotMatrix);
                CvInvoke.WarpAffine(image, imageOutput, rotMatrix, image.Size);
            }
            return imageOutput;
        }
        public static Image<Bgr, byte> LatAnh(Image<Bgr, byte> image, bool X, bool Y)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            if(X)
            {
                CvInvoke.Flip(image, imageOutput, Emgu.CV.CvEnum.FlipType.Horizontal);
            }
            if(Y)
            {
                CvInvoke.Flip(image, imageOutput, Emgu.CV.CvEnum.FlipType.Vertical);
            }
            return imageOutput;
        }
        public static Image<Bgr, byte> LamMoTrungBinh(Image<Bgr, byte> image, int K)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            CvInvoke.Blur(image, imageOutput, new Size(K, K), new Point(-1, -1), Emgu.CV.CvEnum.BorderType.Default);
            return imageOutput;
        }
        public static Image<Bgr, byte> LamMoTrungVi(Image<Bgr, byte> image, int K)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            CvInvoke.MedianBlur(image, imageOutput, K);
            return imageOutput;
        }
        public static Image<Bgr, byte> ChinhDoTuongPhan_DoSang(Image<Bgr, byte> image, double scale, double shift)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            CvInvoke.ConvertScaleAbs(image, imageOutput, scale, shift);
            return imageOutput;
        }
        public static Image<Bgr, byte> TimCanhLaplacian(Image<Bgr, byte> image, int K)
        {
            Image<Bgr, byte> imageOutput = new Image<Bgr, byte>(image.Size);
            CvInvoke.Laplacian(image, imageOutput, Emgu.CV.CvEnum.DepthType.Cv8U, ksize:K);
            return imageOutput;
        }
    }
}
