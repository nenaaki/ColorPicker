using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorPicker
{
    /// <summary>
    /// RingColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class RingColorPicker : UserControl
    {
        public RingColorPicker()
        {
            InitializeComponent();

            int width = 128;
            int height = 128;

            Ring.Source = MakeHueRountRect(width, height);

            //切り抜いて円環にする、PathGeometry
            double ringWidth = 10;//リングの幅指定
            PathGeometry pg = new PathGeometry();
            //外側用Geometry作成追加
            pg.AddGeometry(new EllipseGeometry(new Rect(0, 0, width, height)));
            //内側用Geometry作成追加
            double xCenter = width / 2.0;//50
            double yCenter = height / 2.0;
            double xRadius = xCenter - ringWidth;//40
            double yRadius = yCenter - ringWidth;
            pg.AddGeometry(new EllipseGeometry(new Point(xCenter, yCenter), xRadius, yRadius));

            Ring.Clip = pg;
        }

        private BitmapSource MakeHueRountRect(int width, int height)
        {
            var wb = new WriteableBitmap(width, height, 96, 96, PixelFormats.Rgb24, null);
            //色情報用のバイト配列作成
            int stride = wb.BackBufferStride;//横一列のバイト数、24bit = 8byteに横ピクセル数をかけた値
            byte[] pixels = new byte[height * stride * 8];//*8はbyteをbitにするから

            //100ｘ100のとき中心は50，50
            //ピクセル位置と画像の中心との差
            int xDiff = width / 2;
            int yDiff = height / 2;
            int p = 0;//今のピクセル位置の配列での位置
            for (int y = 0; y < height; y++)//y座標
            {
                for (int x = 0; x < width; x++)//x座標
                {
                    //今の位置の角度を取得、これが色相になる
                    double radian = Math.Atan2(y - yDiff, x - xDiff);
                    //色相をColorに変換
                    var c = HsvColor.ToRgb(radian, 1.0, 1.0);
                    //バイト配列に色情報を書き込み
                    p = y * stride + x * 3;
                    pixels[p] = c.R;
                    pixels[p + 1] = c.G;
                    pixels[p + 2] = c.B;
                }
            }
            //バイト配列をBitmapに書き込み
            wb.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
            return wb;
        }
    }
}