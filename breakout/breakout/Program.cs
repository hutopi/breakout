using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace breakout
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Breakout game = new Breakout())
            {
                try
                {
                    Assembly.Load("Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553");
                }
                catch (FileNotFoundException ex)
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

