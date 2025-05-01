using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CarmenCIT_Extractor
{
    public partial class Form1 : Form
    {

        private Dictionary<byte, List<string>> textBlocks = new Dictionary<byte, List<string>>();
        private Dictionary<byte, List<ushort>> numericBlocks = new Dictionary<byte, List<ushort>>(); // 7B gibi durumlar için
        private byte[] fileBytes; // Dosyanın tamamını içeren byte dizisi (varsayım)
        private Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1", new EncoderReplacementFallback("?"), new DecoderExceptionFallback());


        byte[] pattern = new byte[] { 0x03, 0x90 };//, 0x3f, 0xff, 0xf0, 0xe2, 0x00, 0x01, 0x0f, 0xff  };
        byte[] globalarray;
        byte[] carmenImage;
        Color[] palette= new Color[] 
            {
                Color.Black,
                Color.FromArgb(128, 0, 0),       // Dark Red
                Color.FromArgb(0, 128, 0),       // Dark Green
                Color.FromArgb(128, 128, 0),     // Dark Yellow
                Color.Blue,
                Color.FromArgb(128, 0, 128),     // Dark Magenta
                Color.FromArgb(0, 128, 128),     // Dark Cyan
                Color.FromArgb(192, 192, 192),   // Light Gray
                Color.FromArgb(128, 128, 128),   // Dark Gray
                Color.Red,
                Color.Lime,
                Color.Yellow,
                Color.FromArgb(0, 0, 128),       // Dark Blue
                Color.Magenta,
                Color.FromArgb(0, 128, 128),     // Dark Cyan
                Color.White
            };
        
        //{ Color.Black, Color.Magenta, Color.Cyan, Color.White, Color.};
        
        int offset = 0;
        int xw = 256; //used for mouse hower address calc
        int pixelsize = 6;

        //carmen city gfx location
        int gfxloc=0;
        int gfxlen=0;

        public Form1()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "ERROR");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string[] array2 = Directory.GetFiles(@"C:\", "*.BIN");
            DirectoryInfo dinfo = new DirectoryInfo(txtPath.Text);
            FileInfo[] Files = dinfo.GetFiles("*.*");
            foreach (FileInfo file in Files)
            {
                listBox1.Items.Add(file.Name);
            }

        }



        private byte[] readall(string filename)
        {
            byte[] array = File.ReadAllBytes(txtPath.Text+filename);
            //Console.WriteLine("First byte: {0}", array[0]);
            //Console.WriteLine("Last byte: {0}",
            //    array[array.Length - 1]);
            //Console.WriteLine(array.Length);
            return array;
        }


        void plotywise(byte[] array)
        {
            
            int aspect = 1;
            if (chkAspect.Checked) aspect = 2;
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int end = array.Length;
            Bitmap img=new Bitmap(pictureBox1.Width,pictureBox1.Height);
            FastPixel fp = new FastPixel(img);
            fp.Lock();
            bool reading=true;
            int counter = 0;
            int x = 0;
            int y = 0;

            while (reading)
            {
                for (int n=0;n<8;n++)
                    if (IsBitSet(array[counter], n))
                    {
                        fp.SetPixel(x , y, Color.Black);
                         if (aspect == 2) fp.SetPixel(x+1, y, Color.Black);
                        
                        y++;
                    }
                    else
                    {
                        fp.SetPixel(x, y, Color.White);
                        if (aspect == 2) fp.SetPixel(x + 1, y, Color.White);
                        y++;

                    }
                y -= 8;
                x += aspect;
                if (x > w - 8) { y+=8; x = 0; }
                if (y >= h-8) reading = false;
                counter++;
                if (counter>=end-8) reading=false;
            }
            fp.Unlock(true);
            pictureBox1.Image = img;
            pictureBox1.Refresh();

        }

        void plotxwise(byte[] array)
        {
            int bitDepth = Convert.ToInt32( txtBitDepth.Text);
            int aspect=1;
            if (chkAspect.Checked) aspect = 2;
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int end = array.Length;
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            FastPixel fp = new FastPixel(img);
            fp.Lock();
            bool reading = true;
            int counter = offset;
            int x = 0;
            int y = 0;
            
            while (reading)
            {
                for (int n = 8-bitDepth ; n >= 0; n-=bitDepth)
                {
                    int bits = GetBitValue(array[counter], n, bitDepth);
                    fp.SetPixel(x, y, palette[bits]);
                    if (aspect == 2) fp.SetPixel(x + 1, y, palette[bits]);
                    x += aspect;

                    /*
                    if (IsBitSet(array[counter], n))
                    {
                        fp.SetPixel(x, y, Color.Black);
                        if (aspect == 2) fp.SetPixel(x+1, y, Color.Black);
                        x += aspect;
                    }
                    else
                    {
                        fp.SetPixel(x, y, Color.White);
                        if (aspect == 2) fp.SetPixel(x + 1, y, Color.White);
                        x += aspect;

                    }
                    */
                }
                if (x > w - 8) { y += 1; x = 0; }
                if (y >= h - 8) reading = false;
                counter++;
                if (counter >= end - 8) reading = false;
            }
            fp.Unlock(true);
            pictureBox1.Image = img;
            pictureBox1.Refresh();

        }



        void plotsinclair(byte[] array)
        {
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int end = array.Length;
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            FastPixel fp = new FastPixel(img);
            fp.Lock();
            bool reading = true;
            int counter = 0;
            int x = 0;
            int y = 0;
            
            
            counter = offset;
            List<int> numbers = new List<int>();
            int filter = 224; //masks first 3 bits 11100000
            filter = Convert.ToInt32( txtBitfilter.Text);
            while (reading)
            {
                for (int n = 0; n < pixelsize; n++)
                {
                    
                    
                    if (!numbers.Contains((array[counter] & filter)))
                    {
                        numbers.Add((array[counter] & filter));
                    }
                    
                    
                    int mode=1;
                    if (mode == 1)
                    {
                        Color gray;
                        //gray = Color.FromArgb(array[counter] & 192, array[counter] & 224, (array[counter] & filter) * (int)(255 / filter));
                        //if (!cbit7.Checked) 
                        gray = Color.FromArgb((array[counter] & filter) * (int)(255 / filter), 0, 0);
                        
                        //Color gray = Color.FromArgb(232 - (array[counter] & filter), 232 - (array[counter] & filter), 232 - (array[counter] & filter));
                        if (array[counter] == 0) gray = Color.White;
                        for (int b = 0; b < pixelsize; b++)
                        {
                            if (n != 7) fp.SetPixel(x, y + b, gray); else fp.SetPixel(x, y + b, Color.Black);
                        }
                        x++;
                    }
                    else
                    {
                    
                        if (IsBitSet(array[counter], n))
                        {
                            for (int b = 0; b < 7; b++)
                            {
                                if (n != 7) fp.SetPixel(x, y + b, Color.Black); else fp.SetPixel(x, y + b, Color.Blue); 
                            }
                            x++;
                        }
                        else
                        {
                            for (int b = 0; b < 7; b++) if (n != 7) fp.SetPixel(x, y + b, Color.White); else fp.SetPixel(x, y + b, Color.Blue); 
                            x++;

                        }
                    }
                }
                if (x > w - 8) { xw = x;  y += pixelsize; x = 0; }
                if (y >= h - 8) reading = false;
                counter++;
                if (counter >= end - 8) reading = false;
            }
            fp.Unlock(true);
            pictureBox1.Image = img;
            pictureBox1.Refresh();
            label1.Text = counter.ToString();
            foreach (int item in numbers)
            {
                txtDesc.AppendText(item + Environment.NewLine);
            }
            txtDesc.AppendText("Last Read Addr:"+counter+ Environment.NewLine);
        }


        void plot7bit(byte[] array)
        {
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int end = array.Length;
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            FastPixel fp = new FastPixel(img);
            fp.Lock();
            bool reading = true;
            int counter = 0;
            int x = 0;
            int y = 0;

            while (reading)
            {
                for (int n = 7; n > 0; n --)
                    if (IsBitSet(array[counter], n))
                    {
                        fp.SetPixel(x, y, Color.Black);
                        x++;
                    }
                    else
                    {
                        fp.SetPixel(x, y, Color.White);
                        x++;

                    }


                if (x > w - 8) { y += 1; x = 0; }
                if (y >= h - 8) reading = false;
                counter++;
                if (counter >= end - 8) reading = false;
            }
            fp.Unlock(true);
            pictureBox1.Image = img;
            pictureBox1.Refresh();

        }

        public bool IsBitSet(byte b, int bitIndex)
        {
            return (b & (1 << bitIndex)) != 0;
        }


        public int GetBitValue(byte data, int startBit, int length)
        {
            if (startBit < 0 || startBit > 7 || length < 1 || length > 8 || startBit + length > 8)
            {
                throw new ArgumentOutOfRangeException("Invalid startBit or length.");
            }

            int mask = (1 << length) - 1; // Create a bitmask with 'length' bits set to 1.
            int shiftedData = (data >> startBit) & mask; // Shift the data and apply the mask.

            return shiftedData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            globalarray = readall(listBox1.GetItemText(listBox1.SelectedItem));
            txtDesc.Text = "";
            label2.Text = "Loaded.";
            readCIT();
        }

        private int getInt16(int index)
        {

            if (index >= 0 && index + 1 < globalarray.Length)
            {
                // Combine two bytes into a 16-bit integer
                UInt16 value = (UInt16)((globalarray[index+1] << 8) | globalarray[index ]);

               return value;
            }
            else
            {
               return -1;
            }
        }


        private void ParseStringBlocks(int startOffset) // Checkbox durumunu parametre olarak al
        {
            bool skipShortHexIfChecked = true;
            textBlocks.Clear();
            numericBlocks.Clear();
            int currentOffset = startOffset;
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1", new EncoderReplacementFallback("?"), new DecoderExceptionFallback());

            txtDesc.AppendText($"\r\n--- Parsing String Blocks starting at offset {startOffset} ---\r\n");

            while (currentOffset + 3 <= globalarray.Length)
            {
                byte marker = globalarray[currentOffset];
                ushort count = (ushort)getInt16(currentOffset + 1);
                currentOffset += 3;

                if (marker == 0x7B) // Sayısal bloklar her zaman işlenir
                {
                    if (!numericBlocks.ContainsKey(marker))
                        numericBlocks[marker] = new List<ushort>();

                    char markerCharNum = (marker >= 32 && marker <= 126) || (marker >= 160) ? (char)marker : '?';
                    txtDesc.AppendText($"Found Numeric Marker: 0x{marker:X2} ('{markerCharNum}'), Count: {count}\r\n");

                    for (int i = 0; i < count; i++)
                    {
                        if (currentOffset + 2 > globalarray.Length)
                        {
                            txtDesc.AppendText($"  WARN: Unexpected end of file reading numeric data {i + 1}/{count} for marker 0x{marker:X2}\r\n");
                            currentOffset = globalarray.Length;
                            break;
                        }
                        if (currentOffset >= globalarray.Length) break;

                        ushort value = (ushort)getInt16(currentOffset);
                        numericBlocks[marker].Add(value);
                        txtDesc.AppendText($"  - Value[{i}]: {value}\r\n");
                        currentOffset += 2;
                    }
                }
                else // Metin blokları
                {
                    if (!textBlocks.ContainsKey(marker))
                        textBlocks[marker] = new List<string>();

                    char markerCharText = (marker >= 32 && marker <= 126) || (marker >= 160) ? (char)marker : '?';
                    txtDesc.AppendText($"Found Text Marker: 0x{marker:X2} ('{markerCharText}'), Count: {count}\r\n");

                    if (count > 1000 && globalarray.Length > 0 && currentOffset < globalarray.Length) // Daha güvenli kontrol
                    {
                        txtDesc.AppendText($"  WARN: Unusually large count ({count}) for marker 0x{marker:X2}, parsing might be misaligned.\r\n");
                        // İsteğe bağlı: Burada ayrıştırmayı durdurabilir veya devam edebilirsiniz
                        // currentOffset = globalarray.Length;
                        // break;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        if (currentOffset >= globalarray.Length)
                        {
                            txtDesc.AppendText($"  WARN: Unexpected end of file before reading string {i + 1}/{count} for marker 0x{marker:X2}\r\n");
                            break;
                        }

                        int stringStart = currentOffset;
                        int stringEnd = Array.IndexOf(globalarray, (byte)0x00, stringStart);

                        if (stringEnd == -1)
                        {
                            txtDesc.AppendText($"  WARN: NULL terminator not found for string {i + 1}/{count} for marker 0x{marker:X2} starting at {stringStart}\r\n");
                            currentOffset = globalarray.Length;
                            break;
                        }

                        int len = stringEnd - stringStart;

                        // *** Checkbox Kontrolü ve Kısa String Atlama ***
                        if (len <= 2 && skipShortHexIfChecked) // Eğer string kısaysa VE checkbox işaretliyse
                        {
                            currentOffset = stringEnd + 1; // Offset'i ilerlet ama listeleme/ekleme yapma
                            continue; // Bu string'i atla, döngünün sonraki adımına geç
                        }
                        // *** Kontrol Sonu ***


                        string valueToAddToList;
                        string displayLine;

                        if (len <= 2) // String kısaysa (ve checkbox işaretli değilse buraya gelinir)
                        {
                            StringBuilder hexOutput = new StringBuilder();
                            if (len == 0)
                            {
                                hexOutput.Append("[empty]");
                                valueToAddToList = "";
                            }
                            else
                            {
                                for (int k = 0; k < len; k++)
                                {
                                    hexOutput.Append($"[{globalarray[stringStart + k]:X2}]");
                                }
                                // Değeri hex olarak listeye ekle
                                valueToAddToList = hexOutput.ToString();
                            }
                            displayLine = $"  - String[{i}] (Hex): {hexOutput.ToString()}\r\n";
                        }
                        else // String yeterince uzunsa
                        {
                            string decodedString = iso88591.GetString(globalarray, stringStart, len);
                            // Değeri çözülmüş string olarak listeye ekle
                            valueToAddToList = decodedString;
                            displayLine = $"  - String[{i}]: {decodedString}\r\n";
                        }

                        // Listeye ekle (marker daha önce eklenmişti)
                        if (textBlocks.ContainsKey(marker))
                        {
                            textBlocks[marker].Add(valueToAddToList);
                        }

                        // Textbox'a yazdır
                        txtDesc.AppendText(displayLine);

                        currentOffset = stringEnd + 1; // Offset'i ilerlet

                        if (currentOffset >= globalarray.Length && i < count - 1)
                        {
                            txtDesc.AppendText($"  WARN: Reached end of file after reading string {i + 1}, expected {count} strings for marker 0x{marker:X2}.\r\n");
                            break;
                        }
                    }
                }
                if (currentOffset >= globalarray.Length) break;
            }
            txtDesc.AppendText($"--- Finished parsing blocks at offset {currentOffset} ---\r\n");
        }


        private void readCIT()
        {
            int dataindex = getInt16(0);
            int datalen = getInt16(4);

            txtDesc.AppendText("Header Loc:" + dataindex + Environment.NewLine);
            txtDesc.AppendText("Header Len:" + datalen + Environment.NewLine);
            byte[] header = cutArray(dataindex, datalen);
            if ((dataindex <= 0)||(header.Length<2))
            {
                txtDesc.AppendText("INVALID CIT.");
            }
            else
            {
                gfxloc = getInt16(dataindex + 14);
                gfxlen = getInt16(dataindex + 18);
                txtDesc.AppendText("Image Loc:" + gfxloc + Environment.NewLine);
                txtDesc.AppendText("Image Len:" + gfxlen + Environment.NewLine);
                carmenImage = cutArray(gfxloc, gfxlen);
                if (carmenImage.Length < 2)
                {
                    //no image
                    txtDesc.AppendText("NO IMAGE.\r\n");
                }
                else
                {
                    //
                    txtDesc.AppendText("Carmen City Image Found.\r\n");
                    chkCIMG.Checked = true;
                    int descLoc = getInt16(dataindex + 22);
                    int descLen = getInt16(dataindex + 26);
                    byte[] desc = cutArray(descLoc+2, descLen);
                   
                    string descString = Encoding.GetEncoding("ISO-8859-1", new EncoderReplacementFallback("?"), new DecoderExceptionFallback()).GetString(desc);
                    txtDesc.AppendText(descString + Environment.NewLine);

                    //more info
                    int stringDataStartOffset = descLoc + 2 + descLen; // Açıklama verisinin bittiği yer

                    //ParseStringBlocks(stringDataStartOffset);

                    retrieveTable(globalarray, listBox2);

                    //plotxwise(carmenImage);
                }
            }
        }


        byte[] cutArray(int startIndex, int length)
        {
            //dosyadan belirli uzunlukta data keser

            if (startIndex >= 0 && startIndex < globalarray.Length && length >= 0 && startIndex + length <= globalarray.Length)
            {
                byte[] newArray = new byte[length];
                Array.Copy(globalarray, startIndex, newArray, 0, length);

                return newArray;
            }
            else
            {
                byte[] newArray = { 0 };
                return newArray;

            }
        }


        private void findIndex(byte[] array)
        {
            int end = array.Length;
            int loc = findLoc(array, 0, 0); //find(pattern, array);
            int loce = 0;
            bool reading=true;
            int entry = 1;
            while (reading)
            {

                if (loc >= 0)
                {
                    /*
                    //found pattern;
                     loce = findLoc(array, loc + 2, 0);  // find(new byte[] { 0x00 }, array);
                    if (loce > 0)
                    {
                        string s = BitConverter.ToString(array, loc + 1, loce - (loc + 1)).Replace("-", "");
                        txtDesc.AppendText(loc.ToString() + ":" + s + "\r\n");
                        Application.DoEvents();
                        loc = loce + 1;
                    }
                    */
                    if (array[loc + 3] == 0)
                    {
                        string s = BitConverter.ToString(array, loc+1 ,2).Replace("-", "");
                        txtDesc.AppendText(entry.ToString()+". "+loc.ToString("00000") + ":" + s + "\r\n");
                        Application.DoEvents();
                        loc += 2;
                        entry++;
                    }
                    loc ++;
                    loc = findLoc(array, loc, 0);
                }
                else
                {
                    reading = false;
                    //txtDesc.Text = "Notfound";
                }
                if ((loce < 0)||(loc > end - 3)) reading = false;
            }

        }

        int findLoc(byte[] array, int start, byte needle)
        {
            int end = array.Length;
            int counter=start;
            bool reading=true;
            while (reading)
            {
                if (counter >= end - 1) { counter = 0; break;  }
                if (array[counter] == needle) reading = false;
                counter++;
                
            }
            return counter - 1;
        }


        private int find(byte[] pattern, byte[] bigarray)
        {
            int patternindex=0;
            bool found = false;
            for (int x = 0; x < bigarray.Length; x++)
            {
                if (bigarray[x] == pattern[patternindex])
                {
                    found = true;
                    for (int y = 1; y < pattern.Length; y++)
                    {
                        if (bigarray[x + y] != pattern[y])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found) return x;
                }
            }
            return (-1);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            updateDraw();

        }
        private void updateDraw()
        {
            pictureBox1.Width = trackBar1.Value;
            if (listBox1.SelectedIndex < 0) return;
            byte[] temparray;
            if (chkCIMG.Checked)
                temparray = carmenImage;
            else
                temparray = globalarray;

            if (r7bit.Checked) plot7bit(temparray);
            if (rXwise.Checked) plotxwise(temparray);
            if (rYwise.Checked) plotywise(temparray);
            if (rCyclone.Checked) plotsinclair(temparray);
            
            button2.Text = trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 128*pixelsize+4;
            pictureBox1.Width = trackBar1.Value;
            updateDraw();

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            txtDesc.Text = "";
            byte[] buffer = new byte[] {  0x55, 0x7d, 0x50, 0x50, 0x50, 0x55, 0x78, 0x05, 0x78, 0x55, 0x7d, 0x50, 0x55, 0x00, 0x7d, 0x7d };
            byte[] array = readall(listBox1.GetItemText(listBox1.SelectedItem));
            int found=find(buffer, array);
            if (found >= 0)
            {
                txtDesc.AppendText(found.ToString());

            }
            else
            {
                txtDesc.AppendText("Not found");

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int adr=getadr(e.X, e.Y,pixelsize);
            try
            {
                string binaryString = Convert.ToString(globalarray[adr], 2).PadLeft(8, '0');
                int first3Bits = (globalarray[adr] & 224) ; 
                int last4Bits = globalarray[adr] & 15;

                int groupSize = 2; // Number of bits in each group
                string separatedBinaryString = "";
                string separatedDecimalString = "";
                for (int i = 0; i < binaryString.Length; i += groupSize)
                {
                    int endIndex = Math.Min(i + groupSize, binaryString.Length);
                    string group = binaryString.Substring(i, endIndex - i);

                    if (i > 0)
                    {
                        separatedDecimalString += ",";
                        separatedBinaryString += "-";
                    }

                    int decimalValue = Convert.ToInt32(group, 2);
                    separatedDecimalString += decimalValue.ToString();
                    separatedBinaryString += group;
                }

                if (adr < 65536)
                {
                    label1.Text = e.X.ToString()+","+e.Y.ToString()+" > "+ adr.ToString() + "," + globalarray[adr] + " xw:"+xw.ToString();
                    label2.Text = " B " + separatedBinaryString + " DEC:" +separatedDecimalString+":"+ globalarray[adr] + " 3HiBit:" + first3Bits + " 4LoBit " + last4Bits;
                }
            }
            catch
            {
            }
        }

       


        int getadr(int x, int y, int pxsize)
        {
            int w = xw; //used only for cyclone
            if (rCyclone.Checked == false) w = pictureBox1.Width;
            int h = pictureBox1.Height;

            int wt = ((int)(w / pxsize)) * pxsize;
            if (x > wt) return 0;
            int ht = ((int)(h / pxsize)) * pxsize;
            if (y > ht) return 0;

            int a = ((int)(y - 1) / pxsize) * (int)(wt / pxsize) + (int)(x / pxsize);
            a += offset;


            return a;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            offset = Convert.ToInt32( txtOffset.Text);
            offset += 128;
            txtOffset.Text = offset.ToString();
            updateDraw();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            offset = Convert.ToInt32(txtOffset.Text);
            offset -= 128;
            txtOffset.Text = offset.ToString();
            updateDraw();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Check if the PictureBox contains an image
            if (pictureBox1.Image != null)
            {
                // Create a SaveFileDialog to allow the user to choose the save location
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Image As PNG";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the file path from the SaveFileDialog
                        string filePath = saveFileDialog.FileName;

                        // Save the image as a PNG file
                        pictureBox1.Image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                        label2.Text="Image saved as PNG.";
                    }
                }
            }
            else
            {
                label2.Text="No image to save.";
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            txtBitfilter.Text = trackBar2.Value.ToString();
            trackBar1.Value = 128 * pixelsize + 4;
            pictureBox1.Width = trackBar1.Value;
            updateDraw();
        }

        private void cbit0_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        int calcbits()
        {
            int val = (cbit0.Checked ? 128 : 0) + (cbit1.Checked ? 64 : 0) + (cbit2.Checked ? 32 : 0) + (cbit3.Checked ? 16 : 0) + (cbit4.Checked ? 8 : 0) + (cbit5.Checked ? 4 : 0) + (cbit6.Checked ? 2 : 0) + (cbit7.Checked ? 1 : 0);
            if (val==0) val=1;
            txtBitfilter.Text = val.ToString();
            trackBar1.Value = 128 * pixelsize + 4;
            pictureBox1.Width = trackBar1.Value;
            updateDraw();
            return val;
        }

        private void cbit1_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit2_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit3_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit4_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit5_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit6_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void cbit7_CheckedChanged(object sender, EventArgs e)
        {
            txtBitfilter.Text = calcbits().ToString();
        }

        private void txtOffset_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary Files (*.bin)|*.bin";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Save the byte array to the selected file
                File.WriteAllBytes(filePath, carmenImage);

                label2.Text= ("File saved successfully.");
            }
            else
            {
                label2.Text = ("File save operation canceled.");
            }
        }


        private void retrieveTable(byte[] filearray, ListBox listBox2)
        {
            // ID → Açıklama sözlüğü
            var idDescriptions = new Dictionary<ushort, string>
            {
                { 1,   "Şehir Adı " },
                { 2,   "İmaj Datası " },
                { 3,   "Açıklama Listesi " },
                { 4,   "Eşya Listesi " },
                { 100, "Bilinmeyen 100" },
                { 101, "Bilinmeyen 101" },
                // … diğerleri …
                { 112, "Bilinmeyen İpucu 6" }
            };

            listBox2.Items.Clear();

            // Temel uzunluk kontrolü
            if (filearray == null || filearray.Length < 4)
            {
                listBox2.Items.Add("Hata: Dosya verisi geçersiz veya çok kısa.");
                return;
            }

            // getInt16 uygulaması; istersen BitConverter.ToUInt16 da kullanabilirsin
            ushort tableOffset = BitConverter.ToUInt16(filearray, 0);
            ushort tableLength = BitConverter.ToUInt16(filearray, 4);

            // Offset/length geçerlilik kontrolü
            if (tableOffset == 0
                || tableLength < 8
                || tableOffset + tableLength > filearray.Length)
            {
                listBox2.Items.Add($"Hata: Geçersiz tablo offset ({tableOffset}) veya uzunluk ({tableLength}).");
                return;
            }

            listBox2.Items.Add($"Tablo Başlangıcı: 0x{tableOffset:X4}, Uzunluk: {tableLength} byte");
            listBox2.Items.Add("--- Tablo Girdileri ---");

            int tableEntryStart = tableOffset + 4;
            int tableEnd = tableOffset + tableLength;

            int currentEntryOffset = tableEntryStart;
            // 8 byte’lık kayıtlar
            for (; currentEntryOffset + 8 <= tableEnd; currentEntryOffset += 8)
            {
                ushort id = BitConverter.ToUInt16(filearray, currentEntryOffset);
                ushort dataOffset = BitConverter.ToUInt16(filearray, currentEntryOffset + 2);
                //ushort unknownVal   = BitConverter.ToUInt16(filearray, currentEntryOffset + 4);
                ushort dataLength = BitConverter.ToUInt16(filearray, currentEntryOffset + 6);

                string desc = idDescriptions.TryGetValue(id, out var d) ? d : "Bilinmiyor";
                var line = $"{id} ({desc}) : 0x{dataOffset:X4} [{dataLength} byte]";

                // Kısa blokları string listesi say
                if (dataLength > 0 && dataLength < 512 && id != 2
                    && dataOffset + 2 <= filearray.Length)
                {
                    ushort count = BitConverter.ToUInt16(filearray, dataOffset);
                    line += $" → {count} string";
                }

                listBox2.Items.Add(line);
            }

            // Eksik kalmışsa uyar
            if (currentEntryOffset < tableEnd)
            {
                int leftover = tableEnd - currentEntryOffset;
                listBox2.Items.Add($"Uyarı: Tablo sonunda {leftover} byte tam girdi oluşturmadı.");
            }

            listBox2.Items.Add("--- Tablo Sonu ---");
        }



    private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Seçili bir öğe yoksa veya seçili öğe geçerli bir string değilse çık
        if (listBox2.SelectedItem == null || !(listBox2.SelectedItem is string selectedLine))
        {
            // txtDesc.Clear(); // İsteğe bağlı: seçim yoksa temizle
            return;
        }

        // Satırın veri girdisi formatında olup olmadığını kontrol et (": 0x" içeriyor mu?)
        int offsetMarkerPos = selectedLine.IndexOf(": 0x");
        if (offsetMarkerPos < 0)
        {
            // Bu bir başlık/sonuç satırı, veri içermiyor
            // txtDesc.Clear(); // İsteğe bağlı: başlık seçiliyse temizle
            return;
        }

        txtDesc.Clear(); // Yeni veri gösterileceği için önceki içeriği temizle
        var output = new StringBuilder();

        // Offset'i ayrıştır
        ushort dataOffset = 0;
        int offsetStart = offsetMarkerPos + 4; // ": 0x" den sonraki kısım
        int offsetEnd = selectedLine.IndexOf(" [", offsetStart); // Offset'in bittiği yer
        if (offsetEnd > offsetStart)
        {
            string offsetHex = selectedLine.Substring(offsetStart, offsetEnd - offsetStart);
            if (!ushort.TryParse(offsetHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dataOffset))
            {
                txtDesc.Text = $"Hata: Satırdan offset ayrıştırılamadı: '{offsetHex}'";
                return;
            }
        }
        else
        {
            txtDesc.Text = "Hata: Satır formatı bozuk (offset sonu bulunamadı).";
            return;
        }

            // Satırın string listesi içerip içermediğini kontrol et ("→" içeriyor mu?)
            bool isStringList =  selectedLine.Contains("→");

        // --- String Listesi İşleme ---
        if (isStringList)
        {
            // Offset'in ve count okuma için gereken 2 byte'ın geçerliliğini kontrol et
            if (dataOffset > 0 && dataOffset + 2 <= globalarray.Length)
            {
                ushort stringCount = BitConverter.ToUInt16(globalarray, dataOffset);
                output.AppendLine($"--- {stringCount} String (Başlangıç: 0x{dataOffset:X4}) ---");

                int currentStringPos = dataOffset + 2; // String verisinin başlangıcı

                for (int i = 0; i < stringCount; i++)
                {
                    // Güvenlik: Dosya sonunu aşmadığımızdan emin olalım
                    if (currentStringPos >= globalarray.Length)
                    {
                        output.AppendLine($"HATA: {i + 1}. string okunurken beklenmedik dosya sonu (Pos: {currentStringPos})");
                        break;
                    }

                    // Null sonlandırıcıyı bul
                    int nullPos = Array.IndexOf(globalarray, (byte)0x00, currentStringPos);

                    if (nullPos == -1) // NULL bulunamadıysa
                    {
                        output.AppendLine($"HATA: {i + 1}. string için NULL sonlandırıcı bulunamadı (Başlangıç: {currentStringPos})");
                        // Belki kalan byte'ları göstermek istersin? Veya burada dur.
                        currentStringPos = globalarray.Length; // Daha fazla okuma yapma
                        break;
                    }

                    // String'in uzunluğunu hesapla
                    int len = nullPos - currentStringPos;
                    string strValue;

                    if (len > 0) // Boş string değilse oku
                    {
                        // iso88591 kullanarak string'e çevir (global tanımlı varsayılıyor)
                        strValue = iso88591.GetString(globalarray, currentStringPos, len);
                    }
                    else // Boş string ise
                    {
                        strValue = "[BOŞ]";
                    }

                    output.AppendLine($"[{i}]: {strValue}");

                    // Sonraki string'in başlangıcına git (NULL'dan sonra)
                    currentStringPos = nullPos + 1;
                }
            }
            else
            {
                output.AppendLine($"Hata: String listesi için geçersiz offset (0x{dataOffset:X4}) veya dosya sonu aşıldı.");
            }
        }
        // --- Metin Dışı Veri İşleme ---
        else
        {
            // Verinin uzunluğunu ayrıştır (bilgi amaçlı)
            ushort dataLength = 0;
            int lengthStart = selectedLine.IndexOf("[") + 1;
            int lengthEnd = selectedLine.IndexOf(" byte]", lengthStart);
            if (lengthStart > 0 && lengthEnd > lengthStart)
            {
                string lengthStr = selectedLine.Substring(lengthStart, lengthEnd - lengthStart);
                ushort.TryParse(lengthStr, out dataLength); // Ayrıştırma başarısız olabilir ama sorun değil
            }

            output.AppendLine($"--- Metin Dışı Veri (ID: {selectedLine.Split(' ')[0]}) ---");
            output.AppendLine($"Başlangıç Offset: 0x{dataOffset:X4}");
            output.AppendLine($"Beklenen Uzunluk: {dataLength} byte");
            output.AppendLine();
            output.AppendLine("(Bu veri türü doğrudan metin olarak görüntülenemez.)");
            output.AppendLine("İmaj veya diğer ikili veri olabilir.");

                // İsteğe bağlı: Verinin ilk birkaç byte'ını hex olarak gösterelim
                int bytesToPreview = dataLength;// Math.Min(64, (int)dataLength); // En fazla 64 byte gösterelim
                byte[] imagedata = new byte[dataLength];
            if (dataOffset > 0 && dataOffset + bytesToPreview <= globalarray.Length && bytesToPreview > 0)
            {
                output.AppendLine();
                output.AppendLine("İlk Baytlar (Hex):");
                var hexLine = new StringBuilder();
                for (int k = 0; k < bytesToPreview; k++)
                {
                        imagedata[k] = globalarray[dataOffset + k];
                    hexLine.Append($"{globalarray[dataOffset + k]:X2} ");
                    if ((k + 1) % 16 == 0) // Her 16 byte'ta bir yeni satır
                    {
                        output.AppendLine(hexLine.ToString());
                        hexLine.Clear();
                    }
                }
                if (hexLine.Length > 0) // Kalan hex değerleri yazdır
                {
                    output.AppendLine(hexLine.ToString());
                }
                    showImage(imagedata);
            }
        }

        // Sonucu TextBox'a yaz
        txtDesc.Text = output.ToString();
    }



        public void showImage(byte[] data)
    {
        if (data == null || data.Length < 6) // Minimum başlık boyutu
        {
            if (pictureBox1.Image != null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
            return;
        }

        // 1) Başlık - Doğru değerleri kullan
        int height = BitConverter.ToUInt16(data, 0);         // A4 00 -> 164
                                                             // int widthBytes_ignored = BitConverter.ToUInt16(data, 2); // 44 00 -> 68 (Bunu kullanmıyoruz)
        int scanlineBytes = BitConverter.ToUInt16(data, 4); // 22 00 -> 34 (Doğru satır byte sayısı)
        int pixelWidth = scanlineBytes * 4;                   // 34 * 4 = 136 (Doğru piksel genişliği)

        // Başlangıç offset'ini dene (Önce 6'yı deneyelim)
        int dataStartOffset = 6;
        // int dataStartOffset = 16; // Alternatif: Eğer ilk 10 byte sabitse

        if (height <= 0 || scanlineBytes <= 0 || data.Length <= dataStartOffset)
        {
            if (pictureBox1.Image != null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
            return;
        }

        // 2) PackBits ile dekompresyon (List<byte> kullanarak dinamik boyut)
        List<byte> decompressedData = new List<byte>();
        int src = dataStartOffset;
        int expectedDecompressedSize = height * scanlineBytes; // Hedef boyut: 164 * 34 = 5576

        try
        {
            while ( src < data.Length)
            {
                // Veri sonu kontrolü
                if (src >= data.Length) break;
                int code = data[src++];

                if (code <= 127) // Literal (0-127)
                {
                    int count = code+1 ;
                    // Veri sonunu aşmamayı garantile
                    int bytesToCopy = Math.Min(count, data.Length - src);
                    if (bytesToCopy <= 0) break; // Okunacak veri kalmadı

                    for (int i = 0; i < bytesToCopy; i++)
                    {
                        decompressedData.Add(data[src + i]);
                    }
                    src += bytesToCopy;
                    // Eğer count > bytesToCopy ise veri erken bitti demektir, döngüden çıkabiliriz.
                    if (bytesToCopy < count) break;
                }
                else if (code >= 129) // Tekrar (129-255)
                {
                    // Veri sonu kontrolü
                    if (src >= data.Length) break;
                    int count = 256 - code;
                    byte val = data[src++];
                    for (int i = 0; i < count ; i++)
                    {
                        decompressedData.Add(val);
                    }
                }
                // code == 128 ise NOP, src zaten arttırıldı, bir şey yapma
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Dekompresyon sırasında hata: {ex.Message}");
            if (pictureBox1.Image != null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
            return;
        }


        // Boyut kontrolü
        if (decompressedData.Count != expectedDecompressedSize)
        {
            MessageBox.Show($"Uyarı: Beklenen dekompresyon boyutu ({expectedDecompressedSize}) ile sonuç ({decompressedData.Count}) farklı.");
            // Çok az veri varsa devam etme
            if (decompressedData.Count <= scanlineBytes)
            {
                if (pictureBox1.Image != null) { pictureBox1.Image.Dispose(); pictureBox1.Image = null; }
                return;
            }
        }

        // 3) 4-renk paleti
        Color[] palette = new Color[] {
        Color.Black,      // 0
        Color.Cyan,       // 1
        Color.Magenta,    // 2
        Color.White       // 3
    };

        // 4) Bitmap’e dök (LockBits ile)
        Bitmap bmp = new Bitmap(pixelWidth, height, PixelFormat.Format32bppArgb);
        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

        IntPtr ptr = bmpData.Scan0;
        int stride = bmpData.Stride;
        byte[] pixelBuffer = new byte[stride * height];
        int currentRawIndex = 0;
        int maxBytesToProcess = Math.Min(decompressedData.Count, expectedDecompressedSize);

        for (int y = 0; y < height; y++)
        {
            int currentLineOffset = y * stride;
            for (int xb = 0; xb < scanlineBytes; xb++) // Satır başına doğru byte sayısı (34)
            {
                if (currentRawIndex >= maxBytesToProcess) break; // Çözülmüş veri bitti

                byte b = decompressedData[currentRawIndex++];

                // Dört 2-bitlik pikseli ayıkla ve tampona yaz (BGRA)
                for (int i = 0; i < 4; i++)
                {
                    int pixelX = xb * 4 + i;
                        if (pixelX >= pixelWidth)
                        {
                            continue; // Bitmap genişliğini aşma (ekstra güvenlik)
                        }
                    int shift = (3 - i) * 2;        // 6, 4, 2, 0
                    int idx = (b >> shift) & 0x03;  // 0, 1, 2, veya 3
                    Color c = palette[idx];

                    int offset = currentLineOffset + pixelX * 4; // 4 = bytes per pixel for 32bppArgb

                    // Tampon sınırlarını kontrol et
                    if (offset + 3 < pixelBuffer.Length)
                    {
                        pixelBuffer[offset] = c.B;
                        pixelBuffer[offset + 1] = c.G;
                        pixelBuffer[offset + 2] = c.R;
                        pixelBuffer[offset + 3] = c.A; // Alpha (genellikle 255)
                    }
                }
            }
            if (currentRawIndex >= maxBytesToProcess) break; // Dış döngüden de çık
        }

        // Tamponu bitmap'e kopyala
        Marshal.Copy(pixelBuffer, 0, ptr, pixelBuffer.Length);
        bmp.UnlockBits(bmpData);

        // 5) PictureBox’a ata
        if (pictureBox1.Image != null)
        {
            pictureBox1.Image.Dispose();
        }
        pictureBox1.Image = bmp;
    }


}
}
