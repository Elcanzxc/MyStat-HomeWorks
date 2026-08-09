// 'use client'
// import React, { useState } from "react";

// export default function Page() {

//     const [result,setResult] = useState<string>('')

//     async function add(formData:FormData){

//         const response = await fetch(`/upload/uploadBackend`,{method:"POST",body:formData})

//         const data = await response.json()
//         setResult(`Файл ${data.name} - ${data.sizeBytes} байт`)
//     }

//   return (
//     <div>
//       <p>Загрузите файл чтобы узнать информация о нём</p>

//       <div>
//         <form action={add}>
//             <input type="file" name="file" required/>
//             <button type="submit">Otpravit</button>
//             {result && <p>{result}</p>}
//         </form>
//       </div>
//     </div>
//   );
// }

"use client";
import { useState } from "react";

export default function Page() {
  const [file, setFile] = useState("");

     async function add(form:FormData){
      const data = await fetch(`/upload/uploadBackend`,{method:'POST',body:form})
      
      const fileName = await data.json()

      setFile(`Имя:${fileName.name} - Весит${fileName.sizeBytes}`)
  }
  return (
    <div>
      <p>Загрузите файл чтобы узнать о нём информацию</p>

      <form action={add}>
        <input type="file" placeholder="vibrat fayl" name="file" required/>
        <button type="submit"> otpravit fayl</button>
      </form>

      {file && <h1>{file}</h1>}
    </div>
  );
}
