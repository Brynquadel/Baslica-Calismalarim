using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace COED.Controls
{
    public class Panel
    {
        public BrushConverter bc = new BrushConverter();

        public WrapPanel PanelAdd(Pages.Detail detail)
        {
            Model.cab++;
            WrapPanel ana = new WrapPanel();
            ana.Name = $"panel{Model.cab}";
            detail.upanel.RegisterName(ana.Name, ana);
            ana.MouseUp += detail.Ana_MouseUp;
            ana.MouseMove += detail.Ana_MouseMove;
            ana.MouseLeave += detail.Ana_MouseLeave;
            ana.Cursor = Cursors.Hand;
            ana.Margin = new Thickness(0, 0, 0, 10);
            ana.Cursor = Cursors.Hand;
            ana.Background = (Brush)bc.ConvertFrom("#e6e6e6");



            RichTextBox rctyp = new RichTextBox();
            rctyp.Padding = new Thickness(3, 6, 3, 6);
            rctyp.BorderThickness = new Thickness(0);
            rctyp.HorizontalContentAlignment = HorizontalAlignment.Center;
            rctyp.Background = (Brush)bc.ConvertFrom("transparent");
            rctyp.Width = 70;
            rctyp.Height = 40;
            rctyp.FontSize = 16;
            rctyp.Document.Blocks.Clear();
            rctyp.Document.Blocks.Add(
                new Paragraph(new Run(
                    "TEST"
                    ))
                );
            ana.Children.Add(rctyp);

            RichTextBox rcprt = new RichTextBox();
            rcprt.BorderThickness = new Thickness(0);
            rcprt.Background = (Brush)bc.ConvertFrom("transparent");
            rcprt.Width = 340;
            rcprt.Height = 40;
            rcprt.VerticalContentAlignment = VerticalAlignment.Center;
            rcprt.FontSize = 16;

            rcprt.Document.Blocks.Clear();
            rcprt.Document.Blocks.Add(
                new Paragraph(new Run(
                    "TEST"
                    ))
                );
            ana.Children.Add(rcprt);

            WrapPanel yakala = new WrapPanel();
            yakala.Height = 40;
            yakala.Width = 40;
            yakala.Name = "Yakala" + Model.cab;
            yakala.Background = (Brush)bc.ConvertFrom("transparent");
            yakala.MouseUp += detail.Btn_MouseUp;
            yakala.MouseMove += detail.Btn_MouseMove;
            yakala.MouseLeave += detail.Btn_MouseLeave;

            ana.Children.Add(yakala);

            return ana;
        }

        public void PanelUpp(Pages.Detail detail)
        {
            WrapPanel wrpgelen = new WrapPanel();
            wrpgelen = (WrapPanel)detail.upanel.FindName("panel" + Model.pab);
            detail.upanel.Children.Remove(wrpgelen);

            WrapPanel wrpgiden = new WrapPanel();
            wrpgiden = (WrapPanel)detail.upanel.FindName("panel" + (Model.pab - 1));
            detail.upanel.Children.Remove(wrpgiden);

            detail.upanel.UnregisterName("panel" + (Model.pab)); detail.upanel.UnregisterName("panel" + (Model.pab - 1));
            wrpgelen.Name = "panel" + (Model.pab - 1); detail.upanel.RegisterName(wrpgelen.Name, wrpgelen);
            wrpgiden.Name = "panel" + (Model.pab); detail.upanel.RegisterName(wrpgiden.Name, wrpgiden);

            detail.upanel.Children.Insert((Model.pab - 1), wrpgelen);
            detail.upanel.Children.Insert((Model.pab), wrpgiden);

            Model.pab--;
        }

        public void PanelDown(Pages.Detail detail)
        {
            WrapPanel wrpgelen = new WrapPanel();
            wrpgelen = (WrapPanel)detail.upanel.FindName("panel" + Model.pab);
            detail.upanel.Children.Remove(wrpgelen);

            WrapPanel wrpgiden = new WrapPanel();
            wrpgiden = (WrapPanel)detail.upanel.FindName("panel" + (Model.pab + 1));
            detail.upanel.Children.Remove(wrpgiden);

            detail.upanel.UnregisterName("panel" + (Model.pab)); detail.upanel.UnregisterName("panel" + (Model.pab + 1));
            wrpgelen.Name = "panel" + (Model.pab + 1); detail.upanel.RegisterName(wrpgelen.Name, wrpgelen);
            wrpgiden.Name = "panel" + (Model.pab); detail.upanel.RegisterName(wrpgiden.Name, wrpgiden);

            detail.upanel.Children.Insert((Model.pab), wrpgiden);
            detail.upanel.Children.Insert((Model.pab + 1), wrpgelen);

            Model.pab++;
        }

        public void PanelRemove(Pages.Detail detail)
        {
            Model.cab--; Model.pab = Model.cab;
            int hey = Convert.ToInt32(detail.wrpanel.Name.ToString().Replace("panel", null));
            WrapPanel wrp = (WrapPanel)detail.upanel.FindName("panel" + (hey - 1));
            detail.upanel.Children.Remove(detail.wrpanel);
            detail.upanel.UnregisterName(detail.wrpanel.Name);

            detail.wrpanel = wrp;
        }

        public void TextChange1(WrapPanel dpanel, WrapPanel wrpanel)
        {
            bool oncelik = true; string tp = null, cp = null;
            foreach (RichTextBox item in dpanel.Children)
            {

                if (oncelik == true)
                {
                    oncelik = false;
                    tp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

                else if (oncelik == false)
                {
                    oncelik = true;
                    // cp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

            }

            oncelik = true;
            foreach (var icerik in wrpanel.Children)
            {
                if (icerik is WrapPanel) continue;
                RichTextBox item = (RichTextBox)icerik;

                if (oncelik == true)
                {
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            tp.ToString()
                            ))
                        );
                    oncelik = false;
                }
                else if (oncelik == false)
                {
                    /*
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            cp.ToString()
                            ))
                        );
                        */
                    oncelik = true;
                }



            }
        }

        public void TextChange2(WrapPanel dpanel, WrapPanel wrpanel)
        {
            bool oncelik = true; string tp = null, cp = null;
            foreach (RichTextBox item in dpanel.Children)
            {

                if (oncelik == true)
                {
                    oncelik = false;
                    // tp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

                else if (oncelik == false)
                {
                    oncelik = true;
                    cp = new TextRange(item.Document.ContentStart, item.Document.ContentEnd).Text.TrimEnd();
                }

            }

            oncelik = true;
            foreach (var icerik in wrpanel.Children)
            {
                if (icerik is WrapPanel) continue;
                RichTextBox item = (RichTextBox)icerik;

                if (oncelik == true)
                {
                    /*
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            tp.ToString()
                            ))
                        );
                        */
                    oncelik = false;
                }
                else if (oncelik == false)
                {
                    item.Document.Blocks.Clear();
                    item.Document.Blocks.Add(
                        new Paragraph(new Run(
                            cp.ToString()
                            ))
                        );
                    oncelik = true;
                }
            }
        }

    }
}
