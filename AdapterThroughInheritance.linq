<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Security.Permissions</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	ISmtpClient	client;
	
	client = new SmtpProxy();
	client.Host = "127.0.0.1";
	var msg = new MailMessage()
	{
		From = new MailAddress("me@to.you")
	};
	
	msg.To.Add(new MailAddress("you@from.me"));
	
	client.SendCompleted += (sender, args) =>
			{
				if (args.Error != null)
				{
					string.Format("Notification:{0} dispatch failure.\n Exception.Message:{1}", args.UserState, args.Error.Message).Dump("ERROR");
					args.Error.ToString().Dump("DETAIL");
				}
				client.Dispose();
				msg.Dispose();
			};
	client.SendAsync(msg, "myMessage");
}

public class SmtpProxy : SmtpClient, ISmtpClient
{}

public interface ISmtpClient : IDisposable
{
	event SendCompletedEventHandler SendCompleted;
	
	void Send(MailMessage message);
	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
    void SendAsync(MailMessage message, object userToken);
	string Host {get;set;}
}
