using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        PictureBox[] GamePole = new PictureBox[9];
        int Player = 0, Computer = 0;

        int[] GamePoleMap = {0,0,0,
                             0,0,0,
                             0,0,0 };
 
        string[] ImgName =
        {
            "Blank.png",
            "X.png",
            "O.png"
        };
        public Form1()
        {
            InitializeComponent();
        }
        void MainPole()
        {
            int DX = 0, DY = 0;
            int HeighP = 100, 
                WhidthP = 100,
                IndexPicture = 0;
            string NAME = "P_";

            for (int YY = 0; YY < 3; YY++)
            {
                for (int XX = 0; XX < 3; XX++)
                {
                    GamePole[IndexPicture] = new PictureBox()
                    {
                        Name = NAME + IndexPicture,                 
                        Height = HeighP,                           
                        Width = WhidthP,                           
                        Image = Image.FromFile("Blank.png"),        
                        SizeMode = PictureBoxSizeMode.StretchImage,  
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(DX, DY)
                    };

                    GamePole[IndexPicture].Click += Picture_Click;

                    panel2.Controls.Add(GamePole[IndexPicture]);  
                    IndexPicture++;

                    DX += WhidthP;  
                }
                DY += HeighP; 
                DX = 0; 
            }
        }
        private void Picture_Click(object sender, EventArgs e)
        {
            if (CanStap())
            {
                PictureBox ClickImage = sender as PictureBox;
                string[] ParsName = ClickImage.Name.Split('_');

                int IndexSelectImage = Convert.ToInt32(ParsName[1]);

                GamePole[IndexSelectImage].Image = Image.FromFile(ImgName[Player]);
                GamePoleMap[IndexSelectImage] = Player;

                if (!TestWin(Player))
                {
                    LoockPole();
                    PC_Step();
                    UnLoockPole();
                }
                else
                {
                    panel3.Visible = true;
                    label3.Text = "Ви виграли!";
                    LoockPole();
                }
            }
        }
        void LoockPole()
        {
            foreach (PictureBox P in GamePole) P.Enabled = false;
        }
        void UnLoockPole()
        {
            int Indexx = 0;
            foreach (PictureBox P in GamePole)
            {
                if (GamePoleMap[Indexx++] == 0) P.Enabled = true;
            }
        }
        bool CanStap()
        {
            foreach (int s in GamePoleMap) 
                if (s == 0) return true;
 
            if (TestWin(Player))
            {
                label3.Text = "Ви виграли";
                LoockPole();
                panel3.Visible = true; 
                return false;
            } 
            if (TestWin(Computer))
            {
                label3.Text = "Ви програли";
                panel3.Visible = true; 
                LoockPole();
                panel3.Visible = false;
                return false;
            }
             
            label3.Text = "Нічия"; 

            panel3.Visible = true;
            LoockPole();

            return false;
        }
        bool TestWin(int WHO)
        { 
            int[,] WinVariant =
            {      {    
                    1,1,1,   
                    0,0,0,  
                    0,0,0   
                }, {    
                    0,0,0,  
                    1,1,1,  
                    0,0,0   
                }, {     
                    0,0,0,  
                    0,0,0,  
                    1,1,1   
                }, {    
                    1,0,0,  
                    1,0,0,  
                    1,0,0    
                }, {    
                    0,1,0,  
                    0,1,0,   
                    0,1,0    
                }, {     
                    0,0,1,  
                    0,0,1,  
                    0,0,1   
                }, {     
                    1,0,0,   
                    0,1,0,   
                    0,0,1    
                }, {     
                    0,0,1,    
                    0,1,0,    
                    1,0,0    
                }
            };
             
            int[] TestMap = new int[GamePoleMap.Length];
            for (int I = 0; I < GamePoleMap.Length; I++) 
                if (GamePoleMap[I] == WHO) TestMap[I] = 1;
             
            for (int Variant_Index = 0; Variant_Index < WinVariant.GetLength(0); Variant_Index++)
            { 
                int WinState = 0;
                for (int TestIndex = 0; TestIndex < TestMap.Length; TestIndex++)
                { 
                    if (WinVariant[Variant_Index, TestIndex] == 1)
                    { 
                        if (WinVariant[Variant_Index, TestIndex] == TestMap[TestIndex]) WinState++;
                    } 
                    if (WinState == 3) return true;
                } 
            } 
            return false; 
        }
        void PC_Step()
        { 
            Random Rand = new Random();
        GENER: 
            if (CanStap())
            { 
                int IndexStep = Rand.Next(0, 8); 
                if (GamePoleMap[IndexStep] == 0)
                { 
                    GamePole[IndexStep].Image = Image.FromFile(ImgName[Computer]); 
                    GamePoleMap[IndexStep] = Computer; 
                }
                else goto GENER;
                if (TestWin(Computer))
                {
                    panel3.Visible = true;
                    label3.Text = "Ви Програли";
                }
            }
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            MainPole();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Player = 2;
            Computer = 1;
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Player = 1;
            Computer = 2;
            panel1.Visible = false;
            panel2.Visible = true;
        }
    }
}
