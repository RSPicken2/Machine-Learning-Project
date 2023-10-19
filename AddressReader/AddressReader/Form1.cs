using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetworkLibrary;

namespace AddressReader
{
    public partial class Form1 : Form
    {
        NeuralNetwork nn;
        static List<string> addresses;

        FileSystemWatcher fileWatcher;
        string inputFolderPath = "";
        string outputFilePath = "";
        public Form1()
        {
            InitializeComponent();
            btnAutoOff.Hide();
            lblInputFile.Text = "";
            lblOutputFile.Text = "";

            //load the neural network
            nn = NeuralNetwork.LoadNeuralNetwork("Recources/NeuralNetwork.JSON");            
            //load the list of valid addresses
            addresses = GetAddressList();


            //debug
            addresses.Add("A A");
            //
        }

        private  void btnAutoOn_Click(object sender, EventArgs e)
        {
            //if the input and output locations have been set
            if (inputFolderPath != "" && outputFilePath != "")
            {
                btnAutoOff.Show();
                btnAutoOn.Hide();

                fileWatcher = new FileSystemWatcher(inputFolderPath);
                //when a file is created in the file we're watching call this event
                //This event reads the address in the image that was just saved and outputs the
                //address to the specified text file
                fileWatcher.Created += new FileSystemEventHandler(FileWatcher_Created);
                //for some reason FileWachers can't raise events by default
                fileWatcher.EnableRaisingEvents = true;
            }
            else
            {
                MessageBox.Show("Input and output not both set");
            }
        }

