﻿namespace RobotBrain.LogEntry
{
    // Visitor design pattern
    public interface ILogEntryVisitor
    {
        void Visit(CommandCompleteLogEntry logEntry);
        void Visit(GenericLogEntry logEntry);
    }
}