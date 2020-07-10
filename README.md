# BackgroundQueue

<a href="https://www.nuget.org/packages/BackgroundQueue"><img alt="Nuget" src="https://img.shields.io/nuget/v/BackgroundQueue"></a> <a href="https://www.nuget.org/packages/BackgroundQueue"><img alt="Nuget" src="https://img.shields.io/nuget/dt/BackgroundQueue"></a> <img alt="GitHub issues" src="https://img.shields.io/github/issues-raw/TwentyFourMinutes/BackgroundQueue">

BackgroundQueue is a simple way to queue background Tasks in ASP.Net Core and in .Net in general.
You can download BackgroundQueue either from the Nuget Package Manager or from the official [nuget.org](https://www.nuget.org/packages/BackgroundQueue) website.

## About

This package brings two useful queues with it, which both operate in the Background, that means that they do not block the current Thread. With these queues you can enqueue `Tasks` or `Tickets` which provide even more control. They are also fully thread save and come with a handy `IServiceCollection` extension.

## How to

First up, you'll need to download the `BackgroundQueue` nuget package from one of the sources named above. After that you need to decide which one of the queues you need.

### Basic setup 

1. In your `ConfigureServices` method you want to add `AddBackgroundTaskQueue` or `AddBackgroundResultQueue` depending on your needs.

   ```c#
   public void ConfigureServices(IServiceCollection services)
   {
      [...]
   	services.AddBackgroundTaskQueue();
      //Or
      services.AddBackgroundResultQueue();
   }
   ```

2. In your Controller constructor you need to request the queue implementation of your needs, e.g. `IBackgroundTaskQueue`/`IBackgroundResultQueue` .This would look something like the following.

   ```c#
   public class HomeController : Controller
   {
   	private readonly IBackgroundTaskQueue _taskQueue;
      //Or
   	private readonly IBackgroundResultQueue _resultQueue;
       
   	public DashboardController(IBackgroundTaskQueue taskQueue
   							         //Or
   							         IBackgroundResultQueue resultQueue)
   	{
   		_taskQueue = taskQueue;
   		//Or
   		_resultQueue = resultQueue;
   	}
   }
   ```

3. In any Action you can now consume any of those queues and start enqueuing items.

   ```c#
   public async Task<IActionResult> Index()
   {
   	_taskQueue.Enqueue(async token =>
   	{
   	   await EmailSender.SendEmailAsync("Somone visited our website!");
   	});   // Will return immediately.
       
       //Or
       
      await _backgroundQueue.ProcessInQueueAsync(async token =>
   	{
         // I need to wait for any other items in this queue first!
   	});   // Will continue after all other items, which are in front of it are processed.
       
   	return View();
   }
   ```

   

### BackgroundTaskQueue

The `BackgroundTaskQueue` is a queue which will enqueue items and immediately return to the current execution. You could use this queue for sending emails.

### BackgroundResultQueue

The `BackgroundResultQueue` is a queue which will enqueue items and waits until the `Task`/`Ticket` finished processing including the result. You could use this queue, if you want requests to get processed step by step, but still want everything happen asynchronously.

## Notes

If you feel like something is not working as intended or you are experiencing issues, feel free to create an issue. Also for feature requests just create an issue. For further information feel free to send me a mail to `twenty@translucent.at` or message me on Discord `24_minutes#7496`.
