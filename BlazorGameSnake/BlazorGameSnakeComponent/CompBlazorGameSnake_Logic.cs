using BlazorGameSnakeComponent.Classes;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorGameSnakeComponent
{
    public class CompBlazorGameSnake_Logic : BlazorComponent
    {

        bool IsPageLoaded = false;

     
        public bool input_Is_Bot_Mode
        {
            get
            {
                return Game.Is_Bot_Mode;
            }
            set
            {
                Game.Is_Bot_Mode = value;
                run();
            }

        }



        public bool input_Is_Enable_Audio
        {

            get
            {
                return Game.Is_Enabled_Audio;
            }
            set
            {

                Game.Is_Enabled_Audio = value;
                run();

            }

        }




      
        public bool Are_Borders_Open
        {

            get
            {
                return Game.Are_Borders_Open;
            }


            set
            {
               
                Game.Are_Borders_Open = value;
                run();

            }

        }


        public int input_speed
        {

            get
            {
                return LocalData.global_speed;
            }


            set
            {

                LocalData.global_speed = value;
                run();

            }

        }


        public int input_X_Count
        {

            get
            {
                return Game.x_Length;
            }


            set
            {

                Game.x_Length = value;
                Game.y_Length = input_Y_Count;
                Game.points_Count = Game.x_Length * Game.y_Length;
                Game.point_Width = Math.Round(LocalData.CompWidth * 1.0 / Game.x_Length, 2);
                Game.point_Height = Math.Round(LocalData.CompHeight * 1.0 / Game.y_Length, 2);

                run();

            }

        }

      

        public int input_Y_Count
        {

            get
            {
                return Game.y_Length;
            }


            set
            {
                Game.y_Length = value;
                Game.points_Count = Game.x_Length * Game.y_Length;
                Game.point_Width = Math.Round(LocalData.CompWidth * 1.0 / Game.x_Length, 2);
                Game.point_Height = Math.Round(LocalData.CompHeight * 1.0 / Game.y_Length, 2);

                run();

            }

        }



        public int walls_count
        {
            get
            {
                return LocalData.walls_count;
            }
            set
            {
                LocalData.walls_count = value;
                run();
            }
        }

        public int walls_min_length
        {
            get
            {
                return LocalData.walls_min_length;
            }
            set
            {
                LocalData.walls_min_length = value;
                run();
            }
        }

        public int walls_max_length
        {
            get
            {
                return LocalData.walls_max_length;
            }
            set
            {
                LocalData.walls_max_length = value;
                run();
            }
        }


        public string CurrPoint = "Point 0";

        protected override void OnInit()
        {
            Game.points_Count = Game.x_Length * Game.y_Length;
            Game.point_Width = Math.Round(LocalData.CompWidth * 1.0 / Game.x_Length, 2);
            Game.point_Height = Math.Round(LocalData.CompHeight * 1.0 / Game.y_Length, 2);
            Game.reset();
        }

        public void run()
        {

            if (IsPageLoaded)
            {
                Game.reset();
                StateHasChanged();
                LocalData.Curr_Comp_Board.Refresh();
                LocalData.Curr_Comp_Walls.Refresh();
            }
        }


        protected override void OnAfterRender()
        {

            LocalData.Curr_comp = this;


            base.OnAfterRender();


            IsPageLoaded = true;
        }


        public void Refresh()
        {
            StateHasChanged();
        }


        public void input_X_Count_onchange()
        {
            run();

        }

        public void input_Y_Count_onchange()
        {
            run();
        }


        public void KeyUpFromJS1(int e)
        {
           
            if (Game.Is_Enabled)
            {
                if (!Game.Is_Bot_Mode)
                {

                    ConsoleKey consoleKey = (ConsoleKey)Enum.Parse(typeof(ConsoleKey), e.ToString());


                    switch (consoleKey)
                    {

                        case ConsoleKey.DownArrow:
                            if (LocalData.Curr_Direction != DirectionType.Up)
                            {
                                LocalData.Curr_Direction = DirectionType.Down;
                            }
                            if (!Game.Is_Started)
                            {
                                Game.start();
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (LocalData.Curr_Direction != DirectionType.Left)
                            {
                                LocalData.Curr_Direction = DirectionType.Right;
                            }
                            if (!Game.Is_Started)
                            {
                                Game.start();
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (LocalData.Curr_Direction != DirectionType.Down)
                            {
                                LocalData.Curr_Direction = DirectionType.Up;
                            }
                            if (!Game.Is_Started)
                            {
                                Game.start();
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (LocalData.Curr_Direction != DirectionType.Right)
                            {
                                LocalData.Curr_Direction = DirectionType.Left;
                            }
                            if (!Game.Is_Started)
                            {
                                Game.start();
                            }
                            break;
                        default:
                            break;
                    }

                }
            }

        }


        public void Dispose()
        {

        }

    }
}
