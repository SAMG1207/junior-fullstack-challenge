import React, { useState } from "react";
import URLGlobal from "../URLGlobal";
interface TaskProps {
  id: number;
  name: string;
  description: string;
  dueDate: string;
  isCompleted: boolean;
  priority: number;
}

export const Task: React.FC<TaskProps> = ({
  id,
  name,
  description,
  dueDate,
  isCompleted,
  priority,
}) => {
  const [selectedPriority, setSelectedPriority] = useState<number>(priority);
  const [displayPriority, setDisplayPriority] = useState<number>(priority);
  const [loading, setLoading] = useState<boolean>(false);

  const handlePriorityChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    setSelectedPriority(Number(event.target.value));
  };

  const handleUpdatePriority = async () => {
    try {
      setLoading(true);
      if (displayPriority == selectedPriority) {
        console.log("las prioridades son iguales");
        return;
      }
      const response = await fetch(
        `${URLGlobal}/${id}/priority?priority=${selectedPriority}`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        alert("Hubo un problema al cambiar la prioridad.");
        console.error("Error al cambiar la prioridad", response);
        return;
      }

      alert("Prioridad cambiada correctamente.");

      setDisplayPriority(selectedPriority);
      //window.location.reload();
    } catch (error) {
      alert("Error al cambiar la prioridad.");
      console.error("Error al realizar la solicitud", error);
    } finally {
      setLoading(false);
    }
  };

  const eliminarTarea = async () => {
    try {
      const response = await fetch(`${URLGlobal}/${id}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        alert("Tarea eliminada correctamente");
        window.location.reload();
      } else {
        alert("No se ha podido eliminar la tarea correctamente");
      }
    } catch (err) {
      console.error(`${err} Error`);
    }
  };

  return (
    <div className="m-3 bg bg-secondary-subtle rounded p-3 text-center border border-black shadow-lg">
      <h2>Id: {id}</h2>
      <h4>Nombre: {name}</h4>
      <p>
        <strong>Descripción:</strong> {description}
      </p>
      <p>
        <strong>Vence:</strong>Vence: {new Date(dueDate).toLocaleDateString()}
      </p>
      <p>
        <strong>Completada:</strong>
        {isCompleted ? "✅ Sí" : "❌ No"}
      </p>
      <p>
        <strong>Prioridad:</strong> {displayPriority}
      </p>

      <div className="mb-2">
        <label htmlFor={`priority-select-${id}`}>Cambiar Prioridad: </label>
        <select
          id={`priority-select-${id}`}
          value={selectedPriority}
          onChange={handlePriorityChange}
          disabled={loading}
        >
          <option value={1}>1</option>
          <option value={2}>2</option>
          <option value={3}>3</option>
          <option value={4}>4</option>
          <option value={5}>5</option>
        </select>
      </div>

      <button
        onClick={handleUpdatePriority}
        disabled={loading}
        className="btn btn-primary mb-1"
      >
        Cambiar prioridad
      </button>

      {loading && <p>Cambiando prioridad...</p>}

      <div>
        <button onClick={eliminarTarea} className="btn btn-danger m-2">
          Eliminar Tarea
        </button>
      </div>
    </div>
  );
};
