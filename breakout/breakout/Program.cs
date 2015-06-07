// ***********************************************************************
// Assembly         : Super Roberto Breakout
// Author           : Thomas Fossati
// Created          : 05-24-2015
//
// Last Modified By : Thomas Fossati
// Last Modified On : 06-07-2015
// ***********************************************************************
// <copyright file="Program.cs" company="Hutopi">
//     Copyright ©  2015 Hugo Caille, Pierre Defache & Thomas Fossati.
//      Breakout is released upon the terms of the Apache 2.0 License.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

/// <summary>
/// The breakout namespace.
/// </summary>
namespace breakout
{
#if WINDOWS || XBOX
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        [STAThread]
        static void Main(string[] args)
        {
            using (Breakout game = new Breakout())
            {
                try
                {
                    Assembly.Load("Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(
                        "Unable to locate XNA Game Framework 4.0.",
                        "XNA loading error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                try
                {
                    game.Run();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        String.Format("Unhandled exception: {0}", ex),
                        "Game exception",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    game.Exit();
                }
            }
        }
    }
#endif
}

