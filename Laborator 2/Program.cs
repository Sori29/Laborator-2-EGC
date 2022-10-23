using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;


namespace Laborator2
{
    class SimpleWindow : GameWindow
    {

        // Constructor.
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
        }

        // Tratează evenimentul generat de apăsarea unei taste. Mecanismul standard oferit de .NET
        // este cel utilizat.
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            
        }

        // Setare mediu OpenGL și încarcarea resurselor (dacă e necesar) - de exemplu culoarea de
        // fundal a ferestrei 3D.
        // Atenție! Acest cod se execută înainte de desenarea efectivă a scenei 3D.
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        // Inițierea afișării și setarea viewport-ului grafic. Metoda este invocată la redimensionarea
        // ferestrei. Va fi invocată o dată și imediat după metoda ONLOAD!
        // Viewport-ul va fi dimensionat conform mărimii ferestrei active (cele 2 obiecte pot avea și mărimi 
        // diferite). 
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        // Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
        // automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc.
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Momentan aplicația nu face nimic!
        }
        //Misca obiectul pe axa X daca una din tastele "a" sau "d" sunt apasate
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if(e.KeyChar == 'a')
            {
                
                GL.Translate(-0.05, 0, 0); 
            }
            if (e.KeyChar == 'd')
            {
                GL.Translate(0.05,0, 0);
            }
        }

        //Misca obiectul pe axa Y daca pozitia mouse-ului pe axa Y este schimbata si daca butonul din stanga
        //mouse-ului este apasat in acest timp
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if(e.YDelta < 0 & (e.Mouse.LeftButton == ButtonState.Pressed))
                GL.Translate(0, 0.01, 0);
            if(e.YDelta>=0 & (e.Mouse.LeftButton == ButtonState.Pressed))
                GL.Translate(0, -0.01, 0);
        }

        // Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME.
        // Parametrul de intrare "e" conține informatii de timing pentru randare.
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Modul imediat! Suportat până la OpenGL 3.5 (este ineficient din cauza multiplelor apeluri de
            // funcții).
            
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(-1.0f, 1.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(0.0f, -1.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f, 1.0f);

            GL.End();
            // Sfârșitul modului imediat!

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {

                example.Run(30.0, 0.0);
            }
        }
    }
}
