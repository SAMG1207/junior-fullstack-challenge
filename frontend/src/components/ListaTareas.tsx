import { useState } from "react";
import { Task } from "./Task";
import URLGlobal from "../URLGlobal";
type Tarea = {
  id: number;
  name: string;
  description: string;
  dueDate: string;
  isCompleted: boolean;
  priority: number;
};

export const ListaTareas = () => {
  const [tareas, setTareas] = useState<Tarea[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [tareasMostradas, setTareasMostradas] = useState<boolean>(false);

  const handleClick = async () => {
    if (!tareasMostradas) {
      try {
        setLoading(true);
        const response = await fetch(URLGlobal);

        if (!response.ok) {
          console.error("No se han cargado las tareas");
          return;
        }

        const data = await response.json();
        setTareas(data);
        setTareasMostradas(true);
      } catch (err) {
        console.error("Error al cargar las tareas", err);
      } finally {
        setLoading(false);
      }
    } else {
      // Si las tareas ya están visibles, las ocultamos
      setTareasMostradas(false);
    }
  };

  return (
    <div className="row">
      <div className="col-8 mx-auto">
        <div className="m-4 d-flex justify-content-center">
          <button className="btn btn-success" onClick={handleClick}>
            {tareasMostradas ? "Ocultar Tareas" : "Ver Tareas"}
          </button>
        </div>

        {loading && (
          <p className="text-center text-light">Cargando tareas...</p>
        )}

        {tareasMostradas && (
          <div className="mx-auto">
            {tareas.length > 0 ? (
              tareas.map((tarea) => (
                <Task
                  key={tarea.id}
                  id={tarea.id}
                  name={tarea.name}
                  description={tarea.description}
                  dueDate={tarea.dueDate}
                  isCompleted={tarea.isCompleted}
                  priority={tarea.priority}
                />
              ))
            ) : (
              <p className="text-center text-light">
                No hay tareas disponibles
              </p>
            )}
          </div>
        )}
      </div>
    </div>
  );
};
