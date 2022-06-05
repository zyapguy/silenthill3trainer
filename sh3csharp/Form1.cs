using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace sh3csharp
{
    public partial class Form1 : Form
    {   // booleans to check whether the user has a mode on or not
        public bool infiniteHandgun = false;
        public bool infiniteShotgun = false;
        public bool infiniteUzi = false;
        public bool infiniteHealthDrinks = false;
        public bool infiniteBeefJerky = false;
        public bool infiniteMedkits = false;
        public bool infiniteAmpoules = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Checkbox 1 is God Mode
            if (checkBox1.Checked)
            {
                // Read health value and save it to a file
                // This is to ensure the user can go back to the previous health.
                int originalHealth = ReadWritingMemory.ReadInteger("sh3", 0x898660);
                File.WriteAllText(Directory.GetCurrentDirectory() + "/restorefile.dat", originalHealth.ToString());

                // Set health to -1. This makes it so that no matter how much it decreases, it is never 0
                ReadWritingMemory.WriteInteger("sh3", 0x898660, -1);
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "/restorefile.dat"))
                {
                    try
                    {
                        // Read previous health if exists
                        int restoredHealth = Int32.Parse(File.ReadAllText(Directory.GetCurrentDirectory() + "/restorefile.dat"));
                        // Set health to previous value
                        ReadWritingMemory.WriteInteger("sh3", 0x898660, restoredHealth);
                    }
                    catch
                    {
                        MessageBox.Show("There was an error while reading your restoration file");
                    }
                }
                else // if the file doesn't exist for some reason (A. name changed, B. doesn't exist), display this error
                {
                    MessageBox.Show("Couldn't restore god mode data. You are stuck with god mode. :(");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // Flashlight ON/OFF
            if (checkBox2.Checked)
            {
                // 1065353216 enables the flashlight.
                ReadWritingMemory.WriteInteger("sh3", 0x712CAC4, 1065353216);
            }
            else
            {
                ReadWritingMemory.WriteInteger("sh3", 0x712CAC4, 0);
            }
        }

        private void generalChecks_Tick(object sender, EventArgs e)
        {
            // Set boolean values to checkbox states
            infiniteHandgun = checkBox3.Checked;
            infiniteShotgun = checkBox4.Checked;
            infiniteUzi = checkBox5.Checked;
            infiniteHealthDrinks = checkBox6.Checked;
            infiniteBeefJerky = checkBox7.Checked;
            infiniteMedkits = checkBox8.Checked;
            infiniteAmpoules = checkBox9.Checked;

            if (infiniteHandgun)
            {
                // Set handgun ammo to 99
                ReadWritingMemory.WriteInteger("sh3", 0x712CAAC, 99);
            }
            if (infiniteShotgun)
            {
                // Change shotgun ammo to 6.
                // Game maxes out at 6, so no need to set it to more
                ReadWritingMemory.WriteInteger("sh3", 0x712CAA4, 6);
            }
            if (infiniteUzi)
            {
                // Set uzi ammo to 99
                ReadWritingMemory.WriteInteger("sh3", 0x712CAA4, 6488064);
            }
            if (infiniteHealthDrinks)
            {
                ReadWritingMemory.WriteInteger("sh3", 0x712CAB0, 6488064);
            }
            if (infiniteBeefJerky)
            {
                ReadWritingMemory.WriteInteger("sh3", 0x712CAB8, 99); 
            }
            if (infiniteMedkits)
            {

                ReadWritingMemory.WriteInteger("sh3", 0x712CAB4, 9732195);
            }
            if (infiniteAmpoules)
            {
                ReadWritingMemory.WriteInteger("sh3", 0x712CAB4, 6488361);
            }
        }

        // About button
        private void button4_Click(object sender, EventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Show();
        }

        // Unlock all weapons
        private void button1_Click_1(object sender, EventArgs e)
        {
            ReadWritingMemory.WriteInteger("sh3", 0x712CA80, 0xFFFF);
        }

        // Unlock all clothes
        private void button2_Click(object sender, EventArgs e) // no clue but change val
        {
            ReadWritingMemory.WriteInteger("sh3", 0x712CA8A, 0xFFFFFFC);
        }

        // Unlock all weapons and misc
        private void button3_Click(object sender, EventArgs e) // no clue but change val
        {
            ReadWritingMemory.WriteInteger("sh3", 0x712CA80, 0x520FFFF);
        }


        private void codeTimer_Tick(object sender, EventArgs e)
        {
            int shakespearecombo = ReadWritingMemory.ReadInteger("sh3", 0x715D2E8); // read shakespeare combo
            shakespeareCombo.Text = Convert.ToString(shakespearecombo, 16); // display shakespeare combo

            int keypadcombo = ReadWritingMemory.ReadInteger("sh3", 0xC0112C); // read keypad combo
            hospitalKeypad.Text = Convert.ToString(keypadcombo, 16); // display keypad combo

            int clockcode = ReadWritingMemory.ReadInteger("sh3", 0x715D2FC); // read clock code
            hospitalClock.Text = Convert.ToString(clockcode, 16); // display clock code 

            int bloodcode = ReadWritingMemory.ReadInteger("sh3", 0x715D304); // read blood code
            hospitalBlood.Text = Convert.ToString(bloodcode, 16); // display blood code
        }

        private void positionTimer_Tick(object sender, EventArgs e) // reads the x and y position, then displays it.
        {
            float xPos = ReadWritingMemory.ReadFloat("sh3", 0x8984E0);
            float yPos = ReadWritingMemory.ReadFloat("sh3", 0x8984E8);
            positionBox.Text = "X:" + xPos.ToString() + " Y:" + yPos.ToString();
        }

        // Teleport heather
        private void button5_Click(object sender, EventArgs e)
        {
            try // changes the value of the x and y coordinate
            {
                float xPos = float.Parse(newXBox.Text);
                float yPos = float.Parse(newYBox.Text);

                ReadWritingMemory.WriteFloat("sh3", 0x8984E0, xPos);
                ReadWritingMemory.WriteFloat("sh3", 0x8984E8, yPos);
            }
            catch // error, occurs if the value was incorrect
            {
                MessageBox.Show("Warning : Enter a valid float value!");
            }
        }

        private void saveManagerTimer_Tick(object sender, EventArgs e)
        {
            int saveAmount = ReadWritingMemory.ReadInteger("sh3", 0x70E66E4); // reads the save amount
            currentSaveAmount.Text = "Current Saves : " + saveAmount.ToString(); // displays it
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int newSaveCount = Int32.Parse(textBox1.Text) - 1;  // display the amount of saves -1 because it adds 1 to balance it out
                ReadWritingMemory.WriteInteger("sh3", 0x70E66E4, newSaveCount); // write to memory
            }
            catch
            {
                MessageBox.Show("Warning : Enter a valid integer value!"); // error
            }
        }

        private void clearTimeTimer_Tick(object sender, EventArgs e)
        {
            float clearTime = ReadWritingMemory.ReadFloat("sh3", 0x70E66F4); // read the current clear time
            TimeSpan timePast = TimeSpan.FromSeconds(clearTime); // convert the clear time so that it is easy to display

            gameClearTimeText.Text = timePast.Hours.ToString() + "H " + timePast.Minutes.ToString() + "M " + timePast.Seconds.ToString() + "S"; // display the clear time
        }

        private void button7_Click(object sender, EventArgs e)
        {
            float total = (float.Parse(hoursBox.Text) * 3600) + (float.Parse(minutesBox.Text) * 60) + (float.Parse(secondsBox.Text)); // add the new clear time values to a total 
            ReadWritingMemory.WriteFloat("sh3", 0x70E66F4, total); // write the new values of the clear time to memory
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Changelog changelog = new Changelog();
            changelog.Show();
        }
    }
}
