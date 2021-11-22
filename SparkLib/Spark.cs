namespace SparkNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SparkNet.TickProviders;
    public static class Spark
    {
        #pragma warning disable IDE1006
        private static ITickProvider? _tickProvider;
        #pragma warning restore IDE1006

        public static ITickProvider TickProvider
        {
            get { return _tickProvider ??= DefaultTickProvider; }
            set
            {
                _tickProvider = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        internal static ITickProvider DefaultTickProvider
        {
            get
            {
                return new MsGothicTickProvider();
            }
        }

        public static String Render(params double[] numbers)
        {
            return Render(data: numbers);
        }

        /// <summary>
        /// Convert numbers to spark chart strings.
        /// </summary>
        /// <param name="data">List or Array of numbers</param>
        /// <returns>empty string if <paramref name="data"/>is <c>null</c> or empty.</returns>
        public static String Render(IList<double> data)
        {
            var ticks = TickProvider.Ticks;

            if (data == null || data.Count == 0)
                return string.Empty;

            char[] res = new char[data.Count];

            double min = data.Min();
            double max = data.Max();
            double step = (max - min) / (ticks.Length - 1);
            if (step.Equals(0d)) step = 1;

            for (var i = 0; i < data.Count; i++)
            {
                var val = data[i];
                double d = (val - min) / step;

                // if it's 10^-10 close to its rounded, round; floor otherwise
                int tick = (int)((Math.Abs(Math.Round(d) - d) < 1e-10) ? Math.Round(d) : Math.Floor((val - min) / step));
                res[i] = ticks[tick];
            }
            return new string(res);
        }
    }
}