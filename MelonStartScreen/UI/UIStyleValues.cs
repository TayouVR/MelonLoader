﻿using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MelonLoader.MelonStartScreen.UI
{
    internal static class UIStyleValues
    {
        public static Font TextFont;

        public static Texture2D BackgroundTexture;
        public static Texture2D ProgressBarInnerTexture;
        public static Texture2D ProgressBarOuterTexture;

        public static Image BackgroundImage;
        public static Image LogoImage;
        public static Image LoadingImage;

        internal static void Init()
        {
            TextFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            BackgroundTexture = UIUtils.CreateColorTexture(UICustomization.General.BackgroundColor);
            BackgroundImage = LoadImage("Background");

            if (UICustomization.ProgressBar.Enabled)
            {
                ProgressBarInnerTexture = UIUtils.CreateColorTexture(UICustomization.ProgressBar.InnerColor);
                ProgressBarOuterTexture = UIUtils.CreateColorTexture(UICustomization.ProgressBar.OuterColor);
            }

            if (UICustomization.Logo.Enabled)
            {
                LogoImage = LoadImage("Logo");
                if (LogoImage == null)
                    LogoImage = new Image(
                    (MelonLaunchOptions.Console.Mode == MelonLaunchOptions.Console.DisplayMode.LEMON)
                        ? Properties.Resources.Logo_Lemon
                        : Properties.Resources.Logo_Melon);
            }

            if (UICustomization.Animation.Enabled)
            {
                LoadingImage = LoadImage("Loading");
                if (LoadingImage == null)
                    LoadingImage = new AnimatedImage(
                    (MelonLaunchOptions.Console.Mode == MelonLaunchOptions.Console.DisplayMode.LEMON)
                        ? Properties.Resources.Loading_Lemon
                        : Properties.Resources.Loading_Melon);
            }
        }

        private static Image LoadImage(string filename)
        {
            string filepath = ScanForFile(filename);
            if (string.IsNullOrEmpty(filepath))
                return null;
            string fileext = Path.GetExtension(filepath).ToLowerInvariant();
            if (fileext.Equals(".gif"))
                return new AnimatedImage(filepath);
            if (fileext.Equals(".png")
                || fileext.Equals(".jpg")
                || fileext.Equals(".jpeg"))
                return new Image(filepath);
            return null;
        }

        private static string ScanForFile(string filename)
        {
            string[] files = Directory.GetFiles(Core.FolderPath);
            if (files.Length <= 0)
                return null;
            return files.FirstOrDefault(x =>
                Path.GetFileNameWithoutExtension(x)
                    .ToLowerInvariant()
                    .Equals(
                        filename
                        .ToLowerInvariant()));
        }
    }
}