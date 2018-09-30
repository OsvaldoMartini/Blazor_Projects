﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlazorClockCanvasComponent.Classes;
using Microsoft.AspNetCore.Blazor.Components;


namespace BlazorClockCanvasComponent
{
    public class Component1_Logic : BlazorComponent
    {

        public event EventHandler onDelete;

        public bool IsSubscribedForOnDelete = false;


        #region Properties
        //[Parameter] public Action<int> OnRemoveComponent { get; set; }

        [Parameter]
        public City city { get; set; }

        bool IsRunningProcess = false;
      

        private LupCanvas canvas_1 = null;

        Timer timer = null;

        public string CurrentTime;


      

        public string _log = string.Empty;
        public string _log2 = string.Empty;

       

        Stopwatch stopwatch_1 = new Stopwatch();

        bool IsPageLoaded = false;

       
        #endregion


        #region Constructors

        public Component1_Logic()
        {

           

        }



        #endregion

       

        #region Methods


        protected async override void OnAfterRender()
        {
           
            if (!IsPageLoaded)
            {
                IsPageLoaded = true;


               
                await Preload_Image();


                //FastMode = city.FastMode;
                //FastMode_Increment = city.FastMode_Increment;
                //timerInterval = city.TimerInterval;




                canvas_1 = new LupCanvas(city.CanvasID, city.BgCanvasID, city.TopCanvasID);

                canvas_1.Add_Canvas();

                


                stopwatch_1.Start();


                ClockSettings.radius_2_Times = city.WidthAndHeight;
                ClockSettings.radius_Origin = ClockSettings.radius_2_Times / 2;
                ClockSettings.radius_90_Percent = (float)(ClockSettings.radius_Origin * 0.9);


                

                Cmd_Draw_BG_Canvas();


               

                Cmd_Draw_Top_Canvas();
            

                canvas_1.Swith_To_Canvas(ClockCanvasType.Middle);

                canvas_1.SB_Append(" ", true);
                canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineCap.ToString(), LineCap.Round.ToString().ToLower()));
                canvas_1.SetProperty(new TransferCanvasProperty("fillStyle", ClockSettings.Clock_Center_Small_Point_Color));
                canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.textBaseline.ToString(), TextBaseline.Middle.ToString().ToLower()));
                canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.textAlign.ToString(), TextAlign.Center.ToString().ToLower()));

                canvas_1.SB_Execute();

                Cmd_Draw_Clock();
                this.StateHasChanged();



                if (!ClockSettings.OnlyOneDraw)
                {

                    if (ClockSettings.SecondOnlyOneStep)
                    {
                        timer = new Timer(TimeCallBack, null, 0, 1000);
                    }
                    else
                    {
                        timer = new Timer(TimeCallBack, null, 0, ClockSettings.timerInterval);
                    }

                }
            }



        }


        void Cmd_Draw_BG_Canvas()
        {
            canvas_1.Swith_To_Canvas(ClockCanvasType.Background);

            canvas_1.SB_Append(" ", true);
            canvas_1.ClearCanvas();

            canvas_1.DrawFullSizeRect(ClockSettings.BG_Color);


            canvas_1.CreateRadialGradient(new TransferRadialGradientParameters(ClockSettings.radius_Origin, ClockSettings.radius_Origin, ClockSettings.radius_Origin, ClockSettings.radius_Origin, ClockSettings.radius_Origin, ClockSettings.radius_Origin * 1.05));
            canvas_1.GradientAddColorStop(0, ClockSettings.Clock_Border_Color);
            canvas_1.GradientAddColorStop(1, "LightSkyBlue");
            canvas_1.GradientSetStokeOrFillStyle(true);

            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_Origin * 0.01).ToString()));

            canvas_1.StrokeRect(new TransferRectParameters(0, 0, ClockSettings.radius_Origin * 2, ClockSettings.radius_Origin * 2));



            Draw_Corners();



            canvas_1.SB_Execute();




        }

        void Cmd_Draw_Top_Canvas()
        {
            canvas_1.Swith_To_Canvas(ClockCanvasType.ForeGround);
            canvas_1.SB_Append(" ", true);
            canvas_1.ClearCanvas();


            canvas_1.Translate(ClockSettings.radius_Origin, ClockSettings.radius_Origin);

            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineCap.ToString(), LineCap.Round.ToString().ToLower()));


            drawClockBorder();

            cmd_Draw_60_Lines();

            drawFace();

            canvas_1.SB_Execute();

           
        }



        async Task<string> Preload_Image()
        {
          
            return await JsInterop.Preload_Image();
           
            
        }


        void Draw_Corners()
        {
           

            float wh = (float)(ClockSettings.radius_Origin * 0.6);

            canvas_1.SetTransform();
            canvas_1.Translate(0, 0);
            canvas_1.Draw_Image("cornerShape", new TransferImageParameters(0, 0, wh, wh));

            for (int i = 1; i < 4; i++)
            {
                canvas_1.SetTransform();
                canvas_1.Translate(ClockSettings.radius_Origin, ClockSettings.radius_Origin);
                canvas_1.Rotate((float)(0.5 * i * Math.PI));
                canvas_1.Draw_Image("content/1.png", new TransferImageParameters(-ClockSettings.radius_Origin, -ClockSettings.radius_Origin, wh, wh));
              
            }
        }

        void Cmd_Draw_Clock()
        {


            canvas_1.SB_Append(" ", true);

            ClockSettings.radius_90_Percent = (float)(ClockSettings.radius_Origin * 0.9);


            if (ClockSettings.FastMode)
            {
                ClockSettings.currentCount += ClockSettings.FastMode_Increment;
            }


            Cmd_Log_Time();

            canvas_1.ClearCanvas();

            Cmd_Draw_Clock_Background();

   
            
            cmd_drawTime();
          
            



            canvas_1.RenderToUI();

            canvas_1.SB_Execute();
            
        }


        void Cmd_Log_Time()
        {
            if (ClockSettings.currentCount % 100 == 0)
            {

                _log = "StopWatch: " + stopwatch_1.Elapsed.ToString(@"mm\:ss\.ff");

                stopwatch_1.Restart();
            }
        }

        void Cmd_Draw_Clock_Background()
        {
            canvas_1.SetTransform();
            canvas_1.Translate(ClockSettings.radius_Origin, ClockSettings.radius_Origin);


            canvas_1.Begin_Path();
            canvas_1.SetProperty(new TransferCanvasProperty("fillStyle", "Gainsboro"));
            canvas_1.FillCircle(new TransferParameters(0, 0, (float)(ClockSettings.radius_90_Percent), 0, (float)(2.0 * Math.PI)));
            canvas_1.Fill();


            ClockSettings.radius_90_Percent = (float)(ClockSettings.radius_90_Percent * 0.9);

            canvas_1.Begin_Path();
            canvas_1.SetProperty(new TransferCanvasProperty("fillStyle", ClockSettings.Clock_BG_Color));
            canvas_1.FillCircle(new TransferParameters(0, 0, ClockSettings.radius_90_Percent, 0, (float)(2.0 * Math.PI)));
            canvas_1.Fill();


            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_Origin * 0.01).ToString()));

            canvas_1.SetProperty(new TransferCanvasProperty("strokeStyle", ClockSettings.Clock_Center_Small_Point_Color));
            canvas_1.Stroke();

         
        }


        void drawClockBorder()
        {

            float a = (float)(ClockSettings.radius_90_Percent);


            canvas_1.Begin_Path();
            canvas_1.SetProperty(new TransferCanvasProperty("fillStyle", "Gainsboro"));
            canvas_1.FillCircle(new TransferParameters(0, 0, a, 0, (float)(2.0 * Math.PI)));
            
          

            canvas_1.CreateRadialGradient(new TransferRadialGradientParameters(0,0,a * 0.92,0,0, a * 1.05));
            canvas_1.GradientAddColorStop(0, ClockSettings.Clock_Border_Color);
            canvas_1.GradientAddColorStop(1, ClockSettings.BG_Color);
            canvas_1.GradientSetStokeOrFillStyle(true);

            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_Origin * 0.06).ToString()));

            canvas_1.Stroke();

            canvas_1.SetProperty(new TransferCanvasProperty("strokeStyle", "red"));
        }



        void drawFace()
        {
            canvas_1.SetTransform();
            canvas_1.Translate(ClockSettings.radius_Origin, ClockSettings.radius_Origin);

            canvas_1.Begin_Path();


            float a = (float)(ClockSettings.radius_90_Percent * 0.085);

            canvas_1.CreateRadialGradient(new TransferRadialGradientParameters(0, 0, 0, 0, 0, a));
            canvas_1.GradientAddColorStop(0, ClockSettings.BG_Color);
            canvas_1.GradientAddColorStop(1, ClockSettings.Clock_Center_Small_Point_Color);
            canvas_1.GradientSetStokeOrFillStyle(false);


            canvas_1.FillCircle(new TransferParameters(0, 0, a, 0, (float)(2.0 * Math.PI)));
            canvas_1.Fill();
           
            
        }


        void cmd_draw_Numbers(double second)
        {
            canvas_1.SetProperty(new TransferCanvasProperty("fillStyle",ClockSettings.Clock_Center_Small_Point_Color));
            string a = string.Empty;

            float ang;

            float f = (float)(ClockSettings.radius_90_Percent * 0.80);

            double c = second / 5.0;



           
            for (int num = 0; num < 12; num++)
            {

                if (Math.Abs(c - num) < 0.3)
                {
                    canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha","1.0"));
                   

                   canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.font.ToString(), ClockSettings.radius_90_Percent * 0.20 + "px arial"));
                   

                    if (num % 3 == 0)
                    {
                        canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.font.ToString(), "bold " + ClockSettings.radius_90_Percent * 0.25 + "px arial"));
                       
                    }
                }
                else
                {
                    canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "0.7"));
                 
                    canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.font.ToString(), ClockSettings.radius_90_Percent * 0.15 + "px arial"));
                    

                    if (num % 3 == 0)
                    {
                        canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.font.ToString(), "bold " + ClockSettings.radius_90_Percent * 0.2 + "px arial"));
                       
                    }

                }

                ang = (float)(num * Math.PI / 6);



                if (num == 0)
                {
                    a = "12";
                }
                else
                {
                    a = num.ToString();
                }




                canvas_1.Rotate(ang);
                canvas_1.Translate(0, -f);
                canvas_1.Rotate(-ang);

                canvas_1.Fill_Text(new TransferFillTextParameters(a, 0, 0));

                canvas_1.Rotate(ang);
                canvas_1.Translate(0, f);
                canvas_1.Rotate(-ang);
            }

            
        }



        public void cmd_Draw_60_Lines()
        {

           

            canvas_1.SetProperty(new TransferCanvasProperty("strokeStyle", ClockSettings.Clock_Center_Small_Point_Color));

            double s = 0;

            

            double lenght = ClockSettings.radius_90_Percent*0.9;


            for (int i = 1; i <= 60; i++)
            {
                s = i;
                s = (s * Math.PI / 30);
                draw60LineEnd(s, lenght, i); 
                s = 0;
            }


            canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "1.0"));



        }

        public void cmd_draw_Second_Shadow_Lines(double second)
        {
          

            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.01).ToString()));
            

            double s = 0;

            int k = 6;

            double transp = 1.0;
            canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "1.0"));
           
            double lenght = ClockSettings.radius_90_Percent * 0.64;

            for (int i = k; i > 0; i--)
            {
                

                s = second -i;

                if (s < 0)
                {
                    s= s + 60;
                }

            
                s = (s * Math.PI / 30);

                transp = Math.Round((1.0/(k+2) * (k - i)),2);
             
                canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", transp.ToString()));
               
                drawHandEnd(s, lenght, k - i);

                s = 0;
            }


            canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "1.0"));
            

            
        }



        void draw60LineEnd(double pos, double length, int sec)
        {


            

            double k = length * 0.05;

            if (sec % 5 ==0)
            {
                if (sec % 15 == 0)
                {
                    canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.02).ToString()));
                    canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "1.0"));
                    k = length * 0.07;
                }
                else
                {
                    canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.015).ToString()));
                    canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "0.8"));
                    k = length * 0.06;
                }
            }
            else
            {
                canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.01).ToString()));
                canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "0.7"));
            }



            canvas_1.Begin_Path();
            canvas_1.Move_To(0, 0);

            canvas_1.Rotate((float)pos);
            canvas_1.Move_To(0, (float)(-length + k));

            if (sec % 15 == 0)
            {
                canvas_1.Line_To(0, (float)(-length*0.99));
            }
            else
            {
                canvas_1.Line_To(0, (float)(-length));
            }
            canvas_1.Stroke();
            canvas_1.Rotate((float)-pos);
        }


        void drawHandEnd(double pos, double length, double q)
        {

            canvas_1.Begin_Path();
            
            double k = length * q * 0.07;

            canvas_1.Move_To(0, 0);
            canvas_1.Rotate((float)pos);
            canvas_1.Move_To(0, (float)(-length + k));
            canvas_1.Line_To(0, (float)(-length)); 
            canvas_1.Stroke();
            canvas_1.Rotate((float)-pos);
           
        }


        void drawHand2(double pos, double k)
        {
            canvas_1.Begin_Path();
            canvas_1.Move_To(0, 0);
            canvas_1.Rotate((float)pos);
            canvas_1.Move_To(0, (float)(-ClockSettings.radius_90_Percent * (k - 0.01)));
            canvas_1.Line_To(0, (float)(-ClockSettings.radius_90_Percent * k));
            canvas_1.Stroke();
            canvas_1.Rotate((float)-pos);


        }

        public void cmd_drawTime()
        {
            DateTime now = DateTime.Now.AddHours(city.TimeDiff);

            CurrentTime = now.ToString(@"hh:mm:ss");


            if (ClockSettings.FastMode && !ClockSettings.SecondOnlyOneStep)
            {
                now = now.AddSeconds(ClockSettings.currentCount);
            }


            double hour = now.Hour % 12;
            double minute = now.Minute;


            double second = now.Second;
            

            if (!ClockSettings.SecondOnlyOneStep)
            {
                second += now.Millisecond / 1000.0;
               
            }


           
            

            drawGauge(second, "lightblue", ClockSettings.Second_Hand_Lenght);
            drawGauge(minute + second / 60.0, "yellow", ClockSettings.Minute_Hand_Lenght * 1.03);
            drawGauge((hour + minute / 60.0) * 5.0, "green", ClockSettings.Hour_Hand_Lenght * 1.08);
           
            cmd_draw_Numbers(second);
            

           
            canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", "1.0"));
           
            canvas_1.SetTransform();
            canvas_1.Translate(ClockSettings.radius_Origin, ClockSettings.radius_Origin);
        

          

            hour = (hour * Math.PI / 6) + (minute * Math.PI / (6 * 60.0)) + (second * Math.PI / (360 * 60));

            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.05).ToString()));

            drawHand(hour, ClockSettings.radius_90_Percent * ClockSettings.Hour_Hand_Lenght);
            drawHand2(hour, 1.045);



            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.04).ToString()));
            
            minute = (minute * Math.PI / 30) + (second * Math.PI / (30 * 60.0));
            drawHand(minute, ClockSettings.radius_90_Percent * ClockSettings.Minute_Hand_Lenght);
            drawHand2(minute, 1.045);

            canvas_1.SetProperty(new TransferCanvasProperty("strokeStyle", ClockSettings.Clock_Second_Arrow_Color));
            
            cmd_draw_Second_Shadow_Lines(second);

            second = (second * Math.PI / 30);
            canvas_1.SetProperty(new TransferCanvasProperty(CanvasProperty.lineWidth.ToString(), (ClockSettings.radius_90_Percent * 0.02).ToString()));
            
            drawHand(second, ClockSettings.radius_90_Percent * ClockSettings.Second_Hand_Lenght);

            drawHand2(second,1.045);


            canvas_1.SetProperty(new TransferCanvasProperty("strokeStyle", ClockSettings.Clock_Center_Small_Point_Color));
           
          

        }



        void drawGauge(double k, string color, double lenght)
        {

            canvas_1.SetProperty(new TransferCanvasProperty("globalAlpha", (k / 60).ToString()));
            
           

            canvas_1.FillGauge(color, new TransferParameters(0, 0, (float)(ClockSettings.radius_90_Percent * lenght), (float)(1.5 * Math.PI), (float)(Math.PI*k/30) + (float)(1.5 * Math.PI)));
           

        }

        void drawHand(double pos, double length)
        {
            canvas_1.Begin_Path();
            canvas_1.Move_To(0, 0);
            canvas_1.Rotate((float)pos);
            canvas_1.Line_To(0, (float)(-length));
            canvas_1.Stroke();
            canvas_1.Rotate((float)-pos);


        }


      

        //void StartTimer()
        //{



        //    //var timer = new Timer(new TimerCallback(_ =>
        //    //{

        //    //    Cmd_Draw_Clock();
        //    //    this.StateHasChanged();

        //    //}), null, 1, 1);
        //}


        void TimeCallBack(object state)
        {
            if (IsRunningProcess)
            {
                Console.WriteLine(IsRunningProcess + " " + DateTime.Now.ToString("hh:mm:ss.fff"));
                return;
            }

            IsRunningProcess = true;
            Cmd_Draw_Clock();
            this.StateHasChanged();
           
            IsRunningProcess = false;
        }

        void IncrementCount()
        {
            Cmd_Draw_Clock();
        }


        void Cmd_Load_Image()
        {

            LoadImage();

        }

    


        void SaveImage()
        {

            // RegisteredFunction.Invoke<bool>("JavaScriptSaveCanvasImage", _canvasReference);


        }

        void LoadImage()
        {

            // RegisteredFunction.Invoke<bool>("JavaScriptLoadCanvasImage", _canvasReference);

            //currentCount++;

            //cmd_drawTime();


            //this.StateHasChanged();
        }





        public void cmd_1()
        {
            Cmd_Draw_Clock();

            this.StateHasChanged();
        }




        public void Dispose()
        {
            //Console.WriteLine("       ------");
            //Console.WriteLine("eventDispose " + this.city.Name + this.Canvas_ID);
 
            //Console.WriteLine(Canvas_ID);
            //canvas_1.Remove_Canvas(Canvas_ID);
        }

        public void EventOnDelete()
        {
            //Console.WriteLine("       ------");
            //Console.WriteLine("eventOnDelete " + this.city.Name + this.Canvas_ID);

            onDelete?.Invoke(this, EventArgs.Empty);

        }


        public void cmd_Refresh()
        {

            Cmd_Draw_Clock();
        }


        #endregion

    }
}
