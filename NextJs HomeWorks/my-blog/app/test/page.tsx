import SuspenseDelay from "@/src/components/SuspenseDelay";
import { delay } from "@/src/utils";
import { Suspense } from "react";

// export const revalidate = 10

// async function delay(){
//   return new Promise(resolve => setTimeout(resolve,2000))
// }

export default async function page() {
  // const [time,setTime] = useState<string | null>(null);

  // useEffect(() =>{
  //    setTime(new Date().toLocaleTimeString("ru-RU"));
  // },[])

  // const path = usePathname()
  // const router = useRouter()

  // await delay()

  const [salam,sagol] = await Promise.all([delay(3000),delay(10000)])

  return (
    <div>
      <Suspense fallback={<div>Shas pokaju...</div>}>
        <SuspenseDelay ms={3000} />
      </Suspense>
      <Suspense fallback={<div>Shas pokaju...</div>}>
        <SuspenseDelay ms={1000} />
      </Suspense>

      
      {/* <p>Текущий путь:{path}</p>
      <button onClick={() =>{router.back()}}>Вернутся назад</button> */}
      {/* {(Math.floor(Math.random()*100))} */}
      {/* <p>Отрендерено в : {time? time:'Загрузка'}</p> */}
      {/* <LikeArticleButton/> */}
    </div>
  );
}
