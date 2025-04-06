import { Presentacion } from "./components/Presentacion";
import { ListaTareas } from "./components/ListaTareas";

export const App = () => {
  return (
    <div>
      <div className="container-fluid">
        <Presentacion></Presentacion>
        <ListaTareas></ListaTareas>
      </div>
    </div>
  );
};
