using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using SixLabors.ImageSharp;
using static System.Windows.Forms.LinkLabel;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Reflection;

namespace AutoEpicSeven
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            flagInput.Text = "10";
        }
        private void runAdb()
        {
            //Cap screenshot from phone
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("adb devices");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            var counter = 0;
            while (!cmd.StandardOutput.EndOfStream)
            {
                var line = cmd.StandardOutput.ReadLine();
                counter++;

                if (counter == 6 || counter == 8)
                {
                    if (line.Contains("device"))
                    {
                        adbLabel.Text = "adb starts successfully!";
                        break;
                    }
                    
                }
            }
        }

        private void captureScreenshot(String path)
        {
            //Cap screenshot from phone
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("adb exec-out screencap -p > "+path);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Debug.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        //private string getFlag()
        //{
        //    //Get number of flags in screenshot
        //    Bitmap image = new Bitmap(@"D:\Hoc_tap\C#\AutoEpicSeven\AutoEpicSeven\images\screenshot.png");
        //    Crop filter = new Crop(new System.Drawing.Rectangle(1120, 30, 93, 40));
        //    // apply the filter
        //    Bitmap newImage = filter.Apply(image);
        //    newImage.Save(@"D:\Hoc_tap\C#\AutoEpicSeven\AutoEpicSeven\images\flag.png");
        //    //location to testdata for eng.traineddata
        //    var path = @"D:\Hoc_tap\C#\AutoEpicSeven\AutoEpicSeven\tessdata";
        //    //your sample image location
        //    var sourceFilePath = @"D:\Hoc_tap\C#\AutoEpicSeven\AutoEpicSeven\images\flag.png";
        //    using (var engine = new TesseractEngine(path, "eng"))
        //    {
        //        engine.SetVariable("user_defined_dpi", "70"); //set dpi for supressing warning
        //        using (var img = Pix.LoadFromFile(sourceFilePath))
        //        {
        //            using (var page = engine.Process(img))
        //            {
        //                var text = page.GetText();
        //                return text;
        //            }
        //        }
        //    }
        //}

        private void clickXY(int x1, int x2,int y1, int y2)
        {
            Random r = new Random();
            int x = r.Next(x1, x2);

            int y = r.Next(y1, y2);

            //Debug.WriteLine(x1);
            //Debug.WriteLine(x2);
            //Debug.WriteLine(y1);
            //Debug.WriteLine(y2);
            //Debug.WriteLine(x);
            //Debug.WriteLine(y);

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("adb shell input tap "+x.ToString()+" "+y.ToString());
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Debug.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private int findRetry(String path)
        {
            using (Bitmap myBitmap = new Bitmap(path))
            {
                //Y of Fight buttons and Retry button's coordinates
                int[] pixels = { 270, 435, 600, 765, 935 };

                for (int i = 0; i <= 4; i++)
                {
                    //Get pixel
                    System.Drawing.Color pixelColor = myBitmap.GetPixel(1635, pixels[i]);
                    //Debug.WriteLine(pixelColor.ToString());
                    //Compare the color to distinguish the button need to be clicked.
                    if (pixelColor.R > 20)
                        return pixels[i];
                }
                return 0;
            }  
        }

        private Bitmap cropImage(Bitmap image, int x1, int x2, int y1, int y2)
        {

            Crop filter = new Crop(new System.Drawing.Rectangle(x1,x2,y1,y2));
            // apply the filter
            Bitmap newImage = filter.Apply(image);
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0,0,newImage.Width, newImage.Height);
            Bitmap bitmap = newImage.Clone(rec, PixelFormat.Format24bppRgb);
            
            //Bitmap bp = new Bitmap(newImage.Width, newImage.Height, PixelFormat.Format24bppRgb);
            //using (Graphics gr = Graphics.FromImage(bp))
            //    gr.DrawImage(newImage, new System.Drawing.Rectangle(0, 0, bp.Width, bp.Height));
            return bitmap;
        }

        private bool compareImage(Bitmap croppedSource, Bitmap template, int x, int y, int width, int height)
        {
            Crop filter = new Crop(new System.Drawing.Rectangle(x, y, width, height));
            // apply the filter
            Bitmap croppedTemplate = filter.Apply(template);

            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(0, 0, croppedSource.Width, croppedSource.Height);
            Bitmap formatted_source = croppedSource.Clone(rec, PixelFormat.Format24bppRgb);
            Bitmap formatted_template = croppedTemplate.Clone(rec, PixelFormat.Format24bppRgb);

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            var sourceComb = Path.Combine(outPutDirectory, "images\\non_format_s.png");
            var templateComb = Path.Combine(outPutDirectory, "images\\non_format_t.png");

            String sourcePath = new Uri(sourceComb).LocalPath;
            String templatePath = new Uri(templateComb).LocalPath;

            formatted_source.Save(sourcePath);
            formatted_template.Save(templatePath);

            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            // compare two images
            TemplateMatch[] matchings = tm.ProcessImage(formatted_source, formatted_template);
            // check similarity level
            if (matchings[0].Similarity > 0.95f)
            {
                Debug.WriteLine("The Same");
                return true;
            }
            else
            {
                Debug.WriteLine("Not the Same");
            }
            return false;
        }

        private void CropBtn_Click(object sender, EventArgs e)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var combinePath = Path.Combine(outPutDirectory, "images\\screenshot.png");
            string screenShot = new Uri(combinePath).LocalPath;
            captureScreenshot(screenShot);
            //Get from sources
            Bitmap image = AForge.Imaging.Image.FromFile(@"images\screenshot.png");
            //Change coordinates of the rectangle here            x   y  width height
            Crop filter = new Crop(new System.Drawing.Rectangle(760, 600, 650, 130));
            // apply the filter
            Bitmap newImage = filter.Apply(image);
            //Save the image
            newImage.Save(@"images\croppedImage.png");
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            runAdb();
        }

        //click retry
        //click retry confirm
        //click start battle
        //click menu
        //click yeild
        //click exit
        //click defeat confirm

        //kiểm tra flag
        //chạy theo thứ tự retry 3 lần
        //nếu khớp thì chạy bth
        //nếu ko khớp thì retry

        //từ array screenshot
        //so sánh 2 phần ảnh screenshot
        //  giống: click điểm - đi tới so sánh tiếp theo
        //  khác: lùi lại, so sánh lại cặp trước đó.


        private void clickEvent()
        {
            String s1, s2, s3, s4, s5, s6;
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            var combinePath = Path.Combine(outPutDirectory, "images\\retry_confirm_740_650_670_140.png");
            s1 = new Uri(combinePath).LocalPath;

            combinePath = Path.Combine(outPutDirectory, "images\\start_1550_960_120_60.png");
            s2 = new Uri(combinePath).LocalPath;

            combinePath = Path.Combine(outPutDirectory, "images\\fighting_2015_110_115_110.png");
            s3 = new Uri(combinePath).LocalPath;

            combinePath = Path.Combine(outPutDirectory, "images\\yield_860_355_440_115.png");
            s4 = new Uri(combinePath).LocalPath;

            combinePath = Path.Combine(outPutDirectory, "images\\exit_760_600_650_130.png");
            s5 = new Uri(combinePath).LocalPath;

            combinePath = Path.Combine(outPutDirectory, "images\\defeat_confirm_800_260_560_140.png");
            s6 = new Uri(combinePath).LocalPath;

            String[] sources = { s1, s2, s3, s4, s5, s6 };

            //the screenshot will be cut out using these rectangles and compare it to the source images above, to reduce the time of comparison process
            int[,] recs = new int[,] { { 740, 650, 670, 140 }, { 1550, 960, 120, 60 }, { 2015, 110, 115, 110 }, { 860, 355, 440, 115 }, { 760, 600, 650, 130 }, { 800, 260, 560, 140 } };

            //coordinates to click correspond to each stages, they have ranges to avoid being recognized as illegal action
            int[,] clicks = new int[,] { { 1155, 1320, 700, 740 }, { 1550, 1650, 970, 1010 }, { 2100, 2110, 35, 50 }, { 970, 1200, 400, 430 }, { 1165, 1310, 640, 680 }, { 1915, 2050, 970, 1010 } };

            int[] sleepTime = { 7000, 1000, 1000, 1000 ,1000};

            combinePath = Path.Combine(outPutDirectory, "images\\screenshot.png");
            string screenShot = new Uri(combinePath).LocalPath;
            File.Delete(screenShot);
            captureScreenshot(screenShot);
            Debug.WriteLine("Captured");
            Thread.Sleep(1000);



            for(int i =0;i<3; i++)
            {
                int x = findRetry(screenShot);
                clickXY(1600, 1675, x, x + 50); //Click Retry Button
                Thread.Sleep(1000);
                captureScreenshot(screenShot);
                Thread.Sleep(1000);
                Bitmap croppedSource = new Bitmap(sources[0]);
                using (Bitmap template = new Bitmap(screenShot))
                {
                    if (compareImage(croppedSource, template, recs[0, 0], recs[0, 1], recs[0, 2], recs[0, 3]))
                    {
                        Thread.Sleep(1000);
                        clickXY(clicks[0, 0], clicks[0, 1], clicks[0, 2], clicks[0, 3]); //Click confirm button
                        Thread.Sleep(3000);
                        break;
                    }
                    else
                    {
                        Debug.WriteLine("Captured");
                        clickXY(55, 70, 45, 60); // Click back to find again
                        Thread.Sleep(1000);
                    }
                }

            }

            int count = 3; // variable that store retry times

            //Use i=2 because element [0] of the array has been used in the code above, 
            // and it performs click and then conducts the comparison, so the element [1+1] will be used.

            for (int i = 2; i <= 6; i++)
            {
                while (count != 0)
                {
                    Debug.WriteLine("Step" + i);
                    if ( i>=2 && i<=6)
                    {
                        clickXY(clicks[i-1, 0], clicks[i-1, 1], clicks[i-1, 2], clicks[i-1, 3]); //Click button
                    }
                    Thread.Sleep(sleepTime[i - 2]);
                    captureScreenshot(screenShot);
                    Debug.WriteLine("i="+i);
                    Thread.Sleep(1000);
                    if (i == 6) break;
                    Bitmap croppedSource = new Bitmap(sources[i]);
                    Debug.WriteLine(sources[i].Substring(63, 10));
                    using (Bitmap template = new Bitmap(screenShot))
                    {
                        if (!compareImage(croppedSource, template, recs[i, 0], recs[i, 1], recs[i, 2], recs[i, 3]))
                        {
                            //Thread.Sleep(1000);
                            //clickXY(clicks[i, 0], clicks[i, 1], clicks[i, 2], clicks[i, 3]); //Click button
                            //Thread.Sleep(2000);
                            //break;
                            if (i > 2 && i < 5)
                            {
                                i--;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    count--;
                }
                count = 3;
            }
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var combinePath = Path.Combine(outPutDirectory, "images\\screenshot.png");
            string screenShot = new Uri(combinePath).LocalPath;
            captureScreenshot(screenShot);
            int flag = 0;
            try
            {
                flag = int.Parse(flagInput.Text);
                if (flag <=0) { throw new FormatException(); }
            }
            catch (FormatException)
            {
                string message = "Flag's number must be a positive number!";
                string title = "Error";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            while (flag != 0)
            {
                countLabel.Text = flag.ToString() + " time(s) left";
                clickEvent();
                flag--;
            }
            string message1 = "Running complete!";
            string title1 = "Notification";
            MessageBox.Show(message1, title1);
        }
    }
}
