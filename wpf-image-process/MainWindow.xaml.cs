using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace wpf_image_process
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String imgToProcessPath = null;
        
        public MainWindow()
        {
            InitializeComponent();
        }



        private void button_process_Click(object sender, RoutedEventArgs e)
        {

            info_text.Text = "正在处理...";

            //if(this.imgToProcessPath==null)
            //{
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)this.img_1.Source));
            FileStream stream = new FileStream("temp", FileMode.Create);
            encoder.Save(stream);
     
            this.imgToProcessPath = "temp";

            stream.Dispose();
            //}
            

            //Console.WriteLine("You have selected:" + select_box.SelectionBoxItem);
            Bitmap bitmap = new Bitmap(this.imgToProcessPath);

            int s = select_box.SelectedIndex;
            switch (s)
            {
                case 0: 
                    bitmap = ImageProcessLALGS.FilterRed(bitmap);
                    break;
                case 1:
                    bitmap = ImageProcessLALGS.FilterBlur(bitmap);
                    break;
                case 2:
                    bitmap = ImageProcessLALGS.Contrast(bitmap, 50);
                    break;
                case 3:
                    bitmap = ImageProcessLALGS.Contrast(bitmap, -50);
                    break;
                case 4:
                    bitmap = ImageProcessLALGS.FaceWhiten(bitmap);
                    break;
            }


            img_2.Source = BitmapToBitmapImage(bitmap);
            bitmap.Dispose();
            info_text.Text = "处理完成！";


        }

        private void choose_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Console.WriteLine("File:" + dialog.FileName);
                //this.imgToProcessPath = dialog.FileName;
                img_1.Source = new BitmapImage(new Uri(dialog.FileName));
            }

            

        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter= "Image Files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //默认保存到我的文档图片目录下
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.FileName = "未命名.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)this.img_2.Source));
                using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
                    encoder.Save(stream);
            }
        }







        private BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            ms.Dispose();
            return bitmapImage;
        }
    }
}
