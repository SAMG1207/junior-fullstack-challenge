using LebenChallenge.Utils;

namespace LebenChallenge.Domain
{
    public class TaskItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsCompleted { get; private set; }

        public int Priority { get; private set; }
        public TaskItem()
        {
            // Default constructor for ORM or serialization purposes
        }

        public TaskItem(string name, string description, DateTime dueDate, int priority)
        {
            SetName(name);
            SetDescription(description);
            SetDueDate(dueDate);
            IsCompleted = false;
            SetPriority(priority);
        }

        public void UpdatePriority(int priority)
        {
            SetPriority(priority);
        }

        public void UpdateName(string name)
        {
            SetName(name);
        }

        public void UpdateDescription(string description)
        {
           SetDescription(description);
        }

        public void UpdateDueDate(DateTime dueDate)
        {
            SetDueDate(dueDate);
        }

        public void MarkAsCompleted()
        {
            /*
             * Aviso si se intenta completar una tarea ya realizada,
             * puede ser incomodo ya que paraliza la ejecución del programa
             * y no afecta el resultado final, ya la tarea se completó
             * opcional
             * 
             * if(IsCompleted)
            {
                // Si ya está completa no se modifica
                throw new InvalidOperationException("Task is already completed.");
            }*/
            IsCompleted = true;
        }

        //  VALIDACIONES PRIVADAS DENTRO DE LA CLASE


        private void SetDueDate(DateTime dueDate)
        {
            if (dueDate < DateTime.Now.AddDays(-1)) // Se permite un día de margen
            {
                throw new ArgumentOutOfRangeException(nameof(dueDate), "Due date cannot be in the past.");
            }
            if (dueDate > DateTime.Now.AddYears(2)) // No puede agendar tasks para dentro de más de dos años
            {
                throw new ArgumentOutOfRangeException(nameof(dueDate), "Due date cannot be more than two years in the future.");
            }
            DueDate = dueDate;
        }

        private void SetPriority(int priority)
        {
            if (priority < 1 || priority > 5) // Se permite un rango de 1 a 5
            {
                throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be between 1 and 5.");
            }
            Priority = priority;
        }

        private void SetName(string name)
        {
            // Se limmpian caracteres especiales y potencialmente peligrosos
            Name = StringSanitizer.StringSanitize(name);
        }

        private void SetDescription(string description)
        {
            // Se limmpian caracteres especiales y potencialmente peligrosos
            Description = StringSanitizer.StringSanitize(description);
        }
    }
}
