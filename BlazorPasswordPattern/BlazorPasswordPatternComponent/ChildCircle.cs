using BlazorSvgHelper;
using BlazorSvgHelper.Classes.SubClasses;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPasswordPatternComponent
{
    public class ChildCircle : BlazorComponent
    {
        [Parameter]
        private double par_i { get; set; }

        [Parameter]
        private double par_j { get; set; }


        [Parameter]
        private Action<string> ActionClicked { get; set; }

        string _id = string.Empty;

        //string _id_rect = string.Empty;
        //string _id_bigCircle = string.Empty;
        //string _id_smallCircle = string.Empty;

        //Dictionary<string, ElementRef> dict = new Dictionary<string, ElementRef>();

        //private ElementRef element_rect;
        //private ElementRef element_bigCircle;
        //private ElementRef element_smallCircle;

        private SvgHelper SvgHelper1 = new SvgHelper();

        protected override void OnInit()
        {
            _id = par_i.ToString() + par_j.ToString();
            //_id_rect = "Rect" + _id;
            //_id_bigCircle = "BigCircle" + _id;
            //_id_smallCircle = "SmallCircle" + _id;
        }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            g g1 = new g();

            string StrokeColor = ComponentSettings.BigCircle_Color;


            if (ComponentSettings.SelectedCircles_List.Any(x=>x == _id))
            {
                StrokeColor = ComponentSettings.SelectCircle_Color;
            }


            g1.Children.Add(new circle
            {
                //id = _id_bigCircle,
                cx = par_i * ComponentSettings.w + ComponentSettings.w * 0.5,
                cy = par_j * ComponentSettings.h + ComponentSettings.h * 0.5,
                r = ComponentSettings.r,
                onclick = "notEmpty",
                stroke_width = ComponentSettings.compWidth * 0.01,
                stroke = StrokeColor,
                fill = ComponentSettings.BigCircle_Color,
                //CaptureRef = true,
            });


            g1.Children.Add(new circle
            {
                //id = _id_smallCircle,
                cx = par_i * ComponentSettings.w + ComponentSettings.w * 0.5,
                cy = par_j * ComponentSettings.h + ComponentSettings.h * 0.5,
                r = ComponentSettings.r * 0.4,
                fill = ComponentSettings.SmallCircle_Color,
                onclick = "notEmpty",
                //CaptureRef = true,
            });


            SvgHelper1.Cmd_Render(g1, 0, builder);

            base.BuildRenderTree(builder);



        }


        protected override void OnAfterRender()
        {
            SvgHelper1.ActionClicked = ComponentClicked;



            //Dictionary<string, ElementRef> dict = SvgHelper1.Get_Dictionary();

            //if (!dict.TryGetValue(_id_rect, out element_rect))
            //{
            //    throw new Exception("shoud be captured element reference "+ _id_rect +"!");
            //}
            //if (!dict.TryGetValue(_id_bigCircle, out element_bigCircle))
            //{
            //    throw new Exception("shoud be captured element reference " + _id_bigCircle + "!");
            //}
            //if (!dict.TryGetValue(_id_smallCircle, out element_smallCircle))
            //{
            //    throw new Exception("shoud be captured element reference " + _id_smallCircle + "!");
            //}
        }


        public void ComponentClicked(UIMouseEventArgs e)
        {
            ActionClicked?.Invoke(_id);
        }


        public void Dispose()
        {

        }



    }


}