﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowDOS.HD
{
    class VGAColors
    {
        /// <summary>
        /// The List of VGA Color Pallette Entrys.
        /// </summary>
        public static List<Color> PalletteEntrys = new List<Color>();
        /// <summary>
        /// Clears out all Pallette Entrys.
        /// </summary>
        public static void ClearEntrys()
        {
            PalletteEntrys.Clear();
        }
        /// <summary>
        /// Adds a new VGA Color Entry.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void AddEntry(byte r, byte g, byte b)
        {
            PalletteEntrys.Add(new Color(PalletteEntrys.Count, r, g, b));
        }
        /// <summary>
        /// Finds either the actual index of the color
        /// or a color close to it.
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static int FindIndex(byte R, byte G, byte B)
        {
            int CurrentDistance = 9999;
            int Distance = 0;
            Color KnownColor = new Color((int)0,0,0,0);
            Color input = new Color(R, G, B);
            for (int i = 0; i < PalletteEntrys.Count; i++)
            {
                Distance = delta_RGB(input, PalletteEntrys[i]);
                //The lower the number, the better match.
                if (Distance < CurrentDistance)
                {
                    CurrentDistance = Distance;
                    KnownColor = PalletteEntrys[i];
                }
                //0 means its a direct match, so break.
                if (Distance == 0) { break; }
            }
            return KnownColor.Index;
        }
        public static int delta_RGB(Color color_1, Color color_2)
        {
            int delta_R = Math.Abs(color_1.Red - color_2.Red);
            int delta_G = Math.Abs(color_1.Green - color_2.Green);
            int delta_B = Math.Abs(color_1.Blue - color_2.Blue);
            return (delta_R + delta_G + delta_B);
        }
        private static int SquareRoot(int x)
        {
            if (x == 0) return 0;

            int r = x / 2; // this is inefficient, but I can't find a better way
            // to get a close estimate for the starting value of r
            int last = 0;
            int maxIters = 100;
            for (int i = 0; i < maxIters; i++)
            {
                r = (r + x / r) / 2;
                if (r == last)
                    break;
                last = r;
            }
            return r;
        }
    }
    }

