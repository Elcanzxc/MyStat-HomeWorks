import LikeArticleButton from "@/src/components/LikeArticleButton.tsx";
import RickAndMortyApi from "@/src/components/RickAndMorty";

// export const revalidate = 10

export default async function page() {
  // const [time,setTime] = useState<string | null>(null);

  // useEffect(() =>{
  //    setTime(new Date().toLocaleTimeString("ru-RU"));
  // },[])

  

  return (
    <div>
      {/* {(Math.floor(Math.random()*100))} */}
      {/* <p>Отрендерено в : {time? time:'Загрузка'}</p> */}
      {/* <RickAndMortyApi/> */}
      {/* <LikeArticleButton/> */}
    </div>
  );
}
