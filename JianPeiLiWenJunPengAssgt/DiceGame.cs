/*Name: JianPeiLi C3343365
 * WenJunPeng C3343363
 * Date: 24.3.2020
 * Discription:
 * This applicaption is a game of dice name as Six Of One which can play with 2 players or play with computer
 * the dice can be roll from one to six dice depending on plyer preference. the computer will roll dice automatilly 
 * base on the situation of the goal score.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JianPeiLiWenJunPengAssgt
{
    public partial class DiceGame : System.Windows.Forms.Form
    {
        // the initial value of starting dice number, seu up score, the current both player score
        //two of the arrays for the dice picturebox, win times count and the computer model boolean value.
        int diceNum1 = 6, diceNum2 = 6;
        int setScore = 0;
        int play1Score = 0, play2Score = 0;
        Graphics[] gra1 = new Graphics[6];
        Graphics[] gra2 = new Graphics[6];
        int Player1Win = 0,Player2Win = 0;
        int currentS1 = 0, currentS2 = 0;
        int P1W = 0, C1W = 0;
        Boolean Comp = false;

        public DiceGame()
        {
            InitializeComponent();
            //set up the graphices for dice in picturebox
            gra1[0] = pbxPp1.CreateGraphics();
            gra1[1] = pbxPp2.CreateGraphics();
            gra1[2] = pbxPp3.CreateGraphics();
            gra1[3] = pbxPp4.CreateGraphics();
            gra1[4] = pbxPp5.CreateGraphics();
            gra1[5] = pbxPp6.CreateGraphics();
            gra2[0] = pbxCp1.CreateGraphics();
            gra2[1] = pbxCp2.CreateGraphics();
            gra2[2] = pbxCp3.CreateGraphics();
            gra2[3] = pbxCp4.CreateGraphics();
            gra2[4] = pbxCp5.CreateGraphics();
            gra2[5] = pbxCp6.CreateGraphics();
        }

        private void btnP1Roll_Click(object sender, EventArgs e)
        {
            //reset some value before every click
            Player1Win = 0;
            lblPRes.Text = "";
            //switch two button visible or not
            if(Comp != true)
            {
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = true;
            }
            //make a dice array
            Dice[] dice1 = new Dice[diceNum1];
            //clear dice picture every time
            for (int n = 0; n < 6; n++)
            {
                clearDice(gra1[n]);
            }
            
            //for show the dice in picturebox
            for (int n = 0; n < 5; n++)
            {
                for (int c = 0; c < diceNum1; c++)
                {
                    //clear picturebox, call a roll function from dice Class and do sleep event after each dice roll 
                    dice1[c] = new Dice();
                    clearDice(gra1[c]);
                    dice1[c].Roll();
                    System.Threading.Thread.Sleep(5);
                    Application.DoEvents();
                    drawDice(gra1[c], dice1[c].DValue);
                }
                System.Threading.Thread.Sleep(200);
                Application.DoEvents();
            }

            //call for calculate the result of this turn roll
            countResult(dice1, ref play1Score, ref Player1Win,ref currentS1);
            //display the result for this click event 
            //Show the current score for player1
            tbxOutputP1.Text = currentS1.ToString();
            lblP1.Text = play1Score.ToString();
            //decide who win or lose the game  
            if (Player1Win == 3)
            {
                lblWinner.Text = "Sorry "+ tbxStartP.Text +" Lose !" + "";
                C1W += 1;
                //end of the game button control hands of
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
                lblPRes.Text = "Dead drop!";
            }
            else if (Player1Win == 4)
            {
                lblWinner.Text = "Congratulation! "+tbxStartP.Text +" Win!" + "";
                P1W += 1;
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
                lblPRes.Text = "Boojum!";
            }
            else if (play1Score >= setScore)
            {
                lblWinner.Text = "Congratulation! "+tbxStartP.Text +" Win!" + "";
                P1W += 1;
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
            }
            lblP1W.Text = P1W + "";
            //For computer model which do event for the first player
            if (Comp == true && (setScore > play1Score && Player1Win != 4 && Player1Win != 3))
            {
                System.Threading.Thread.Sleep(1000);
                Application.DoEvents();
                PlayGme(countDrop());
            }
            //show game result in the one's condition
            if(Player1Win == 1)
            {
                lblPRes.Text = "No Score turn!";
            }
            else if(Player1Win == 2)
            {
                lblPRes.Text = "snake's eyes!";
            }
        }

        public void clearDice(Graphics d)
        {
            //clear up the picturebox list
            d.Clear(Color.White);
        }

        //Draw dice face function
        public void drawDice(Graphics d, int num)
        {
            //draw dice one face
            if (num == 1)
            {
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 2) - 10, (pbxPp1.Height / 2) - 10, 20, 20);
            }
            //draw dice two face
            else if (num == 2)
            {
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
            }
            //draw dice three face
            else if (num == 3)
            {
                d.FillEllipse(Brushes.Red, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 2) - 10, (pbxPp1.Height / 2) - 10, 20, 20);
            }
            //draw dice four face
            else if (num == 4)
            {
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
            }
            //draw dice five face
            else if (num == 5)
            {
                d.FillEllipse(Brushes.Red, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, pbxPp1.Width - (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Red, (pbxPp1.Width / 2) - 10, (pbxPp1.Height / 2) - 10, 20, 20);
            }
            //draw dice six face
            else
            {
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, pbxPp1.Height - (pbxPp1.Height / 4) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 2) - 10, 20, 20);
                d.FillEllipse(Brushes.Blue, pbxPp1.Width - (pbxPp1.Width / 4) - 10, (pbxPp1.Height / 2) - 10, 20, 20);
            }
        }

        //radio check button for player1
        private void btnPRoll_Click(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            if (rad.Checked) diceNum1 = Convert.ToInt32(rad.Text);
        }
        
        //function that calculate the result of the dice roll 
        private void countResult(Dice[] d, ref int sco, ref int Player,ref int current)
        {
            //local variable that for this function
            int score = 0;
            int oneCount = 0;
            int two = 0, three = 0, four = 0, five = 0, six = 0;
            //count score
            for (int e = 0; e < d.Length; e++) score += d[e].DValue;
            //count One in dice
            for (int c = 0; c < d.Length; c++) if (d[c].DValue == 1) oneCount++;
            //count others in dice
            for (int c = 0; c < d.Length; c++)
            {
                if (d[c].DValue == 2) two++;
                else if (d[c].DValue == 3) three++;
                else if (d[c].DValue == 4) four++;
                else if (d[c].DValue == 5) five++;
                else if (d[c].DValue == 6) six++;
            }
            //check dice's one condition
            if (oneCount == 1)
            {
                current = score = 0;
                Player = 1;
            }
            else if (oneCount == 2)
            {
                current = score = 0;
                sco = 0;
                Player = 2;
            }
            else if (oneCount == 3) Player = 3;
            else if (oneCount >= 4) Player = 4;
            //check double score condition
            else if ((two >= 3 || three >= 3 || four >= 3 || five >= 3 || six >= 3) && oneCount != 1) current = score *= 2;
            else current = score;
            sco += current;
        }

        ////radio check button for player2
        private void btnRoll_Click2(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            if (rad.Checked) diceNum2 = Convert.ToInt32(rad.Text);
        }

        //player2 roll dice and all the other functionality been put into a function
        //for reuse purpose when doing the computer model
        private void PlayGme(int numDice)
        {
            //reset some value before every click
            Player2Win = 0;
            lblCRes.Text = "";
            Dice[] dice2 = new Dice[numDice];
            //clear up the picture before roll
            for (int n = 0; n < 6; n++)
            {
                clearDice(gra2[n]);
            }
            //roll and draw the dice for player2 
            for (int n = 0; n < 5; n++)
            {
                for (int c = 0; c < numDice; c++)
                {
                    dice2[c] = new Dice();
                    clearDice(gra2[c]);
                    dice2[c].Roll();
                    System.Threading.Thread.Sleep(5);
                    Application.DoEvents();
                    drawDice(gra2[c], dice2[c].DValue);
                }
                System.Threading.Thread.Sleep(200);
                Application.DoEvents();
            }
            //calculate the result of the dice that player2 rolled
            countResult(dice2, ref play2Score, ref Player2Win, ref currentS2);
            //show the result of this roll
            tbxOutPutC1.Text = currentS2.ToString();
            lblP2.Text = play2Score.ToString();
            if (Player2Win == 3)
            {
                lblWinner.Text = "Sorry "+tbxStartC.Text +" Lose !" + "";
                P1W += 1;
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
                lblCRes.Text = "Dead drop!";
            }
            else if (Player2Win == 4)
            {
                lblWinner.Text = "Congratulation! " + tbxStartC.Text + " Win!" + "";
                C1W += 1;
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
                lblCRes.Text = "Boojum";
            }
            else if (play2Score >= setScore)
            {
                lblWinner.Text = "Congratulation! " + tbxStartC.Text + " Win!" + "";
                C1W += 1;
                btnP1Roll.Visible = false;
                btnC1Roll.Visible = false;
                btnNext.Visible = true;
            }
            lblC1W.Text = C1W + "";
            //show game result in special one condition
            if (Player2Win == 1)
            {
                lblCRes.Text = "No Score turn!";
            }
            else if (Player2Win == 2)
            {
                lblCRes.Text = "snake's eyes!";
            }
        }

        //Computer model for calculate the dice that should be roll
        private int countDrop()
        {
            //first roll will be six dice 
            if (play2Score == 0)
            {
                return 4;
            }
            else
            {
                // depending one the goal score and current score difference to roll dice
                int cur = setScore - play2Score;
                int[] dropNum = new int[6] {6,12,18,24,30,36};
                if (cur > dropNum[5]) return 6;
                else if (cur < dropNum[5] && dropNum[4] <= cur) return 6;
                else if (cur < dropNum[4] && dropNum[3] <= cur) return 5;
                else if (cur < dropNum[3] && dropNum[2] <= cur) return 4;
                else if (cur < dropNum[2] && dropNum[1] <= cur) return 3;
                else if (cur < dropNum[1] && dropNum[0] <= cur) return 2;
                else return 1;
            }
        }

        //button to show start game and image
        private void btnStart_Click(object sender, EventArgs e)
        {
            if(tbxStartC.Text != "" && tbxStartP.Text != "")
            {
                tbxStartC.Enabled = false;
                tbxStartP.Enabled = false;
                btnStart.Visible = false;
                pbxImg.Visible = false;
            }
        }

        //player2 click event 
        private void btnC1Roll_Click(object sender, EventArgs e)
        {
            //switch from player1 and player2
            btnC1Roll.Visible = false;
            btnP1Roll.Visible = true;
            PlayGme(diceNum2);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //set everything to restart
            setScore = 0; play1Score = 0; play2Score = 0; Player1Win = 0;
            Player2Win = 0; currentS1 = 0; currentS2 = 0; P1W = 0; C1W = 0;
            lblP1.Text = play1Score.ToString();
            lblP2.Text = play2Score.ToString();
            lblP1W.Text = P1W + "";
            lblC1W.Text = C1W + "";
            tbxOutputP1.Text = "";
            tbxOutPutC1.Text = "";
            tbxScore.Text = "";
            lblWinner.Text = "Please enter Goal Score!";
            rbtnCom.Visible = true;
            rbtnPer.Visible = true;
            tbxScore.Enabled = true;
            tbxStartC.Enabled = true;
            tbxStartP.Enabled = true;
            btnStart.Visible = true;
            pbxImg.Visible = true;
            btnC1Roll.Visible = false;
            btnP1Roll.Visible = false;
            btnNext.Visible = false;
            for (int n = 0; n < 6; n++)
            {
                clearDice(gra1[n]);
                clearDice(gra2[n]);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //reset everything except the win times for both side
            setScore = 0; play1Score = 0; play2Score = 0; Player1Win = 0;
            Player2Win = 0; currentS1 = 0; currentS2 = 0;
            lblP1.Text = play1Score.ToString();
            lblP2.Text = play2Score.ToString();
            tbxOutputP1.Text = "";
            tbxOutPutC1.Text = "";
            tbxScore.Enabled = true;
            btnNext.Visible = false;
            for (int n = 0; n < 6; n++)
            {
                clearDice(gra1[n]);
                clearDice(gra2[n]);
            }
        }

        //check for player model or computer model
        private void rbtnCom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCom.Checked == true)
            {
                Comp = true;
            }
            else Comp = false;
        }

        //mouseLeave for set up the score to play 
        private void tbxScore_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                setScore = Convert.ToInt32(tbxScore.Text);
                if (Comp != true)
                {
                    Random r = new Random();
                    int ran = r.Next(1, 3);
                    if (ran == 1) btnP1Roll.Visible = true;
                    else btnC1Roll.Visible = true;
                    rbtnCom.Visible = false;
                }
                else
                {
                    btnP1Roll.Visible = true;
                    rbtnPer.Visible = false;
                }
                lblWinner.Text = " ";
                tbxScore.Enabled = false;
            }
            //catch the invalid set score and prompt up a messageBox
            catch
            {
                MessageBox.Show("Invalid score!" + "\r\n" );
            }
        }

    }
}
