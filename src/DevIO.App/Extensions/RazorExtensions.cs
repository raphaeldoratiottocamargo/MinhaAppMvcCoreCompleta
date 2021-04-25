using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace DevIO.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this RazorPage page, int tipoPessoa, string Documento)
        {
            return tipoPessoa == 1 ?
                Convert.ToUInt64(Documento).ToString(@"000\.000\.000\-00") :
                Convert.ToUInt64(Documento).ToString(@"00\.000\.000\/0000\-00");
        }

    }
}
