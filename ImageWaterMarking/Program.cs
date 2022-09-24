using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;



namespace ImagewaterMarking
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainDirectory = "I:\\My Drive\\SVS Sarees";
            char foldersStartsWith = 'K';
            char foldersStartsWith1 = 'H';


            string encodedFolderName = "I:\\My Drive\\SVS Sarees\\";

            string fileExtenstion = ".jpg";

            Font font = new("Arial", 80, FontStyle.Italic, GraphicsUnit.Pixel);

            Color color = Color.FromArgb(255, 0, 0, 0);

            SolidBrush brush = new(color);

            StringFormat sf = new();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            var subdirs1 = Directory.GetDirectories(mainDirectory)
                            .Select(p => new
                            {
                                Path = p,
                                Name = Path.GetFileName(p)
                            }).Where(p => p.Name.StartsWith(foldersStartsWith) || p.Name.StartsWith(foldersStartsWith1))
                            .ToArray();
            int i = 0;
            foreach (var a in subdirs1)
            {
                i++;
                Console.WriteLine("Transfer strated for folder " + i + "of" + subdirs1.Length);
                try
                {
                    string directoryName = a.Name;

                    string[] splitName = directoryName.Split('-');

                    string billcode = splitName[0];

                    string itemNo = splitName[1];

                    string rate = splitName[2];

                    string encodedprice = GetEncodedRate(rate);

                    var files = Directory.GetFiles(a.Path).Select(Path.GetFileNameWithoutExtension);

                    foreach (string file in files)
                    {
                        if (file != "desktop")
                        {
                            Image bitmap = Bitmap.FromFile(a.Path + "\\" + file + ".jpg");
                            Point atpoint = new Point(bitmap.Width - (bitmap.Width / 7), bitmap.Height - (bitmap.Height - 100));
                            Graphics graphics = Graphics.FromImage(bitmap);

                            string formatedWaterMark = encodedprice + "-" + billcode + "-" + itemNo + "-" + file;
                            graphics.DrawString(formatedWaterMark, font, brush, atpoint, sf);
                            graphics.Dispose();

                            using (MemoryStream m = new())
                            {
                                bitmap.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                                byte[] convertedToBytes = m.ToArray();
                                string saveto = encodedFolderName + GetDestinationFolderName(rate) + "\\" + formatedWaterMark + fileExtenstion;
                                File.WriteAllBytes(saveto, convertedToBytes);
                            }
                        }

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Transfer failed for folder" + i + "of" + subdirs1.Length + "Failed folder name = " + a.Name);
                }
                
                Console.WriteLine("Transfer completed for folder" + i + "of" + subdirs1.Length);
            }
        }
        
        
        public static string GetDestinationFolderName(string price)
        {
            if(Convert.ToInt32(price) < 1000)
            {
                return "0-999";
            }

            if (Convert.ToInt32(price) < 2000)
            {
                return "1000-1999";
            }

            if (Convert.ToInt32(price) < 3000)
            {
                return "2000-2999";
            }

            if (Convert.ToInt32(price) < 4000)
            {
                return "3000-3999";
            }

            if (Convert.ToInt32(price) < 5000)
            {
                return "4000-4999";
            }

            if (Convert.ToInt32(price) < 6000)
            {
                return "5000-5999";
            }

            if (Convert.ToInt32(price) < 7000)
            {
                return "6000-6999";
            }

            if (Convert.ToInt32(price) < 8000)
            {
                return "7000-7999";
            }

            if (Convert.ToInt32(price) < 10000)
            {
                return "8000-9999";
            }

            if (Convert.ToInt32(price) < 10000)
            {
                return "8000-9999";
            }

            if (Convert.ToInt32(price) < 15000)
            {
                return "10000-14999";
            }

            if (Convert.ToInt32(price) < 20000)
            {
                return "15000-19999";
            }

            return string.Empty;
        }

        public static string GetEncodedRate(string price)
        {
            StringBuilder result = new();
            foreach (char a in price)
            {
                switch (a)
                {
                    case '1':
                        result.Append('S');
                        break;
                    case '2':
                        result.Append('U');
                        break;
                    case '3':
                        result.Append('P');
                        break;
                    case '4':
                        result.Append('E');
                        break;
                    case '5':
                        result.Append('R');
                        break;
                    case '6':
                        result.Append('G');
                        break;
                    case '7':
                        result.Append('O');
                        break;
                    case '8':
                        result.Append('L');
                        break;
                    case '9':
                        result.Append('D');
                        break;
                    case '0':
                        result.Append('Z');
                        break;
                    default:
                        break;
                }
            }
            return result.ToString();
        }
    }
}                
            

      




