using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorChessComponent
{
    public class JsInterop
    {
        public static Task<string> alert(string message)
        {

            return JSRuntime.Current.InvokeAsync<string>(
                "JsInteropChessComp.alert",
                message);
        }

        //public static Task<bool> GetElementBoundingClientRect(string id)
        //{

        //    return JSRuntime.Current.InvokeAsync<bool>(
        //        "JsInteropChessComp.GetElementBoundingClientRect",
        //        id);
        //}

        public static Task<bool> GetElementBoundingClientRect(string id, DotNetObjectRef dotnethelper)
        {
           
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropChessComp.GetElementBoundingClientRect",
                new { id, dotnethelper });
        }


        public static Task<bool> SetCursor(string cursorStyle = "default")
        {

            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropChessComp.SetCursor",
                cursorStyle);
        }
    }
}
