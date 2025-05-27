/*  Created by: 
 *  Project: Brick Breaker
 *  Date: 
 */ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown;

        // Game values
        Random Randgen = new Random();
        public static int lives, slimex, slimey;
        int count;
        int powerupchance;
        int poweruptype;
        int level;

        // Paddle and Ball objects
        public static Paddle paddle;
        Ball ball;

        // list of all blocks for current level
        public List<Block> blocks = new List<Block>();

        // list of powerup balls that has a chance to spawn when a block is hit
        public static List<Ball> powerupballs = new List<Ball>();

        // Brushes
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void OnStart()
        {
            //set life counter
            lives = 3;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;
            
            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 20;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 40;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            #region Creates blocks for generic level. Need to replace with code that loads levels.

            //TODO - replace all the code in this region eventually with code that loads levels from xml files

            // if (blocks == null)
            //{
            //ExtractLevel(1);

            // level++;
            // }



            blocks.Clear();
            int x = 10;

            while (blocks.Count < 12)
            {
                x += 57;
                Block b1 = new Block(x, 10, 1, "White");
                blocks.Add(b1);
            }

            #endregion

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Escape:
                    this.FindForm().Close();
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }
            
            // Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;


                if (lives == 0)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    blocks.Remove(b);

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }

                    powerupchance = Randgen.Next(0, 101);

                    if (powerupchance <= 75)
                    {
                        Ball pub = new Ball(b.x+20, b.y, 0, 200, 20);
                        powerupballs.Add(pub);
                    }

                    break;
                }
            }

            // makes the powerup ball fall and checks if the powerup ball has been hit yet, if it has then it chooses a random powerup
            foreach (Ball pub in powerupballs)
            {
                pub.y++;

                if (pub.LuckCollision(paddle))
                {
                    // int powerupselect = Randgen.Next(1, 11);
                    int powerupselect = 6;

                    if (powerupselect == 1)
                    {
                        Powerups.Speed_I(paddle);
                    }
                    else if (powerupselect == 2)
                    {
                        Powerups.Speed_II(paddle);
                    }
                    else if (powerupselect == 3)
                    {
                        Powerups.Speed_III(paddle);
                    }
                    else if (powerupselect == 4)
                    {
                        Powerups.Golden_Carrot();
                    }
                    else if (powerupselect == 5)
                    {
                        Powerups.Golden_Apple();
                    }
                    else if (powerupselect == 6)
                    {
                        slimex = pub.x;
                        slimey = pub.y;

                        Powerups.Slime();
                    }
                    else if (powerupselect == 7)
                    {
                        Powerups.stonetool();
                    }
                    else if (powerupselect == 8)
                    {
                        Powerups.irontool();
                    }
                    else if (powerupselect == 9)
                    {
                        Powerups.diamondtool();
                    }
                    else if (powerupselect == 10)
                    {
                        Powerups.netheritetool();
                    }

                    powerupballs.Remove(pub);
                    break;
                }
            }

            foreach (Ball eb in Powerups.extraballs)
            {
                eb.Move();

                eb.WallCollision(this);

                if (eb.BottomCollision(this))
                {
                    Powerups.extraballs.Remove(eb);
                    break;
                }
            }

            //Check if ball is pushed out of bounds, reset ball
            if (ball.PushedOutOfBounds(paddle, this))
            {

            }

            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();
            
            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void ExtractLevel(int level)
        {
            XmlReader reader = XmlReader.Create($"level{level}.xml");

            while (reader.Read())
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        int x = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("y");
                        int y = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("hp");
                        int hp = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("bType");
                        string bType = reader.ReadString();

                        Block b = new Block(x, y, hp, bType);
                        blocks.Add(b);
                    }
                }
                reader.Close();

            }
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            whiteBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(whiteBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.FillRectangle(redBrush, b.x, b.y, b.width, b.height);
            }

            // Draws power up balls
            foreach (Ball pub in powerupballs)
            {
                e.Graphics.FillRectangle(redBrush, pub.x, pub.y, pub.size, pub.size);
            }

            // Draws extra balls from power up
            foreach (Ball eb in Powerups.extraballs)
            {
                e.Graphics.FillRectangle(whiteBrush, eb.x, eb.y, eb.size, eb.size);
            }

            // Draws ball
            e.Graphics.FillRectangle(whiteBrush, ball.x, ball.y, ball.size, ball.size);

            //Draws hearts

            Rectangle heartBox1 = new Rectangle(25, 25, 50, 50);
            Rectangle heartBox2 = new Rectangle(25 + 50 + 25, 25, 50, 50);
            Rectangle heartBox3 = new Rectangle(25 + 50 + 25 + 50 + 25, 25, 50, 50);

            switch (lives)
            {
                case 3:
                    e.Graphics.FillRectangle(whiteBrush, heartBox1);
                    e.Graphics.FillRectangle(whiteBrush, heartBox2);
                    e.Graphics.FillRectangle(whiteBrush, heartBox3);
                    break;
                case 2:
                    e.Graphics.FillRectangle(whiteBrush, heartBox1);
                    e.Graphics.FillRectangle(whiteBrush, heartBox2);
                    break;
                case 1:
                    e.Graphics.FillRectangle(whiteBrush, heartBox1);
                    break;

            }
        }
    }
}
