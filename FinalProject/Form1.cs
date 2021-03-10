using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        // Variable declarations
        private string difficulty = "";                                                // when radio button 1,2,3 is clicked this stores the choice
        private string operation = "";                                                 // when operation +,-,* is clicked, this stores the choice
        private int correctAnswers, totalAnswers;                                      // these are the student's score for the quiz                                     
        private int q1_FirstNum, q1_SecondNum;                                         // Question 1 variables
        private int q2_FirstNum, q2_SecondNum;                                         // Question 2 variables
        private int q3_FirstNum, q3_SecondNum;                                         // Question 3 variables

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide label name from student
            label1.Text = "";                                                           // this is where the question text goes
            label2.Text = "";
            label3.Text = "";

            label4.Text = "";                                                          // this is where the result text goes
            label5.Text = "";
            label6.Text = "";

            Result.Text = "";                                                          // this is where the score goes

            correctAnswers = 0;
            totalAnswers = 0;
        }

        private void button1_Click(object sender, EventArgs e)                         // Generate quiz button method
        {
           if (difficulty == "" || operation == "")                                    // if no difficulty or operation is chosen, exit
            {
                return;
            }
                     
            // get the random numbers
            Random rndm = new Random();                                                 // this begins the random number genertor
            buildRandomNumber(ref q1_FirstNum, ref q1_SecondNum, difficulty, rndm);
            buildRandomNumber(ref q2_FirstNum, ref q2_SecondNum, difficulty, rndm);
            buildRandomNumber(ref q3_FirstNum, ref q3_SecondNum, difficulty, rndm);

            // build the questions
            label1.Text = buildQuestion(q1_FirstNum, q1_SecondNum, operation);
            label2.Text = buildQuestion(q2_FirstNum, q2_SecondNum, operation);
            label3.Text = buildQuestion(q3_FirstNum, q3_SecondNum, operation);

            // clear out previous results
            label4.Text = "";   // question 1 result
            label5.Text = "";
            label6.Text = "";
        }

        private void buildRandomNumber(ref int num1, ref int num2, string difficulty, Random rnd)   // 'ref' to change the original number to a random one
        {
            if (difficulty == "OneDidgit")                                             // generates two single didgit numbers for quiz
            {
                num1 = rnd.Next(0, 9);
                num2 = rnd.Next(0, 9);
            }

            else if (difficulty == "TwoDidgit")                                        // generates two double didgit numbers for quiz
            {
                num1 = rnd.Next(10, 99);
                num2 = rnd.Next(10, 99);
            }

            else if (difficulty == "ThreeDidgit")                                      // generates two triple didgit numbers for quiz
            {
                num1 = rnd.Next(100, 999);
                num2 = rnd.Next(100, 999);
            }
        }

        private string buildQuestion(int num1, int num2, string operation)              // builds a question for the quiz
        {
            string question = num1.ToString() + " " + operation + " " + num2.ToString() + " =";
            return question;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)            // one-didgit button
        {
            difficulty = "OneDidgit";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)            // two-didgit button
        {
            difficulty = "TwoDidgit";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)            // three-didgit button
        {
            difficulty = "ThreeDidgit";
        }

        private void button3_Click(object sender, EventArgs e)                          // check answers button method
        {
            int totalCorrect = 0;                                                       // updated every time they get an answer correct

            if (answer1.Text == computeAnswer(q1_FirstNum, q1_SecondNum, operation).ToString())
            {
                label4.Text = " Correct"; totalCorrect++;
            }
            else label4.Text = " Incorrect";

            if (answer2.Text == computeAnswer(q2_FirstNum, q2_SecondNum, operation).ToString())
            {
                label5.Text = " Correct"; totalCorrect++;
            }
            else label5.Text = " Incorrect";

            if (answer3.Text == computeAnswer(q3_FirstNum, q3_SecondNum, operation).ToString())
            {
                label6.Text = " Correct"; totalCorrect++;
            }
            else label6.Text = " Incorrect";

            correctAnswers += totalCorrect;              // accumulates answer
            totalAnswers += 3;                          // accumulates total questions done

            Result.Text = "Your score is " + correctAnswers.ToString() + " / " + totalAnswers.ToString();
        }

        private int computeAnswer(int num1, int num2, string op)                        // computes the answer to the question
        {
            if (op == "+")
            {
                return num1 + num2;
            }

            if (op == "-")
            {
                return num1 - num2;
            }

            if (op == "*")
            {
                return num1 * num2;
            }

            return 0;                                                                       // gets rid of red line 
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)                // '+' button 
        {
            operation = "+";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)                // '-' button
        {
            operation = "-";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)                // '*' button
        {
            operation = "*";
        }
    }
}
