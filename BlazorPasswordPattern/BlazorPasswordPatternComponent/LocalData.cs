using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPasswordPatternComponent
{
    public static class LocalData
    {
        public static CompBlazorPassword_Logic Curr_comp;


        [JSInvokable]
        public static Task<bool> KeyUpFromjs(int e)
        {

            Curr_comp.KeyUpFromJS1(e);

            return Task.FromResult(true);
        }
    }
}