        private async void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                Image image = Image.FromFile(e.FullPath);
                string address = await ReadAddressAsync(image);
                using (StreamWriter writer = new StreamWriter(outputFilePath, true))
                {
                    writer.WriteLine(address);
                }
            }
            catch (Exception)
            {
                //this shouldn't happen becuase of the file type filtering we're using 
                //but its better to be safe
                MessageBox.Show("Invalid file");
            }

        }

        private void btnAutoOff_Click(object sender, EventArgs e)
        {
            //disposing of the file watcher stops us checking if files have been saved 
            //so it stops auto reading new addresses
            fileWatcher.Dispose();
            btnAutoOn.Show();
            btnAutoOff.Hide();         
        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {
            //sets the folder where we will detect new input images.

            //filewatcher will be null if the program isn't autoreading
            //we only want to change the sources while not reading
            if (fileWatcher == null)
            {
                //FolderBrowserDialgue works similar to OpenFileDialogue but only lets you
                //select a folder
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {

                    dialog.ShowDialog();
                    inputFolderPath = dialog.SelectedPath;
                    //split the directories
                    string[] path = inputFolderPath.Split('\\');
                    //get the file name which will be at the last index of the path
                    lblInputFile.Text = path[^1];
                }
            }
            else
            {
                MessageBox.Show("Cannot change folder while auto reading");
            }
         
        }

        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            //sets the file where we will output read addresses.

            //filewatcher will be null if the program isn't autoreading
            //we only want to change the sources while not reading
            if (fileWatcher == null)
            {
                //FolderBrowserDialgue works similar to OpenFileDialogue but only lets you
                //select a folder
                using (OpenFileDialog dialog = new OpenFileDialog())
                {

                    dialog.ShowDialog();
                    //the output has to be a text file
                    if (dialog.FileName.EndsWith(".txt"))
                    {
                        outputFilePath = dialog.FileName;
                        //split the directories
                        string[] path = outputFilePath.Split('\\');
                        //get the file name which will be at the last index of the path
                        lblOutputFile.Text = path[^1];
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid text file");
                    }

                }
            }
            else
            {
                MessageBox.Show("Cannot change folder while auto reading");
            }
        }

        private async void btnReadImage_Click(object sender, EventArgs e)
        {
            ////debug
            //addresses.Add("A A");
            ////

            //lets us dispose of the dialogue automatically
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                //open the windows file dialogue to let a user select a file
                dialog.ShowDialog();
                try
                {
                    //try reading the file selected as an image
                    //output an error to the user if an exception is thrown
                    Image image = Image.FromFile(dialog.FileName);

                    //convert the image of an address into a string storing that address
                    //run readAddress asyncronously because it is an intensive process
                    //that would freeze the ui thread for a couple seconds
                    string address = await ReadAddressAsync(image);

                    //output read address
                    lblReadAddress.Text = address;
                    //output image selected
                    pcbxOut.Image = image;
                    //the address will be empty if the image had no words in it or the address
                    //wasn't in the PAF
                    if (address == "")
                    {
                        MessageBox.Show("No Address Detected");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid File");
                }
                
            }
        }
        //for an async function, the type in the <> is the return datatype
        async Task<string> ReadAddressAsync(Image inputImage)
        {
            //finds the letters in input image, converts them to a queue of chars and then 
            //finds the address they match if they are a valid address. This address is then
            //returned
            
            //get the queue of every letter in the image
            Queue<char> readLetters = await ReadLettersInImageAsync(inputImage);
            string address = "";
            
            //if the read letters are a valid address
            if(TryGetAddress(ref address, readLetters))
            {
                //return the address detected
                return address;
            }
            //else
            return "";
        }
        private async Task<Queue<char>> ReadLettersInImageAsync(Image image)
        {
            Queue<Bitmap> letters = await GetProcessedImagesAsync(image);
            Queue<char> readLetters = new Queue<char>();
            while (letters.Count > 0)
            {
                Bitmap letter = letters.Dequeue();
                letter = Invert(letter);
                float[] pixelVals = BitmapToArray(letter);
                readLetters.Enqueue(
                        nn.RecogniseImage(pixelVals));
            }
            return readLetters;
        }

        #region Image Processing
        private Queue<Bitmap> GetProcessedImages(Image unprocessedImage)
        {
            //convert input image to a bitmap
            Bitmap image = new Bitmap(unprocessedImage);
            image = ThresholdImage(image, 0.5f);

            //split image into rows of letters
            Queue<Bitmap> rows = SplitIntoRows(image);
            //split each row into square images of each letter
            Queue<Bitmap> letters = SplitIntoLetters(rows);

            //compress each letter
            Queue<Bitmap> compressedImages = new Queue<Bitmap>();
            while (letters.Count > 0)
            {
                Bitmap letter = letters.Dequeue();
                compressedImages.Enqueue(CompressBitmap(28, 28, letter));
            }

            return compressedImages;
        }
        Task<Queue<Bitmap>> GetProcessedImagesAsync(Image unprocessedImage)
        {
            //converts GetProcessedImages to a task that can be ran asynchronously
            //using a lambda expression representing a function that calls GetProcessedImages
            //with unprocessedImage as its argument
            return Task.Run(() => GetProcessedImages(unprocessedImage));
        }

        private float[] BitmapToArray(Bitmap image)
        {
            //combines each column in a bitmap to form a single 1D float array

            // 7 9 1
            // 5 2 6        - >      7 5 3 9 2 0 1 6 8
            // 3 0 8

            //1D array to store all our values
            float[] outArray = new float[image.Height * image.Width];
            //iterate through each pixel in the image
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    //get the index in outarray
                    int index = image.Width * x + y;
                    //set that index's value
                    //all our values will be black and white by now so
                    //the brightness 0-1 is all we need to represent the pixel
                    outArray[index] = image.GetPixel(x, y).GetBrightness();
                }
            }
            return outArray;
        }

        private Bitmap Invert(Bitmap letter)
        {
            //inverts an image to the opposite grey shade
            for (int i = 0; i < letter.Width; i++)
            {
                for (int j = 0; j < letter.Height; j++)
                {
                    //get a value between 1 and 0 that's the inverted form of the original image's
                    int inverted = (int)(255f * (1f - letter.GetPixel(i, j).GetBrightness()));
                    //convert this value to a colour
                    Color invertedPixel = Color.FromArgb(inverted, inverted, inverted);
                    //set inverted colour
                    letter.SetPixel(i, j, invertedPixel);
                }
            }
            return letter;
        }

        private Bitmap ThresholdImage(Bitmap image, float threshold)
        {
            //threshold an image to pure black and white
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    var currentPxl = image.GetPixel(i, j);
                    //if pixel value is above the threshold, round up to white
                    //else round down to black
                    if (currentPxl.GetBrightness() > threshold)
                    {
                        image.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        image.SetPixel(i, j, Color.Black);
                    }
                }
            }
            return image;
        }
        private Queue<Bitmap> SplitIntoRows(Bitmap image)
        {
            var rows = new Queue<Bitmap>();
            int yLowerBound = 0;
            int yUpperBound;

            while (yLowerBound < image.Height)
            {
                //search through image until a row contains a black pixel
                //-top of row found
                //or we reach the end of the image - && means the second half
                //wont be called if the first is false so we can avoid an out of bounds exception
                while (yLowerBound < image.Height &&
                    !DoesRowContainBlackPixel(image, yLowerBound))
                {
                    yLowerBound++;
                }
                //if the lower bound for a row is at the end of the image, that means there isn't
                //another row as we're at the end so we skip the section for the ipperbound and 
                //splitting the row off
                if (yLowerBound < image.Height)
                {
                    //set the upperbound of the current row's y to be the same as the lowerbound
                    yUpperBound = yLowerBound;

                    //search through the image until a row doesn't contain a black pixel 
                    //-bottom of row found
                    //or we reach the end of the image - && means the second half
                    //wont be called if the first is false so we can avoid an out of bounds exception
                    while (yUpperBound < image.Height &&
                        DoesRowContainBlackPixel(image, yUpperBound))
                    {
                        yUpperBound++;
                    }
                    //split the row from the image and equeue it 
                    rows.Enqueue(SplitRow(image, yLowerBound, yUpperBound));

                    //for the next row the lower bound will be the pixel after the currrent upperbound
                    yLowerBound = yUpperBound + 1;
                }
            }
            return rows;
        }
        private Queue<Bitmap> SplitIntoLetters(Queue<Bitmap> rows)
        {
            Queue<Bitmap> letters = new Queue<Bitmap>();
            while (rows.Count > 0)
            {
                Queue<Bitmap> columns = SplitIntoColumns(rows.Dequeue());
                //enqueue all the items in temp to letters
                while (columns.Count > 0)
                {
                    //letters.Enqueue(temp.Dequeue());
                    Bitmap letter = columns.Dequeue();
                    //the height of each letter will be the same as the tallest letter in the 
                    //row so we have to crop all the others
                    letter = VerticalCrop(letter);
                    //letters.Enqueue(letter);
                    letters.Enqueue(ConvertToSquare(letter));
                }
            }
            return letters;
        }

        private Bitmap VerticalCrop(Bitmap letter)
        {
            //crops the image to remove any white space


            //offsets from letter to crop where its just empty space
            int top = 0, bottom = 0;
            bool edgeFound;

            //get offset from top of image
            do
            {
                //start at furthest at the top, move inwards
                edgeFound = DoesRowContainBlackPixel(letter, top);
                if (!edgeFound) top++;

            } while (!edgeFound);

            //get offset from bottom of image
            do
            {
                //start at furthest at the bottom, move inwards
                edgeFound = DoesRowContainBlackPixel(letter,
                             letter.Height - bottom - 1);
                if (!edgeFound) bottom++;

            } while (!edgeFound);



            Bitmap croppedImage = new Bitmap(letter.Width,
                                             letter.Height - top - bottom);

            //crop imgage with the offsets we've found
            for (int i = 0; i < croppedImage.Height; i++)
            {
                for (int j = 0; j < croppedImage.Width; j++)
                {
                    //start from top away from the top
                    Color temp = letter.GetPixel(j, i + top);
                    croppedImage.SetPixel(j, i, temp);
                    //pcbxImageOutput.Image = croppedImage; //remove me
                }
            }
            return croppedImage;
        }

        private Queue<Bitmap> SplitIntoColumns(Bitmap image)
        {
            int xLowerBound = 0;
            int xUpperBound;
            Queue<Bitmap> Columns = new Queue<Bitmap>();
            while (xLowerBound < image.Width)
            {
                //search through image until a column contains a black pixel
                //-left side of a letter found
                //or we reach the end of the image - && means the second half
                //wont be called if the first is false so we can avoid an out of bounds exception
                while (xLowerBound < image.Width &&
                    !DoesColumnContainBlackPixel(image, xLowerBound))
                {
                    xLowerBound++;
                }
                //if the lower bound for a row is at the end of the image, that means there isn't
                //another row as we're at the end so we skip the section for the ipperbound and 
                //splitting the row off
                if (xLowerBound < image.Width)
                {
                    //set the upperbound of the current row's y to be the same as the lowerbound
                    xUpperBound = xLowerBound;

                    //search through the image until a row doesn't contain a black pixel 
                    //-right side of letter found
                    //or we reach the end of the image - && means the second half
                    //wont be called if the first is false so we can avoid an out of bounds exception
                    while (xUpperBound < image.Width &&
                        DoesColumnContainBlackPixel(image, xUpperBound))
                    {
                        xUpperBound++;
                    }
                    //split the row from the image and equeue it 
                    Columns.Enqueue(SplitColumn(image, xLowerBound, xUpperBound));

                    //for the next row the lower bound will be the pixel after the currrent upperbound
                    xLowerBound = xUpperBound + 1;
                }
            }
            return Columns;
        }

        private Bitmap SplitColumn(Bitmap image, int xLowerBound, int xUpperBound)
        {
            //splits a column of a letter from a base image using the upper and lower bounds
            //of its x value

            //instantiate row to be the same width as the original image
            //and height of the difference between the bounds
            Bitmap column = new Bitmap(xUpperBound - xLowerBound, image.Height);

            //loop through each pixel in the range
            for (int x = xLowerBound; x < xUpperBound; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    //set the rows' pixel value to image's equivalent
                    column.SetPixel(x - xLowerBound, y, image.GetPixel(x, y));
                }
            }
            return column;
        }

        private Bitmap SplitRow(Bitmap image, int yLowerBound, int yUpperBound)
        {
            //splits a row of letters from a base image using the upper and lower bounds
            //of its y value

            //instantiate row to be the same width as the original image
            //and height of the difference between the bounds
            Bitmap row = new Bitmap(image.Width, yUpperBound - yLowerBound);

            //int a = image.Height;
            //int b = row.Height;
            //loop through each pixel in the range
            for (int y = yLowerBound; y < yUpperBound; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    //set the rows' pixel value to image's equivalent
                    row.SetPixel(x, y - yLowerBound, image.GetPixel(x, y));
                }
            }
            return row;
        }
        private Bitmap ConvertToSquare(Bitmap image)
        {
            //stores the imaage after being converted to a square
            Bitmap squareImage;

            //stores the offset from one end of the square image
            //to where the original image's pixels start
            int offset;
            //if the image is taller
            if (image.Height > image.Width)
            {
                squareImage = new Bitmap(image.Height, image.Height);
                offset = (image.Height - image.Width) / 2;

                //go through each row
                for (int y = 0; y < image.Height; y++)
                {
                    //go through each column
                    for (int x = 0; x < offset; x++)
                    {
                        squareImage.SetPixel(x, y, Color.White);
                    }
                    //add offset number of white pixels at the start
                    for (int x = offset; x < image.Width + offset; x++)
                    {
                        squareImage.SetPixel(x, y,
                                image.GetPixel(x - offset, y));
                        //offset is subtracted from x for image indexes to get it back to starting
                        //from 0
                    }
                    //add offset number of white pixels at end
                    for (int x = image.Width + offset; x < squareImage.Width; x++)
                    {
                        squareImage.SetPixel(x, y, Color.White);
                    }
                }
            }
            //if the image is wider
            else if (image.Height < image.Width)
            {
                squareImage = new Bitmap(image.Width, image.Width);
                offset = (image.Width - image.Height) / 2;

                //go through each column
                for (int x = 0; x < image.Width; x++)
                {
                    //add offset number of white pixels at the start 
                    for (int y = 0; y < offset; y++)
                    {
                        squareImage.SetPixel(x, y, Color.White);
                    }
                    //add the original pixels 
                    for (int y = offset; y < image.Height + offset; y++)
                    {
                        squareImage.SetPixel(x, y,
                                image.GetPixel(x, y - offset));
                    }
                    //add offset number of white pixels at end
                    for (int y = image.Height + offset; y < squareImage.Height; y++)
                    {
                        squareImage.SetPixel(x, y, Color.White);
                    }
                }
            }
            else
            {
                //if it gets here its already a square
                return image;
            }
            return squareImage;
        }

        private bool DoesColumnContainBlackPixel(Bitmap bitmap, int x)
        {
            //run down a column and check if there is a black pixel
            for (int i = 0; i < bitmap.Height; i++)
            {
                if (bitmap.GetPixel(x, i).GetBrightness() == 0)
                    return true;
            }
            return false;
        }
        private bool DoesRowContainBlackPixel(Bitmap bitmap, int y)
        {
            //run along a row and check if there is a black pixel
            for (int i = 0; i < bitmap.Width; i++)
            {
                //1 is white
                if (bitmap.GetPixel(i, y).GetBrightness() == 0)
                    return true;
            }
            return false;
        }

        private Bitmap CompressBitmap(int width, int height, Bitmap image)
        {
            //compresses image down to the size of width*height using lossy compression
            //and averaging the pixel values in regular areas of the original image

            //if the original image length isn't a multiple of the new length,
            //not all the data will be stored in the compressed image so we widen the image
            //with white borders until it is a multiple
            //This also means we can skip the step of widening the image to add an outline later too
            if (image.Width % width > 0)
            {
                //half the remainder of the image's width and the new width to get the 
                //size of the white outline on both sides of the original 
                int extention = (image.Width % width) / 2;
                //temp is a bitmap with the dimentions of image after being extended
                var temp = new Bitmap(image.Width + (2 * extention), image.Height); ;

                //loop through each row in image
                for (int i = 0; i < image.Height; i++)
                {
                    //add white border to the left side
                    for (int j = 0; j < extention; j++)
                    {
                        temp.SetPixel(j, i, Color.White);
                    }
                    //add original image pixels for that row
                    for (int j = extention; j < extention + image.Width; j++)
                    {
                        temp.SetPixel(j, i, image.GetPixel(j - extention, i));
                    }
                    //add white border to the right side
                    for (int j = extention + image.Width; j < temp.Width; j++)
                    {
                        temp.SetPixel(j, i, Color.White);
                    }
                }
                //set image to the extended image in temp
                image = temp;
            }

            //if the original image's height isn't a multiple of the new height
            //extend the image with a white outline so it is
            if (image.Height % height > 0)
            {
                //half the remainder of the image's height and the new height to get the 
                //size of the white outline added on both sides of the original (top and bottom)
                int extention = (image.Height % height) / 2;
                //temp is a bitmap with the dimentions of image after being extended
                var temp = new Bitmap(image.Width, image.Height + (2 * extention));

                //loop through each column in the original image
                for (int i = 0; i < image.Width; i++)
                {
                    //add white border to the top
                    for (int j = 0; j < extention; j++)
                    {
                        temp.SetPixel(i, j, Color.White);
                    }
                    //add original image pixels for that column
                    for (int j = extention; j < extention + image.Height; j++)
                    {
                        temp.SetPixel(i, j, image.GetPixel(i, j - extention));
                    }
                    //add white border to the bottom
                    for (int j = extention + image.Height; j < temp.Height; j++)
                    {
                        temp.SetPixel(i, j, Color.White);
                    }
                }
                //set image to the extended image in temp
                image = temp;
            }

            //new bitmap which will store image after being resized down to width*height
            Bitmap compressedImage = new Bitmap(width, height);

            //width of a section to be compressed to 1 pixel
            int xStep = image.Width / width;
            //width of a section to be compressed to 1 pixel
            int yStep = image.Height / height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    float sumBrightness = GetSumBrightnessForArea(image,
                                                                x * xStep, y * yStep,
                                                                x * xStep + xStep,
                                                                y * yStep + yStep);

                    float avrgBrightness = sumBrightness / (xStep * yStep);
                    avrgBrightness *= 255f;
                    Color newColour = Color.FromArgb((int)avrgBrightness,
                        (int)avrgBrightness, (int)avrgBrightness);
                    compressedImage.SetPixel(x, y, newColour);
                }
            }

            return compressedImage;
        }
        private float GetSumBrightnessForArea(Bitmap image, int xStart, int yStart, int xEnd, int yEnd)
        {
            //iterate through a region of pixels and sum the brightnesses for each pixel
            float sum = 0;
            for (int i = yStart; i < yEnd; i++)
            {
                for (int j = xStart; j < xEnd; j++)
                {
                    sum += image.GetPixel(j, i).GetBrightness();
                }
            }
            return sum;
        }
        #endregion
        #region Address
        static List<string> GetAddressList()
        {
            //read the paf database to get a list of every valid address

            List<string> addresses = new List<string>();
            using (StreamReader reader = new StreamReader("Recources/full_paf_sample.csv"))
            {
                //skip headers line
                reader.ReadLine();

                //until we're at the end of the file
                while (!reader.EndOfStream)
                {
                    //read a line and split it accross the commas splitting the address parts
                    string line = reader.ReadLine();
                    string[] splitAddress = line.Split(',');
                    string address = "";

                    //convert the split up address array into a string split up by spaces
                    for (int i = 0; i < splitAddress.Length - 2; i++)
                    {
                        //some of the parts will be empty on some addresses so skip those ones
                        //as that would lead to empty spaces
                        if (splitAddress[i] != "")
                        {
                            address += splitAddress[i] + " ";
                        }
                    }
                    //remove the last space added at the end
                    address = address.Remove(address.Length - 1);
                    //enqueue this address to the list of valid addresses
                    addresses.Add(address);
                }
            }
            return addresses;
        }



        private static bool TryGetAddress(ref string currentAddress, Queue<char> detectedLetters)
        {
            //This is a recursive function that takes a current address and a queue of letters
            //detected by the neural network and tries to assemble an address using them
            //if a valid address is found, true is retuned and currentAddress will store the address
            //otherwise it returns false and currentAddress can be discarded

            //temp is used to store the current address if we need to revert to a previous state
            //it's also needed for the LINQ query because that doen't like ref variables
            string temp = currentAddress;

            //search for all addresses which start with the currentAddress string using LINQ
            //make sure they are longer than the address first so we dont get an out of bounds error
            var query = from address in addresses
                        where address.Length >= temp.Length
                        && address.Substring(0, temp.Length) == temp
                        select address;

            //if any addresses start with the current address
            if (query.Count() > 0)
            {
                //if all detected letters have been added to the current address - base case
                if (detectedLetters.Count() == 0)
                {
                    //if the detected address is in the query, we've found a valid address
                    //so return true
                    //else that means that an address starting with what was read by the nn 
                    //is in the query but it continues further. Therefore return false
                    if (query.Contains(currentAddress)) return true;
                    return false;
                }

                //see if adding a space results in a valid address
                currentAddress += ' ';
                //use a new queue with the same values as detectedLetters so we can create a byval copy
                //otherwise dequeues in deeper instances would affect this one's queue
                if (TryGetAddress(ref currentAddress, new Queue<char>(detectedLetters)))
                {
                    //we've found a valid address so return true - base case
                    return true;
                }
                else
                {
                    //reset currentAddress to remove the space just added
                    currentAddress = temp;
                }
                //see if adding the next detected letter results in a valid address
                currentAddress += detectedLetters.Dequeue();
                if (TryGetAddress(ref currentAddress, new Queue<char>(detectedLetters)))
                {
                    //we've found a valid address so return true - base case
                    return true;
                }
            }
            //if the program gets here then the current address is invalid so return
            //false to the previous instance
            return false;
        }
        #endregion

        //private async void Form1_Load(object sender, EventArgs e)
        //{
        //    Image image = Image.FromFile("C:\\Users\\Rebecca\\Documents\\A Level Work\\CS\\Coursework\\images\\20211204_161839.jpg");
        //     var letter = await ReadLettersInImageAsync(image);
        //    MessageBox.Show(letter.Dequeue().ToString());

        //}
    }
}
