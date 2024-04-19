import React from "react";
import TodoList from "./Todo/TodoList";

function App() {
  const todos = [
    {id: 1, completed: false, title: 'Buy milk'},
    {id: 2, completed: false, title: 'Buy bread'},
    {id: 3, completed: false, title: 'Buy pizza'}
  ];

  return (
    <div className="wrapper">
      <h1>React test project</h1>

      <TodoList todos={todos} />
    </div>
  );
}

export default App;
