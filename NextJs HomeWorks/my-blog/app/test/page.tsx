'use client'
import { usePathname,useRouter } from "next/navigation";


// export const revalidate = 10



// async function delay(){
//   return new Promise(resolve => setTimeout(resolve,2000))
// }


export default  function page() {
  // const [time,setTime] = useState<string | null>(null);

  // useEffect(() =>{
  //    setTime(new Date().toLocaleTimeString("ru-RU"));
  // },[])

  const path = usePathname()
  const router = useRouter()

  // await delay()

  return (
    <div>
      <p>Текущий путь:{path}</p>
      <button onClick={() =>{router.back()}}>Вернутся назад</button>
      {/* {(Math.floor(Math.random()*100))} */}
      {/* <p>Отрендерено в : {time? time:'Загрузка'}</p> */}
      {/* <RickAndMortyApi/> */}
      {/* <LikeArticleButton/> */}
    </div>
  );
}
