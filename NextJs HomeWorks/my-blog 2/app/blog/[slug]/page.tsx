import { delay } from "@/utils/Delay";
import { notFound } from "next/navigation";
import styles from "./page.module.css";


const blog = [
  {
    id:1,
    text:"Сегодня мы расскажем о новых технологиях, которые помогают сделать повседневные задачи проще и эффективнее."
  },

  {
    id:2,
    text:"В этом посте делимся полезными советами по развитию навыков, организации времени и поиску вдохновения."
  },

  {
    id:3,
    text:"Интересные идеи появляются тогда, когда мы экспериментируем и пробуем что-то новое."
  }
];



export async function generateStaticParams(){
  return [
    {slug:"1"},
    {slug:"2"},
    {slug:"3"}
  ];
}



export default async function SlugPage({
 params
}:{
 params:Promise<{slug:string}>
}){


 await delay(2000);


 const {slug}=await params;


 const post=blog.find(
   item=>item.id===Number(slug)
 );


 if(!post){
   notFound();
 }


 return (

  <article className={styles.container}>

    <h1 className={styles.title}>
      Блог под айди: {slug}
    </h1>


    <p className={styles.text}>
      {post.text}
    </p>


  </article>

 );

}