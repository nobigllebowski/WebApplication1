namespace Application.Common.LogTemplates
{
    public static partial class LogDescriptions
    {
        public static string GetDescription(
            LogActionType action,
            LogEntityType entity,
            params object[] args)
        {
            return (action, entity) switch
            {
                (LogActionType.Create, LogEntityType.Student)
                    => string.Format("Добавлен новый студент: {0}", args),
                (LogActionType.Update, LogEntityType.Student)
                    => string.Format("Обновлен студент {0}", args),
                (LogActionType.Create, LogEntityType.Department)
                    => string.Format("Добавлен новый факультет {0}", args),
                _ => "Действие выполнено"
            };
        }
    }
}
