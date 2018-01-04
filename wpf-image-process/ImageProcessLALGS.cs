using System;
using System.Drawing;

namespace wpf_image_process
{
    class ImageProcessLALGS
    {
        static public Bitmap FilterRed(Bitmap bitmap)
        {
            int x, y;

            // 循环选择像素点然后重置每一个像素点
            for (x = 0; x < bitmap.Width; x++)
            {
                for (y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
        }

        static public Bitmap FilterBlur(Bitmap bitmap)
        {
            int x, y;
            for (x = 1; x < bitmap.Width-1; x++)
            {
                for (y = 1; y < bitmap.Height-1; y++)
                {
                    Color pixelColor1 = bitmap.GetPixel(x-1, y-1);
                    Color pixelColor2 = bitmap.GetPixel(x, y-1);
                    Color pixelColor3 = bitmap.GetPixel(x+1, y-1);
                    Color pixelColor4 = bitmap.GetPixel(x-1, y);
                    Color pixelColor5 = bitmap.GetPixel(x, y);
                    Color pixelColor6 = bitmap.GetPixel(x+1, y);
                    Color pixelColor7 = bitmap.GetPixel(x-1, y+1);
                    Color pixelColor8 = bitmap.GetPixel(x, y+1);
                    Color pixelColor9 = bitmap.GetPixel(x-1, y+1);

                    int nr = (pixelColor1.R + pixelColor2.R + pixelColor3.R + pixelColor4.R + pixelColor6.R + pixelColor7.R + pixelColor8.R + pixelColor9.R) / 8;
                    int ng = (pixelColor1.G + pixelColor2.G + pixelColor3.G + pixelColor4.G + pixelColor6.G + pixelColor7.G + pixelColor8.G + pixelColor9.G) / 8;
                    int nb = (pixelColor1.B + pixelColor2.B + pixelColor3.B + pixelColor4.B + pixelColor6.B + pixelColor7.B + pixelColor8.B + pixelColor9.B) / 8;
                    Color newColor = Color.FromArgb(nr, ng, nb);

                    bitmap.SetPixel(x, y, newColor);
                }
            }

            return bitmap;
        }
        static public Bitmap Contrast(Bitmap bitmap,float ratio)
        {
            int x, y;
            double r = (100.0 + ratio) / 100.0;
            for (x = 0; x < bitmap.Width; x++)
            {
                for (y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int newR = Convert.ToInt32((((pixelColor.R / 255.0 - 0.5) * r + 0.5)) * 255);
                    int newG = Convert.ToInt32((((pixelColor.G / 255.0 - 0.5) * r + 0.5)) * 255);
                    int newB = Convert.ToInt32((((pixelColor.B / 255.0 - 0.5) * r + 0.5)) * 255);

                    //处理RGB值越界
                    if (newR < 0)
                        newR = 0;
                    else if (newR > 255)
                        newR = 255;

                    if (newB < 0)
                        newB = 0;
                    else if (newB > 255)
                        newB = 255;

                    if (newG < 0)
                        newG = 0;
                    else if (newG > 255)
                        newG = 255;


                    bitmap.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return bitmap;
        }
        static public Bitmap FaceWhiten(Bitmap bitmap)
        {
            int x, y;
            for (x = 1; x < bitmap.Width - 1; x++)
            {
                for (y = 1; y < bitmap.Height - 1; y++)
                {

                    Color pixelColor = bitmap.GetPixel(x, y);
                    int oldR = pixelColor.R;
                    int oldG = pixelColor.G;
                    int oldB = pixelColor.B;
                    if (oldR > oldG && oldG > oldB && Math.Abs(oldR-oldG)>30)
                    {
                        int brighteness = 30;

                        int newR = Convert.ToInt32((((pixelColor.R / 255.0 - 0.5) * 1.2 + 0.5)) * 255) + brighteness;
                        int newG = Convert.ToInt32((((pixelColor.G / 255.0 - 0.5) * 1.2 + 0.5)) * 255) + brighteness;
                        int newB = Convert.ToInt32((((pixelColor.B / 255.0 - 0.5) * 1.1 + 0.5)) * 255) + brighteness;



                        //处理RGB值越界
                        if (newR < 0)
                            newR = 0;
                        else if (newR > 255)
                            newR = 255;

                        if (newB < 0)
                            newB = 0;
                        else if (newB > 255)
                            newB = 255;

                        if (newG < 0)
                            newG = 0;
                        else if (newG > 255)
                            newG = 255;

                        Color newColor = Color.FromArgb(newR, newG, newB);
                        bitmap.SetPixel(x, y, newColor);
                    }


                    
                }
            }

            return bitmap;
        }
    }
}
