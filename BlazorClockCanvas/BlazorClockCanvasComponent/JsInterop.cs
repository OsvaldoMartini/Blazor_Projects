using System;
using System.Threading.Tasks;
using BlazorClockCanvasComponent.Classes;
using Microsoft.JSInterop;

namespace BlazorClockCanvasComponent
{
    public static class JsInterop
    {
        public static Task<string> Prompt(string message)
        {
            return JSRuntime.Current.InvokeAsync<string>(
                "JsInteropClockCanvas.Prompt",
                message);
        }


        public static Task<bool> Alert(string message)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropClockCanvas.Alert",
                message);
        }


        public static Task<bool> Log_Canvas_Array()
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropClockCanvas.Log_Canvas_Array");
        }

        public static Task<bool> Render_To_UI(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Render_To_UI", canvasID);
        }

        

        public static Task<bool> Draw_Circle(string canvasID, TransferParameters transferParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Draw_Circle",
                new { canvasID, transferParameters });
        }

        public static Task<bool> Stroke_Rect(string canvasID, TransferRectParameters transferRectParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Stroke_Rect",
                new { canvasID, transferRectParameters });
        }


        

        public static Task<bool> Draw_Image(string canvasID, string imgName, TransferImageParameters transferImageParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Draw_Image",
                new { canvasID, imgName, transferImageParameters });
        }


        public static Task<bool> Draw_Gauge(string canvasID, string color, TransferParameters transferParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Draw_Gauge",
                new { canvasID, color, transferParameters });
        }

        public static Task<bool> Set_Property(string canvasID, TransferCanvasProperty transferCanvasProperty)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Set_Property",
                new { canvasID, transferCanvasProperty });
        }

        public static Task<bool> Fill_Text(string canvasID, TransferFillTextParameters transferFillTextParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Fill_Text",
                new { canvasID, transferFillTextParameters });
        }

        public static Task<bool> Create_Radial_Gradient(string canvasID, TransferRadialGradientParameters transferRadialGradientParameters)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Create_Radial_Gradient",
                new { canvasID, transferRadialGradientParameters });
        }
        

        public static Task<bool> Clear_Canvas(string canvasID)
        {
            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Clear_Canvas", canvasID);
        }

  

        public static Task<bool> Save_State(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.CanvasSaveState", canvasID);
        }

        public static Task<bool> Restore_State(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.CanvasRestoreState", canvasID);
        }

        public static Task<bool> Set_Transform(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Set_Transform", canvasID);
        }

        public static Task<bool> Begin_Path(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Begin_Path", canvasID);
        }


        public static Task<bool> Stroke(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Stroke", canvasID);
        }

        public static Task<bool> Fill(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Fill", canvasID);
        }

        public static Task<bool> Draw_Full_Size_Rect(string canvasID, string color)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.draw_Full_Size_Rect", new { canvasID, color });
        }


        public static Task<bool> Translate(string canvasID, float x, float y)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Translate",
                new { canvasID, x, y });
        }

        public static Task<bool> Rotate(string canvasID, float angle)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Rotate",new { canvasID, angle });
        }



        public static Task<bool> Move_To(string canvasID, float x, float y)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Move_To",
                new {canvasID, x, y });
        }

        public static Task<bool> Line_To(string canvasID, float x, float y)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Line_To",
                new { canvasID, x, y });
        }


        public static Task<bool> Add_Canvas(string canvasID, string BgCanvasID, string TopCanvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Add_Canvas",new { canvasID, BgCanvasID, TopCanvasID });


        }


        public static Task<bool> Remove_Canvas(string canvasID)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Remove_Canvas", canvasID);
        }


        public static Task<bool> DrawPieChart(string canvasID)
        {
        
                return JSRuntime.Current.InvokeAsync<bool>("JavaScriptDrawPieChart", canvasID);
        }



        public static async Task<string> Preload_Image()
        {

            return await JSRuntime.Current.InvokeAsync<string>("JsInteropClockCanvas.Preload_Image");
        }


        public static Task<bool> Gradient_Add_Color_Stop(float stop, string color)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Gradient_Add_Color_Stop",
                new { stop, color });
        }


        public static Task<bool> Gradient_Set_Stoke_Or_Fill_Style(string canvasID, bool StrokeOrFill)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Gradient_Set_Stoke_Or_Fill_Style",
                new { canvasID, StrokeOrFill });
        }

        public static Task<bool> Execute_Dynamic_Script(string cmd)
        {

            return JSRuntime.Current.InvokeAsync<bool>("JsInteropClockCanvas.Execute_Dynamic_Script", cmd);
        }

    }
}
