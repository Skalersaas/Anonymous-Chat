namespace Anonymous_Chat.Helpers
{
    enum SysMessageTypes
    {
        Transfer,
        Start,
        Next,
        Waiting,
        Stop,
    }
    enum ParsingResult
    {
        Last,
        Error,
        Partial,
    }
}
