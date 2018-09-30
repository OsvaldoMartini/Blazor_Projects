using BlazorClockSvgComponent.Classes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClockSvgComponent
{
    public static class JsInterop2
    {

        public static Task<bool> Run(TransferParameters _params)
        {
            return JSRuntime.Current.InvokeAsync<bool>(
                "JsInterop2.Run", _params);
        }
    }
}
