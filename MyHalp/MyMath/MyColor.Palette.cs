// -----------------------------------------------------------------------------
// Original code from SharpDX project. https://github.com/sharpdx/SharpDX/
// -----------------------------------------------------------------------------
// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace MyHalp.MyMath
{
    /// <summary>
    /// List of predefined <see cref="MyColor"/>.
    /// </summary>
    public partial struct MyColor
    {
        /// <summary>
        /// Zero color.
        /// </summary>
        public static readonly MyColor Zero = MyColor.FromBgra(0x00000000);

        /// <summary>
        /// Transparent color.
        /// </summary>
        public static readonly MyColor Transparent = MyColor.FromBgra(0x00000000);

        /// <summary>
        /// AliceBlue color.
        /// </summary>
        public static readonly MyColor AliceBlue = MyColor.FromBgra(0xFFF0F8FF);

        /// <summary>
        /// AntiqueWhite color.
        /// </summary>
        public static readonly MyColor AntiqueWhite = MyColor.FromBgra(0xFFFAEBD7);

        /// <summary>
        /// Aqua color.
        /// </summary>
        public static readonly MyColor Aqua = MyColor.FromBgra(0xFF00FFFF);

        /// <summary>
        /// Aquamarine color.
        /// </summary>
        public static readonly MyColor Aquamarine = MyColor.FromBgra(0xFF7FFFD4);

        /// <summary>
        /// Azure color.
        /// </summary>
        public static readonly MyColor Azure = MyColor.FromBgra(0xFFF0FFFF);

        /// <summary>
        /// Beige color.
        /// </summary>
        public static readonly MyColor Beige = MyColor.FromBgra(0xFFF5F5DC);

        /// <summary>
        /// Bisque color.
        /// </summary>
        public static readonly MyColor Bisque = MyColor.FromBgra(0xFFFFE4C4);

        /// <summary>
        /// Black color.
        /// </summary>
        public static readonly MyColor Black = MyColor.FromBgra(0xFF000000);

        /// <summary>
        /// BlanchedAlmond color.
        /// </summary>
        public static readonly MyColor BlanchedAlmond = MyColor.FromBgra(0xFFFFEBCD);

        /// <summary>
        /// Blue color.
        /// </summary>
        public static readonly MyColor Blue = MyColor.FromBgra(0xFF0000FF);

        /// <summary>
        /// BlueViolet color.
        /// </summary>
        public static readonly MyColor BlueViolet = MyColor.FromBgra(0xFF8A2BE2);

        /// <summary>
        /// Brown color.
        /// </summary>
        public static readonly MyColor Brown = MyColor.FromBgra(0xFFA52A2A);

        /// <summary>
        /// BurlyWood color.
        /// </summary>
        public static readonly MyColor BurlyWood = MyColor.FromBgra(0xFFDEB887);

        /// <summary>
        /// CadetBlue color.
        /// </summary>
        public static readonly MyColor CadetBlue = MyColor.FromBgra(0xFF5F9EA0);

        /// <summary>
        /// Chartreuse color.
        /// </summary>
        public static readonly MyColor Chartreuse = MyColor.FromBgra(0xFF7FFF00);

        /// <summary>
        /// Chocolate color.
        /// </summary>
        public static readonly MyColor Chocolate = MyColor.FromBgra(0xFFD2691E);

        /// <summary>
        /// Coral color.
        /// </summary>
        public static readonly MyColor Coral = MyColor.FromBgra(0xFFFF7F50);

        /// <summary>
        /// CornflowerBlue color.
        /// </summary>
        public static readonly MyColor CornflowerBlue = MyColor.FromBgra(0xFF6495ED);

        /// <summary>
        /// Cornsilk color.
        /// </summary>
        public static readonly MyColor Cornsilk = MyColor.FromBgra(0xFFFFF8DC);

        /// <summary>
        /// Crimson color.
        /// </summary>
        public static readonly MyColor Crimson = MyColor.FromBgra(0xFFDC143C);

        /// <summary>
        /// Cyan color.
        /// </summary>
        public static readonly MyColor Cyan = MyColor.FromBgra(0xFF00FFFF);

        /// <summary>
        /// DarkBlue color.
        /// </summary>
        public static readonly MyColor DarkBlue = MyColor.FromBgra(0xFF00008B);

        /// <summary>
        /// DarkCyan color.
        /// </summary>
        public static readonly MyColor DarkCyan = MyColor.FromBgra(0xFF008B8B);

        /// <summary>
        /// DarkGoldenrod color.
        /// </summary>
        public static readonly MyColor DarkGoldenrod = MyColor.FromBgra(0xFFB8860B);

        /// <summary>
        /// DarkGray color.
        /// </summary>
        public static readonly MyColor DarkGray = MyColor.FromBgra(0xFFA9A9A9);

        /// <summary>
        /// DarkGreen color.
        /// </summary>
        public static readonly MyColor DarkGreen = MyColor.FromBgra(0xFF006400);

        /// <summary>
        /// DarkKhaki color.
        /// </summary>
        public static readonly MyColor DarkKhaki = MyColor.FromBgra(0xFFBDB76B);

        /// <summary>
        /// DarkMagenta color.
        /// </summary>
        public static readonly MyColor DarkMagenta = MyColor.FromBgra(0xFF8B008B);

        /// <summary>
        /// DarkOliveGreen color.
        /// </summary>
        public static readonly MyColor DarkOliveGreen = MyColor.FromBgra(0xFF556B2F);

        /// <summary>
        /// DarkOrange color.
        /// </summary>
        public static readonly MyColor DarkOrange = MyColor.FromBgra(0xFFFF8C00);

        /// <summary>
        /// DarkOrchid color.
        /// </summary>
        public static readonly MyColor DarkOrchid = MyColor.FromBgra(0xFF9932CC);

        /// <summary>
        /// DarkRed color.
        /// </summary>
        public static readonly MyColor DarkRed = MyColor.FromBgra(0xFF8B0000);

        /// <summary>
        /// DarkSalmon color.
        /// </summary>
        public static readonly MyColor DarkSalmon = MyColor.FromBgra(0xFFE9967A);

        /// <summary>
        /// DarkSeaGreen color.
        /// </summary>
        public static readonly MyColor DarkSeaGreen = MyColor.FromBgra(0xFF8FBC8B);

        /// <summary>
        /// DarkSlateBlue color.
        /// </summary>
        public static readonly MyColor DarkSlateBlue = MyColor.FromBgra(0xFF483D8B);

        /// <summary>
        /// DarkSlateGray color.
        /// </summary>
        public static readonly MyColor DarkSlateGray = MyColor.FromBgra(0xFF2F4F4F);

        /// <summary>
        /// DarkTurquoise color.
        /// </summary>
        public static readonly MyColor DarkTurquoise = MyColor.FromBgra(0xFF00CED1);

        /// <summary>
        /// DarkViolet color.
        /// </summary>
        public static readonly MyColor DarkViolet = MyColor.FromBgra(0xFF9400D3);

        /// <summary>
        /// DeepPink color.
        /// </summary>
        public static readonly MyColor DeepPink = MyColor.FromBgra(0xFFFF1493);

        /// <summary>
        /// DeepSkyBlue color.
        /// </summary>
        public static readonly MyColor DeepSkyBlue = MyColor.FromBgra(0xFF00BFFF);

        /// <summary>
        /// DimGray color.
        /// </summary>
        public static readonly MyColor DimGray = MyColor.FromBgra(0xFF696969);

        /// <summary>
        /// DodgerBlue color.
        /// </summary>
        public static readonly MyColor DodgerBlue = MyColor.FromBgra(0xFF1E90FF);

        /// <summary>
        /// Firebrick color.
        /// </summary>
        public static readonly MyColor Firebrick = MyColor.FromBgra(0xFFB22222);

        /// <summary>
        /// FloralWhite color.
        /// </summary>
        public static readonly MyColor FloralWhite = MyColor.FromBgra(0xFFFFFAF0);

        /// <summary>
        /// ForestGreen color.
        /// </summary>
        public static readonly MyColor ForestGreen = MyColor.FromBgra(0xFF228B22);

        /// <summary>
        /// Fuchsia color.
        /// </summary>
        public static readonly MyColor Fuchsia = MyColor.FromBgra(0xFFFF00FF);

        /// <summary>
        /// Gainsboro color.
        /// </summary>
        public static readonly MyColor Gainsboro = MyColor.FromBgra(0xFFDCDCDC);

        /// <summary>
        /// GhostWhite color.
        /// </summary>
        public static readonly MyColor GhostWhite = MyColor.FromBgra(0xFFF8F8FF);

        /// <summary>
        /// Gold color.
        /// </summary>
        public static readonly MyColor Gold = MyColor.FromBgra(0xFFFFD700);

        /// <summary>
        /// Goldenrod color.
        /// </summary>
        public static readonly MyColor Goldenrod = MyColor.FromBgra(0xFFDAA520);

        /// <summary>
        /// Gray color.
        /// </summary>
        public static readonly MyColor Gray = MyColor.FromBgra(0xFF808080);

        /// <summary>
        /// Green color.
        /// </summary>
        public static readonly MyColor Green = MyColor.FromBgra(0xFF008000);

        /// <summary>
        /// GreenYellow color.
        /// </summary>
        public static readonly MyColor GreenYellow = MyColor.FromBgra(0xFFADFF2F);

        /// <summary>
        /// Honeydew color.
        /// </summary>
        public static readonly MyColor Honeydew = MyColor.FromBgra(0xFFF0FFF0);

        /// <summary>
        /// HotPink color.
        /// </summary>
        public static readonly MyColor HotPink = MyColor.FromBgra(0xFFFF69B4);

        /// <summary>
        /// IndianRed color.
        /// </summary>
        public static readonly MyColor IndianRed = MyColor.FromBgra(0xFFCD5C5C);

        /// <summary>
        /// Indigo color.
        /// </summary>
        public static readonly MyColor Indigo = MyColor.FromBgra(0xFF4B0082);

        /// <summary>
        /// Ivory color.
        /// </summary>
        public static readonly MyColor Ivory = MyColor.FromBgra(0xFFFFFFF0);

        /// <summary>
        /// Khaki color.
        /// </summary>
        public static readonly MyColor Khaki = MyColor.FromBgra(0xFFF0E68C);

        /// <summary>
        /// Lavender color.
        /// </summary>
        public static readonly MyColor Lavender = MyColor.FromBgra(0xFFE6E6FA);

        /// <summary>
        /// LavenderBlush color.
        /// </summary>
        public static readonly MyColor LavenderBlush = MyColor.FromBgra(0xFFFFF0F5);

        /// <summary>
        /// LawnGreen color.
        /// </summary>
        public static readonly MyColor LawnGreen = MyColor.FromBgra(0xFF7CFC00);

        /// <summary>
        /// LemonChiffon color.
        /// </summary>
        public static readonly MyColor LemonChiffon = MyColor.FromBgra(0xFFFFFACD);

        /// <summary>
        /// LightBlue color.
        /// </summary>
        public static readonly MyColor LightBlue = MyColor.FromBgra(0xFFADD8E6);

        /// <summary>
        /// LightCoral color.
        /// </summary>
        public static readonly MyColor LightCoral = MyColor.FromBgra(0xFFF08080);

        /// <summary>
        /// LightCyan color.
        /// </summary>
        public static readonly MyColor LightCyan = MyColor.FromBgra(0xFFE0FFFF);

        /// <summary>
        /// LightGoldenrodYellow color.
        /// </summary>
        public static readonly MyColor LightGoldenrodYellow = MyColor.FromBgra(0xFFFAFAD2);

        /// <summary>
        /// LightGray color.
        /// </summary>
        public static readonly MyColor LightGray = MyColor.FromBgra(0xFFD3D3D3);

        /// <summary>
        /// LightGreen color.
        /// </summary>
        public static readonly MyColor LightGreen = MyColor.FromBgra(0xFF90EE90);

        /// <summary>
        /// LightPink color.
        /// </summary>
        public static readonly MyColor LightPink = MyColor.FromBgra(0xFFFFB6C1);

        /// <summary>
        /// LightSalmon color.
        /// </summary>
        public static readonly MyColor LightSalmon = MyColor.FromBgra(0xFFFFA07A);

        /// <summary>
        /// LightSeaGreen color.
        /// </summary>
        public static readonly MyColor LightSeaGreen = MyColor.FromBgra(0xFF20B2AA);

        /// <summary>
        /// LightSkyBlue color.
        /// </summary>
        public static readonly MyColor LightSkyBlue = MyColor.FromBgra(0xFF87CEFA);

        /// <summary>
        /// LightSlateGray color.
        /// </summary>
        public static readonly MyColor LightSlateGray = MyColor.FromBgra(0xFF778899);

        /// <summary>
        /// LightSteelBlue color.
        /// </summary>
        public static readonly MyColor LightSteelBlue = MyColor.FromBgra(0xFFB0C4DE);

        /// <summary>
        /// LightYellow color.
        /// </summary>
        public static readonly MyColor LightYellow = MyColor.FromBgra(0xFFFFFFE0);

        /// <summary>
        /// Lime color.
        /// </summary>
        public static readonly MyColor Lime = MyColor.FromBgra(0xFF00FF00);

        /// <summary>
        /// LimeGreen color.
        /// </summary>
        public static readonly MyColor LimeGreen = MyColor.FromBgra(0xFF32CD32);

        /// <summary>
        /// Linen color.
        /// </summary>
        public static readonly MyColor Linen = MyColor.FromBgra(0xFFFAF0E6);

        /// <summary>
        /// Magenta color.
        /// </summary>
        public static readonly MyColor Magenta = MyColor.FromBgra(0xFFFF00FF);

        /// <summary>
        /// Maroon color.
        /// </summary>
        public static readonly MyColor Maroon = MyColor.FromBgra(0xFF800000);

        /// <summary>
        /// MediumAquamarine color.
        /// </summary>
        public static readonly MyColor MediumAquamarine = MyColor.FromBgra(0xFF66CDAA);

        /// <summary>
        /// MediumBlue color.
        /// </summary>
        public static readonly MyColor MediumBlue = MyColor.FromBgra(0xFF0000CD);

        /// <summary>
        /// MediumOrchid color.
        /// </summary>
        public static readonly MyColor MediumOrchid = MyColor.FromBgra(0xFFBA55D3);

        /// <summary>
        /// MediumPurple color.
        /// </summary>
        public static readonly MyColor MediumPurple = MyColor.FromBgra(0xFF9370DB);

        /// <summary>
        /// MediumSeaGreen color.
        /// </summary>
        public static readonly MyColor MediumSeaGreen = MyColor.FromBgra(0xFF3CB371);

        /// <summary>
        /// MediumSlateBlue color.
        /// </summary>
        public static readonly MyColor MediumSlateBlue = MyColor.FromBgra(0xFF7B68EE);

        /// <summary>
        /// MediumSpringGreen color.
        /// </summary>
        public static readonly MyColor MediumSpringGreen = MyColor.FromBgra(0xFF00FA9A);

        /// <summary>
        /// MediumTurquoise color.
        /// </summary>
        public static readonly MyColor MediumTurquoise = MyColor.FromBgra(0xFF48D1CC);

        /// <summary>
        /// MediumVioletRed color.
        /// </summary>
        public static readonly MyColor MediumVioletRed = MyColor.FromBgra(0xFFC71585);

        /// <summary>
        /// MidnightBlue color.
        /// </summary>
        public static readonly MyColor MidnightBlue = MyColor.FromBgra(0xFF191970);

        /// <summary>
        /// MintCream color.
        /// </summary>
        public static readonly MyColor MintCream = MyColor.FromBgra(0xFFF5FFFA);

        /// <summary>
        /// MistyRose color.
        /// </summary>
        public static readonly MyColor MistyRose = MyColor.FromBgra(0xFFFFE4E1);

        /// <summary>
        /// Moccasin color.
        /// </summary>
        public static readonly MyColor Moccasin = MyColor.FromBgra(0xFFFFE4B5);

        /// <summary>
        /// NavajoWhite color.
        /// </summary>
        public static readonly MyColor NavajoWhite = MyColor.FromBgra(0xFFFFDEAD);

        /// <summary>
        /// Navy color.
        /// </summary>
        public static readonly MyColor Navy = MyColor.FromBgra(0xFF000080);

        /// <summary>
        /// OldLace color.
        /// </summary>
        public static readonly MyColor OldLace = MyColor.FromBgra(0xFFFDF5E6);

        /// <summary>
        /// Olive color.
        /// </summary>
        public static readonly MyColor Olive = MyColor.FromBgra(0xFF808000);

        /// <summary>
        /// OliveDrab color.
        /// </summary>
        public static readonly MyColor OliveDrab = MyColor.FromBgra(0xFF6B8E23);

        /// <summary>
        /// Orange color.
        /// </summary>
        public static readonly MyColor Orange = MyColor.FromBgra(0xFFFFA500);

        /// <summary>
        /// OrangeRed color.
        /// </summary>
        public static readonly MyColor OrangeRed = MyColor.FromBgra(0xFFFF4500);

        /// <summary>
        /// Orchid color.
        /// </summary>
        public static readonly MyColor Orchid = MyColor.FromBgra(0xFFDA70D6);

        /// <summary>
        /// PaleGoldenrod color.
        /// </summary>
        public static readonly MyColor PaleGoldenrod = MyColor.FromBgra(0xFFEEE8AA);

        /// <summary>
        /// PaleGreen color.
        /// </summary>
        public static readonly MyColor PaleGreen = MyColor.FromBgra(0xFF98FB98);

        /// <summary>
        /// PaleTurquoise color.
        /// </summary>
        public static readonly MyColor PaleTurquoise = MyColor.FromBgra(0xFFAFEEEE);

        /// <summary>
        /// PaleVioletRed color.
        /// </summary>
        public static readonly MyColor PaleVioletRed = MyColor.FromBgra(0xFFDB7093);

        /// <summary>
        /// PapayaWhip color.
        /// </summary>
        public static readonly MyColor PapayaWhip = MyColor.FromBgra(0xFFFFEFD5);

        /// <summary>
        /// PeachPuff color.
        /// </summary>
        public static readonly MyColor PeachPuff = MyColor.FromBgra(0xFFFFDAB9);

        /// <summary>
        /// Peru color.
        /// </summary>
        public static readonly MyColor Peru = MyColor.FromBgra(0xFFCD853F);

        /// <summary>
        /// Pink color.
        /// </summary>
        public static readonly MyColor Pink = MyColor.FromBgra(0xFFFFC0CB);

        /// <summary>
        /// Plum color.
        /// </summary>
        public static readonly MyColor Plum = MyColor.FromBgra(0xFFDDA0DD);

        /// <summary>
        /// PowderBlue color.
        /// </summary>
        public static readonly MyColor PowderBlue = MyColor.FromBgra(0xFFB0E0E6);

        /// <summary>
        /// Purple color.
        /// </summary>
        public static readonly MyColor Purple = MyColor.FromBgra(0xFF800080);

        /// <summary>
        /// Red color.
        /// </summary>
        public static readonly MyColor Red = MyColor.FromBgra(0xFFFF0000);

        /// <summary>
        /// RosyBrown color.
        /// </summary>
        public static readonly MyColor RosyBrown = MyColor.FromBgra(0xFFBC8F8F);

        /// <summary>
        /// RoyalBlue color.
        /// </summary>
        public static readonly MyColor RoyalBlue = MyColor.FromBgra(0xFF4169E1);

        /// <summary>
        /// SaddleBrown color.
        /// </summary>
        public static readonly MyColor SaddleBrown = MyColor.FromBgra(0xFF8B4513);

        /// <summary>
        /// Salmon color.
        /// </summary>
        public static readonly MyColor Salmon = MyColor.FromBgra(0xFFFA8072);

        /// <summary>
        /// SandyBrown color.
        /// </summary>
        public static readonly MyColor SandyBrown = MyColor.FromBgra(0xFFF4A460);

        /// <summary>
        /// SeaGreen color.
        /// </summary>
        public static readonly MyColor SeaGreen = MyColor.FromBgra(0xFF2E8B57);

        /// <summary>
        /// SeaShell color.
        /// </summary>
        public static readonly MyColor SeaShell = MyColor.FromBgra(0xFFFFF5EE);

        /// <summary>
        /// Sienna color.
        /// </summary>
        public static readonly MyColor Sienna = MyColor.FromBgra(0xFFA0522D);

        /// <summary>
        /// Silver color.
        /// </summary>
        public static readonly MyColor Silver = MyColor.FromBgra(0xFFC0C0C0);

        /// <summary>
        /// SkyBlue color.
        /// </summary>
        public static readonly MyColor SkyBlue = MyColor.FromBgra(0xFF87CEEB);

        /// <summary>
        /// SlateBlue color.
        /// </summary>
        public static readonly MyColor SlateBlue = MyColor.FromBgra(0xFF6A5ACD);

        /// <summary>
        /// SlateGray color.
        /// </summary>
        public static readonly MyColor SlateGray = MyColor.FromBgra(0xFF708090);

        /// <summary>
        /// Snow color.
        /// </summary>
        public static readonly MyColor Snow = MyColor.FromBgra(0xFFFFFAFA);

        /// <summary>
        /// SpringGreen color.
        /// </summary>
        public static readonly MyColor SpringGreen = MyColor.FromBgra(0xFF00FF7F);

        /// <summary>
        /// SteelBlue color.
        /// </summary>
        public static readonly MyColor SteelBlue = MyColor.FromBgra(0xFF4682B4);

        /// <summary>
        /// Tan color.
        /// </summary>
        public static readonly MyColor Tan = MyColor.FromBgra(0xFFD2B48C);

        /// <summary>
        /// Teal color.
        /// </summary>
        public static readonly MyColor Teal = MyColor.FromBgra(0xFF008080);

        /// <summary>
        /// Thistle color.
        /// </summary>
        public static readonly MyColor Thistle = MyColor.FromBgra(0xFFD8BFD8);

        /// <summary>
        /// Tomato color.
        /// </summary>
        public static readonly MyColor Tomato = MyColor.FromBgra(0xFFFF6347);

        /// <summary>
        /// Turquoise color.
        /// </summary>
        public static readonly MyColor Turquoise = MyColor.FromBgra(0xFF40E0D0);

        /// <summary>
        /// Violet color.
        /// </summary>
        public static readonly MyColor Violet = MyColor.FromBgra(0xFFEE82EE);

        /// <summary>
        /// Wheat color.
        /// </summary>
        public static readonly MyColor Wheat = MyColor.FromBgra(0xFFF5DEB3);

        /// <summary>
        /// White color.
        /// </summary>
        public static readonly MyColor White = MyColor.FromBgra(0xFFFFFFFF);

        /// <summary>
        /// WhiteSmoke color.
        /// </summary>
        public static readonly MyColor WhiteSmoke = MyColor.FromBgra(0xFFF5F5F5);

        /// <summary>
        /// Yellow color.
        /// </summary>
        public static readonly MyColor Yellow = MyColor.FromBgra(0xFFFFFF00);

        /// <summary>
        /// YellowGreen color.
        /// </summary>
        public static readonly MyColor YellowGreen = MyColor.FromBgra(0xFF9ACD32);
    }
}