# NLog

[NLog](http://nlog-project.org/) is a logging library for .NET.

If logging is provided in units under test, then it is useful to output logging information during unit test runs.

## Logging Configuration

The [App.Config](./Logger/App.config) for the [Logger](./Logger/Logger.cs) (wrapper around NLog) is configured to output to the console. All that is necessary
for the unit tests to log to the console is to have a configuration suitable for testing.

The are multiple strategies available:
* add an App.Config to the unit test project and add suitable NLog configuration
* add a link in the unit test project to the existing [App.Config](./Logger/App.config) in the Logger project - this solution
* add a .testsettings file that deploys a suitable App.Config (e.g. from Logger)

Since it is likely that unit test loggers with either be mocked or simplified from a deployment example then, in the latter case, a suitable App.Config should be
created in the unit test project.

## Testing

The [App.Config](./Logger/App.config) is configured for console output only:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="nlog" type="NLog.Config.ConfigSectionHandler, NLog" />
  </configSections>
    <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
    <targets>
      <target name="console" xsi:type="Console" />
    </targets>
    <rules>
      <logger name="*" minlevel="Trace" writeTo="console" />
    </rules>
  </nlog>
</configuration>
```

In [ModelTests](./ModelTest/ModelTests.cs) the console output is redirected to a `StringWriter` at the start of each test `Console.SetOut(_writer)` so
that the console logging can be examined after each test has run. The examination of the console log is done using `StringAssert.EndsWith` because the start
of each log entry includes date- and time-stamps which will change with each run.
