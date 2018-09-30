using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorGameSnakeComponent
{
    public class JsInterop
    {
        public static Task<bool> Alert(string a)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropSnake.Alert", a);
        }


        public static Task<bool> InitializeSound(int id, string path, bool loop)
        {

            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropSnake.InitializeSound", new {id, path, loop });
        }


        public static Task<bool> ManageSound(int id, string command)
        {
            id = id - 1;
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInteropSnake.ManageSound", new { id, command});
        }


        
    }
}
