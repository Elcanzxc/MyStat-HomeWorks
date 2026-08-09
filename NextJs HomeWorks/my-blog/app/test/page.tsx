'use client'

import SuspenseDelay from "@/src/components/SuspenseDelay";
import { delay } from "@/src/utils";
import { Suspense, useState } from "react";


// export const revalidate = 10

// async function delay(){
//   return new Promise(resolve => setTimeout(resolve,2000))
// }

export default  function page() {
  // const [time,setTime] = useState<string | null>(null);

  // useEffect(() =>{
  //    setTime(new Date().toLocaleTimeString("ru-RU"));
  // },[])

  // const path = usePathname()
  // const router = useRouter()

  // await delay()

  // const [salam,sagol] = await Promise.all([delay(3000),delay(10000)])

   
  const [text,setText] = useState('')

  const  add = async() =>{
   const request = await fetch('/test/api/apiTest',{method:'POST',body:JSON.stringify({text})})
   setText('')
   get()
  }


  const [data,setData] = useState<{id:String,text:String | null}[]>()
  const get = async () =>{
    const response = await fetch('/test/api/apiTest')
    const responseJson = await response.json()
    setData(responseJson)
  }
  return (
    <div>

      <input value={text} onChange={(e) => setText(e.target.value)} placeholder="vvodi text"></input>
      <button onClick={add}>otrpavit</button>
      <div>
       {data?.map((item) => <p>{item.id} : {item.text}</p>)}
      </div>

      {/* <Suspense fallback={<div>Shas pokaju...</div>}>
        <SuspenseDelay ms={3000} />
      </Suspense>
      <Suspense fallback={<div>Shas pokaju...</div>}>
        <SuspenseDelay ms={1000} />
      </Suspense> */}

      
      {/* <p>Текущий путь:{path}</p>
      <button onClick={() =>{router.back()}}>Вернутся назад</button> */}
      {/* {(Math.floor(Math.random()*100))} */}
      {/* <p>Отрендерено в : {time? time:'Загрузка'}</p> */}
      {/* <LikeArticleButton/> */}
    </div>
  );
}
