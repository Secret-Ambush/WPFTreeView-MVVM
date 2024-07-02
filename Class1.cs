using PropertyChanged;
using System.ComponentModel;

namespace WPFTreeView
{

    [AddINotifyPropertyChangedInterface]

    public class Class1 : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public string Test { get; set; }

        public Class1()
        {
            Task.Run(async () =>
            {
                int i = 0;
                while (true)
                {
                    await Task.Delay(200);
                    Test = (i++).ToString();

                }
            });
        }

        public override string ToString()
        {
            return "Hello World";
        }
    }
}
