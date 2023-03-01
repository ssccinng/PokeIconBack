using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace PokeIconBack
{
    public class AutoGrayImage : Image
    {
        public AutoGrayImage()
        {
          //  IsEnabledChanged += new
          //DependencyPropertyChangedEventHandler(AutoGrayImage_IsEnabledChanged);
        }



        public string MyProperty
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(string), typeof(AutoGrayImage), new PropertyMetadata("", OnIsGaryChanged));





        public bool IsGary
        {
            get { return (bool)GetValue(IsGaryProperty); }
            set { 
                SetValue(IsGaryProperty, value);
                 
            }
        }



        // Using a DependencyProperty as the backing store for IsGary.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsGaryProperty =
            DependencyProperty.Register("IsGary", typeof(bool), typeof(AutoGrayImage), new PropertyMetadata(false, OnIsGaryChanged));
        static void OnIsGaryChanged(DependencyObject sender,
                                     DependencyPropertyChangedEventArgs e)
        {
            (sender as AutoGrayImage).Change();
        }

        public void Change()
        {
            Source = IsGary ? GrayedImage : Source2;

        }
        void AutoGrayImage_IsEnabledChanged(object sender,
                                DependencyPropertyChangedEventArgs e)
        {
            Source = IsEnabled ? Source2 : GrayedImage;
        }
        FormatConvertedBitmap GrayedImage = null;

        public static readonly DependencyProperty Source2Property =
                     DependencyProperty.Register("Source2", typeof(BitmapSource),
                            typeof(AutoGrayImage), new PropertyMetadata(null,
                            OnSource2Changed));
        /// <summary>
        /// Sets the image to be grayed, or not.
        /// </summary>
        public BitmapSource Source2
        {
            get { return (BitmapSource)GetValue(Source2Property); }
            set { SetValue(Source2Property, value); }
        }
        static void OnSource2Changed(DependencyObject sender,
                                      DependencyPropertyChangedEventArgs e)
        {
            AutoGrayImage s = sender as AutoGrayImage;
            if (s.Source2 == null)
            {
                s.GrayedImage = null;
            }
            else
            {
                s.GrayedImage = new FormatConvertedBitmap(s.Source2,
                                    PixelFormats.Gray8, null, 0);
                s.OpacityMask = new ImageBrush(s.Source2);
            }
            //OnIsGaryChanged(sender, e);
            s.AutoGrayImage_IsEnabledChanged(s, new
                                 DependencyPropertyChangedEventArgs());
        }
    }
}
