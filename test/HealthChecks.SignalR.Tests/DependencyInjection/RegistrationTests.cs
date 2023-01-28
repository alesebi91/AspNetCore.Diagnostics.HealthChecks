namespace HealthChecks.SignalR.Tests.DependencyInjection
{
    public class signalr_registration_should
    {
        [Fact]
        public void add_health_check_when_properly_configured()
        {
            var services = new ServiceCollection();
            services
                .AddHealthChecks()
                .AddSignalRHub("https://signalr.com/echo");
            using var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<HealthCheckServiceOptions>>();
            var registration = options.Value.Registrations.First();
            var check = registration.Factory(serviceProvider);

            registration.Name.ShouldBe("signalr");
            check.ShouldBeOfType<SignalRHealthCheck>();
        }
    }
}
