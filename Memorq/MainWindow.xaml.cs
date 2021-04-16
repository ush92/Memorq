using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

         /*       
                    algorithm SM-2 is
            input:  user grade q
                    repetition number n
                    easiness factor EF
                    interval I
            output: updated values of n, EF, and I

            if q ≥ 3 (correct response) then
                if n = 0 then
                    I ← 1
                else if n = 1 then
                    I ← 6
                else
                    I ← ⌈I × EF⌉
                end if
                EF ← EF + (0.1 − (5 − q) × (0.08 + (5 − q) × 0.02))
                if EF < 1.3 then
                    EF ← 1.3
                end if
                increment n
            else (incorrect response)
                n ← 0
                I ← 1
            end if

            return (n, EF, I)

         */

        }
    }
}
