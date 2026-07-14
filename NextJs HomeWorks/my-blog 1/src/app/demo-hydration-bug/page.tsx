"use client";

import { useEffect, useState } from "react";

function Page() {

   const [time,setTime] = useState<string | null>(null)

   useEffect(() =>{
     setTime(new Date().toLocaleTimeString("ru-RU"));
   },[])
  return (
    <div>

   
      <p>{time ?? "Загрузка..."}</p>
  
    </div>
  );
}

export default Page;
