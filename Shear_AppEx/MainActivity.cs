using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Shear_AppEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _ShareText, _ShareLink, _ShareAttachment, _ShareMulAttachment;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            UIReferences();
            UIClick();

        }

        private void UIReferences()
        {
            _ShareText = FindViewById<Button>(Resource.Id.btn1);
            _ShareLink = FindViewById<Button>(Resource.Id.btn2);
            _ShareAttachment = FindViewById<Button>(Resource.Id.btn3);
            _ShareMulAttachment = FindViewById<Button>(Resource.Id.btn4);
        }

        private void UIClick()
        {
            _ShareText.Click += ButtonA_Click;
            _ShareLink.Click += ButtonB_Click;
            _ShareAttachment.Click += ButtonC_Click;
            _ShareMulAttachment.Click += ButtonD_Click;
        }

        private void ButtonA_Click(object sender, EventArgs e)
        {
            _ = ShareText("Hello");
        }

        private async Task ShareText(string v)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Hello"
            });
        }

        private void ButtonB_Click(object sender, EventArgs e)
        {
            _ = ShareUri("https://www.canva.com/colors/color-palettes/facing-forward/");
        }

        private async Task ShareUri(string v)
        {
            await Share.RequestAsync(new ShareTextRequest 
            {
                Title = "https://www.canva.com/colors/color-palettes/facing-forward/"
            });
            
        }

        private void ButtonC_Click(object sender, EventArgs e)
        {
            _ = ShareFile();
        }

        private async Task ShareFile()
        {
            var res = "H1.txt";
            var file = Path.Combine(FileSystem.CacheDirectory, res);
            File.WriteAllText(file, "Hiii");

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = Title,
                File = new ShareFile(file)
            });
        }

        private void ButtonD_Click(object sender, EventArgs e)
        {
            _ = ShareMultipleFiles();
        }

        private async Task ShareMultipleFiles()
        {
            var file1 = Path.Combine(FileSystem.CacheDirectory, "Attachment1.txt");
            File.WriteAllText(file1, "Content1");

            var file2 = Path.Combine(FileSystem.CacheDirectory, "Attachment2.txt");
            File.WriteAllText(file2,"Content2");

            await Share.RequestAsync(new ShareMultipleFilesRequest { 
              Title= "Title",
              Files = new List<ShareFile> { new ShareFile(file1), new ShareFile(file2)}
            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}