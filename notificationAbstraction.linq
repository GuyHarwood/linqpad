<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var lpLog = new LpLog();
	var svc = new NotificationService(new []{new EmailNotificationDispatcher(new EmailNotificationConverter(),lpLog)},lpLog);
	svc.Send(new Notification()
	{
		Message = "Testing",
		Subject = "Test"
	});
}

public interface ILog
{
	void Trace(string message, params object[] args);
	void Error(string message, params object[] args);
	void LogException(Exception ex);
	
}

public class LpLog : ILog
{
	public void Trace(string message, params object[] args)
	{
		string.Format(message,args).Dump("Trace");
	}
	public void Error(string message, params object[] args)
	{
		string.Format(message,args).Dump("Error");
	}
	public void LogException(Exception ex)
	{
		ex.Message.Dump("Exception.Message");
		ex.ToString().Dump("Exception.Detail");
	}
}

public class NotificationService
{
	private readonly IEnumerable<INotificationDispatcher> dispatchers;
	private readonly ILog log;

	public NotificationService(IEnumerable<INotificationDispatcher> dispatchers, ILog log)
	{
		this.dispatchers = dispatchers;
		this.log = log;
	}

	public void Send(Notification notification)
	{
		foreach (var dispatcher in dispatchers)
		{
			try
			{
				dispatcher.Dispatch(notification);
			}
			catch (Exception ex)
			{
				log.LogException(ex);
			}
		}
	}
}

public class EmailNotificationConverter : INotificationConverter<MailMessage>
{
	public MailMessage Convert(Notification notification)
	{
		return new MailMessage
		{
			Subject = notification.Subject,
			Body = notification.Message
		};
	}
}

public class EmailNotificationDispatcher : INotificationDispatcher
{
	private readonly INotificationConverter<MailMessage> converter;
	private readonly ILog log;

	public EmailNotificationDispatcher(INotificationConverter<MailMessage> converter, ILog log)
	{
		this.converter = converter;
		this.log = log;
	}

	public void Dispatch(Notification notification)
	{
		var msg = converter.Convert(notification);
		var mailServer = new SmtpClient();
		mailServer.SendCompleted += (sender, args) =>
		{
			if (args.Error != null)
			{
				log.Error("Notification:{0} failed to dispatch.\n Exception.Message:{1}", args.UserState, args.Error.Message);
			}
			mailServer.Dispose();
			msg.Dispose();
		};
		log.Trace("Dispatching Notification:{0} via SMTP", notification.Id.ToString("N"));
		mailServer.SendAsync(msg, notification.Id.ToString("N"));
	}
}

public interface INotificationConverter<out TDestination>
{
	TDestination Convert(Notification notification);
}

public interface INotificationDispatcher
{
	void Dispatch(Notification notification);
}

public class Notification
{
	public Notification()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; private set; }
	public string Message { get; set; }
	public string Subject { get; set; }
}