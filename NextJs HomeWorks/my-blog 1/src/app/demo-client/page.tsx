"use client";

import { useEffect, useState } from "react";

function DemoServerPage() {
  const [renderAt, setRenderAt] = useState("");
  const [counter, setCounter] = useState(0);

  useEffect(() => {
    setRenderAt(new Date().toLocaleTimeString("ru-RU"));
  }, []);

  return (
    <div>
      <h1>Client Component</h1>

      <p>Страница отрендерена в: {renderAt}</p>

      <h2>Кнопка: {counter}</h2>

      <button onClick={() => setCounter((prev) => prev + 1)}>+1</button>
    </div>
  );
}

export default DemoServerPage;
