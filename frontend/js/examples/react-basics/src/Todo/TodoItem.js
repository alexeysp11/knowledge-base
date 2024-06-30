import React from "react";

export default function TodoItem({todo, index}) {
    return (
        <li>
            <strong>{index + 1}. </strong>
            {todo.title}
        </li>
    );
}