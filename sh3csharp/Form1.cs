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

        public long chosenFov;

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

        private void button7_Click(object sender, EventArgs e) // BUG! - values above 24 hours will be treated with modulus. For example 25 hours = 1 hour.
        {
            float total = (float.Parse(hoursBox.Text) * 3600) + (float.Parse(minutesBox.Text) * 60) + (float.Parse(secondsBox.Text)); // add the new clear time values to a total 
          
            //float total = Convert.ToSingle((Convert.ToDecimal(hoursBox.Text) * 3600) + (Convert.ToDecimal(minutesBox.Text) * 60) + (Convert.ToDecimal(secondsBox.Text))); alternative
            ReadWritingMemory.WriteFloat("sh3", 0x70E66F4, total); // write the new values of the clear time to memory
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Changelog changelog = new Changelog();
            changelog.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set default FOV
            chosenFov = 4123149057 + (224 - 1);
            SetFov();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Broken, please don't reactivate. Will crash SH3.
            //ReadWritingMemory.WriteXBytes("sh3", 0x8984E0 + 0x0000011C, "20");
        }

        private void sliderFov_Scroll(object sender, EventArgs e)
        {
            fovTextbox.Text = sliderFov.Value.ToString();
        }

        private void fovTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int parsedValue = Int32.Parse(fovTextbox.Text);
                if (parsedValue >= 0 && parsedValue <= 255)
                { 
                    sliderFov.Value = Int32.Parse(fovTextbox.Text);
                }
                else
                {
                    fovTextbox.Text = "224";
                    MessageBox.Show("Value of FOV should be between 0 and 255");
                }
            }
            catch
            {
                fovTextbox.Text = "224";
                MessageBox.Show("Value of FOV should be between 0 and 255");
            }
        }

        private void fovTimer_Tick(object sender, EventArgs e)
        {
            long receivedValue = ReadWritingMemory.ReadLong("sh3", 0x0712C722);
            long fov = receivedValue - 4123149057 + 1;
            currentFovBox.Text = fov.ToString();
            SetFov();
        }

        private void setFovButton_Click(object sender, EventArgs e)
        {
            int requestedValue = sliderFov.Value;
            long fovToWrite = 4123149057 + (requestedValue - 1);
            chosenFov = fovToWrite;
        }

        public void SetFov()
        {
            ReadWritingMemory.WriteLong("sh3", 0x0712C722, chosenFov);
        }

        private void currentFovBox_TextChanged(object sender, EventArgs e)
        {

        }

        // Default FOV
        private void button10_Click(object sender, EventArgs e)
        {
            chosenFov = 4123149057 + (224 - 1);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        // FOV Override Box
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                fovTimer.Start();
            }
            else
            {
                fovTimer.Stop();
            }
        }

        private void button11_Click(object sender, EventArgs e) // change Heather into a spawn of satan.
        {
            Random r = new Random();
            int randomByte = r.Next(0, 255);
            ReadWritingMemory.WriteXBytes("sh3", 0x0089875C, randomByte.ToString());
        }

        
    }
}
