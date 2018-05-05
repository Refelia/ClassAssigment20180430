using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SortingNames
{
    public partial class Form1 : Form
    {
        const int SIZE = 5000;

        //Declare a string array.
        string[] countArray = new string[SIZE];

        // Pass the file path and file name to the StreamReader constructor
        StreamReader strReader = new StreamReader("Names.csv"); 
        string ListOfName = string.Empty;

        int index;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { }

        private void loadBtn_Click(object sender, EventArgs e)
        {
           
            DateTime start = DateTime.Now;
            double end = (DateTime.Now - start).TotalSeconds;
            try
            {
                //Read the first line of text
                ListOfName = strReader.ReadLine();
                int index = 0;

                //Continue to read until you reach end of file
                while (ListOfName != null)
                {
                    countArray[index] = ListOfName.ToString();
                    this.listBox1.Items.Add(ListOfName);
                    //Read the next line
                    ListOfName = strReader.ReadLine();
                    // start incrementing
                    index++;
                    //Display the the total number of items in the listbox
                    countTextBox.Text = listBox1.Items.Count.ToString() + ", " + " Names found in : " + " " + (end.ToString()) + " " +
                         " Seconds";
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }
        //Create a method selectionSort.
        public void SelectionSort(string[] countArray)
        {
            //declare varable
            int lowestIndex = 0 ;
            string lowestValue ;

            for (int searchValue = 0; searchValue <countArray.Length; searchValue++)
            {
                lowestValue = countArray[searchValue];
                lowestIndex = searchValue;

                for (int index = searchValue + 1; index < countArray.Length; index++)
                {
                    if (string.Compare(lowestValue, countArray[index], true) == 1)
                    {
                        lowestIndex = index;
                        lowestValue = countArray[index];
                    }
                }
                swap(ref countArray[lowestIndex], ref countArray[searchValue]);
            }

        }
        public void swap(ref string smallestValue, ref string biggestValue)
        {
            if (smallestValue != biggestValue)
            {
                string temp = smallestValue;
                smallestValue = biggestValue;
                biggestValue = temp;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        { }

        private void sortAZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Clear all the text in the listbox before start sorting. 
                listBox1.Items.Clear();

                //Declare a datetime variable and initialize with the Datetime.Now value
                DateTime start = DateTime.Now;
                double end = (DateTime.Now - start).TotalSeconds;


                while (index < countArray.Length && !strReader.EndOfStream)
                {
                    countArray[index] = strReader.ReadLine();
                    index++;
                }

                listBox1.Items.Clear();
                SelectionSort(countArray);

                foreach (string value in countArray)
                {
                    listBox1.Items.Add(value);
                }
                timeTextBox.Text = listBox1.Items.Count.ToString()
                + " , " + "name found! SORTING TIME:  " + " " + (end.ToString()) + " " + "Seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pathFinder = "sortedNames.csv";
            StreamWriter strWriter = new StreamWriter(pathFinder);
            
            foreach(string listOfNames in listBox1.Items)
            {
                //write a text file.
                strWriter.WriteLine(listOfNames.ToString());

                //Display message
                MessageBox.Show("Exported!");
            }
        }
        private int binarySearch(string[] countArray, string value)
        {
            //Declare a variable 
                int leftFromMidPoint = 0;
                int rightFromMidPoint = countArray.Length - 1;
                int midPoint;
                int possition = -1;
                bool found = false;

                //While loop to search for the value.
                while (!found && leftFromMidPoint <= rightFromMidPoint)
                {
                    //Assigning value to the midpoint of array
                    midPoint = (leftFromMidPoint + rightFromMidPoint) / 2;

                    //if found
                    if (countArray[midPoint] == value)
                    {
                        found = true;
                        possition = midPoint;
                    }
                    // if the value is in the lefside of the midpoint 
                    else if (string.Compare(countArray[midPoint], value, false) > 0)
                    {
                        rightFromMidPoint = midPoint - 1;
                    }
                    //if the value is in the right side of the midpoint
                    else
                    {
                        leftFromMidPoint = midPoint + 1;
                    }
                }
                return possition;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit the program.
            Application.Exit();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
               //Declare variable of the time
                DateTime start = DateTime.Now;
                double end = (DateTime.Now - start).TotalSeconds;

                //Decalre the variable and initialize with value
                string name = searchTextBox.Text;
                int position = binarySearch(countArray, searchTextBox.Text);
                bool found = countArray.Contains(name);

                //Checking if the statement is true.
                if(position >= 0)
                {
                    found = true;
                }
                // if  false then ...
                if(found == false)
                {
                    //display "name does not exist"
                    searchTextBox.Text = " Name does not exist!";
                }

                //Checking if the statement is true
              listBox1.SetSelected(position, true);

                //declare a string text and initialized with value
                string text = listBox1.GetItemText(listBox1.SelectedItem);

                //Display the name and index location and time that it took for scanning.
                searchTextBox.Text = text + " " + " NAME FOUND!" +  " AT INDEX LOCATION: " + position + " " + 
                                     " SCANNING TIME : "  + (end.ToString()) + " " + "seconds";
            }
            catch (Exception)
            {
                //Display the error message.
                MessageBox.Show("Name does not exist");
            }
        }

        private void resetBtn_Click_1(object sender, EventArgs e)
        {
            //Clear all the text in the search textbox.
            searchTextBox.Text = "";

            //Set the focus.
            searchTextBox.Focus();
        }
    }
    
}




    

