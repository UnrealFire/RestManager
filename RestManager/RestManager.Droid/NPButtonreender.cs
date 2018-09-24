using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestManager;
using RestManager.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Controls;
using Color = Android.Graphics.Color;

//Класс реендра кастомной кнопки. В оригинальных кнопках текст имеет большие поля до границ кнопки изнутри и при маленьком размере кнопки не помещается в нее
[assembly: ExportRenderer(typeof(NPButton), typeof(NPButtonreender))]
namespace RestManager.Droid
{
    class NPButtonreender : ButtonRenderer
    {



        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var nativeButton = (global::Android.Widget.Button)this.Control;
            nativeButton.Background = null;
            nativeButton.SetBackgroundColor(Color.LightSlateGray);
            nativeButton.SetPadding(0,0,0,0);
        }

        
    }
}