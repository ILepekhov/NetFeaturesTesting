using System.Reactive.Linq;

namespace LogicToTest
{
    public sealed class MachineMonitor
    {
        #region Consts

        public const int MAXIMAL_ALERT_BURST_IN_SECONDS = 5;
        public const int MINIMAL_ALERT_PAUSE_IN_SECONDS = 5;
        public const int MAXIMAL_TIME_WITH_NO_MOVEMENT_IN_SECONDS = 3;
        public const int RISKY_TEMPERATURE = 70;

        #endregion

        #region Fields

        private readonly IConcurrencyProvider _concurrencyProvider;
        private readonly ITemperatureSensor _temperatureSensor;
        private readonly IProximitySensor _proximitySensor;

        #endregion

        #region Properties

        /// <summary>
        /// The maximal amount of time we consider a sequence of notifications as a burst
        /// even if the notification are emitted close to each other, after this amount
        /// of time a burst is "closed"
        /// </summary>
        public TimeSpan MaxAlertBurstTime { get; set; } = TimeSpan.FromSeconds(MAXIMAL_ALERT_BURST_IN_SECONDS);

        /// <summary>
        /// The amount of time we allow between two consecutive alerts.
        /// If two alerts are notified with a short time between them, we consider them as one
        /// </summary>
        public TimeSpan MinAlertPause { get; set; } = TimeSpan.FromSeconds(MINIMAL_ALERT_PAUSE_IN_SECONDS);

        /// <summary>
        /// If no proximity notification is emmited during this time, we conclude that
        /// there is no one near the sensor
        /// </summary>
        public TimeSpan MaximalTimeWithoutMovement { get; set; } = TimeSpan.FromSeconds(MAXIMAL_TIME_WITH_NO_MOVEMENT_IN_SECONDS);

        #endregion

        #region Constructor

        public MachineMonitor(
            IConcurrencyProvider concurrencyProvider,
            ITemperatureSensor temperatureSensor,
            IProximitySensor proximitySensor)
        {
            _concurrencyProvider = concurrencyProvider ?? throw new ArgumentNullException(nameof(concurrencyProvider));
            _temperatureSensor = temperatureSensor ?? throw new ArgumentNullException(nameof(temperatureSensor));
            _proximitySensor = proximitySensor ?? throw new ArgumentNullException(nameof(proximitySensor));
        }

        #endregion

        #region Methods

        public IObservable<Alert> ObserveAlerts()
        {
            return Observable.Defer(() =>
            {
                var scheduler = _concurrencyProvider.TimeBasedOperations;
                var proximities = _proximitySensor.Readings.Publish().RefCount();
                var temperatures = _temperatureSensor.Readings.Replay(1).RefCount();

                var riskyTemperatures = temperatures.Where(t => t >= RISKY_TEMPERATURE);
                var proximityWindowBoundaries = proximities.Throttle(MaximalTimeWithoutMovement);

                var mainQuery =
                    from proximityWindows in proximities.Window(proximityWindowBoundaries)
                    from t in proximityWindows.CombineLatest(riskyTemperatures, (p, t) => t)
                    select t;

                return mainQuery
                    .FilterBursts(MinAlertPause, MaxAlertBurstTime, scheduler)
                    .Select(_ => new Alert("Temperature is too hot! Please move away!", scheduler.Now));
            });
        }

        #endregion
    }
}
