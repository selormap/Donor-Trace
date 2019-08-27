using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DonorTraceMobile
{
    public class NumberValidator : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            ((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.FromHex("#FBC5D0");

        }
    }
}
