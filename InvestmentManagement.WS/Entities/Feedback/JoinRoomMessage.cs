namespace InvestmentManagement.WS.Entities.Feedback;

public class JoinRoomMessage : FeedbackMessage
{
    public required string RoomName { get; set; }
}
